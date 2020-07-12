using System;
using UnityEngine;

public class VisualNodeFilter : MonoBehaviour
{
	public Transform VisualNodeContainer;
	
	private bool[] visualRarities;


	private void Awake()
	{
		visualRarities = new bool[Enum.GetValues(typeof(NodeRarity)).Length];
		for(int i = 0; i < visualRarities.Length; i++)
		{
			visualRarities[i] = true;
		}
	}

	public void FilterRarity(int r, bool isVisible) 
	{
		if(visualRarities[r] != isVisible)
		{
			visualRarities[r] = isVisible;
			ReloadNodes();
		}
	}

	private void ReloadNodes()
	{
		int visualNodeCount = VisualNodeContainer.childCount;
		for(int i = 0; i < visualNodeCount; i++)
		{
			GameObject child = VisualNodeContainer.GetChild(i).gameObject;
			VisualNode node =  child.GetComponent<VisualNode>();
			int rarity = (int)node.GetRarity;
			bool isVisible = visualRarities[rarity];
			child.SetActive(isVisible);
		}
	}
}
