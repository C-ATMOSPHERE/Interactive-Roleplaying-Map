using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFileExplorer
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
			if (type != null)
			{
				type.text = "File folder";
			}
		}
	}
}
