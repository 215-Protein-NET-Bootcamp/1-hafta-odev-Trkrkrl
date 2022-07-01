using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CurrencyCalculator.Entities
{
    public class Entity
    {
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

        public class Root1
        {
            [JsonPropertyName("query")]
            public Query Query { get; set; }

            [JsonPropertyName("result")]
            public double Result { get; set; }

            [JsonPropertyName("success")]
            public bool Success { get; set; }
            //-
            [JsonPropertyName("base")]
            public string Base { get; set; }
            [JsonPropertyName("rates")]
            public IDictionary<string, double> Rates { get; set; }
            //-

            [JsonPropertyName("symbols")]
            public IDictionary<string, string> Symbols { get; set; }



           

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
    }
}
