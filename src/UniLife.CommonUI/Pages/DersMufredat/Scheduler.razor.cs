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
        [Inject]
        public AppState appState { get; set; }


        public string[] GroupData = new string[] { "MeetingRoomzx" };

        List<DerslikDto> derslikDtos { get; set; } = new List<DerslikDto>();
        List<DerslikRezervDto> derslikRezervDtos { get; set; } = new List<DerslikRezervDto>();

        SfDropDownList<int?, DonemDto> DropDonem;
        SfDropDownList<int?, KeyValueDto> DropFakulte;
        SfDropDownList<int?, KeyValueDto> DropBolum;
        SfDropDownList<int?, KeyValueDto> DropProgram;
        SfDropDownList<int?, SinifDto> DropSinif;
        SfDropDownList<int?, BinaDto> DropBina;


        List<DonemDto> donemDtos = new List<DonemDto>();
        List<KeyValueDto> fakulteDtos = new List<KeyValueDto>();
        List<KeyValueDto> bolumDtos = new List<KeyValueDto>();
        List<KeyValueDto> programDtos = new List<KeyValueDto>();
        List<BinaDto> binaDtos = new List<BinaDto>();

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

        bool isDersPrgDialogOpen;
        string dialogBaslik = "Seçilen dersin programı";


        Syncfusion.Blazor.Grids.SfGrid<DerslikRezervDto> SecDrsProgramGrid;

        List<DerslikRezervDto> SecDrsPrgDtos = new List<DerslikRezervDto>();

        bool isUyariOpen;
        string dialogUyariText = "";

        bool isBinaSelected;
        List<int> selectedDersliksByBina = new List<int>();

        string bosBinaText;
        //protected override void OnInitialized()
        //{
        //    ReadDersliks();
        //    ReadDerlikRezervs();
        //}


        bool isSinav;

        protected async override Task OnInitializedAsync()
        {
            //ReadDersliks();
            //ReadDerlikRezervs();

            await ReadFakultes();
            await ReadDonems();
            await ReadBinas();
        }
        async Task ReadDersliks(int binaId)
        {
            //ApiResponseDto<List<DerslikDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DerslikDto>>>("api/derslik").Result;
            //derslikDtos = apiResponse.Result;
            OData<DerslikDto> apiResponse = await Http.GetFromJsonAsync<OData<DerslikDto>>($"odata/dersliks?$filter=BinaId eq {binaId}");
            derslikDtos = apiResponse.Value;
            selectedDersliksByBina = derslikDtos.Select(x => x.Id).ToList();
        }

        async Task ReadDerlikRezervs()
        {
            string odataQuery = "odata/derslikrezervs?$filter=";
            foreach (var item in selectedDersliksByBina)
            {
                odataQuery += "DerslikId eq " + item + " or ";
            }

            odataQuery = odataQuery.TrimEnd('o', 'r', ' ');

            OData<DerslikRezervDto> apiResponse = await Http.GetFromJsonAsync<OData<DerslikRezervDto>>(odataQuery);
            derslikRezervDtos = apiResponse.Value;

            //ApiResponseDto<List<DerslikRezervDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DerslikRezervDto>>>("api/derslikrezerv").Result;
            //if (apiResponse.StatusCode == StatusCodes.Status200OK)
            //{
            //    derslikRezervDtos = apiResponse.Result;
            //}
            //else
            //{
            //    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Rezervayson bilgileri getirilirken hata oluştu!");
            //}

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
        async Task ReadBinas()
        {
            ApiResponseDto<List<BinaDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<BinaDto>>>("api/keyvalues/getbina");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                binaDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Binalar getirilirken hata oluştu!");
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
            SelectedDersAcilanGridRow = null;

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


        public async Task OnPopupOpen(PopupOpenEventArgs<DerslikRezervDto> args)
        {
            if (args.Data.DersAcilanId == 0 && SelectedDersAcilanGridRow == null)
            {
                args.Cancel = true;
                if (isSinav)
                {
                    dialogUyariText = "Sinav programı oluşturmadan önce yukarıdan bir ders seçmelisiniz.";
                }
                else
                {
                    dialogUyariText = "Ders programı oluşturmadan önce yukarıdan bir ders seçmelisiniz.";
                }
                isUyariOpen = true;
            }
            if (args.Type == PopupType.QuickInfo)
            {
                //args.Cancel = true;
            }
            else if (args.Type == PopupType.Editor)
            {
                //args.Data.DersAcilanId = SelectedDersAcilanGridRow.DersAcilanId;
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

        public void OnActionBegin(Syncfusion.Blazor.Schedule.ActionEventArgs<DerslikRezervDto> args)
        {
            if (args.RequestType == "eventCreate")   //To check for request type is event delete
            {
                if (args.AddedRecords[0].Subject == " ")
                {
                    args.AddedRecords[0].Subject = SelectedDersAcilanGridRow.DersAd;
                }
                else
                {
                    args.AddedRecords[0].Subject = SelectedDersAcilanGridRow.DersAd + "-" + args.AddedRecords[0].Subject;

                }
                if (isSinav)
                {
                    args.AddedRecords[0].Subject += " Sınav";
                }
                args.AddedRecords[0].DersAcilanId = SelectedDersAcilanGridRow.DersAcilanId;
                args.AddedRecords[0].IsSinav = isSinav;
                //args.AddedRecords[0].IsBlock = appState.AppSettings.NotAllowOneDerslikMultiReserv;
            }
        }

        public void OnAppointmentResize(Syncfusion.Blazor.Schedule.ResizeEventArgs<DerslikRezervDto> args)
        {
            args.Interval = 15;
        }

        public async Task OnActionCompleted(Syncfusion.Blazor.Schedule.ActionEventArgs<DerslikRezervDto> args)
        {
            if (args.RequestType == "eventCreated")
            {
                args.AddedRecords[0].DersAcilanId = SelectedDersAcilanGridRow.DersAcilanId;
                args.AddedRecords[0].IsSinav = isSinav;
                //args.AddedRecords[0].IsBlock = appState.AppSettings.NotAllowOneDerslikMultiReserv;
                //args.AddedRecords[0].Subject = SelectedDersAcilanGridRow.DersAd + "-" + args.AddedRecords[0].Subject;
                ApiResponseDto<DerslikRezervDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<DerslikRezervDto>>("api/derslikrezerv", args.AddedRecords.FirstOrDefault());
                if (apiResponse.StatusCode == StatusCodes.Status200OK)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                    DersProgramSche.Refresh();
                    //derslikRezervDtos.FirstOrDefault(x => x.Id == 0).Id = apiResponse.Result.Id;
                    //DersProgramSche.Refresh();
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
                //args.ChangedRecords[0].DersAcilanId = 2;
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>("api/derslikrezerv", args.ChangedRecords.FirstOrDefault());
                if (apiResponse.StatusCode == StatusCodes.Status200OK)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);

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
                    matToaster.Add("Rezervasyon silinemedi!: " + response.StatusCode, MatToastType.Danger);
                }
            }
        }


        public async Task CommandClickHandler(Syncfusion.Blazor.Grids.CommandClickEventArgs<ResDersAcilansByLongFilters> args)
        {
            if (args.CommandColumn.Title == "Tanımlı Ders Programları")
            {
                string oDataQuery = $"odata/DerslikRezervs?$expand=ResourceData($select=Id,Ad)&$filter=DersAcilanId eq {args.RowData.DersAcilanId}";
                OData<DerslikRezervDto> apiResponse = await Http.GetFromJsonAsync<OData<DerslikRezervDto>>(oDataQuery);
                SecDrsPrgDtos = apiResponse.Value;

                dialogBaslik = $"{args.RowData.DersAd} dersinin programı";
                isDersPrgDialogOpen = true;

                //await DersAcGrid.ClearSelection();
                //DersAcGrid.SelectedRowIndex = clickedRowIndex;

                //GetSinavsByDersAcilanId(args.RowData);
            }
        }

        //private void BinaOnChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string> args)
        //{
        //    //ReadDersliks();
        //    //ReadDerlikRezervs();

        //    //DropVal = args.Value;
        //    //isBinaSelected = true;
        //    //StateHasChanged();
        //}
        public async Task BinaOnChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            await ReadDersliks((int)args.Value);
            if (selectedDersliksByBina.Count > 0)
            {
                await ReadDerlikRezervs();
                isBinaSelected = true;
                bosBinaText = "";
            }
            else
            {
                isBinaSelected = false;
                bosBinaText = "Binada tanımlı derslik bulunmamaktadır.";
            }



            StateHasChanged();
        }


    }
}
