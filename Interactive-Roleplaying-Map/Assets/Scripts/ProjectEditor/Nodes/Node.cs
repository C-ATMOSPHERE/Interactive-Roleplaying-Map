using UnityEngine;

public class Node
{
	public long Id;
	public string Name;
	public string Rarity;
	public string Description;
	public float PositionX;
	public float PositionY;

	public Node() { }
	public Node(long id)
	{
		this.Id = id;
	}
}
