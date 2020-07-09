﻿using System.Collections.Generic;
using SimpleJsonLibrary;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class InteractiveMap
{
	public string Name;
	public Image image;

	[JsonIgnore] public List<Node> MapNodes = new List<Node>();

	public long CurrentId = 0;
	
	
	[JsonIgnore] public long GetNextId { get { return CurrentId++; } }

	// This field is only used for json (de)serialization.
	public Node[] SerializableNodes
	{
		get
		{
			return MapNodes.ToArray();
		}
		set
		{
			MapNodes = new List<Node>(value);
		}
	}

	public InteractiveMap() { }

	public InteractiveMap(string name)
	{
		Name = name;
		MapNodes = new List<Node>();
	}
}
