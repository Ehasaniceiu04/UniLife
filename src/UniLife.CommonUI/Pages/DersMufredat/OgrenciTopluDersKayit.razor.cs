using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.CommonUI.Pages.Definitions.Tabs;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class OgrenciTopluDersKayit : ComponentBase
    {
        [InjectAttribute]
        public System.Net.Http.HttpClient Http { get; set; }
        [InjectAttribute]
        public MatBlazor.IMatToaster matToaster { get; set; }

        bool hedefVisible=true;
        bool kaynakVisible=true;
        
        string OdataQuery = "odata/ogrencis";
        public Query totalQuery = new Query();

        
            string OdataQueryDers = "odata/dersacilans";
        public Query totalQueryDers = new Query();

        public SfGrid<OgrenciDto> OgrGrid;

        public SfGrid<DersAcilanDto> dersAcGrid;

        int? programId;
        int? bolumId;
        int? fakulteId;
        int? mufredatId;
        public DateTime? StartValue { get; set; }
        public DateTime? EndValue { get; set; }

        int? programId2;
        int? bolumId2;
        int? fakulteId2;
        int? mufredatId2;
        int? donemId;

        string dialogUyariText;
        bool isUyariOpen;
        async Task Kayit()
        {
            try
            {
                HedefKaynakDto hedefKaynakDto = new HedefKaynakDto
                {
                    HedefIdList = (await dersAcGrid.GetSelectedRecords()).Select(x => x.Id).ToList(),
                    KaynakIdList = (await OgrGrid.GetSelectedRecords()).Select(x => x.Id).ToList()
                };
                if (hedefKaynakDto.HedefIdList.Count<1 || hedefKaynakDto.KaynakIdList.Count<1)
                {
                    dialogUyariText = "Öğrenci ve ders seçimlerini yaptığınızdan emin olunuz.";
                    isUyariOpen = true;
                }

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/derskayit/HedefKaynakOgrDersKayit", hedefKaynakDto);

                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    //await HedefChange();
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        async Task Refresh()
        {
            totalQuery = new Query();

            if (StartValue.HasValue && EndValue.HasValue)
            {
                var ColPre = new WhereFilter();
                List<WhereFilter> Predicate = new List<WhereFilter>();
                Predicate.Add(new WhereFilter()
                {
                    Field = "KayitTarih",
                    value = StartValue,
                    Operator = "greaterthanorequal",
                    IgnoreCase = true
                });
                Predicate.Add(new WhereFilter()
                {
                    Field = "KayitTarih",
                    value = EndValue,
                    Operator = "lessthanorequal",
                    IgnoreCase = true
                });
                ColPre = WhereFilter.And(Predicate);
                totalQuery = new Query().Where(ColPre);
            }


            if (mufredatId.HasValue)
            {
                totalQuery.Where("mufredatId", "equal", mufredatId);
            }
            else if (programId.HasValue)
            {
                totalQuery.Where("programId", "equal", programId);
            }
            else if (bolumId.HasValue)
            {
                totalQuery.Where("bolumId", "equal", bolumId);
            }
            else if (fakulteId.HasValue)
            {
                totalQuery.Where("fakulteId", "equal", fakulteId);
            }
        }
        async Task RefreshHedef()
        {
            totalQueryDers = new Query();

            if (donemId.HasValue)
            {
                totalQueryDers.Where("donemId", "equal", donemId);
            }

            if (mufredatId2.HasValue)
            {
                totalQueryDers.Where("mufredatId", "equal", mufredatId2);
            }
            else if (programId2.HasValue)
            {
                totalQueryDers.Where("programId", "equal", programId2);
            }
            else if (bolumId2.HasValue)
            {
                totalQueryDers.Where("bolumId", "equal", bolumId2);
            }
            else if (fakulteId2.HasValue)
            {
                totalQueryDers.Where("fakulteId", "equal", fakulteId2);
            }
        }
        
    }
}
