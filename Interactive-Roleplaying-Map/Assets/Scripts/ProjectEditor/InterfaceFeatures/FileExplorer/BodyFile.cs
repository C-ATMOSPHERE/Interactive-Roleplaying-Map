using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFileExplorer
{
	internal class BodyFile : DirectoryEntry
	{
		[SerializeField] private Text label;
		[SerializeField] private Text lastModifiedOn;
		[SerializeField] private Text type;

		protected override void Set(string path)
		{
			FileInfo file = new FileInfo(path);
			label.text = Path.GetFileNameWithoutExtension(file.Name);
			lastModifiedOn.text = file.LastWriteTime.ToString();
			type.text = file.Extension;
		}
	}
}
