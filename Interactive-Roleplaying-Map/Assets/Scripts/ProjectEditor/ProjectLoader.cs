using Framework.Storage;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityFileExplorer;
using JsonUtility = SimpleJsonLibrary.JsonUtility;

public class ProjectLoader : MonoBehaviour
{
    public RawImage MapImageTarget;
    public MapEditor InteractiveMapEditor;
    public Editor Editor;
    public bool AllowClearAppData = true;

    public string OnSaveProjectMessage;
    public string OnSaveProjectCaption;
    public string OnLoadErrorMessage;
    public string OnLoadErrorCaption;

    private BasicSettings settings;
    private InteractiveMap interactiveMap;

    private bool endingSession = false;

    private void Start()
    {
        settings = BasicSettings.Instance;

        if (settings.IsNewProject)
        {
            CreateNewProject();
        }
        else
        {
            LoadProjectFromPath();
        }

        LoadImage();
        InteractiveMapEditor.Set(interactiveMap);
    }


    private void CreateNewProject()
    {
        interactiveMap = new InteractiveMap(settings.Name);
        File.Copy(settings.ImagePath, settings.StaticImagePath, true);
    }

    private void LoadProjectFromPath()
    {
        try
        {
            SimpleZipper zipper = new SimpleZipper();
            byte[] data = File.ReadAllBytes(settings.MapFilePath);
            zipper.Unzip(data, settings.StaticPath);

            string dataJson = File.ReadAllText(settings.StaticDataPath);
            interactiveMap = JsonUtility.FromJson<InteractiveMap>(dataJson);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
            Action<MessageResult> onComplete = (MessageResult result) =>
            {
                Editor.GoToMainMenu();
            };

            MessageBox.ShowMessage(MessageType.OK, onComplete, OnLoadErrorMessage, OnLoadErrorCaption);
        }
    }

    private void LoadImage()
    {
        byte[] image = File.ReadAllBytes(settings.StaticImagePath);
        Texture2D texture = new Texture2D(2, 2);
        bool isLoaded = texture.LoadImage(image);

        if (isLoaded)
        {
            MapImageTarget.texture = texture;
            Vector2 size = new Vector2(texture.width, texture.height);
            MapImageTarget.rectTransform.sizeDelta = size;
            
            BoxCollider collider = MapImageTarget.GetComponent<BoxCollider>();
            collider.size = size;
            collider.center = size / 2;
        }
        else
        {
            throw new Exception("Could not load image file");
        }
    }

    public void EndSession()
    {
        endingSession = true;
        SaveProject();
    }

    public void SaveProject()
    {
        Action<MessageResult> onComplete = (MessageResult result) =>
        {
            if (result == MessageResult.Yes)
            {
                string mapJson = JsonUtility.ToJson(interactiveMap, false);
                File.WriteAllText(settings.StaticDataPath, mapJson);

                SimpleZipper zipper = new SimpleZipper();
                string targetPath = settings.IsNewProject
                    ? Path.ChangeExtension(
                        Path.Combine(
                            settings.StoragePath,
                            settings.Name),
                        "irm")
                    : settings.MapFilePath;

                zipper.Zip(settings.StaticPath, targetPath);

                if (endingSession)
                {
                    ClearAppData();
                } 

                Editor.SessionEnded();
            }
        };

        MessageBox.ShowMessage(MessageType.YesNoCancel, onComplete, OnSaveProjectMessage, OnSaveProjectCaption);
    }

    private void ClearAppData()
    {
        if (AllowClearAppData)
        {
            Directory.Delete(settings.StaticPath, true);
        }
    }
}
