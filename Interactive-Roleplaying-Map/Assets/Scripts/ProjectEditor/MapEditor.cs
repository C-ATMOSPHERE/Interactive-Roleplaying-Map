using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapEditor: MonoBehaviour
{
	private enum MapEditorState
	{
		Idle,
		MovingNode
	}


	public VisualNodeFactory VisualNodeFactory;
	public NodeEditor NodeEditor;
	public Editor Editor;

	private InteractiveMap interactiveMap;
	private MapEditorState state = MapEditorState.Idle;
	private VisualNode current;


	private void Update()
	{
		switch (state)
		{
			case MapEditorState.MovingNode:
				MovingNode();
				break;
			default:
				break;
		}
	}


	public void Set(InteractiveMap interactiveMap)
	{
		this.interactiveMap = interactiveMap;
		SpawnMapItems();
	}

	private void SpawnMapItems()
	{
		foreach(Node node in interactiveMap.MapNodes)
		{
			VisualNode visualNode = VisualNodeFactory.CreateNode(node);
			visualNode.transform.position = new Vector3(node.PositionX, node.PositionY, 0);
			// The node is forced to set its state to placed, 
			// as the node is definitely up-to-date there is no need to call Place().
			visualNode.IsPlaced = true; 
		}
	}

	public void CreateNewNode()
	{
		long id = interactiveMap.GetNextId;
		Node node = new Node(id);
		VisualNode visualNode = VisualNodeFactory.CreateNode(node);
		StartMovingNode(visualNode);
	}

	public void StartMovingNode(VisualNode node)
	{
		node.IsPlaced = false;
		current = node;
		state = MapEditorState.MovingNode;
	}

	public void PlaceNode(Node node)
	{
		// Updates the node's position to the visual's position.
		node.PositionX = current.transform.position.x;
		node.PositionY = current.transform.position.y;

		// Tests if it is a new node or not. 
		IEnumerable<Node> old = interactiveMap.MapNodes.Where(n => n.Id == node.Id);
		if (old == null)
		{
			interactiveMap.MapNodes.Add(node);
		}

		// Resets the state. 
		NodeEditor.StartEdit(current, node);
		state = MapEditorState.Idle;
		current = null;
		Editor.StartEditingNode();
	}


	public void StopPlacingNode()
	{
		Destroy(current.gameObject);
		state = MapEditorState.Idle;
	}

	private void MovingNode()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo))
		{
			Vector3 position = hitInfo.point;
			position.z = -1;
			current.transform.position = position;
		}
	}

	public void StopEditing()
	{
		switch (state)
		{
			case MapEditorState.MovingNode:
				StopPlacingNode();
				break;
			default:
				break;
		}
	}
}
