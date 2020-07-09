using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


// contains all methodologies for the visual aspect of the nodes. 
public class VisualNode : MonoBehaviour
{
	private Node node = null;

	public void Set(Node node)
	{
		this.node = node;
	}
}
