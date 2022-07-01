# 1-Hafta-Odev
![1-odev](https://user-images.githubusercontent.com/95723369/175775209-eb119b21-ef50-4650-9c8a-b07c1feea55b.jpg)


2. Secenek secilmistir.

Doviz Kuru Hesaplama API
Bu API  parabimi kodlari ile calismaktadir.
<br/>

####  1. ozellik: Tek Para Birimi  (1to1) ceviri: Get/SingleCurrency

<br/>

Girilen para Biriminin (girdiBirimi) ciktiParabirimi'ndeki karsiligi gosterilmektedir.

<img src="https://github.com/215-Protein-NET-Bootcamp/1-hafta-odev-Trkrkrl/blob/main/CurrencyCalculator/CurrencyCalculator/Images/1.ozellik.png?raw=true"/>

<br/>

Hata Kontrolu
<img src="https://github.com/215-Protein-NET-Bootcamp/1-hafta-odev-Trkrkrl/blob/main/CurrencyCalculator/CurrencyCalculator/Images/1.ozellik-hata-kontrol.png?raw=true"/>

<br/>

####  2. ozellik: Tek Para Birimi  (1 to many  ) ceviri: Get/MultipleCurrency

<br/>

Girdi biriminin cikti birimlerindeki karsiliklari verilir.
<img src="https://github.com/215-Protein-NET-Bootcamp/1-hafta-odev-Trkrkrl/blob/main/CurrencyCalculator/CurrencyCalculator/Images/2.ozeliik.png?raw=true"/>

<br/>

2.ozellik hata kontrolu
<img src="https://github.com/215-Protein-NET-Bootcamp/1-hafta-odev-Trkrkrl/blob/main/CurrencyCalculator/CurrencyCalculator/Images/2.ozellik-hata-kontrol.png?raw=true"/>

<br/>

####  3. ozellik: Mevcut Butun parabirimlerini gosterir : Get/AvailableCurrencies
<br/>

<img src="https://github.com/215-Protein-NET-Bootcamp/1-hafta-odev-Trkrkrl/blob/main/CurrencyCalculator/CurrencyCalculator/Images/3.%C3%B6zellik.pngg?raw=true"/>

<br/>

####  Kullanilan arac ve eklentiler
RestSharp (son surum sikinti cikariyor bir alt surum kullaniniz)
NewtonsoftJson
Microsoft.Extensions.Configuration - appsettings'teki Api bilgilerine erisimde kullanildi.
Json2csharp  sitesi external apiden gelen formati tagleyerek .net'e uygun hale getirdi.
