using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace BitInformation
{
    public class Repositorio
    {
        public RootObject Get()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var responseString = client.GetStringAsync("https://api.bitvalor.com/v1/ticker.json").Result;
                    var root = JsonConvert.DeserializeObject<RootObject>(responseString);

                    return root;

                }

            }
            catch (Exception ex)
            {
                return new RootObject();
            }
        }
    }
}
