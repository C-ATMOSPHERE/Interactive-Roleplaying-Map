using UnityEngine;

public class Editor : MonoBehaviour
{
	public MapEditor InteractiveMapEditor;
	public GameObject MiniMenu;
	public NodeEditor NodeEditor;
	public MapEditor MapEditor;
	public ProjectLoader ProjectLoader;

	public void CreateNewNode()
	{
		MiniMenu.SetActive(false);
		InteractiveMapEditor.CreateNewNode();
	}

	public void StopCreatingNewNode()
	{
		MiniMenu.SetActive(true);
	}

	public void SaveProject()
	{
		NodeEditor.CloseEdit();
		MapEditor.StopEditing();
		ProjectLoader.SaveProject();
	}
}
