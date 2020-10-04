using EgitimBilgisiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EgitimBilgisiService.YuksekOgretimEgitimBilgisiPortClient;

namespace UniLife.Vendor
{
    public class YokSoap
    {
        public async Task<string> EgitimBilgisiOgrenci()
        {
            YuksekOgretimEgitimBilgisiPortClient client = new YuksekOgretimEgitimBilgisiPortClient(EndpointConfiguration.YuksekOgretimEgitimBilgisiPortSoap11);
            client.ClientCredentials.UserName.UserName = ServisBilgi.UserName;
            client.ClientCredentials.UserName.Password = ServisBilgi.Password;

            EgitimBilgisiOgrenciRequestType req = new EgitimBilgisiOgrenciRequestType();
            req.Istek = new IstekBilgiTip()
            {
                IstekId = "ID",
                IstekTarihi = DateTime.Now.ToString(),
                KullaniciAdi = "kullanici.adi",
                UygulamaAdi = "uygulama.adi",
                UygulamaSunucuAdi = "sunucu.adi"
            };
            req.TcKimlikNo = 0;//TC

            //EgitimBilgisiOgrenciResponse res = client.EgitimBilgisiOgrenci(req); //BU da olabilir !!!! orjinali buydu.
            EgitimBilgisiOgrenciResponse1 res = await client.EgitimBilgisiOgrenciAsync(req);
            return res.EgitimBilgisiOgrenciResponse.Sonuc.SonucMesaj;
        }
    }
}
