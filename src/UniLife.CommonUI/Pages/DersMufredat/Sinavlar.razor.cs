using MatBlazor;
using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using Syncfusion.Blazor.DropDowns;
using System.ComponentModel;
using UniLife.CommonUI.Extensions;
using System;
using System.Linq;
using UniLife.Shared.DataModels;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class Sinavlar : ComponentBase
    {
        [InjectAttribute]
        public System.Net.Http.HttpClient Http { get; set; }
        [InjectAttribute]
        public MatBlazor.IMatToaster matToaster { get; set; }

        public DersAcilanDto _dersAcilanDto { get; set; } = new DersAcilanDto();



        SfDropDownList<int?, DonemDto> DropDonem;
        SfDropDownList<int?, KeyValueDto> DropFakulte;
        SfDropDownList<int?, KeyValueDto> DropBolum;
        SfDropDownList<int?, KeyValueDto> DropProgram;
        SfDropDownList<int?, SinifDto> DropSinif;



        List<DonemDto> donemDtos = new List<DonemDto>();
        List<KeyValueDto> fakulteDtos = new List<KeyValueDto>();
        List<KeyValueDto> bolumDtos = new List<KeyValueDto>();
        List<KeyValueDto> programDtos = new List<KeyValueDto>();

        List<SinavTipDto> sinavTipDtos = new List<SinavTipDto>();
        List<SinavTurDto> sinavTurDtos = new List<SinavTurDto>();

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


        Syncfusion.Blazor.Grids.SfGrid<DersAcilanDto> DersAcGrid;
        public List<DersAcilanDto> DersAcDtos = new List<DersAcilanDto>();

        Syncfusion.Blazor.Grids.SfGrid<SinavDto> SinavGrid;
        public List<SinavDto> SinavDtos = new List<SinavDto>();

        
        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> OgrenciGrid;
        public List<OgrenciDto> OgrenciDtos = new List<OgrenciDto>();

        public List<DersAcilanDto> SelectedDersAcilans { get; set; } = new List<DersAcilanDto>();


        //protected override void OnInitialized()
        //{

        //}

        protected async override Task OnInitializedAsync()
        {
            await ReadFakultes();
            await ReadDonems();
            await ReadSinavTip();
            await ReadSinavTur();
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

        async Task ReadSinavTip()
        {
            ApiResponseDto<List<SinavTipDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<SinavTipDto>>>("api/Keyvalues/GetSinavTip");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                sinavTipDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav tipleri getirilirken hata oluştu!");
            }
        }

        async Task ReadSinavTur()
        {
            ApiResponseDto<List<SinavTurDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<SinavTurDto>>>("api/Keyvalues/GetSinavTur");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                sinavTurDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav turleri getirilirken hata oluştu!");
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
        void SinifChange()
        {
            _dersAcilanDto.Sinif = DropSinif.Value;
        }

        async Task Refresh()
        {

            OData<DersAcilanDto> apiResponse;
            //apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<BolumDto>>>("api/bolum/GetBolumByFakulteIds/" + string.Join(',', fakulteId));
            if (_dersAcilanDto.Sinif == 0 || _dersAcilanDto.Sinif == null)
            {
                apiResponse = await Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersacilans?$filter=DonemId eq {_dersAcilanDto.DonemId} and ProgramId eq {_dersAcilanDto.ProgramId} and ProgramId eq {_dersAcilanDto.ProgramId} &$expand=Akademisyen($select=Id,Ad)");
            }
            else
            {
                apiResponse = await Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersacilans?$filter=DonemId eq {_dersAcilanDto.DonemId} and ProgramId eq {_dersAcilanDto.ProgramId} and ProgramId eq {_dersAcilanDto.ProgramId} and Sinif eq {_dersAcilanDto.Sinif} &$expand=Akademisyen($select=Id,Ad)");
            }

            DersAcDtos = apiResponse.Value;
            StateHasChanged();

            if (apiResponse.Value != null)
            {

            }
            else
            {
                matToaster.Add("", MatToastType.Danger, "Bölüm getirilirken hata oluştu!");
            }
        }

        public double clickedRowIndex { get; set; }
        public void OnRecordClickHandler(Syncfusion.Blazor.Grids.RecordClickEventArgs<DersAcilanDto> args)
        {
            clickedRowIndex = args.RowIndex;
        }

        public void OnSinavRecordClickHandler(Syncfusion.Blazor.Grids.RecordClickEventArgs<SinavDto> args)
        {
            ApiResponseDto<List<OgrenciDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciDto>>>($"api/Ogrenci/GetOgrenciListBySinavId/{args.RowData.Id}").Result;
            if (apiResponse.Result.Count>1)
            {
                OgrenciDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sınava tabi öğrenicler getirilirken hata oluştu.");
            }
        }

        public async Task RowSelectedHandler(Syncfusion.Blazor.Grids.RowSelectEventArgs<DersAcilanDto> args)
        {
            List<DersAcilanDto> seciliDersler = await DersAcGrid.GetSelectedRecords();
            if (seciliDersler.Count > 1)
            {
                CokDersSecili(seciliDersler);
            }
        }


        public void CokDersSecili(List<DersAcilanDto> args)
        {
            SelectedDersAcilans = args;
            SinavDtos = new List<SinavDto>();

        }

        public void CommandClickHandler(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersAcilanDto> args)
        {
            if (args.CommandColumn.Title == "Tanımlı Sınavlar")
            {
                DersAcGrid.ClearSelection();
                DersAcGrid.SelectedRowIndex = clickedRowIndex;

                ApiResponseDto<List<SinavDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<SinavDto>>>($"api/Sinav/GetSinavListByAcilanDersId/{args.RowData.Id}").Result;
                if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    SinavDtos = apiResponse.Result;
                    SelectedDersAcilans = new List<DersAcilanDto> { args.RowData };
                }
                else
                {
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav bilgisi getirilirken hata oluştu");
                }
            }
        }
        public void ActionCompletedHandler(Syncfusion.Blazor.Grids.ActionEventArgs<SinavDto> args)
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

        public void ActionCompletedHandlerOgrenci(Syncfusion.Blazor.Grids.ActionEventArgs<OgrenciDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "add")
                {
                    CreateOgrenci(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                DeleteOgrenci(args.Data);
            }
        }

        public void CreateOgrenci(OgrenciDto ogrenciDto)
        {

        }
        public void DeleteOgrenci(OgrenciDto ogrenciDto)
        {

        }

        public async Task Create(SinavDto sinavDto)
        {

            //sinavDto.DersAcilanId = (await DersAcGrid.GetSelectedRecords()).FirstOrDefault().Id;

            if (SelectedDersAcilans.Count() == 1)
            {
                try
                {
                    sinavDto.DersAcilanId = SelectedDersAcilans.FirstOrDefault().Id;

                    ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>
                    ("api/sinav", sinavDto);
                    if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success);
                    }
                    else
                    {
                        SinavDtos.RemoveAll(x => x.Id == 0);
                        SinavGrid.Refresh();

                        matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav Creation Failed");
                    }
                }
                catch (Exception ex)
                {
                    SinavDtos.RemoveAll(x => x.Id == 0);
                    SinavGrid.Refresh();
                    matToaster.Add(ex.Message, MatToastType.Danger, "Sinav Creation Failed");
                }
            }
            else
            {
                try
                {
                    sinavDto.DersAcilanIds = SelectedDersAcilans.Select(x => x.Id);
                    ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/sinav/PostBulkCreate", sinavDto);
                    if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success);
                    }
                    else
                    {
                        SinavDtos.RemoveAll(x => x.Id == 0);
                        SinavGrid.Refresh();

                        matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav Creation Failed");
                    }
                }
                catch (Exception ex)
                {
                    SinavDtos.RemoveAll(x => x.Id == 0);
                    SinavGrid.Refresh();
                    matToaster.Add(ex.Message, MatToastType.Danger, "Sinav Creation Failed");
                }
            }



        }

        public async void Update(SinavDto sinavDto)
        {
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/sinav", sinavDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                }
                else
                {
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav Save Failed");
                    //update failed gridi boz !
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Sinav Save Failed");
                //update failed gridi boz !
            }
        }

        public async Task Delete(SinavDto sinavDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/sinav/" + sinavDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("Sinav Deleted", MatToastType.Success);
                    SinavDtos.Remove(sinavDto);
                }
                else
                {
                    matToaster.Add("Sinav Delete Failed: " + response.StatusCode, MatToastType.Danger);
                }
                //deleteDialogOpen = false;
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Sinav Save Failed");
            }
        }

    }
}
