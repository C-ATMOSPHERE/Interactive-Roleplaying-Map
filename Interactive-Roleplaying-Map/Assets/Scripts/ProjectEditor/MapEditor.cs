using UnityEngine;

public class MapEditor: MonoBehaviour
{
	private enum MapEditorState
	{
		Idle,
		MovingNode
	}


	public VisualNodeFactory visualNodeFactory;
	public Editor editor;

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
			VisualNode visualNode = visualNodeFactory.CreateNode(node);
			visualNode.transform.position = new Vector3(node.PositionX, node.PositionY, 0);
			visualNode.ForcePlace();
		}
	}

	public void CreateNewNode()
	{
		state = MapEditorState.MovingNode;
		long id = interactiveMap.GetNextId;
		Node node = new Node(id);
		current = visualNodeFactory.CreateNode(node);
	}

	public void PlaceNode(Node node)
	{
		interactiveMap.MapNodes.Add(node);
		state = MapEditorState.Idle;
		current = null;
		editor.StopCreatingNewNode();
	}

	private void MovingNode()
	{
		Vector3 position = Input.mousePosition;
		current.transform.position = position;
	}
}

