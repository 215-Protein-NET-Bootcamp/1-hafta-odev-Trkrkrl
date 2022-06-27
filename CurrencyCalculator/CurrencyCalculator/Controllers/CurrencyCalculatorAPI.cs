using CurrencyCalculator.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }


    public class CurrencyCalculatorAPI : Controller
    {
        /* ICalculationService _calculationService;


         [HttpGet]
         public IActionResult GetBySpecs(int miktar,string girdiBirimi,string ciktiBirimi)
         {

             var result = _calculationService.GetBySpecs(miktar, girdiBirimi, ciktiBirimi);
             if (result.Success)
             {
                 return Ok(result);

             }
             return BadRequest(result);
         }


         */

        //direkt gelir mi acaba-----------------
        [HttpGet]

        public async Task<IActionResult> GetBySpecs(int miktar, string girdiBirimi, string ciktiBirimi)
        {

            //bu ozelliklere sahip bir girdi
            Girdi girdi = new Girdi()
            {
                Miktar = miktar,
                GirdiBirimi = girdiBirimi,
                CiktiBirimi = ciktiBirimi
            };

            //var adresYolu = "https://api.apilayer.com/exchangerates_data/convert?to=" + $"{ciktiBirimi}" + "&from=" + $"{girdiBirimi}" + "&amount=" + $"{miktar}";
            var adresYolu = "https://api.apilayer.com/exchangerates_data/convert?to={ciktiBirimi}&from={girdiBirimi}&amount={miktar}";

            var client = new RestClient(adresYolu);

            // client.Timeout = -1;


            var request = new RestRequest(Method.Get);

            request.AddHeader("apikey", "voPcVVVFrHYEI4qhECMzQqui4WWW6Qm");// api  keyi environmete atmalısın

            IRestResponse response = await client.ExecuteAsync(request);//burasını çözmek için restsharpın 106.15 versiyonu çalıyor sadece



            Cikti cikti = new Cikti()
            {
                Miktar = response.
                GirdiBirimi = response.query.to,
                CiktiBirimi = response.query.from

            };

            return cikti;

            //Console.WriteLine(response.Content);//APi den geleni cikti nin proplarina esitleyip atayacagim
        }


    }
}

