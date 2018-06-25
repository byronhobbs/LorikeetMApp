using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace LorikeetMApp.Data
{
	public interface IRestService
	{
		Task<string> LoginAsync(string userName, string password);
		Task<List<LorikeetMApp.Models.Member>> RefreshMemberDataAsync(string access_token);
		Task<List<LorikeetMApp.Models.Contact>> RefreshContactDataAsync(string access_token);
        Task<string> DownloadImageFileAsync(string file, string pathToSave); 
	}
}
