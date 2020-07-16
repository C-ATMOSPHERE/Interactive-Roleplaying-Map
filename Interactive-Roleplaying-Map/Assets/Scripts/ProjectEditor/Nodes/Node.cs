using System;

[Serializable]
public class Node
{
	public long Id;
	public string Name;
	public NodeRarity Rarity;
	public NodeTimeOfDay TimeOfDay;
	public string Description;
	public float PositionX;
	public float PositionY;

	public Node() { }
	public Node(long id)
	{
		this.Id = id;
	}
}
