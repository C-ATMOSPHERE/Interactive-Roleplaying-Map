using UnityEngine;

public class Menu : MonoBehaviour
{
	public GameObject CreateNewPanel;
	public GameObject LoadPanel;

	public void CreateNew()
	{
		CreateNewPanel.SetActive(true);
		gameObject.SetActive(false);
	}

	public void StopCreateNew()
	{
		CreateNewPanel.SetActive(false);
		gameObject.SetActive(true);
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
