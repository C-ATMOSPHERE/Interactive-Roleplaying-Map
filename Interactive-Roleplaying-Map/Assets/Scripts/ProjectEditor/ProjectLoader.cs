using Framework.Storage;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ProjectLoader : MonoBehaviour
{
    public RawImage MapImageTarget;
    public MapEditor InteractiveMapEditor;

    private BasicSettings settings;
    private InteractiveMap interactiveMap;

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

    private void OnApplicationQuit()
    {
        //TODO: make a pop-up asking for save. 
        SaveProject();
    }

    private void CreateNewProject()
    {
        interactiveMap = new InteractiveMap(settings.Name);
        File.Copy(settings.ImagePath, settings.StaticImagePath, true);
    }

    private void LoadProjectFromPath()
    {
        SimpleZipper zipper = new SimpleZipper();
        byte[] data = File.ReadAllBytes(settings.MapFilePath);
        zipper.Unzip(data, Application.dataPath);

        string dataJson = File.ReadAllText(settings.StaticDataPath);
        interactiveMap = JsonUtility.FromJson<InteractiveMap>(dataJson);
    }

    private void LoadImage()
    {
        byte[] image = File.ReadAllBytes(settings.StaticImagePath);
        Texture2D texture = new Texture2D(2, 2);
        bool isLoaded = texture.LoadImage(image);

        if (isLoaded)
        {
            MapImageTarget.texture = texture;
            MapImageTarget.rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
        }
        else
        {
            throw new Exception("Could not load image file");
        }
    }

    private void SaveProject()
    {
        string mapJson = JsonUtility.ToJson(interactiveMap);
        File.WriteAllText(settings.StaticDataPath, mapJson);

        SimpleZipper zipper = new SimpleZipper();
        string targetPath = Path.ChangeExtension(
            Path.Combine(
                settings.StoragePath,
                settings.Name),
            "irm");

        zipper.Zip(settings.StaticPath, targetPath);

    }
}
