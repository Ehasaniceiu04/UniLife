using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.Ogrenci
{
    public partial class OgrenciSinavProgram : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }
        [Inject]
        public AppState appState { get; set; }

        Syncfusion.Blazor.Schedule.SfSchedule<DerslikRezervDto> DersProgramSche;
        public string[] GroupData = new string[] { "MeetingRoomzx" };
        List<DerslikDto> derslikDtos { get; set; } = new List<DerslikDto>();
        List<DerslikRezervDto> derslikRezervDtos { get; set; } = new List<DerslikRezervDto>();

        OgrenciDto ogrenciDto;// = new OgrenciDto();
        bool isProgramOpen;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                ogrenciDto = await appState.GetOgrenciState();
                if (ogrenciDto == null)
                {
                    matToaster.Add("Öğrenci bilgisi alınamadı", MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }

            await ReadDersliksAndDerslikRezByMufredatId((int)ogrenciDto.MufredatId,ogrenciDto.Id);
            isProgramOpen = true;
        }

        async Task ReadDersliksAndDerslikRezByMufredatId(int mufredatId, int ogrenciId)
        {
            ApiResponseDto<DersliksAndDerslikRezervsDto> apiResponse =await Http.GetFromJsonAsync<ApiResponseDto<DersliksAndDerslikRezervsDto>>($"api/derslik/GetDersliksAndDerslikRezsByMufredatId/{mufredatId}/{ogrenciId}/{true}");
            derslikRezervDtos = apiResponse.Result.DerslikRezervs;
            derslikDtos = apiResponse.Result.Dersliks;
            //OData<DerslikDto> apiResponse = await Http.GetFromJsonAsync<OData<DerslikDto>>($"odata/dersliks?$filter=BinaId eq {binaId}");
            //derslikDtos = apiResponse.Value;
            //selectedDersliksByBina = derslikDtos.Select(x => x.Id).ToList();
        }

        //async Task Asd()
        //{
        //    await ReadDersliksAndDerslikRezByMufredatId((int)ogrenciDto.MufredatId);
        //    isProgramOpen = true;
        //}
    }
}
