using UnityEngine;

// Contains all functionality for the visual aspect of the nodes. 
public class VisualNode : MonoBehaviour
{
	private Node node = null;
	private MapEditor editor;

	public void Set(Node node, MapEditor editor)
	{
		this.node = node;
		this.editor = editor;
	}


	public void Place()
	{
		editor.PlaceNode(this.node);
	}
}
