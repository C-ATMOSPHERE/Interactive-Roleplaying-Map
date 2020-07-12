﻿using Framework.Storage;
using System;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using JsonUtility = SimpleJsonLibrary.JsonUtility;

public class ProjectLoader : MonoBehaviour
{
    public RawImage MapImageTarget;
    public MapEditor InteractiveMapEditor;
    public bool AllowClearAppData = true;
    public string OnSaveProjectMessage;
    public string OnSaveProjectCaption;

    private BasicSettings settings;
    private InteractiveMap interactiveMap;


    private void Start()
    {
        settings = BasicSettings.Instance;

        if (settings.IsNewProject)
        {
            Debug.Log("Loading New Project!");
            CreateNewProject();
        }
        else
        {
            Debug.Log("Loading Existing Project!");
            LoadProjectFromPath();
        }

        LoadImage();
        InteractiveMapEditor.Set(interactiveMap);
    }

    // TODO: This caused BSOD when triggered with the default Windows quit button, due to the System.Windows.Forms implementation.
    public void OnApplicationQuit()
    {
        SaveProject();
        ClearAppData();
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
        zipper.Unzip(data, settings.StaticPath);

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

    public void SaveProject()
    {
        DialogResult result = MessageBox.Show(OnSaveProjectMessage, OnSaveProjectCaption, MessageBoxButtons.YesNoCancel);
        if (result == DialogResult.Yes)
        {
            string mapJson = JsonUtility.ToJson(interactiveMap, true);
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
        }
    }

    private void ClearAppData()
    {
        if (AllowClearAppData)
        {
            Directory.Delete(settings.StaticPath, true);
        }
    }
}