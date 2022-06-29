
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CurrencyCalculator.Controllers
{


    public class Doviz
    {
        public int Miktar { get; set; }
        public string GirdiBirimi { get; set; }
        public string CiktiBirimi { get; set; }
        public DateTime Date { get; set; }

    }
    public class Girdi
    {
        public int Miktar { get; set; }
        public string GirdiBirimi { get; set; }
        public string CiktiBirimi { get; set; }
    }
    public class Cikti
    {
        public int Miktar { get; set; }
        public string GirdiBirimi { get; set; }
        public string CiktiBirimi { get; set; }
        public double Sonuc { get; set; }
    }


    public class CurrencyCalculatorAPI : Controller
    {
        private readonly IConfiguration _configuration;//api için lazım- bunu ctorla

        public CurrencyCalculatorAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //-
        public class Query//json to c# sitesinden  cevirdik bunu- hangisi olduğunu deserializeda anlasın diye
        {
            [JsonPropertyName("amount")]
            public int Amount { get; set; }

            [JsonPropertyName("from")]
            public string From { get; set; }

            [JsonPropertyName("to")]
            public string To { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("query")]
            public Query Query { get; set; }

            [JsonPropertyName("result")]
            public double Result { get; set; }

            [JsonPropertyName("success")]
            public bool Success { get; set; }
        }

        //-

        [HttpGet("GetBySpecs")]
        

        public async Task<IActionResult> GetBySpecs(int miktar, string girdiBirimi, string ciktiBirimi)
        {
                                
            Girdi girdi = new Girdi()
            {
                Miktar = miktar,
                GirdiBirimi = girdiBirimi,
                CiktiBirimi = ciktiBirimi
            };

            var adresYolu = "https://api.apilayer.com/exchangerates_data/convert?to=" + $"{ciktiBirimi}" + "&from=" + $"{girdiBirimi}" + "&amount=" + $"{miktar}";
            //var adresYolu = "https://api.apilayer.com/exchangerates_data/convert?to={ciktiBirimi}&from={girdiBirimi}&amount={miktar}";-bu çalışmadı
            var client = new RestClient(adresYolu);

             client.Timeout = -1; 


            var request = new RestRequest(Method.GET);

            var apiKey = _configuration.GetValue<string>("API_KEY");   //configuration paketi yukledim

            request.AddHeader("apikey", apiKey);// api  keyi environmete atmalısın

            IRestResponse response = await client.ExecuteAsync(request);//burasını çözmek için restsharpın 106.15 versiyonu çalıyor sadece

            Console.WriteLine(response.Content);
            
            
                var donus = JsonConvert.DeserializeObject<Root>(response.Content);//newtonsoft json gerekli-<> içerisine query yazınca olmuyor-root yaz asagida o kökten ilerle
           

                var sonuc = JsonConvert.DeserializeObject<Root>(response.Content);//toplam para tutari ve issuccesful veriyor


           
            if (sonuc.Success)
            {
                Cikti cikti = new Cikti()
                {
                    Miktar = donus.Query.Amount,
                    GirdiBirimi = donus.Query.From,
                    CiktiBirimi = donus.Query.To,
                    Sonuc = sonuc.Result//ok bunu alıyoz

                };
                return Ok(cikti);

            }
            else 
                return BadRequest("GİRDİĞİNİZ  BİLGİLERİ KONTROL EDİNİZ");//ok-apiye rastgele hatalı veri girince bu fon çalışıyot




            //--- LATEST METHODU İLE  TO ya birden çok para birimi atayarak bir çözüm vereceğiz





        }


    }
}

