
public class Explorer { }


//using System;
//using System.IO;
//using UnityEngine;
//using UnityEngine.UI;

//namespace FileExplorer
//{
//    public class FileExplorer : MonoBehaviour
//    {
//        private static FileExplorer fileBrowserInstance;
//        private static FileExplorer folderBrowserInstance;
//        private const string AllFilesFilter = ".*";

//        [SerializeField] private Dropdown filterDropdown;
//        [SerializeField] private InputField inputField;

//        [SerializeField] private DirectoryEntryPool bodyFolderPool;
//        [SerializeField] private DirectoryEntryPool bodyFilePool;
//        [SerializeField] private DirectoryEntryPool sidePanelFolderPool;


//        private Action<string> onComplete;
//        private string[] filters;
//        private int currentFilter = 0;

//        private string current;

//        private void Awake()
//        {
//            if (fileBrowserInstance == null)
//            {
//                fileBrowserInstance = this;
//                fileBrowserInstance.gameObject.SetActive(false);
//            }
//            else
//            {
//                throw new DuplicateSingletonException("FileExplorer");
//            }
//        }


//        public static void Browse(Action<string> onComplete, params string[] filters)
//        {
//            fileBrowserInstance.StartFileBrowser(onComplete, filters);
//        }


//        private void StartFileBrowser(Action<string> onComplete, string[] filters)
//        {
//            this.onComplete = onComplete;

//            SetFilters(filters);
//            CreateSidePanelContent();
//            CreateBodyContent(current);

//            fileBrowserInstance.gameObject.SetActive(true);
//        }


//        private void SetFilters(string[] filters)
//        {
//            filterDropdown.options.Clear();

//            for (int i = 0; i < filters.Length; i++)
//            {
//                if (filters[i][0] != '.')
//                {
//                    filters[i] = '.' + filters[i];
//                }

//                Dropdown.OptionData data = new Dropdown.OptionData(filters[i]);
//                filterDropdown.options.Add(data);
//            }

//            this.filters = filters;
//        }

//        private void Exit()
//        {
//            onComplete.Invoke(current);
//            fileBrowserInstance.gameObject.SetActive(false);
//            ClearExplorer();
//        }


//        #region DirectoryEntry Methods

//        internal void OnFolderClicked(string path)
//        {
//            current = path;
//            CreateBodyContent(path);
//        }

//        internal void OnFileClicked(string path)
//        {
//            current = path;
//            inputField.text = Path.GetFileName(current);
//        }

//        #endregion


//        #region Visual Management

//        private void CreateBodyContent(string path)
//        {
//            CreateBodyContentFolders(path);
//            CreateBodyContentFiles(path);
//        }

//        private void CreateBodyContentFolders(string path)
//        {
//            if (!bodyFolderPool.IsSet)
//                return;

//            bodyFolderPool.DisableAll();

//            string[] directories = Directory.GetDirectories(path);

//            foreach (string dir in directories)
//            {
//                DirectoryInfo info = new DirectoryInfo(dir);
//                if ((info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
//                {
//                    DirectoryEntry entry = bodyFolderPool.GetNext();
//                    entry.Set(dir, OnFolderClicked);
//                }
//            }
//        }

//        private void CreateBodyContentFiles(string path)
//        {
//            if (!bodyFilePool.IsSet)
//                return;

//            bodyFilePool.DisableAll();

//            string filter = filters[currentFilter];
//            string[] files = Directory.GetFiles(path);
//            foreach (string file in files)
//            {
//                FileInfo info = new FileInfo(file);
//                if ((Path.GetExtension(file).ToUpper() == filter || filter == AllFilesFilter)
//                    && (info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
//                {
//                    DirectoryEntry entry = bodyFilePool.GetNext();
//                    entry.Set(file, OnFileClicked);
//                }
//            }
//        }

//        private void CreateSidePanelContent()
//        {
//            if (!sidePanelFolderPool.IsSet)
//                return;

//            sidePanelFolderPool.DisableAll();

//            DriveInfo[] drives = DriveInfo.GetDrives();

//            foreach (DriveInfo drive in drives)
//            {
//                DirectoryEntry entry = sidePanelFolderPool.GetNext();
//                entry.Set(drive.Name, OnFolderClicked);
//            }

//            current = drives[0].Name;
//        }

//        private void ClearExplorer()
//        {
//            if (sidePanelFolderPool.IsSet)
//                sidePanelFolderPool.DisableAll();

//            if (bodyFolderPool.IsSet)
//                bodyFolderPool.DisableAll();

//            if (bodyFilePool.IsSet)
//                bodyFilePool.DisableAll();
//        }

//        #endregion


//        #region Button Calls

//        public void ButtonClose()
//        {
//            current = null;
//            Exit();
//        }

//        public void ButtonMoveBack()
//        {
//            string next = Path.GetDirectoryName(current);
//            if (Directory.Exists(next))
//            {
//                current = next;
//                CreateBodyContent(current);
//            }
//        }

//        public void ButtonMoveForth()
//        {
//            throw new NotImplementedException();
//        }

//        public void ButtonSelect()
//        {
//            Exit();
//        }

//        public void ButtonCancel()
//        {
//            ButtonClose();
//        }

//        public void DropdownFilterChanged()
//        {
//            currentFilter = filterDropdown.value;
//            CreateBodyContent(current);
//        }

//        #endregion
//    }
//}
