using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using Newtonsoft.Json;
//using Quartz;
//using Quartz.Impl;

namespace the_Cryptocurrencies_bot
{
    public class API
    {
        static async Task Main(string[] args)
        {
            ConeccionAPI api = new ConeccionAPI();
            var botClient = new TelegramBotClient("5138490742:AAFK7ZVo8CDaggoCYWc716c1llfqnJCx0zk");
            var chatId = "@CryptosWorldTrading";
            Message message = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text:
                $"BTC en las ultimas 24h\n" +
                $"Symbol = {await api.APISymbol()}\n" +
                $"Price = {await api.APIPrice()}\n" +
                $"Change 24h = {await api.APIChangeRate()}%\n" +
                $"Low = {await api.APILow()}\n" +
                $"High = {await api.APIHigh()}");
            //bool a = false;
            //while (a == false)
            //{
            //    DateTime dateTime = DateTime.Now; ;
            //    if (dateTime.Hour == 20)
            //    {
            //        a = true;
            //    }
            //    if (a == true && dateTime.Minute == 05)
            //    {
            //        a = true;
            //    }
            //    else
            //    {
            //        a = false;
            //    }
            //}
            //    // 1. Create a scheduler Factory
            //    ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            //    // 2. Get and start a scheduler
            //    IScheduler scheduler = await schedulerFactory.GetScheduler();
            //    await scheduler.Start();
            //    // 3. Create a job
            //    IJobDetail job = JobBuilder.Create<SimpleJob>()
            //            .WithIdentity("number generator job", "number generator group")
            //            .Build();
            //    // 4. Create a trigger
            //    ITrigger trigger = TriggerBuilder.Create()
            //        .WithIdentity("number generator trigger", "number generator group")
            //        .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
            //        .Build();
            //    // 5. Schedule the job using the job and trigger 
            //    await scheduler.ScheduleJob(job, trigger);
            //    Console.ReadLine();
            //var botClient = new TelegramBotClient("5138490742:AAFK7ZVo8CDaggoCYWc716c1llfqnJCx0zk");



            //var me = await botClient.GetMeAsync();

            //await EnviarMensaje();
        }

        

        
    }

    //public class SimpleJob : IJob
    //{
    //    async Task IJob.Execute(IJobExecutionContext context)
    //    {
    //        int a = 1;
    //        Console.WriteLine($"{a}");
    //        a++;
    //        ConeccionAPI api = new ConeccionAPI();
    //        var botClient = new TelegramBotClient("5138490742:AAFK7ZVo8CDaggoCYWc716c1llfqnJCx0zk");
    //        var chatId = "@abcde123456789abcde123";
    //        Message message = await botClient.SendTextMessageAsync(
    //            chatId: chatId,
    //            text:
    //            $"Symbol = {await api.APISymbol()}\n" +
    //            $"Price = {await api.APIPrice()}\n" +
    //            $"Change 24h = {await api.APIChangeRate()}%\n" +
    //            $"Low = {await api.APILow()}\n" +
    //            $"High = {await api.APIHigh()}");


    //        //return Task.CompletedTask;

    //    }
    //}

    public class ConeccionAPI
    {
        public async Task<string> APISymbol()
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

        public async Task<string> APIPrice()
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

        public async Task<string> APIChangeRate()
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

        public async Task<string> APILow()
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

        public async Task<string> APIHigh()
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
    }
}