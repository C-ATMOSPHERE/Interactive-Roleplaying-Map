using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Produces visual Nodes. 

public class NodeFactory : MonoBehaviour
{
	public VisualNode NodePrefab;
	public Transform NodeContainer;

	public void CreateNode(Node node)
	{
		// set things like onClick references etc. as well here. 
		VisualNode newNode = Instantiate(NodePrefab, NodeContainer);
		newNode.Set(node);
	}
}
