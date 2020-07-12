using UnityEngine;

// Factory object to create visual nodes and initialize them. 
public class VisualNodeFactory : MonoBehaviour
{
	public VisualNode NodePrefab;
	public Transform NodeContainer;
	public MapEditor MapEditor;
	public NodeEditor NodeEditor;
	public Editor Editor;

	public VisualNode CreateNode(Node node)
	{
		// set things like onClick references etc. as well here. 
		VisualNode visualNode = Instantiate(NodePrefab, NodeContainer);
		visualNode.name = "VisualNode_" + node.Id;
		visualNode.Set(node, MapEditor, NodeEditor, Editor);
		return visualNode;
	}
}
