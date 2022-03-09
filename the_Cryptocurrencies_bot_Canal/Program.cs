using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using Newtonsoft.Json;

namespace the_Cryptocurrencies_bot
{
    public class API
    {

        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("5138490742:AAFK7ZVo8CDaggoCYWc716c1llfqnJCx0zk");

            

            var me = await botClient.GetMeAsync();

            await EnviarMensaje();

           
        }

        public static async Task<string> APISymbol()
        {
            var url = "https://api.kucoin.com/api/v1/market/stats?symbol=BTC-USDT";

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error conectando a la api");
                throw new Exception();
            }

            var json = await response.Content.ReadAsStringAsync();

            var info = JsonConvert.DeserializeObject<Root>(json);

            return info.Data.Symbol;

        }

        public static async Task<string> APIPrice()
        {
            var url = "https://api.kucoin.com/api/v1/market/stats?symbol=BTC-USDT";

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error conectando a la api");
                throw new Exception();
            }

            var json = await response.Content.ReadAsStringAsync();

            var info = JsonConvert.DeserializeObject<Root>(json);

            return info.Data.Buy;

        }

        public static async Task<string> APIChangeRate()
        {
            var url = "https://api.kucoin.com/api/v1/market/stats?symbol=BTC-USDT";

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error conectando a la api");
                throw new Exception();
            }

            var json = await response.Content.ReadAsStringAsync();

            var info = JsonConvert.DeserializeObject<Root>(json);

            string cambio = info.Data.ChangeRate;

            decimal cambioD = decimal.Parse(cambio);

            decimal resultado = cambioD * 100;

            cambio = resultado.ToString();

            int t = 0;
            string abc = string.Empty;
            bool q = false;
            for (int i = 0; i < cambio.Length; i++)
            {
                abc += cambio[i];
                if (q == true)
                {
                    t++;
                }
                if (cambio[i] == '.')
                {
                    q = true;
                }

                if (t == 2)
                {
                    i = cambio.Length - 1;
                }
            }

            return abc;
        }

        public static async Task<string> APILow()
        {
            var url = "https://api.kucoin.com/api/v1/market/stats?symbol=BTC-USDT";

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error conectando a la api");
                throw new Exception();
            }

            var json = await response.Content.ReadAsStringAsync();

            var info = JsonConvert.DeserializeObject<Root>(json);

            return info.Data.Low;

        }

        public static async Task<string> APIHigh()
        {
            var url = "https://api.kucoin.com/api/v1/market/stats?symbol=BTC-USDT";

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error conectando a la api");
                throw new Exception();
            }

            var json = await response.Content.ReadAsStringAsync();

            var info = JsonConvert.DeserializeObject<Root>(json);

            return info.Data.High;

        }

        //// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Ticker
        {
            [JsonProperty("time")]
            public long Time { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("buy")]
            public string Buy { get; set; }

            [JsonProperty("sell")]
            public string Sell { get; set; }

            [JsonProperty("changeRate")]
            public string ChangeRate { get; set; }

            [JsonProperty("changePrice")]
            public string ChangePrice { get; set; }

            [JsonProperty("high")]
            public string High { get; set; }

            [JsonProperty("low")]
            public string Low { get; set; }

            [JsonProperty("vol")]
            public string Vol { get; set; }

            [JsonProperty("volValue")]
            public string VolValue { get; set; }

            [JsonProperty("last")]
            public string Last { get; set; }

        }

        public class Root
        {
            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("data")]
            public Ticker Data { get; set; }
        }

        public static async Task EnviarMensaje()
        {
            var botClient = new TelegramBotClient("5138490742:AAFK7ZVo8CDaggoCYWc716c1llfqnJCx0zk");
            var chatId = "@CryptosWorldTrading";
            Message message = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text:
                $"Symbol = {await APISymbol()}\n" +
                $"Price = {await APIPrice()}\n" +
                $"Change 24h = {await APIChangeRate()}%\n" +
                $"Low = {await APILow()}\n" +
                $"High = {await APIHigh()}");
        }
    }
}