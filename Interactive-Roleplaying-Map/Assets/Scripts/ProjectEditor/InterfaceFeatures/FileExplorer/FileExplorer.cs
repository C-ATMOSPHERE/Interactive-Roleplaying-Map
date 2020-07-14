﻿using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace FileExplorer
{
    public class FileExplorer : MonoBehaviour
    {
        private static FileExplorer instance;

        [SerializeField] private Dropdown filterDropdown;
        [SerializeField] private InputField inputField;

        [SerializeField] private DirectoryEntryPool bodyFolderPool;
        [SerializeField] private DirectoryEntryPool bodyFilePool;
        [SerializeField] private DirectoryEntryPool sidePanelFolderPool;

        private Action<string> onComplete;
        private string[] filters;
        private int currentFilter = 0; 

        private string current;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                instance.gameObject.SetActive(false);
            }
            else
            {
                throw new DuplicateSingletonException("FileExplorer");
            }
        }


        public static void Browse(Action<string> onComplete, params string[] filters)
        {
            instance.StartFileBrowser(onComplete, filters);
        }


        private void StartFileBrowser(Action<string> onComplete, string[] filters)
        {
            this.onComplete = onComplete;

            SetFilters(filters);
            CreateSidePanelContent();
            CreateBodyContent(current);

            instance.gameObject.SetActive(true);
        }


        private void SetFilters(string[] filters)
        {
            filterDropdown.options.Clear();

            for(int i = 0; i < filters.Length; i++)
            {
                if(filters[i][0] != '.')
                {
                    filters[i] = '.' + filters[i];
                }

                Dropdown.OptionData data = new Dropdown.OptionData(filters[i]);
                filterDropdown.options.Add(data);
            }

            this.filters = filters;
        }

        private void Exit()
        {
            onComplete.Invoke(current);
            instance.gameObject.SetActive(false);
            ClearExplorer();
        }


		#region DirectoryEntry Methods

        internal void OnFolderClicked(string path)
        {
            current = path;
            CreateBodyContent(path);
        }

        internal void OnFileClicked(string path)
        {
            current = path;
            inputField.text = current;
        }

		#endregion


		#region Visual Management

		private void CreateBodyContent(string path)
        {
            CreateBodyContentFolders(path);
            CreateBodyContentFiles(path);
        }

        private void CreateBodyContentFolders(string path)
        {
            bodyFolderPool.DisableAll();

            string[] directories = Directory.GetDirectories(path);

            foreach (string dir in directories)
            {
                DirectoryInfo info = new DirectoryInfo(dir);
                if((info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    DirectoryEntry entry = bodyFolderPool.GetNext();
                    entry.Set(dir, OnFolderClicked);
                }
            }
        }

        private void CreateBodyContentFiles(string path)
        {
            bodyFilePool.DisableAll();

            string filter = filters[currentFilter];
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (Path.GetExtension(file).ToUpper() == filter)
                {
                    DirectoryEntry entry = bodyFilePool.GetNext();
                    entry.Set(file, OnFileClicked);
                }
            }
        }

        private void CreateSidePanelContent()
        {
            sidePanelFolderPool.DisableAll();

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach(DriveInfo drive in drives)
            {
                DirectoryEntry entry = sidePanelFolderPool.GetNext();
                entry.Set(drive.Name, OnFolderClicked);
            }

            current = drives[0].Name;
        }
        
        private void ClearExplorer()
        {
            sidePanelFolderPool.DisableAll();
            bodyFolderPool.DisableAll();
            bodyFilePool.DisableAll();
        }

		#endregion


		#region Button Calls

		public void ButtonClose()
        {
            current = null;
            Exit();
        }

        public void ButtonMoveBack()
        {
            string next = Path.GetDirectoryName(current);
            if (Directory.Exists(next))
            {
                current = next;
                CreateBodyContent(current);
            }
        }

        public void ButtonMoveForth()
        {

        }

        public void ButtonSelect()
        {
            Exit();
        }

        public void ButtonCancel()
        {
            ButtonClose();
        }

        public void DropdownFilterChanged()
        {
            currentFilter = filterDropdown.value;
            CreateBodyContent(current);
        }

        #endregion
    }
}