using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

using Syncfusion.Blazor.DropDowns;
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

        SfDropDownList<int?, DonemDto> DropDonem;
        SfDropDownList<int?, KeyValueDto> DropFakulte;
        SfDropDownList<int?, KeyValueDto> DropBolum;
        SfDropDownList<int?, KeyValueDto> DropProgram;
        SfDropDownList<int?, SinifDto> DropSinif;


        List<DonemDto> donemDtos = new List<DonemDto>();
        List<KeyValueDto> fakulteDtos = new List<KeyValueDto>();
        List<KeyValueDto> bolumDtos = new List<KeyValueDto>();
        List<KeyValueDto> programDtos = new List<KeyValueDto>();

        List<SinifDto> sinifDtos = new List<SinifDto>
        {
            new SinifDto() { Ad = "Tümü", Id = 0 },
            new SinifDto() { Ad = "1", Id = 1 },
            new SinifDto() { Ad = "2", Id = 2 },
            new SinifDto() { Ad = "3", Id = 3 },
            new SinifDto() { Ad = "4", Id = 4 },
            new SinifDto() { Ad = "5", Id = 5 },
            new SinifDto() { Ad = "6", Id = 6 },
            new SinifDto() { Ad = "7", Id = 7 },
            new SinifDto() { Ad = "8", Id = 8 },
            new SinifDto() { Ad = "9", Id = 9 },
        };

        public DersAcilanDto _dersAcilanDto { get; set; } = new DersAcilanDto(); //For to keep filter parameters.

        Syncfusion.Blazor.Grids.SfGrid<ResDersAcilansByLongFilters> DersAcGrid;
        public List<ResDersAcilansByLongFilters> DersAcDtos = new List<ResDersAcilansByLongFilters>();

        public ResDersAcilansByLongFilters SelectedDersAcilanGridRow { get; set; }


        Syncfusion.Blazor.Schedule.SfSchedule<DerslikRezervDto> DersProgramSche;
        //protected override void OnInitialized()
        //{
        //    ReadDersliks();
        //    ReadDerlikRezervs();
        //}




        protected async override Task OnInitializedAsync()
        {
            ReadDersliks();
            ReadDerlikRezervs();

            await ReadFakultes();
            await ReadDonems();
        }
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

        async Task ReadFakultes()
        {
            OData<KeyValueDto> apiResponse = await Http.GetFromJsonAsync<OData<KeyValueDto>>($"odata/fakultes?$select=Id,Ad");

            if (apiResponse.Value.Count != 0)
            {
                fakulteDtos = apiResponse.Value;
                StateHasChanged();
            }
            else
            {
                matToaster.Add("", MatToastType.Danger, "fakulte getirilirken hata oluştu!");
            }
        }

        async Task ReadDonems()
        {
            ApiResponseDto<List<DonemDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DonemDto>>>("api/donem/current");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                donemDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Donem getirilirken hata oluştu!");
            }
        }

        private void FakulteToBolum(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            bolumDtos = new List<KeyValueDto>();
            programDtos = new List<KeyValueDto>();
            _dersAcilanDto.BolumId = null;
            _dersAcilanDto.ProgramId = null;
            if (_dersAcilanDto.Id == 0)
            {
                ReadBolums(args.Value).ConfigureAwait(true);
            }
            else
            {
                ReadBolums(_dersAcilanDto.FakulteId).ConfigureAwait(true);
            }
            StateHasChanged();
        }

        async Task ReadBolums(int? fakulteId)
        {
            OData<KeyValueDto> apiResponse;
            //apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<BolumDto>>>("api/bolum/GetBolumByFakulteIds/" + string.Join(',', fakulteId));
            apiResponse = await Http.GetFromJsonAsync<OData<KeyValueDto>>($"odata/bolums?$filter=FakulteId eq {fakulteId}&select=Id,Ad");


            if (apiResponse.Value.Count != 0)
            {
                bolumDtos = apiResponse.Value;
                StateHasChanged();
            }
            else
            {
                matToaster.Add("", MatToastType.Danger, "Bölüm getirilirken hata oluştu!");
            }
        }

        void SinifChange()
        {
            _dersAcilanDto.Sinif = DropSinif.Value;
        }

        private void BolumToProgram(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            programDtos = new List<KeyValueDto>();
            _dersAcilanDto.ProgramId = null;
            if (_dersAcilanDto.Id == 0)
            {

                ReadPrograms(args.Value).ConfigureAwait(true);
            }
            else
            {
                ReadPrograms(_dersAcilanDto.BolumId).ConfigureAwait(true);
            }
            //StateHasChanged();
        }

        async Task ReadPrograms(int? bolumId)
        {
            OData<KeyValueDto> apiResponse;
            //apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<BolumDto>>>("api/bolum/GetBolumByFakulteIds/" + string.Join(',', fakulteId));
            apiResponse = await Http.GetFromJsonAsync<OData<KeyValueDto>>($"odata/programs?$filter=BolumId eq {bolumId}&select=Id,Ad");


            if (apiResponse.Value != null)
            {
                programDtos = apiResponse.Value;
                StateHasChanged();
            }
            else
            {
                matToaster.Add("", MatToastType.Danger, "Bölüm getirilirken hata oluştu!");
            }
        }

        async Task Refresh()
        {

            //await GetDersAcilansByFilters();

            ApiResponseDto<List<ResDersAcilansByLongFilters>> apiResponse = await Http.PostJsonAsync<ApiResponseDto<List<ResDersAcilansByLongFilters>>>("api/DersAcilan/DersAcilansByLongFilters", _dersAcilanDto);
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                DersAcDtos = apiResponse.Result;
                DersAcGrid.Refresh();
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Açılan Derslerin bilgisi getirilirken hata oluştu");
            }
        }


        public void OnPopupOpen(Syncfusion.Blazor.Schedule.PopupOpenEventArgs<DerslikRezervDto> args)
        {


            
            if (args.Type == PopupType.QuickInfo)
            {
                args.Cancel = true;
            }
            else if (args.Type == PopupType.Editor)
            {
                //args.Data.Subject = SelectedDersAcilanGridRow.DersAd;
                //DersProgramSche.Refresh();
            }
        }
        //public void OnCellDoubleClick(CellClickEventArgs args)
        //{
        //    //args.Cancel = true;   //To prevent the opening of editor window
        //}

        public async Task RowSelectedHandler(Syncfusion.Blazor.Grids.RowSelectEventArgs<ResDersAcilansByLongFilters> args)
        {
            SelectedDersAcilanGridRow = args.Data;
        }

        //public async Task OnActionBegin(ActionEventArgs<DerslikRezervDto> args)
        //{
        //    if (args.RequestType == "eventRemove")   //To check for request type is event delete
        //    {
        //        args.Cancel = true;   //To prevent the appointment deletion
        //    }
        //}
        public async Task OnActionCompleted(Syncfusion.Blazor.Schedule.ActionEventArgs<DerslikRezervDto> args)
        {
            if (args.RequestType == "eventCreated")
            {
                args.AddedRecords[0].DersAcilanId = 2;
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
            else if (args.RequestType == "eventChanged")
            {
                args.ChangedRecords[0].DersAcilanId = 2;
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
            else if (args.RequestType == "eventRemoved")
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
