using UnityEngine;
using UnityEngine.UI;

// Contains all functionality for the visual aspect of the nodes. 
public class VisualNode : MonoBehaviour
{
	private Node node;
	private MapEditor mapEditor;
	private NodeEditor nodeEditor;
	private bool isPlaced;

	public void Set(Node node, MapEditor editor, NodeEditor nodeEditor)
	{
		this.node = node;
		this.mapEditor = editor;
		this.nodeEditor = nodeEditor;
		isPlaced = false;
		ApplyRarityColor();
	}

	public void OnClick()
	{
		if (isPlaced)
		{
			Interact();
		}
		else
		{
			Place();
		}
	}

	public void ForcePlace()
	{
		isPlaced = true;
	}

	private void Place()
	{
		node.PositionX = this.transform.position.x;
		node.PositionY = this.transform.position.y;
		mapEditor.PlaceNode(this.node);
		isPlaced = true;
	}

	private void Interact()
	{
		nodeEditor.StartEdit(this, this.node);
	}

	private void ApplyRarityColor()
	{
		Color32 color = RarityColors.GetColor((int)node.Rarity);
		Image image = GetComponent<Image>();
		image.color = color;
	}

	public void OnUpdate()
	{
		//TODO: Update things.
		ApplyRarityColor();
	}
}
