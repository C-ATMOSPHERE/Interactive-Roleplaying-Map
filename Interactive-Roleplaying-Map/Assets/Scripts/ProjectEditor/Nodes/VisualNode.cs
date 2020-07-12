using UnityEngine;
using UnityEngine.UI;

// Contains all functionality for the visual aspect of the nodes. 
public class VisualNode : MonoBehaviour
{
	private Node node;
	private MapEditor mapEditor;
	private NodeEditor nodeEditor;
	private Editor editor;

	public bool IsPlaced
	{
		get; set;
	}


	public void Set(Node node, MapEditor mapEditor, NodeEditor nodeEditor, Editor editor)
	{
		this.node = node;
		this.mapEditor = mapEditor;
		this.nodeEditor = nodeEditor;
		this.editor = editor;
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
		Color32 color = RarityColors.GetColor((int)node.Rarity);
		Image image = GetComponent<Image>();
		image.color = color;
	}

	public void OnUpdate()
	{
		ApplyRarityColor();
	}
}
