using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Produces visual Nodes. 

public class VisualNodeFactory : MonoBehaviour
{
	public VisualNode NodePrefab;
	public Transform NodeContainer;
	public MapEditor Editor;

	public VisualNode CreateNode(Node node)
	{
		// set things like onClick references etc. as well here. 
		VisualNode visualNode = Instantiate(NodePrefab, NodeContainer);
		visualNode.name = "VisualNode_" + node.Id;
		visualNode.Set(node, Editor);
		return visualNode;
	}
}
