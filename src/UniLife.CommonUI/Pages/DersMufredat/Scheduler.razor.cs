using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Syncfusion.Blazor.Schedule;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class Scheduler : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        List<DerslikDto> derslikDtos { get; set; } = new List<DerslikDto>();
        List<DerslikRezervDto> derslikRezervDtos { get; set; } = new List<DerslikRezervDto>();

        protected override void OnInitialized()
        {
            ReadDersliks();
            ReadDerlikRezervs();
        }

        //protected async override Task OnInitializedAsync()
        //{
        //    await ReadDersliks();
        //    //await ReadDerlikRezervs();
        //}

        void ReadDersliks()
        {
            ApiResponseDto<List<DerslikDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DerslikDto>>>("api/derslik").Result;
            derslikDtos = apiResponse.Result;
        }

        void ReadDerlikRezervs()
        {
            ApiResponseDto<List<DerslikRezervDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DerslikRezervDto>>>("api/derslikrezerv").Result;
            if (apiResponse.StatusCode == StatusCodes.Status200OK)
            {
                derslikRezervDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Rezervayson bilgileri getirilirken hata oluştu!");
            }

        }

        public async Task OnActionCompleted(Syncfusion.Blazor.Schedule.ActionEventArgs<DerslikRezervDto> args)
        {
            if (args.RequestType == "eventCreated" ) 
            {
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/derslikrezerv", args.AddedRecords.FirstOrDefault());
                if (apiResponse.StatusCode == StatusCodes.Status200OK)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                    //dersAcilanDtos.FirstOrDefault(x => x.Id == 0).Id = apiResponse.Result.Id;
                    //DersAcilanGrid.Refresh();
                }
                else
                {
                    //dersAcilanDtos.Remove(dersAcilanDto);
                    //DersAcilanGrid.Refresh();
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Rezervasyon oluşturma hatası!");
                }
            }
            else if(args.RequestType == "eventChanged")
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>("api/derslikrezerv", args.ChangedRecords.FirstOrDefault());
                if (apiResponse.StatusCode == StatusCodes.Status200OK)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                    //dersAcilanDtos.FirstOrDefault(x => x.Id == 0).Id = apiResponse.Result.Id;
                    //DersAcilanGrid.Refresh();
                }
                else
                {
                    //dersAcilanDtos.Remove(dersAcilanDto);
                    //DersAcilanGrid.Refresh();
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Rezervasyon düzenleme hatası!");
                }
            }
            else if(args.RequestType == "eventRemoved")
            {
                var response = await Http.DeleteAsync("api/derslikrezerv/" + args.DeletedRecords.FirstOrDefault().Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("Rezervasyon Silindi", MatToastType.Success);
                }
                else
                {
                    matToaster.Add("Rezervasyon oluşturulamadı!: " + response.StatusCode, MatToastType.Danger);
                }
            }
        }


    }
}
