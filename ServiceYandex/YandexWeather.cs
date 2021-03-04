using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using HtmlAgilityPack; 
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace ServiceYandex
{
    public partial class YandexWeather : ServiceBase
    {
        private const string UrlTemplate = @"https://yandex.ru/pogoda/kotlas";

        public YandexWeather()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            GetWeather();
        }

        protected override void OnStop()
        {
            Thread.Sleep(1000);
        }
        public async void GetWeather()
        {
            while (true)
            {
                string[] arr = File.ReadLines(@"d:\gw.txt").ToArray();//считываем данные
                string stroka1 = arr[0];
                string stroka2 = arr[1];
                string stroka3 = arr[2];
                string stroka4 = arr[3];
                string stroka5 = arr[4];
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, UrlTemplate);
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage responce = await httpClient.SendAsync(requestMessage);
                var res = await responce.Content.ReadAsStringAsync();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(res);
                HtmlNode span = doc.DocumentNode.SelectSingleNode("//div[@class='temp fact__temp fact__temp_size_s']");
                //    Console.WriteLine($"надпись1 - {span.InnerText}");
                stroka1 = $"{DateTime.Now.ToString("dd.mm.yyyy hh:mm:ss")} Температура - ";
                File.WriteAllText(@"d:\gw4.txt", $"{stroka1}" + "\n324\n341\n3412\n34512\n" + $"{span.InnerText}");// создаем файл
                Thread.Sleep(20000);
            }
            //File.WriteAllText("gw4.txt", "3412\n324\n341\n3412\n34512\n"+ $"{span.InnerText}");// создаем файл
            //File.WriteAllText("gw4.txt", $"{span.InnerText}");// создаем файл
        }

    }
}
