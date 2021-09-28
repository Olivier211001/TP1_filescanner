using FileScanner.Commands;
using FileScanner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FileScanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string selectedFolder;
        private ObservableCollection<ContentFolder> folderItems = new ObservableCollection<ContentFolder>();
        private ContentFolder currentContent;

        public DelegateCommand<string> OpenFolderCommand { get; private set; }
        public DelegateCommand<string> ScanFolderCommand { get; private set; }

        public ObservableCollection<ContentFolder>  FolderItems { 
            get =>  folderItems;
            set 
            { 
                folderItems = value;
                OnPropertyChanged();
            }
        }

       

        public string SelectedFolder
        {
            get => selectedFolder;
            set
            {
                selectedFolder = value;
                OnPropertyChanged();
                ScanFolderCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel()
        {
            OpenFolderCommand = new DelegateCommand<string>(OpenFolder);
            ScanFolderCommand = new DelegateCommand<string>(ScanFolder, CanExecuteScanFolder);
        }

        private bool CanExecuteScanFolder(string obj)
        {
            return !string.IsNullOrEmpty(SelectedFolder);
        }

        private void OpenFolder(string obj)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    SelectedFolder = fbd.SelectedPath;
                }
            }
        }

        private async void ScanFolder(string dir)
        {

            await GetDirs(dir); 
        }

        private async Task GetDirs(string dir)
        {
            try
            {
                foreach (var d in Directory.EnumerateDirectories(dir, "*"))
                {
                    currentContent = new ContentFolder(d, "../ViewModels/folder.jpg");
                    folderItems.Add(currentContent);
                    foreach (var f in Directory.EnumerateFiles(dir, "*"))
                    {
                        currentContent = new ContentFolder(f, "../ViewModels/fichier.png");
                        folderItems.Add(currentContent);
                    }
                    await GetDirs(d);
                }
            }
            catch (Exception e)
            { }        
        }

        ///TODO : Tester avec un dossier avec beaucoup de fichier
        ///TODO : Rendre l'application asynchrone
        ///TODO : Ajouter un try/catch pour les dossiers sans permission


    }
}
