using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateNew : MonoBehaviour
{
    public InputField NameField;
    public InputField SavePathField;
    public InputField FilePathField;

    public Scenes EditorScene;


    public void Complete()
    {
        // TODO: test legitimacy.
        string name = NameField.text;
        string savePath = SavePathField.text;
        string filePath = FilePathField.text;

        BasicSettings.Instance.Set(name, savePath, filePath);
        SceneManager.LoadScene((int)EditorScene);
    }

    public void BrowseSaveLocation()
    {
        using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
        {
            DialogResult result = folderBrowser.ShowDialog();
            string path = folderBrowser.SelectedPath;

            if(result == DialogResult.OK 
                && Directory.Exists(path))
            {
                SavePathField.text = path;
            }
        }
    }

    public void BrowseImageLocation()
    {
        using (OpenFileDialog fileBrowser = new OpenFileDialog())
        {
            fileBrowser.Filter = @"Image Files (PSD, TIFF, JPG, TGA, PNG, GIF, BMP, IFF, PICT)|*.PSD;*.TIFF;*.JPG;*.TGA;*.PNG;*.GIF;*.BMP;*.IFF;*.PICT";
            DialogResult result = fileBrowser.ShowDialog();
            string path = fileBrowser.FileName;

            if (result == DialogResult.OK
                && File.Exists(path))
            {
                FilePathField.text = path;
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);

        NameField.text = "";
        SavePathField.text = "";
        FilePathField.text = "";
    }
}
