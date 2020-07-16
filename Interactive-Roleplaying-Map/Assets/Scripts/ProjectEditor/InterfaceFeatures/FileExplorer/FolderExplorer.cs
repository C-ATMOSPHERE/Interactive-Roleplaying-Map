using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UnityFileExplorer
{
    internal class FolderExplorer : MonoBehaviour
	{
        private static FolderExplorer instance;

        [SerializeField] protected InputField inputField;
        [SerializeField] protected DirectoryEntryPool bodyFolderPool;
        [SerializeField] protected DirectoryEntryPool sidePanelFolderPool;
        [SerializeField] protected GameObject screenBlocker;

        protected Action<string> onComplete;
        protected string current;


        public static void BrowseFolder(Action<string> onComplete)
        {
            instance.StartBrowse(onComplete);
        }


        virtual protected void Awake()
        {
            if (instance == null)
            {
                instance = this;
                instance.gameObject.SetActive(false);
            }
            else
            {
                throw new DuplicateSingletonException("Folder Explorer");
            }
        }


        protected virtual void StartBrowse(Action<string> onComplete)
        {
            this.onComplete = onComplete;

            screenBlocker.SetActive(true);
            gameObject.SetActive(true);

            CreateSidePanelContent(out current);
            CreateBodyFolders(current);
        }

        protected void CreateSidePanelContent(out string startPoint)
        {
            if (!sidePanelFolderPool.IsSet)
            {
                startPoint = null;
                return;
            }

            sidePanelFolderPool.DisableAll();

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                DirectoryEntry entry = sidePanelFolderPool.GetNext();
                entry.Set(drive.Name, OnFolderClicked);
            }

            startPoint = drives[0].Name;
        }

        protected void CreateBodyFolders(string path)
        {
            if (!bodyFolderPool.IsSet)
            {
                return;
            }

            bodyFolderPool.DisableAll();

            string[] directories = Directory.GetDirectories(path);

            foreach (string dir in directories)
            {
                DirectoryInfo info = new DirectoryInfo(dir);
                if ((info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    DirectoryEntry entry = bodyFolderPool.GetNext();
                    entry.Set(dir, OnFolderClicked);
                }
            }
        }


        protected void Exit()
        {
            gameObject.SetActive(false);
            screenBlocker.SetActive(false);
            onComplete.Invoke(current);
            ClearExplorer();
        }

        protected virtual void ClearExplorer()
        {
            if (sidePanelFolderPool.IsSet)
            {
                sidePanelFolderPool.DisableAll();
            }

            if (bodyFolderPool.IsSet)
            {
                bodyFolderPool.DisableAll();
            }
        }


        protected virtual void OnFolderClicked(string path)
        {

            if (current == path)
            {
                CreateBodyFolders(path);
            }
            else
            {
                current = path;
                inputField.text = new DirectoryInfo(path).Name;
            }
        }


        public virtual void ButtonClose()
        {
            current = null;
            Exit();
        }


        public virtual void ButtonMoveBack()
        {
            string next = Path.GetDirectoryName(current);
            if (Directory.Exists(next))
            {
                current = next;
                CreateBodyFolders(current);
            }
        }

        public virtual void ButtonMoveForth()
        {
            throw new NotImplementedException();
        }

        public virtual void ButtonSelect()
        {
            Exit();
        }

        public virtual void ButtonCancel()
        {
            ButtonClose();
        }
    }
}
