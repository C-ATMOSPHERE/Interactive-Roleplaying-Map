using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class Load : MonoBehaviour
{
    public Scenes EditorScene;

	public void OnEnable()
    {
        Action<string> onComplete = (string path) =>
        {
            if (path != null && File.Exists(path))
            {
                BasicSettings.Instance.LoadSettingsFrom(path);
                SceneManager.LoadScene((int)EditorScene);
            }
            else
            {
                gameObject.SetActive(false);
            }
        };

        FileExplorer.FileExplorer.Browse(onComplete, "IRM", "*");


        //using (OpenFileDialog fileBrowser = new OpenFileDialog())
        //{
        //    fileBrowser.Filter = @"Interactive Roleplaying Map|*.irm;*.IRM";
        //    DialogResult result = fileBrowser.ShowDialog();
        //    string path = fileBrowser.FileName;

        //    if (result == DialogResult.OK
        //        && File.Exists(path))
        //    {
        //        BasicSettings.Instance.LoadSettingsFrom(path);
        //        SceneManager.LoadScene((int)EditorScene);
        //    }
        //    else
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}
    }
}
