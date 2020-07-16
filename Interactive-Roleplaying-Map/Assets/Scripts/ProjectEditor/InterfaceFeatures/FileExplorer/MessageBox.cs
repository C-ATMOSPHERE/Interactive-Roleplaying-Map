using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFileExplorer
{
	internal class MessageBox : MonoBehaviour
	{
		private static MessageBox instance;

		private readonly MessageResult[,] results = new MessageResult[6, 3]
		{
			{ MessageResult.Abort, MessageResult.Retry, MessageResult.Ignore },
			{ MessageResult.OK, MessageResult.Null, MessageResult.Null },
			{ MessageResult.OK, MessageResult.Cancel, MessageResult.Null },
			{ MessageResult.Retry, MessageResult.Cancel, MessageResult.Null },
			{ MessageResult.Yes, MessageResult.No, MessageResult.Null },
			{ MessageResult.Yes, MessageResult.No, MessageResult.Cancel }
		};

		[SerializeField] private Text Caption;
		[SerializeField] private Text Message;
		[SerializeField] private MessageBoxButton[] buttons;
		[SerializeField] private GameObject screenBlocker;

		private Action<MessageResult> onComplete;


		internal static void ShowMessage(
			MessageType type, 
			Action<MessageResult> onComplete,
			string message, 
			string caption)
		{
			instance.StartShowMessage(type, onComplete, message, caption);
		}


		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				gameObject.SetActive(false);
			}
			else
			{
				throw new DuplicateSingletonException("MessageBox");
			}
		}


		private void StartShowMessage(
			MessageType type, 
			Action<MessageResult> onComplete,
			string message,
			string caption)
		{
			gameObject.SetActive(true);
			screenBlocker.SetActive(true);

			this.onComplete = onComplete;
			Message.text = message;
			Caption.text = caption;

			int t = (int)type;
			for (int i = 0; i < results.GetLength(1); i++)
			{
				MessageResult label = results[t,i];
				MessageBoxButton button = buttons[i];

				if (label == MessageResult.Null)
				{
					button.gameObject.SetActive(false);
				}
				else
				{
					button.gameObject.SetActive(true);
					button.Set(label, ShowMessage);
				}
			}
		}

		private void ShowMessage(MessageResult result)
		{
			gameObject.SetActive(false);
			screenBlocker.SetActive(false);

			if (onComplete != null)
			{
				onComplete.Invoke(result);
			}
		}
	}

	internal enum MessageType
	{
		AbortRetryIgnore,
		OK,
		OKCancel,
		RetryCancel,
		YesNo,
		YesNoCancel
	}

	internal enum MessageResult
	{
		Null,
		Abort,
		Retry,
		Ignore,
		OK,
		Cancel,
		Yes,
		No
	}
}
