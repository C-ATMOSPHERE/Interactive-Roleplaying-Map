using System;
using UnityEngine;
using UnityEngine.UI;

namespace FileExplorer
{
	internal abstract class DirectoryEntry : MonoBehaviour
	{
		protected Action<string> onEntryClicked;
		protected string path;

		internal void Start()
		{
			GetComponent<Button>().onClick.AddListener(delegate
			{ 
				onEntryClicked.Invoke(this.path); 
			});
		}

		internal void Set(string path, Action<string> onEntryClicked)
		{
			this.onEntryClicked = onEntryClicked;
			this.path = path;
			Set(path);
		}

		protected abstract void Set(string path);
	}
}
