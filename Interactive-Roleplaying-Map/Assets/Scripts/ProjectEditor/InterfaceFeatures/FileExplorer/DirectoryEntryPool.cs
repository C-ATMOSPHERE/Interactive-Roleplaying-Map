using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FileExplorer
{
	[Serializable]
	internal class DirectoryEntryPool
	{
		[SerializeField] private DirectoryEntry entryPrefab;
		[SerializeField] private Transform container;
		[SerializeField] private bool moveToEnd;

		private List<DirectoryEntry> pool = new List<DirectoryEntry>();
		private int pointer = 0;

		internal DirectoryEntry GetNext()
		{
			if(pointer >= pool.Count)
			{
				DirectoryEntry newEntry = MonoBehaviour.Instantiate(entryPrefab, container);
				pool.Add(newEntry);
			}

			DirectoryEntry entry = pool[pointer];
			entry.gameObject.SetActive(true);

			if (moveToEnd)
			{
				entry.transform.SetAsLastSibling();
			}

			pointer++;

			return entry;
		}

		internal void Disable(DirectoryEntry entry)
		{
			entry.gameObject.SetActive(false);
			int index = pool.IndexOf(entry);
			pool.RemoveAt(index);
			pool.Add(entry);
			pointer--;
		}

		internal void DisableAll()
		{
			foreach(DirectoryEntry entry in pool)
			{
				entry.gameObject.SetActive(false);
			}

			pointer = 0;
		}
	}
}
