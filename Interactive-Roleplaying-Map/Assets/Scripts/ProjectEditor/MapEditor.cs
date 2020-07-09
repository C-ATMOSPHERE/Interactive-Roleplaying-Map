using UnityEngine;

public class MapEditor: MonoBehaviour
{
	public VisualNodeFactory visualNodeFactory;

	private InteractiveMap interactiveMap;
	private MapEditorState state = MapEditorState.Idle;
	
	private VisualNode current;

	public void Set(InteractiveMap interactiveMap)
	{
		this.interactiveMap = interactiveMap;
		// TODO: Do stuff.
	}

	public void CreateNewNode()
	{
		state = MapEditorState.MovingNode;
		long id = interactiveMap.GetNextId;
		Node node = new Node(id);
		visualNodeFactory.CreateNode(node);
	}

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


	private void MovingNode()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		position.z = 0;
		current.transform.position = position;
	}
}

public enum MapEditorState 
{
	Idle, 
	MovingNode
}
