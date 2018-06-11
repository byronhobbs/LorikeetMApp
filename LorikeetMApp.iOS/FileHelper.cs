using System;
using System.IO;
using Xamarin.Forms;
using LorikeetMApp.iOS;
using LorikeetMApp.Data;

[assembly: Dependency(typeof(FileHelper))]
namespace LorikeetMApp.iOS
{
	public class FileHelper : IFileHelper
    {
        public FileHelper()
        {
        }

		public void DeleteLocalDBFile(string filename)
		{
			string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

			string filePath = Path.Combine(libFolder, filename);

			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		public string GetLocalFilePath(string filename)
		{
			string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
		}

		public bool GetSqlFileExists(string filename)
		{
			string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

			string filePath = Path.Combine(libFolder, filename);

			if (File.Exists(filePath))
				return true;
			else return false;
		}

        
	}
}
