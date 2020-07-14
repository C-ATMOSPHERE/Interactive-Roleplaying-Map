using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace FileExplorer
{
	internal class SidePanelFolder : DirectoryEntry
	{
		[SerializeField] private Text label;

		protected override void Set(string path)
		{
			label.text = path;
		}
	}
}
