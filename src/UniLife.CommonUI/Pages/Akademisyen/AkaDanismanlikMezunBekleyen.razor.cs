using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.Akademisyen
{
    public partial class AkaDanismanlikMezunBekleyen : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }
        [Inject]
        public AppState appState { get; set; }

        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> mezunGrid;

        string OdataQuery = "odata/ogrencis";
        public Query totalQuery = new Query().Expand(new List<string> { "KayitNeden($select=Id,Ad)", "Danisman($select=Id,Ad)", "Program($select=Id,Ad)" });

        int OgrId;
        bool isUyariOpen;
        int akademisyenId;

        SfTab Tab;

        protected override async Task OnInitializedAsync()
        {
            akademisyenId = (await appState.GetAkademisyenState()).Id;

            totalQuery.Where("DanismanId", "equal", akademisyenId).Where("MezunOnay", "equal", (int)MezunOnayDurum.DanismanOnayinda);

        }


        void AlDersler(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }
        void MufDurum(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }
        void Transkript(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;

        }
        void MezuniyetTranskript(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }

        public void CommandClickHandler(CommandClickEventArgs<OgrenciDto> args)
        {
            if (args.CommandColumn.Title == "Mezun Onay")
            {

                try
                {
                    ApiResponseDto<OgrenciDto> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<OgrenciDto>>($"api/ogrenci/UpdateMezunDanismanOnayli/{args.RowData.Id}").Result;

                    if (apiResponse.IsSuccessStatusCode)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success, $"{args.RowData.Ad} {args.RowData.Soyad} adlı öğrenciye danışman onayı verildi!");
                        mezunGrid.Refresh();
                    }
                    else
                        matToaster.Add(apiResponse.Message, MatToastType.Danger, "Öğrenci bilgileri alınamadı!");
                }
                catch (Exception ex)
                {
                    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Öğrenci bilgileri alınırken hata oluştu!");
                }


            }
        }
    }
}
