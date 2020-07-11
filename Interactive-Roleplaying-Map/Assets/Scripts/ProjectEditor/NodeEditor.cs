using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class NodeEditor : MonoBehaviour
{
	public InputField NameField;
	public InputField DescriptionField;
	public Dropdown RarityField;
	public string OnNodeChangedMessage;
	public string OnNodeChangedCaption;

	private Node currentNode;
	private VisualNode currentVisualNode;

	private bool contentUpdated;

	public void StartEdit(VisualNode visualNode, Node node)
	{
		StopEditing(); 

		this.contentUpdated = false; 
		this.currentVisualNode = visualNode;
		this.currentNode = node;

		NameField.text = node.Name;
		DescriptionField.text = node.Description;
		RarityField.value = (int)node.Rarity;

		gameObject.SetActive(true);
	}

	public void StopEditing()
	{
		if (currentNode != null && contentUpdated)
		{
			DialogResult result = MessageBox.Show(
				OnNodeChangedMessage, 
				OnNodeChangedCaption, 
				MessageBoxButtons.YesNoCancel);

			if (result == DialogResult.Yes)
			{
				SaveNodeChanges();
			}
			else if (result == DialogResult.Cancel)
			{
				return;
			}
		}

		CloseEdit();
	}

	public void SaveNodeChanges()
	{
		string name = NameField.text;
		string description = DescriptionField.text;
		NodeRarity rarity = (NodeRarity)RarityField.value;

		currentNode.Name = name;
		currentNode.Description = description;
		currentNode.Rarity = rarity;

		currentVisualNode.OnUpdate();

		contentUpdated = false;
	}

	public void MarkAsChanged()
	{
		this.contentUpdated = true;
	}

	public void CloseEdit()
	{
		gameObject.SetActive(false);

		currentNode = null;
		currentVisualNode = null;
	}
}
