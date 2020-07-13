using System;
using System.Collections.Generic;
using UnityEngine;

public class VisualNodeFilter : MonoBehaviour
{
	public Transform VisualNodeContainer;
	public string[] NodeEnumerators;
	private Dictionary<Type, bool[]> visualValues = new Dictionary<Type, bool[]>();


	private void Awake()
	{
		foreach(string e in NodeEnumerators)
		{
			Type t = Type.GetType(e);
			AddElement(t);
		}
	}

	private void AddElement(Type enumType)
	{
		Array enumValues = Enum.GetValues(enumType);
		visualValues.Add(enumType, new bool[enumValues.Length]);
	}

	public void Filter(Type enumType, int index, bool isVisible)
	{
		visualValues[enumType][index] = !isVisible;
		Func<VisualNode, int> getValue = null;

		// TODO: make an attempt to standardize this. 
		if(enumType == typeof(NodeRarity))
		{
			getValue = GetRarity;
		}
		else if (enumType  == typeof(NodeTimeOfDay))
		{
			getValue = GetTimeOfDay;
		}

		ReloadNodes(getValue, enumType);
	}

	private int GetRarity(VisualNode node)
	{
		return (int) node.GetRarity();
	}

	private int GetTimeOfDay(VisualNode node)
	{
		return (int) node.GetTimeOfDay();
	}

	private void ReloadNodes(Func<VisualNode, int> getValue, Type enumType)
	{
		int visualNodeCount = VisualNodeContainer.childCount;
		for(int i = 0; i < visualNodeCount; i++)
		{
			GameObject child = VisualNodeContainer.GetChild(i).gameObject;
			VisualNode node =  child.GetComponent<VisualNode>();
			int v = getValue.Invoke(node);
			bool isVisible = !visualValues[enumType][v];
			child.gameObject.SetActive(isVisible);
		}
	}
}
