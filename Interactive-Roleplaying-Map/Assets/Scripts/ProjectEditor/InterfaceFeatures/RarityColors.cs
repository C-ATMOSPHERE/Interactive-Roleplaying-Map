using UnityEngine;

public class RarityColors : MonoBehaviour
{
	public static RarityColors Instance;

	public Color32 DefaultColor;
	public Color32[] Colors;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			throw new DuplicateSingletonException("RarityColor");
		}
	}

	public static Color32 GetColor(int i)
	{
		if(i >= Instance.Colors.Length)
		{
			return Instance.DefaultColor;
		}
		else
		{
			return Instance.Colors[i];
		}
	}
}
