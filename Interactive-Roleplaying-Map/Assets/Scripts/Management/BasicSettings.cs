
using UnityEngine;

public class BasicSettings : MonoBehaviour
{
	public static BasicSettings instance; 

	public string Name;
	public string StoragePath;
	public string ImagePath;
	
	public void Start()
	{
		if (instance == null)
		{
			DontDestroyOnLoad(this);
			instance = this;
		}
		else
		{
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

	}
}
