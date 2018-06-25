using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LorikeetMApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace LorikeetMApp.Data
{
	public class RestService : IRestService
    {
		HttpClient client;

		public List<Member> Members { get; private set; }
		public List<Contact> Contacts { get; private set; }

        public RestService()
		{
            client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<string> LoginAsync(string userName, string password)
		{
			try
			{
				object userInfos = new { email = userName, password = password };
				var jsonObj = JsonConvert.SerializeObject(userInfos);

				StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
				var request = new HttpRequestMessage()
				{
					RequestUri = new Uri(Constants.TOKEN_URL),
					Method = HttpMethod.Post,
					Content = content
				};

				var response = await client.SendAsync(request);

				if (response.IsSuccessStatusCode)
				{
					var content2 = await response.Content.ReadAsStringAsync();
					JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content2);
					var accessToken = jwtDynamic.Value<string>("accessToken");
					return accessToken;
				}
				else
				{
					Debug.WriteLine(response.ReasonPhrase);
					return null;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<List<Member>> RefreshMemberDataAsync(string access_token)
		{
			try
            {                
			    Members = new List<Member>();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
				var json = await client.GetStringAsync(Constants.MEMBER_URL);
				Members = JsonConvert.DeserializeObject<List<Member>>(json);
			}
            catch
            {
				return null;
            }
            
            return Members;
		}

		public async Task<List<Contact>> RefreshContactDataAsync(string access_token)
		{
			try
			{
			    Contacts = new List<Contact>();
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
				var json = await client.GetStringAsync(Constants.CONTACT_URL);
				Contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
            }
            catch
            {
				return null;
            }
            
			return Contacts;
		}

        public async Task<string> DownloadImageFileAsync(string file, string pathToSave)
        {
            Stream responseStream = null;
            FileStream fileStream = null;
            HttpResponseMessage response = null;

            try
            {
                var uri = new Uri(string.Concat(Constants.DOWNLOAD_URL, file));

                response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    responseStream = await response.Content.ReadAsStreamAsync();
                    fileStream = new FileStream(pathToSave, FileMode.Create, FileAccess.Write, FileShare.None);

                    byte[] buffer = new byte[1024];
                    int length = responseStream.Read(buffer, 0, buffer.Length);
                    while (length > 0)
                    {
                        fileStream.Write(buffer, 0, length);
                        fileStream.Flush();
                        length = responseStream.Read(buffer, 0, buffer.Length);
                    }
                }
                else return "Couldn't Download file";
            }
            catch (WebException ex)
            {
                return ex.Message + " - " + response.ReasonPhrase;
            }
            finally
            {
                if (fileStream != null) fileStream.Close();
                if (responseStream != null) responseStream.Close();
            }
            return "worked";
        }
    }
}
