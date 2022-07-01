
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
using static CurrencyCalculator.Entities.Entity;

namespace CurrencyCalculator.Controllers
{
    


    public class CurrencyCalculatorAPI : Controller
    {
        private readonly IConfiguration _configuration;

        public CurrencyCalculatorAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        [HttpGet("SingleCurrency")]
        

        public async Task<IActionResult> GetBySpecs([FromQuery]int miktar, string girdiBirimi, string ciktiBirimi)
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

            
            
            
                var donus = JsonConvert.DeserializeObject<Root1>(response.Content);//newtonsoft json gerekli-<> içerisine query yazınca olmuyor-root yaz asagida o kökten ilerle
           

                var sonuc = JsonConvert.DeserializeObject<Root1>(response.Content);//toplam para tutari ve issuccesful veriyor
                
                 

           
            if (sonuc.Success)
            {
                Cikti cikti = new Cikti()
                {
                    Miktar = donus.Query.Amount,
                    GirdiBirimi = donus.Query.From,
                        
                    CiktiBirimi = donus.Query.To,
                    Sonuc = sonuc.Result//buralari donus olarak duzelt

                };
                return Ok(cikti);

            }
            else 
                return BadRequest("GİRDİĞİNİZ  BİLGİLERİ KONTROL EDİNİZ");//ok-apiye rastgele hatalı veri girince bu fon çalışıyot




            //--- LATEST METHODU İLE  TO ya birden çok para birimi atayarak bir çözüm vereceğiz





        }


        [HttpGet("MultipleCurrencies")]
        public async Task<IActionResult> GetLatest(string ciktiBirimleri, string girdiBirimi)
        {
            string symbols = ciktiBirimleri.Replace(@",", "%2C");
            
            var adresYolu = "https://api.apilayer.com/exchangerates_data/latest?symbols=" + $"{symbols}" + "&base=" + $"{girdiBirimi}";
            var client = new RestClient(adresYolu);

           

            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            var apiKey = _configuration.GetValue<string>("API_KEY");

            request.AddHeader("apikey", apiKey);
          

            IRestResponse response = client.Execute(request);
            
            var donus = JsonConvert.DeserializeObject<Root1>(response.Content);
         

            if (donus.Success)
            {
                List<Cikti> list= new List<Cikti>();

                foreach (var currency in donus.Rates)
                {
                    Cikti cikti = new Cikti()
                    {
                        Miktar = 1,
                        GirdiBirimi =girdiBirimi ,

                        CiktiBirimi = currency.Key,
                        Sonuc = currency.Value

                    };
                    list.Add(cikti);

                }
                return Ok(list);
                
                
            }
            else
                return BadRequest("GİRDİĞİNİZ  BİLGİLERİ KONTROL EDİNİZ");

        }
        //--
        [HttpGet("AvailableCurrencies")]
        public async Task<IActionResult> GetAvailables()
        {
            var client = new RestClient("https://api.apilayer.com/exchangerates_data/symbols");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            var apiKey = _configuration.GetValue<string>("API_KEY");
            request.AddHeader("apikey", apiKey);

            IRestResponse response = client.Execute(request);



            var donus = JsonConvert.DeserializeObject<Root1>(response.Content);


            if (donus.Success)
            {
                return Ok(donus.Symbols);
            }
            else return BadRequest("Hatamız varsa affola");
        }


    }
    
}

