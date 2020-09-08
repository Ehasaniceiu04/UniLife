using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.Dto.Definitions;
using Microsoft.AspNetCore.Components;
using UniLife.Shared.Dto;
using Syncfusion.Blazor.DropDowns;
using System.Net.Http.Json;
using MatBlazor;
using UniLife.CommonUI.Extensions;
using Syncfusion.Blazor.Grids;

namespace UniLife.CommonUI.Pages.Admin.OgrenciIslem
{
    public partial class OgrenciSinifAtlatma : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        int? programId;
        int? bolumId;
        int? fakulteId;
        int? kayitNedenId;
        int? donemId;
        int? donemIdIki;
        int? yontemId;


        List<KeyValueDto> kayitNedenDtos = new List<KeyValueDto>();
        SfDropDownList<int?, KeyValueDto> DropKayitNeden;

        List<DonemDto> donemDtos = new List<DonemDto>();
        SfDropDownList<int?, DonemDto> DropDonem;

        
        SfDropDownList<int?, DonemDto> DropDonemIki;

        List<KeyValueDto> yontemDtos = new List<KeyValueDto>
        {
                new KeyValueDto() { Ad = "2. dönemin AGNO'su sınır değerden büyük veya eşitse", Id = 1 },
                new KeyValueDto() { Ad = "2. dönemin ANO'su sınır değerden büyük veya eşitse", Id = 2 },
                new KeyValueDto() { Ad = "1. dönemin ANO'su veay 2. Dönemin ANO'su sınır değerden büyük veya eşitse", Id = 3 },
                new KeyValueDto() { Ad = "Okuduğu dönem sayısı(Ders Kayıtlı güz-bahar dönem sayısına göre)", Id = 4 }
        };
        SfDropDownList<int?, KeyValueDto> DropYontem;


        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> OgrencilerGrid;


        bool isOgrGridVisible;

        bool isUyariOpen;
        string dialogUyariText;

        protected async override Task OnInitializedAsync()
        {
            await ReadKayitNedens();
            await ReadDonems();
        }

        async Task ReadDonems()
        {
            ApiResponseDto<List<DonemDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DonemDto>>>("api/donem");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                donemDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Bölüm bilgisi getirilirken hata oluştu!");
            }
        }

        async Task ReadKayitNedens()
        {
            ApiResponseDto<List<KeyValueDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<KeyValueDto>>>("api/KeyValues/GetKayitNeden");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                kayitNedenDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Kayit Neden bilgisi getirilirken hata oluştu!");
            }
        }


        public Syncfusion.Blazor.Data.Query topAtaQuery = new Syncfusion.Blazor.Data.Query().AddParams("$expand", "program($select=Id,Ad),KayitNeden($select=Id,Ad),OgrenimDurum($select=Id,Ad)");

        async Task Refresh()
        {
            string OdataQueryParameters = "";

            if (fakulteId.HasValue)
            {
                OdataQueryParameters = $"fakulteId eq {fakulteId} and ";
            }
            if (bolumId.HasValue)
            {
                OdataQueryParameters += $"bolumId eq {bolumId} and ";
            }
            if (programId.HasValue)
            {
                OdataQueryParameters += $"programId eq {programId} and ";
            }
            if (kayitNedenId.HasValue)
            {
                OdataQueryParameters += $"kayitNedenId eq {kayitNedenId}";
            }

            OdataQueryParameters = OdataQueryParameters.TrimEnd('a', 'n', 'd', ' ');

            if (!string.IsNullOrWhiteSpace(OdataQueryParameters))
            {
                topAtaQuery.AddParams("$filter", OdataQueryParameters);
            }


            isOgrGridVisible = true;
            if (OgrencilerGrid != null)
            {
                OgrencilerGrid.Refresh();
            }
        }

        async Task Atlat()
        {
            var secilenOgrs= (await OgrencilerGrid.GetSelectedRecords()).Select(x => x.Id);
            if (yontemId == null || secilenOgrs.Count()<1)
            {
                matToaster.Add("Öğrenci, yöntem ve dönem alanlarını seçiniz.", MatToastType.Danger, "Uyarı!");
            }
            else
            {
                ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds = new ReqEntityIdWithOtherEntitiesIds();
                reqEntityIdWithOtherEntitiesIds.EntityId = (int)yontemId;
                reqEntityIdWithOtherEntitiesIds.OtherEntityIds = secilenOgrs.ToList();

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/ogrenci/OgrencisSinifAtlat", reqEntityIdWithOtherEntitiesIds);
                if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    OgrencilerGrid.Refresh();
                    matToaster.Add("", MatToastType.Success, "Seçilen öğrencilerin sınıf atlatma işlemleri gerçekleştirildi.");
                }
                else
                {
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sınıf atlatma işlemi başarısız oldu!");
                }
            }
            
        }

        public async Task OnRowSelecting(RowSelectingEventArgs<OgrenciDto> args)
        {
            if (!string.IsNullOrWhiteSpace(args.Data.DnmSnfGecBilgi))
            {
                args.Cancel = true;
                isUyariOpen = true;
                dialogUyariText = $"{args.Data.Ad} Öğrencisine bu yıl için ders atlatma zaten yapılmış.";
            }
        }

        async Task SinitAtlaTemizle()
        {
            try
            {
                HedefKaynakDto hedefKaynakDto = new HedefKaynakDto
                {
                    KaynakIdList = (await OgrencilerGrid.GetSelectedRecords()).Select(x => x.Id).ToList()
                };

                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                ("api/Ogrenci/SinifAtlaTemizle", hedefKaynakDto);

                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    //isOgrGridVisible = false;
                    //await Task.Delay(100);
                    //isOgrGridVisible = true;
                    OgrencilerGrid.Refresh();
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }

        }
    }
}
