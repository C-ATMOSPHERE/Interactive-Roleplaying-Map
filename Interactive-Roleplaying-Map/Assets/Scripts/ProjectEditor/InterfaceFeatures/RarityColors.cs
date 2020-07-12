using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class RarityColors : MonoBehaviour
{
	public static RarityColors Instance;

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
		return Instance.Colors[i];
	}
}
