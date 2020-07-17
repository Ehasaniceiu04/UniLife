using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class Dersler : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }


        [Parameter]
        public EventCallback<int> TabDegis { get; set; }

        SfGrid<DersDto> DersGrid;

        List<DersDto> dersDtos = new List<DersDto>();

        List<MufredatDto> mufredatDtos = new List<MufredatDto>();

        List<DonemDto> donemDtos = new List<DonemDto>();

        public string[] MenuItems = new string[] { "Group", "Ungroup", "ColumnChooser", "Filter" };

        private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "900px" };

        protected override void OnInitialized()
        {
            ReadDerss();


            ApiResponseDto apiResponse = Http.GetFromJsonAsync<ApiResponseDto>("api/mufredat").Result;
            mufredatDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<MufredatDto[]>(apiResponse.Result.ToString()).ToList<MufredatDto>();
            ApiResponseDto apiResponseDersTur = Http.GetFromJsonAsync<ApiResponseDto>("api/donem").Result;
            donemDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<DonemDto[]>(apiResponseDersTur.Result.ToString()).ToList<DonemDto>();

        }

        void ReadDerss()
        {
            try
            {
                ApiResponseDto<List<DersDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersDto>>>("api/ders/GetDersByMufredatId/" + appState.MufredatState.MufredatId).Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    dersDtos = apiResponse.Result;
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }


            

            //if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            //{
            //    matToaster.Add(apiResponse.Message, MatToastType.Success, "ders getirildi");
            //    dersDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<DersDto[]>(apiResponse.Result.ToString()).ToList<DersDto>();
            //    DersGrid.Refresh();
            //}
            //else
            //{
            //    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "ders bilgisi getirilirken hata oluştu!");
            //}
        }



        public async void ActionCompletedHandler(ActionEventArgs<DersDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {
                    Update(args.Data);
                }
                else if (args.Action == "Add")
                {
                    await Create(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await Delete(args.Data);
            }
        }

        public async Task Create(DersDto dersDto)
        {
            try
            {
                dersDto.MufredatId = appState.MufredatState.MufredatId;
                dersDto.ProgramId = appState.MufredatState.ProgramId;
                dersDto.BolumId = appState.MufredatState.BolumId;
                dersDto.FakulteId = appState.MufredatState.FakulteId;

                ApiResponseDto<DersDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<DersDto>>("api/ders", dersDto);

                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                    dersDtos.FirstOrDefault(x => x.Id == 0).Id = apiResponse.Result.Id;
                    //DersGrid.Refresh();
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");

                }
                else
                {
                    //TODO Ahmet 1**
                    //TODO Ahmet 2**
                    dersDtos.Remove(dersDto);
                    //DersGrid.Refresh();
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                }
            }
            catch (Exception ex)
            {
                //TODO Ahmet 1**: liste içinden değinde gride eklediğini sil demeli !!
                //TODO Ahmet 2**: Dbden hata geldiği zaman Bu hata sebebini mantıklı şekilde buraya vermemiz gerekiyor. Aynı Idli kayıt gönder patlatıyon.

                dersDtos.Remove(dersDto);
                DersGrid.Refresh();
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }
        }


        public async void Update(DersDto dersDto)
        {
            //this updates the IsCompleted flag only
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/ders", dersDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }
        }

        public async Task Delete(DersDto dersDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/ders/" + dersDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("", MatToastType.Success, "İşlem başarılı.");
                    dersDtos.Remove(dersDto);
                }
                else
                {
                    matToaster.Add("", MatToastType.Danger, "İşlem başarısız!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }
        }

        public string CssClass { get; set; }
        public void OnInput(InputEventArgs args)
        {
            //if (!System.Text.RegularExpressions.Regex.IsMatch(args.Value, "^[0-9]*$"))
            if (!System.Text.RegularExpressions.Regex.IsMatch(args.Value, "^[a-zA-Z0-9_.-]*$"))
            {
                CssClass = "e-error";
            }
            else
            {
                CssClass = "e-success";
            }
            this.StateHasChanged();
        }


    }
}
