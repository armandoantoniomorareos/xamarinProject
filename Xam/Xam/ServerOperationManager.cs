using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Xam
{
   public  class ServerOperationManager<T>
    {
        private HttpClient client;
        private string apiKey = "apiKey=vYmaV0JFyk3iA8uGh0ZqbcPkL4uL8Etr";
        private string db_name = "/xamarindos";
        private string base_url = Uri.EscapeUriString("https://api.mlab.com/api/1/databases");
        

        public ServerOperationManager()
        {
            client = new HttpClient(new NativeMessageHandler());
        }


        //TODO
        public async Task<bool> validateIfDataExist(string collection, string field)
        {
            return true;
        }


        public async Task<List<T>> getLatestId(string collectionName)
        {
            List<T> t = await consultInfo(collectionName);
            return t;
        }
        
        //TODO
        /*public async Task<bool> updateDocument(string collection, T item)
        {
           
            string updateUrl = base_url + "&q={\"_id\" : {\"oid\" : \"" + id.oid + "\"}}";

            Uri url_t = new Uri(updateUrl);
            string output = JsonConvert.SerializeObject(p);
            var content = new StringContent(output, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PutAsync(url_t, content);
            return true;
        }*/
        
        public async Task<T> getId(string collection, string fieldToSearch, string value)
        {
            string query = "{\""+fieldToSearch+"\":\""+value+"\"}";
            List<T> data= await consultInfo(collection, query);
            return data[0];
            
        }

        public async Task<bool> deleteDocument(string collection, string id)
        {
            string url = base_url + db_name + "/collections/" + collection + "?" + id + "&" + apiKey;
        
            HttpResponseMessage response = null;
           
            try
            {
                response = await client.DeleteAsync(url);

            }catch(Exception err)
            {
                return false;
            }

            return response.IsSuccessStatusCode;
        }

        
        public async Task<bool> addDocument(string collectionName, T item, bool isNew)
        {
            string url = base_url + db_name + "/collections/" + collectionName + "?" + apiKey;

            Uri url_t = new Uri(url);
            string output = JsonConvert.SerializeObject(item);
            var content = new StringContent(output, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(url_t, content);
            return response.IsSuccessStatusCode;
        }


        //Get data base information according a query gave by the user
        //TODO: use a generic type instead of a string for query
        public async Task<List<T>> consultInfo(string collectionName, string query = "")

        {
            List<T> data = null;
            try
            {

                string url = base_url + db_name + "/collections/" + collectionName + "?" + apiKey + "&q=" + query;
                var response2 = await client.GetAsync(url);
                if (response2.IsSuccessStatusCode)
                {
                    string content = await response2.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return data;

        }
    }
}
