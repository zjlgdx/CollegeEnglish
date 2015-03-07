using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CollegeEnglish.Common
{
    class StorageDataHelper
    {
        public const string RootFolder = "WP.CE";

        public static async Task<string> GetJsonFileNameFromMusicLibraryAsync(string foldername, string filename)
        {
            try
            {
                var devices = KnownFolders.RemovableDevices;

                var sdCards = await devices.GetFoldersAsync();

                if (sdCards.Count == 0) return string.Empty;

                var firstCard = sdCards[0];

                StorageFolder folder = await firstCard.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
                StorageFolder subfolder = await folder.CreateFolderAsync("Json", CreationCollisionOption.OpenIfExists);

                var file = await subfolder.GetFileAsync(filename);
                return file.Path;
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("File not found");

                return string.Empty;
            }


        }

        public static async Task<string> GetAudioFileFromMusicLibraryAsync(string foldername, string filename)
        {
            try
            {
                var devices = KnownFolders.RemovableDevices;

                var sdCards = await devices.GetFoldersAsync();

                if (sdCards.Count == 0) return string.Empty;

                var firstCard = sdCards[0];

                StorageFolder folder = await firstCard.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
                StorageFolder subfolder = await folder.CreateFolderAsync("audio", CreationCollisionOption.OpenIfExists);
                var file = await subfolder.GetFileAsync(filename);
                return file.Path;
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("File not found");

                return string.Empty;
            }


        }


        public async static Task writeTextToSDCard(string foldername, string filename, string logData)
        {
            var devices = Windows.Storage.KnownFolders.RemovableDevices;

            var sdCards = await devices.GetFoldersAsync();

            if (sdCards.Count == 0) return;

            var firstCard = sdCards[0];

            StorageFolder notesFolder = await firstCard.CreateFolderAsync(foldername, CreationCollisionOption.OpenIfExists);
            StorageFolder subfolder = await notesFolder.CreateFolderAsync("Json", CreationCollisionOption.OpenIfExists);
            StorageFile file = await subfolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, logData);
        }

        public async static Task<string> readTextFromSDCard(string foldername, string filename)
        {
            var devices = Windows.Storage.KnownFolders.RemovableDevices;

            var sdCards = await devices.GetFoldersAsync();

            if (sdCards.Count == 0) return null;

            var firstCard = sdCards[0];
            StorageFolder notesFolder = await firstCard.GetFolderAsync(foldername);
            StorageFolder subfolder = await notesFolder.GetFolderAsync("Json");
            StorageFile file = await subfolder.GetFileAsync(filename);

            string result = await FileIO.ReadTextAsync(file);
            return result;


        }

        public static async Task<string> GetCourse(string courseId)
        {
            var bookId = courseId.Substring(0, 1);
            var unitIndex = GetUnitFolder(courseId);
            var subfolder = Constants.DATA_BASE_PATH + ("integrated" + bookId);
            var subsubfolder = subfolder + "/" + unitIndex;

            var file = subsubfolder + "/" + (GetUnitJsonFileFolder(courseId, unitIndex));
            StorageFile sourceFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(file.Replace("\\", "/")));
            string result = await FileIO.ReadTextAsync(sourceFile);
            return result;
        }

        private static string GetUnitFolder(string courseId)
        {
            var index = courseId.IndexOf("/");
            var unitIndex = courseId.Substring(index + 1, 2);
            return unitIndex;
        }

        private static string GetUnitJsonFileFolder(string courseId, string unitIndex)
        {
            if (courseId.Contains("p3newword1"))
            {
                return unitIndex+"_B.json";
            }

            return unitIndex + "_A.json";
        }

        //E:\collegeEnglish\integrated1\unitlist
        public static async Task<string> GetUnitlist(string bookId)
        {
            var basefoler = Constants.DATA_BASE_PATH;
            var file = basefoler + ("integrated" + bookId + "/unitlist/UnitList.json");

            StorageFile sourceFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(file.Replace("\\", "/")));
            string result = await FileIO.ReadTextAsync(sourceFile);
            return result;
        }
    }
}
