using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.Personel
{
    public partial class PersonelProgram: ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        private DateTime CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);//new DateTime(2020, 1, 22);
        List<PersonelTaskDto> personelTaskDtos;// { get; set; } = new List<PersonelTaskDto>();

        Syncfusion.Blazor.Schedule.SfSchedule<PersonelTaskDto> PersonelTaskSche;

        //string schQuery = "odata//PersonelTasks//";



        public async Task OnActionCompleted(Syncfusion.Blazor.Schedule.ActionEventArgs<PersonelTaskDto> args)
        {
            if (args.RequestType == "dateNavigate")
            {
                string odataRezervQuery = $"odata/personeltasks?$filter=(StartTime ge {CurrentDate.AddMonths(-1).ToString("s") + "Z"}) and (EndTime le {CurrentDate.AddMonths(1).ToString("s") + "Z"})";
                OData<PersonelTaskDto> apiResponse = await Http.GetFromJsonAsync<OData<PersonelTaskDto>>(odataRezervQuery);
                personelTaskDtos = apiResponse.Value;
                
            }
            if (args.RequestType == "eventCreated")
            {
                int selectedId = args.AddedRecords[0].Id;
                args.AddedRecords[0].Id = 0;
                try
                {
                    ApiResponseDto<PersonelTaskDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<PersonelTaskDto>>("api/personelTask", args.AddedRecords.FirstOrDefault());

                    if (apiResponse.IsSuccessStatusCode)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                        await PersonelTaskSche.RefreshEvents();//.Refresh();
                        personelTaskDtos.FirstOrDefault(x => x.Id == selectedId).Id = apiResponse.Result.Id;
                        PersonelTaskSche.RefreshEvents();//.Refresh();
                        //dersAcilanDtos.FirstOrDefault(x => x.Id == 0).Id = apiResponse.Result.Id;
                        //DersAcilanGrid.Refresh();
                    }
                    else
                    {
                        matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Olay oluşturma hatası!");
                        //dersAcilanDtos.Remove(dersAcilanDto);
                        //DersAcilanGrid.Refresh();
                    }

                }
                catch (Exception ex)
                {
                    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
                }
            }
            else if (args.RequestType == "eventChanged")
            {
                try
                {
                    ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>("api/personeltask", args.ChangedRecords.FirstOrDefault());
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success);
                    }
                    else
                        matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                }
                catch (Exception ex)
                {
                    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Olay düzenleme hatası!");
                }
            }
            else if (args.RequestType == "eventRemoved")
            {
                var response = await Http.DeleteAsync("api/personeltask/" + args.DeletedRecords.FirstOrDefault().Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("Olay Silindi", MatToastType.Success);
                }
                else
                {
                    matToaster.Add("Olay silinemedi!: " + response.StatusCode, MatToastType.Danger);
                }
            }
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {

                
        //    }
        //}

        protected override async Task OnInitializedAsync()
        {
            string odataRezervQuery = "odata/personeltasks";
            OData<PersonelTaskDto> apiResponse = await Http.GetFromJsonAsync<OData<PersonelTaskDto>>(odataRezervQuery);
            personelTaskDtos = apiResponse.Value;
        }
    }
}
