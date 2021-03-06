﻿using System;
using System.Collections.Generic;
using SimpleJsonLibrary;

[Serializable]
public class InteractiveMap
{
	public string Name;

	[JsonIgnore] public List<Node> MapNodes = new List<Node>();

	public long CurrentId = 0;
	
	
	[JsonIgnore] public long GetNextId { get { return CurrentId++; } }

	[JsonIgnore] public int Count { get { return MapNodes.Count; } }


	public Node Get(int i)
	{
		return MapNodes[i];
	}

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
