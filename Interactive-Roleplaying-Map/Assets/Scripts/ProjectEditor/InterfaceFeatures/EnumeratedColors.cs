using UnityEngine;

[CreateAssetMenu(menuName = "Enumerated Colors")]
public class EnumeratedColors: ScriptableObject
{
	public Color32 DefaultColor;
	public Color32[] Colors;

	public Color32 GetColor(int i)
	{
		if(i >= Colors.Length)
		{
			return DefaultColor;
		}
		else
		{
			return Colors[i];
		}
	}
}
