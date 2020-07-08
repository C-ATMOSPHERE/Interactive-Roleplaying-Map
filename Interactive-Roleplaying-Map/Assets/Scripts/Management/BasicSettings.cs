using UnityEngine;

public class BasicSettings : MonoBehaviour
{
	public static BasicSettings Instance;

	public string MapFilePath;
	public string Name;
	public string StoragePath;
	public string ImagePath;
	
	public bool IsNewProject
	{
		get 
		{ 
			return MapFilePath == null; 
		}
	}


	public void Start()
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(this);
			Instance = this;
		}
		else
		{
			Debug.LogWarning("BasicSettings already initialized, destroying self...");
			Destroy(this);
		}
	}

	public void Set(string name, string storagePath, string imagePath)
	{
		this.Name = name;
		this.StoragePath = storagePath;
		this.ImagePath = imagePath;
	}

	public void LoadSettingsFrom(string path)
	{
		this.MapFilePath = path;
	}
}
