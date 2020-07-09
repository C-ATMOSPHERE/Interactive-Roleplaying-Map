using UnityEngine;
using UnityEngine.UI;

public class NodeEditor : MonoBehaviour
{
	public InputField NameField;
	public InputField DescriptionField;
	public Dropdown RarityField;

	private Node currentNode;
	private VisualNode currentVisualNode;

	public void StartEdit(VisualNode visualNode, Node node)
	{
		this.currentVisualNode = visualNode;
		this.currentNode = node;

		NameField.text = node.Name;
		DescriptionField.text = node.Description;

		int index = 0;
		for (int i = 0; i < RarityField.options.Count; i++)
		{
			Dropdown.OptionData option = RarityField.options[i];
			if(option.text == node.Rarity)
			{
				index = i;
				break;
			}
		}

		RarityField.value = index;

		gameObject.SetActive(true);
	}

	public void CloseEdit()
	{
		// TODO: add some popup.
		string name = NameField.text;
		string description = DescriptionField.text;
		string rarity = RarityField.options[RarityField.value].text;

		currentNode.Name = name;
		currentNode.Description = description;
		currentNode.Rarity = rarity;

		currentVisualNode.OnUpdate();

		gameObject.SetActive(false);
	}
}
