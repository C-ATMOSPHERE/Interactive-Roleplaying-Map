using UnityEngine;
using UnityEngine.UI;

public class NodeEditor : MonoBehaviour
{
	public InputField NameField;
	public InputField DescriptionField;
	public Dropdown RarityField;

	private Node currentNode;
	private VisualNode currentVisualNode;

	private bool contentUpdated;

	public void StartEdit(VisualNode visualNode, Node node)
	{
		this.contentUpdated = false; // TODO: test whether previous content was updated prior to switching.
		this.currentVisualNode = visualNode;
		this.currentNode = node;

		NameField.text = node.Name;
		DescriptionField.text = node.Description;
		RarityField.value = (int)node.Rarity;

		gameObject.SetActive(true);
	}

	public void CloseEdit()
	{
		if (currentNode != null)
		{
			// TODO: add some popup.
			string name = NameField.text;
			string description = DescriptionField.text;
			NodeRarity rarity = (NodeRarity)RarityField.value;

			currentNode.Name = name;
			currentNode.Description = description;
			currentNode.Rarity = rarity;
			
			currentNode = null;
			currentVisualNode = null;

			currentVisualNode.OnUpdate();


			gameObject.SetActive(false);
		}
	}

	public void MarkAsChanged()
	{
		this.contentUpdated = true;
	}
}
