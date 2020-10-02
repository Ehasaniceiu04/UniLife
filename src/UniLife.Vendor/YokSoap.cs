using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniLife.Vendor
{
    public class YokSoap
    {
        

        public string AskerlikErtelemeTalepGonder()
        {
            try
            {
                string styleCreateUrl = "https://servisler.yok.gov.tr/rest/obs/askerlikErtelemeTalep";
                var client = new RestSharp.RestClient(styleCreateUrl);
                client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(ServisBilgi.UserName, ServisBilgi.Password);
                client.PreAuthenticate = true;
                AskerlikErtelemeTalep askerlikErtelemeTalep = new AskerlikErtelemeTalep
                {
                    tcKimlikNo = 0L,
                    birimId = 1L,
                    teklifTuru = TeklifTuru.E,
                    teklifNedeniNo = 901,
                    azamiSureArtUnsur = 21,
                    ilaveSure = 1,
                    imzalayanTcNo = 1,
                    imzalayanAdSoyad = "Ali"
                };

                var js = Newtonsoft.Json.JsonConvert.SerializeObject(askerlikErtelemeTalep);
                var request = new RestSharp.RestRequest(RestSharp.Method.POST);
                request.AddParameter("application/json; charset=utf-8", js, RestSharp.ParameterType.RequestBody);
                var response = client.Execute(request);

                return response.Content;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        class AskerlikErtelemeTalep
        {
            public long tcKimlikNo { get; internal set; }
            public long birimId { get; internal set; }
            public TeklifTuru teklifTuru { get; internal set; }
            public int teklifNedeniNo { get; internal set; }
            public int azamiSureArtUnsur { get; internal set; }
            public int ilaveSure { get; internal set; }
            public int imzalayanTcNo { get; internal set; }
            public string imzalayanAdSoyad { get; internal set; }
        }

        public enum TeklifTuru
        {

            /// Ereteleme
            E,
            /// Ertleme Iptal
            I,
        }
    }
}
