//using System.Windows.Forms;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityFileExplorer;

public class NodeEditor : MonoBehaviour
{
	public MapEditor MapEditor;
	public Editor Editor;
	public InputField NameField;
	public InputField DescriptionField;
	public Dropdown RarityField;
	public Dropdown TimeOfDayField;
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
		TimeOfDayField.value = (int)node.TimeOfDay;
	}

	private void InitializeWithnewNode(VisualNode visualNode, Node node)
	{
		//TODO: Continue here. 
	}

	public void StopEditing()
	{
		if (currentNode != null && contentUpdated)
		{
			Action<MessageResult> onComplete = (MessageResult result) =>
			{
				if (result == MessageResult.Yes)
				{
					SaveNodeChanges();
				}
				else if (result == MessageResult.Cancel)
				{
					return;
				}

				CloseEdit();
			};

			MessageBox.ShowMessage(MessageType.YesNoCancel, onComplete, OnNodeChangedMessage, OnNodeChangedCaption);
		}
		else
		{
			CloseEdit();
		}
	}


	public void MoveNode()
	{
		gameObject.SetActive(false);
		MapEditor.StartMovingNode(currentVisualNode);
	}

	public void DeleteNode()
	{
		MapEditor.DeleteNode(currentVisualNode, currentNode);
		contentUpdated = false;
		CloseEdit();
	}

	public void SaveNodeChanges()
	{
		string name = NameField.text;
		string description = DescriptionField.text;
		NodeRarity rarity = (NodeRarity)RarityField.value;
		NodeTimeOfDay timeOfDay = (NodeTimeOfDay)TimeOfDayField.value;

		currentNode.Name = name;
		currentNode.Description = description;
		currentNode.Rarity = rarity;
		currentNode.TimeOfDay = timeOfDay;

		currentVisualNode.OnUpdate();

		contentUpdated = false;
	}

	public void MarkAsChanged()
	{
		this.contentUpdated = true;
	}

	public void CloseEdit()
	{
		currentNode = null;
		currentVisualNode = null;
		Editor.StopEditingNode(); // TODO: Remove all the editor access from this script. It's turning spagetti.
	}
}
