using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using Microsoft.AspNetCore.Http;
using MatBlazor;
using Syncfusion.Blazor.Grids;
using UniLife.CommonUI.Extensions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class AcilanDersler : ComponentBase
    {
        Syncfusion.Blazor.Grids.SfGrid<DersAcilanDto> DersAcilanGrid;

        List<DersAcilanDto> dersAcilanDtos { get; set; }

        List<ProgramDto> programDtos { get; set; }
        List<AkademisyenDto> akademisyenDtos { get; set; }


        public void Change(@Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            if (args.Value == null)
            {
                DersAcilanGrid.ClearFiltering();
            }
            else
            {
                //startswith
                DersAcilanGrid.FilterByColumn("ProgramId", "equal", 1);
            }
        }


        protected override void OnInitialized()
        {
            ReadDersAcilans();

            ReadPrograms();
            ReadAkademisyens();
        }

        void ReadDersAcilans()
        {

            ApiResponseDto apiResponse = Http.GetFromJsonAsync<ApiResponseDto>("api/dersAcilan").Result;

            if (apiResponse.StatusCode == StatusCodes.Status200OK)
            {
                matToaster.Add(apiResponse.Message, MatToastType.Success, "Açılan Dersler getirildi");
                dersAcilanDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<DersAcilanDto[]>(apiResponse.Result.ToString()).ToList<DersAcilanDto>();
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Açılan Ders bilgisi getirilirken hata oluştu!");
            }
        }

        void ReadPrograms()
        {
            ApiResponseDto<List<ProgramDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<ProgramDto>>>("api/program").Result;
            programDtos = apiResponse.Result;
        }
        void ReadAkademisyens()
        {
            ApiResponseDto<List<AkademisyenDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<AkademisyenDto>>>("api/akademisyen").Result;
            akademisyenDtos = apiResponse.Result;
        }



        public void ActionCompletedHandler(ActionEventArgs<DersAcilanDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "edit")
                {
                    Update(args.Data);
                }
                else if (args.Action == "add")
                {
                    Create(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                Delete(args.Data);
            }
        }

        public async Task Create(DersAcilanDto dersAcilanDto)
        {
            try
            {
                ApiResponseDto<DersAcilanDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<DersAcilanDto>>
                    ("api/dersAcilan", dersAcilanDto);
                if (apiResponse.StatusCode == StatusCodes.Status200OK)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                    dersAcilanDtos.FirstOrDefault(x => x.Id == 0).Id = apiResponse.Result.Id;
                    DersAcilanGrid.Refresh();
                }
                else
                {
                    dersAcilanDtos.Remove(dersAcilanDto);
                    DersAcilanGrid.Refresh();
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Açılan Ders Creation Failed");
                }
            }
            catch (Exception ex)
            {
                dersAcilanDtos.Remove(dersAcilanDto);
                DersAcilanGrid.Refresh();
                matToaster.Add(ex.Message, MatToastType.Danger, "Açılan Ders Creation Failed");
            }
        }

        public async void Update(DersAcilanDto dersAcilanDto)
        {
            //this updates the IsCompleted flag only
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/dersAcilan", dersAcilanDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                }
                else
                {
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Açılan Ders Save Failed");
                    //update failed gridi boz !
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Açılan Ders Save Failed");
                //update failed gridi boz !
            }
        }

        public async Task Delete(DersAcilanDto dersAcilanDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/dersAcilan/" + dersAcilanDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("Açılan Ders Deleted", MatToastType.Success);
                    dersAcilanDtos.Remove(dersAcilanDto);
                }
                else
                {
                    matToaster.Add("Açılan Ders Delete Failed: " + response.StatusCode, MatToastType.Danger);
                }
                //deleteDialogOpen = false;
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Açılan Ders Save Failed");
            }
        }
    }
}
