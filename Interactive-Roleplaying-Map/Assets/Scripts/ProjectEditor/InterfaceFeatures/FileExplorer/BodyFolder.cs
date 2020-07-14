using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace FileExplorer
{
	internal class BodyFolder : DirectoryEntry
	{
		[SerializeField] private Text label;
		[SerializeField] private Text lastModifiedOn;
		[SerializeField] private Text type;
		protected override void Set(string path)
		{
			DirectoryInfo directory = new DirectoryInfo(path);
			label.text = directory.Name;
			lastModifiedOn.text = directory.LastWriteTime.ToString();
			type.text = "File folder";
		}
	}
}
