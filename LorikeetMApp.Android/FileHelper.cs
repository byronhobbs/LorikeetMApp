using System;
using System.IO;
using Xamarin.Forms;
using LorikeetMApp.Droid;
using LorikeetMApp.Data;

[assembly: Dependency(typeof(FileHelper))]
namespace LorikeetMApp.Droid
{
	public class FileHelper : IFileHelper
	{
		public void DeleteLocalDBFile(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, filename);

			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		public string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
		}

		public bool GetSqlFileExists(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string filePath = Path.Combine(path, filename);

			if (File.Exists(filePath))
				return true;
			else return false;
		}
	}
}
