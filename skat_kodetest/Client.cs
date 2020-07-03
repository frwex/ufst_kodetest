using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace skat_kodetest
{
    public class Client : IDisposable
    {
        public async Task<T> Post<T>(string url, object obj)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var result = await client.PostAsync(url, content);
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
        }

        public async Task<T> Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var data = await client.GetAsync(url);
                    return JsonConvert.DeserializeObject<T>(data.Content.ReadAsStringAsync().Result);
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
