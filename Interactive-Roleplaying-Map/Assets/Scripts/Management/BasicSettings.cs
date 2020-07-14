using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			return MapFilePath == ""; 
		}
	}

	public string StaticPath 
	{ 
		get
		{
			return Path.Combine(Application.persistentDataPath, "data");
		}
	}

	public string StaticImagePath
	{
		get
		{
			return Path.Combine(StaticPath, "image");
		}
	}

	public string StaticDataPath
	{
		get
		{
			return Path.Combine(StaticPath, "data");
		}
	}

	public void Awake()
	{
		if (Instance == null)
		{
			Initialize();
		}
		else if (SceneManager.GetActiveScene().buildIndex == (int)Scenes.MenuScene)
		{
			Destroy(Instance.gameObject);
			Initialize();
		}
		else
		{
			Debug.LogWarning("BasicSettings already initialized, destroying self...");
			Destroy(this);
		}
	}

	private void Initialize()
	{
		DontDestroyOnLoad(this);
		Instance = this;

		if (!Directory.Exists(StaticPath))
		{
			Directory.CreateDirectory(StaticPath);
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
