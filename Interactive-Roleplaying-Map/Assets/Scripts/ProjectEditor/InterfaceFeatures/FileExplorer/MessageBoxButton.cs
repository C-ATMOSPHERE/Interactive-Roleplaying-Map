using System;
using UnityEngine;
using UnityEngine.UI;
using UnityFileExplorer;

internal class MessageBoxButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private Text label;

	private Action<MessageResult> onClick;
	private MessageResult result;


	private void Awake()
	{
		button.onClick.AddListener(delegate { onClick.Invoke(result); });
	}

	internal void Set(MessageResult result, Action<MessageResult> onClick)
	{
		this.result = result;
		this.onClick = onClick;
		label.text = result.ToString();
	}
}
