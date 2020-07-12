using UnityEngine;

public class Menu : MonoBehaviour
{
	public GameObject CreateNewPanel;
	public GameObject LoadPanel;

	public void CreateNew()
	{
		CreateNewPanel.SetActive(true);
	}

	public void Load()
	{
		LoadPanel.SetActive(true);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
