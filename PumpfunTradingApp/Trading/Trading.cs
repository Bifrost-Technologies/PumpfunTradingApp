using Newtonsoft.Json;

namespace TradingTower
{
    public static class Trading
    {

        public static async Task<Pumpfundata?> GetPumpfunData(string tokenaddress)
        {
            string url = "https://frontend-api.pump.fun/coins/" + tokenaddress;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Pumpfundata? pfdata = JsonConvert.DeserializeObject<Pumpfundata>(responseBody);
                if (pfdata != null)
                {
                    return pfdata;
                }
                else
                {
                    return null;
                }
            }
        }
        public static async Task<string?> GetRaydiumPool(string tokenaddress)
        {
            string url = "https://frontend-api.pump.fun/coins/" + tokenaddress;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Pumpfundata? pfdata = JsonConvert.DeserializeObject<Pumpfundata>(responseBody);
                if (pfdata != null)
                {
                    if (pfdata.raydium_pool != null && pfdata.raydium_pool != "")
                    {
                        return pfdata.raydium_pool;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
        }
    }
}
