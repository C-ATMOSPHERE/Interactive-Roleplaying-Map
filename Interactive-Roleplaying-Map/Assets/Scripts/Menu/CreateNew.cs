using UnityFileExplorer;
using System;
using System.IO;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateNew : MonoBehaviour
{
    public Menu Menu;

    public InputField NameField;
    public InputField SavePathField;
    public InputField FilePathField;

    public string ErrorMessageCaption;
    public string InvalidProjectNameMessage;
    public string InvalidSavePathMessage;
    public string InvalidFilePathMessage;

    public Scenes EditorScene;


    public void Complete()
    {
        string name = NameField.text;
        if(!TestNameValidity(name))
        {
            MessageBox.ShowMessage(MessageType.OK, null, InvalidProjectNameMessage, ErrorMessageCaption);
            return;
        }

        string savePath = SavePathField.text;
        if (!Directory.Exists(savePath))
        {
            MessageBox.ShowMessage(MessageType.OK, null, InvalidSavePathMessage, ErrorMessageCaption);
            return;
        }

        string filePath = FilePathField.text;
        if(!File.Exists(filePath))
        {
            MessageBox.ShowMessage(MessageType.OK, null, InvalidFilePathMessage, ErrorMessageCaption);
            return;
        }

        BasicSettings.Instance.Set(name, savePath, filePath);
        SceneManager.LoadScene((int)EditorScene);
    }

    private bool TestNameValidity(string name)
    {
        char[] invalidChars = Path.GetInvalidPathChars();

        foreach(char c in name.ToCharArray())
        {
            foreach(char o in invalidChars)
            {
                if (c == o)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void BrowseSaveLocation()
    {
        Action<string> onComplete = (string path) =>
        {
            if (path != null)
            {
                SavePathField.text = path;
            }
        };

        FolderExplorer.BrowseFolder(onComplete);
    }

    public void BrowseImageLocation()
    {
        Action<string> onComplete = (string path) =>
        {
            if (path != null)
            {
                FilePathField.text = path;
            }
        };

        FileExplorer.SetFilters("*", "PSD", "TIFF", "JPG", "TGA", "PNG", "GIF", "BMP", "IFF", "PICT");
        FileExplorer.BrowseFile(onComplete);
    }

    public void Close()
    {
        Menu.StopCreateNew();
        
        NameField.text = "";
        SavePathField.text = "";
        FilePathField.text = "";
    }
}
