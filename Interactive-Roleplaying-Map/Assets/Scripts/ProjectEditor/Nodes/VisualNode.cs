using UnityEngine;
using UnityEngine.UI;

// Contains all functionality for the visual aspect of the nodes. 
public class VisualNode : MonoBehaviour
{
	private Node node;
	private MapEditor mapEditor;
	private NodeEditor nodeEditor;
	private Editor editor;
	private EnumeratedColors rarityColors;

	public bool IsPlaced
	{
		get; set;
	}


	public NodeRarity GetRarity()
	{
		return node.Rarity;
	}

	public NodeTimeOfDay GetTimeOfDay()
	{
		return node.TimeOfDay;
	}



	public void Set(Node node, MapEditor mapEditor, NodeEditor nodeEditor, Editor editor, EnumeratedColors rarityColors)
	{
		this.node = node;
		this.mapEditor = mapEditor;
		this.nodeEditor = nodeEditor;
		this.editor = editor;
		this.rarityColors = rarityColors;
		IsPlaced = false;
		ApplyRarityColor();
	}

	public void OnClick()
	{
		if (IsPlaced)
		{
			Interact();
		}
		else
		{
			Place();
		}

		editor.StartEditingNode();
	}

	private void Place()
	{
		IsPlaced = true;
		mapEditor.PlaceNode(this.node);
	}

	private void Interact()
	{
		nodeEditor.StartEdit(this, this.node);
	}

	private void ApplyRarityColor()
	{
		Color32 color = rarityColors.GetColor((int)node.Rarity);
		Image image = GetComponent<Image>();
		image.color = color;
	}

	public void OnUpdate()
	{
		ApplyRarityColor();
	}
}
