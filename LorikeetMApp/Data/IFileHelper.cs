namespace LorikeetMApp.Data
{
	public interface IFileHelper
    {
		string GetLocalFilePath(string filename);
		bool GetSqlFileExists(string filename);
		void DeleteLocalDBFile(string filename);
    }
}
