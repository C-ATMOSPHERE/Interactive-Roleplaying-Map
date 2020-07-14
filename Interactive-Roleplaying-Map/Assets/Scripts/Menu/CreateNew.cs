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
            // TODO: Implement Custom MessageBox!
            //MessageBox.Show(InvalidProjectNameMessage, ErrorMessageCaption);
            return;
        }

        string savePath = SavePathField.text;
        if (!Directory.Exists(savePath))
        {
            // TODO: Implement Custom MessageBox!
            //MessageBox.Show(InvalidSavePathMessage, ErrorMessageCaption);
            return;
        }

        string filePath = FilePathField.text;
        if(!File.Exists(filePath))
        {
            // TODO: Implement Custom MessageBox!
            //MessageBox.Show(InvalidFilePathMessage, ErrorMessageCaption);
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
        // TODO: Implement Custom Folder Dialog!
        //using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
        //{
        //    DialogResult result = folderBrowser.ShowDialog();
        //    string path = folderBrowser.SelectedPath;

        //    if(result == DialogResult.OK 
        //        && Directory.Exists(path))
        //    {
        //        SavePathField.text = path;
        //    }
        //}
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

        string[] filters = new string[] { "PSD", "TIFF", "JPG", "TGA", "PNG", "GIF", "BMP", "IFF", "PICT" };
        FileExplorer.FileExplorer.Browse(onComplete, filters);

        //using (OpenFileDialog fileBrowser = new OpenFileDialog())
        //{
        //    fileBrowser.Filter = @"Image Files (PSD, TIFF, JPG, TGA, PNG, GIF, BMP, IFF, PICT)|*.PSD;*.TIFF;*.JPG;*.TGA;*.PNG;*.GIF;*.BMP;*.IFF;*.PICT";
        //    DialogResult result = fileBrowser.ShowDialog();
        //    string path = fileBrowser.FileName;

        //    if (result == DialogResult.OK
        //        && File.Exists(path))
        //    {
        //        FilePathField.text = path;
        //    }
        //}
    }

    public void Close()
    {
        Menu.StopCreateNew();
        
        NameField.text = "";
        SavePathField.text = "";
        FilePathField.text = "";
    }
}
