using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFileExplorer
{
	internal sealed class FileExplorer : FolderExplorer
	{
		private static FileExplorer instance;
		private const string AllFilesFilter = ".*";

		[Space, SerializeField] private Dropdown filterDropdown;
		[SerializeField] private DirectoryEntryPool bodyFilePool;

		private string[] filters;
		private int currentFilter;


		public static void BrowseFile(Action<string> onComplete)
		{
			instance.StartBrowse(onComplete);
		}

		public static void SetFilters(params string[] filters)
		{
			instance.filters = filters;
		}


		protected override void Awake()
		{
			if (instance == null)
			{
				instance = this;
				instance.gameObject.SetActive(false);
			}
			else
			{
				throw new DuplicateSingletonException("File Explorer");
			}
		}


		protected override void StartBrowse(Action<string> onComplete)
		{
			base.StartBrowse(onComplete);
			CreateFilters(filters);
			CreateBodyFiles(current);
		}


		private void CreateFilters(string[] filters)
		{
			filterDropdown.options.Clear();

			for (int i = 0; i < filters.Length; i++)
			{
				if (filters[i][0] != '.')
				{
					filters[i] = '.' + filters[i];
				}

				Dropdown.OptionData data = new Dropdown.OptionData(filters[i]);
				filterDropdown.options.Add(data);
			}

			this.filters = filters;
		}



		protected override void OnFolderClicked(string path)
		{
			base.OnFolderClicked(path);
			CreateBodyFiles(path);
		}

		private void OnFileClicked(string path)
		{
			current = path;
			inputField.text = Path.GetFileName(current);
		}


		private void CreateBodyFiles(string path)
		{
			if (!bodyFilePool.IsSet)
			{
				return;
			}

			bodyFilePool.DisableAll();

			string filter = filters[currentFilter];
			string[] files = Directory.GetFiles(path);
			foreach (string file in files)
			{
				FileInfo info = new FileInfo(file);
				if ((Path.GetExtension(file).ToUpper() == filter || filter == AllFilesFilter)
					&& (info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
				{
					DirectoryEntry entry = bodyFilePool.GetNext();
					entry.Set(file, OnFileClicked);
				}
			}
		}


		protected override void ClearExplorer()
		{
			base.ClearExplorer();
			if (bodyFilePool.IsSet)
			{
				bodyFilePool.DisableAll();
			}
		}


		public void DropdownFilterChanged()
		{
			currentFilter = filterDropdown.value;
			CreateBodyFiles(current);
		}
	}
}