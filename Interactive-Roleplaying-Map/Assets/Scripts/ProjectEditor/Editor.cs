using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
	public MapEditor InteractiveMapEditor;
	public GameObject CreateNodeButton;

	public void CreateNewNode()
	{
		CreateNodeButton.SetActive(false);
		InteractiveMapEditor.CreateNewNode();
	}

	public void StopCreatingNewNode()
	{
		CreateNodeButton.SetActive(true);
	}
}
