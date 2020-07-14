using UnityEngine;
using UnityEngine.SceneManagement;

public class Editor : MonoBehaviour
{
	public MapEditor InteractiveMapEditor;
	public GameObject MiniMenu;
	public NodeEditor NodeEditor;
	public MapEditor MapEditor;
	public ProjectLoader ProjectLoader;
	public CameraController CameraController;


	public void CreateNewNode()
	{
		MiniMenu.SetActive(false);
		InteractiveMapEditor.CreateNewNode();
	}

	public void StartEditingNode()
	{
		MiniMenu.SetActive(false);
		NodeEditor.gameObject.SetActive(true);
		CameraController.CanMove = false;
	}

	public void StopEditingNode()
	{
		NodeEditor.gameObject.SetActive(false);
		MiniMenu.SetActive(true);
		CameraController.CanMove = true;
	}

	public void SaveProject()
	{
		NodeEditor.StopEditing();
		MapEditor.StopEditing();
		ProjectLoader.SaveProject();
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene((int)Scenes.MenuScene);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
