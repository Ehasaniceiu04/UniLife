using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;


namespace UniLife.CommonUI.Pages.Akademisyen
{
    public partial class AkaSinavTakvim : ComponentBase
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

        AkademisyenDto akademisyenDto;// = new AkademisyenDto();
        bool isProgramOpen;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                akademisyenDto = await appState.GetAkademisyenState();
                if (akademisyenDto == null)
                {
                    matToaster.Add("Akademisyen bilgisi alınamadı", MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }

            await GetDersliksAndDerslikRezsByAkaId((int)akademisyenDto.Id);
            isProgramOpen = true;
        }

        async Task GetDersliksAndDerslikRezsByAkaId(int akaId)
        {
            ApiResponseDto<DersliksAndDerslikRezervsDto> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<DersliksAndDerslikRezervsDto>>($"api/derslik/GetDersliksAndDerslikRezsByAkaId/{akaId}/{true}");
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
