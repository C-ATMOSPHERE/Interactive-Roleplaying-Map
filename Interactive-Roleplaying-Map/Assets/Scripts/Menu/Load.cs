using UnityEngine;
using System.Windows.Forms;
using System.IO;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public Scenes EditorScene;

	public void OnEnable()
	{
        using (OpenFileDialog fileBrowser = new OpenFileDialog())
        {
            fileBrowser.Filter = @"Interactive Roleplaying Map|*.irm;*.IRM";
            DialogResult result = fileBrowser.ShowDialog();
            string path = fileBrowser.FileName;

            if (result == DialogResult.OK
                && File.Exists(path))
            {
                BasicSettings.Instance.LoadSettingsFrom(path);
                SceneManager.LoadScene((int)EditorScene);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
