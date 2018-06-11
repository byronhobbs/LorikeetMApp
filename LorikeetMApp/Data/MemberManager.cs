using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LorikeetMApp.Data
{
    public class MemberManager
    {
		IRestService restService;

		public MemberManager(IRestService service)
        {
			restService = service;
        }

		public async Task<List<LorikeetMApp.Models.Member>> GetMembersAsync()
		{
			string access_token = await restService.LoginAsync(Constants.Username, Constants.Password);
			return await restService.RefreshMemberDataAsync(access_token);
		}

		public async Task<List<LorikeetMApp.Models.Contact>> GetContactsAsync()
		{
			string access_token = await restService.LoginAsync(Constants.Username, Constants.Password);
			return await restService.RefreshContactDataAsync(access_token);
		}
    }
}
