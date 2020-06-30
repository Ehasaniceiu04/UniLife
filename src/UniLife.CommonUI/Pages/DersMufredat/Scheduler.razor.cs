using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Schedule;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using Microsoft.AspNetCore.Http;
using MatBlazor;
using System.Net.Http.Json;
using Syncfusion.Blazor.Gantt;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class Scheduler : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }

        List<DerslikDto> derslikDtos { get; set; } = new List<DerslikDto>();
        List<DerslikRezervDto> derslikRezervDtos { get; set; } = new List<DerslikRezervDto>();

        //protected override void OnInitialized()
        //{
        //    //ReadDersliks();
        //}

        protected async override Task OnInitializedAsync()
        {
            await ReadDersliks();
            //await ReadDerlikRezervs();
        }

        async Task ReadDersliks()
        {
            ApiResponseDto<List<DerslikDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DerslikDto>>>("api/derslik");
            derslikDtos = apiResponse.Result;
        }

        //async Task ReadDerlikRezervs()
        //{
        //    ApiResponseDto<List<DerslikRezervDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DerslikRezervDto>>>("api/derslikrezerv");
        //    if (apiResponse)
        //    {

        //    }
        //    derslikRezervDtos = apiResponse.Result;
        //}

        //public void OnActionCompleted(ActionEventArgs<DerslikRezervDto> args)
        //{
        //    if (args.RequestType == "eventCreated" || args.RequestType == "eventChanged")   //To check for request type is add event or edit event
        //    {

        //    }
        //}
    }
}
