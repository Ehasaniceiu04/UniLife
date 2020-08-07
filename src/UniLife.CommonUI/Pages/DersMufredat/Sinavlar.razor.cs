using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Navigations;
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

        List<SinavDto> mazeretSinavDtos = new List<SinavDto>();


        List<SinifDto> sinifDtos = new List<SinifDto>
{
            new SinifDto() { Ad = "0", Id = 0 },
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

        public int selectedSinavId { get; set; }
        public SinavDto SelectedSinav { get; set; }


        bool ogrenciSecDialogOpen;
        //List<OgrenciDto> SecmeliOgrenciDtos = new List<OgrenciDto>();
        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> SecmeliOgrenciGrid;
        
        string dialogUyariText;
        bool isUyariOpen;
        //protected override void OnInitialized()
        //{

        //}

        string mazeretConfirmDialogTitle;
        bool isMazeretConfirmDialogOpen;
        DateTime mazeretTarihi;
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

            await GetDersAcilansByFilters();
        }


        private async Task GetDersAcilansByFilters()
        {
            SinavDersAcDto sinavDersAcDto = new SinavDersAcDto
            {
                donemId = _dersAcilanDto.DonemId,
                programId = _dersAcilanDto.ProgramId,
                sinif = _dersAcilanDto.Sinif,
                dersKodu = _dersAcilanDto.Kod
            };

            //var reqURL = $"api/DersAcilan/GetDersAcilansByFilters/{_dersAcilanDto.DonemId ?? 0}/{_dersAcilanDto.ProgramId ?? 0}/{_dersAcilanDto.Sinif ?? 0}/{_dersAcilanDto.Kod ?? ""}";



            ApiResponseDto<List<DersAcilanDto>> apiResponse = await Http.PostJsonAsync<ApiResponseDto<List<DersAcilanDto>>>("api/DersAcilan/PostDersAcilansByFilters", sinavDersAcDto);
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                DersAcDtos = apiResponse.Result;
                DersAcGrid.Refresh();
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Açılan Derslerin bilgisi getirilirken hata oluştu");
            }

            await SinavGridTemizle();
        }

        async Task SinavGridTemizle()
        {
            SinavDtos = new List<SinavDto>();
            SinavGrid.Refresh();
        }

        public double clickedRowIndex { get; set; }
        public async Task OnRecordClickHandler(Syncfusion.Blazor.Grids.RecordClickEventArgs<DersAcilanDto> args)
        {
            clickedRowIndex = args.RowIndex;
        }

        //public async Task OnSinavRecordClickHandler(Syncfusion.Blazor.Grids.RecordClickEventArgs<SinavDto> args)
        //{
        //    ApiResponseDto<List<OgrenciDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciDto>>>($"api/Ogrenci/GetOgrenciListBySinavId/{args.RowData.Id}").Result;
        //    if (apiResponse.Result.Count > 1)
        //    {
        //        OgrenciDtos = apiResponse.Result;
        //    }
        //    else
        //    {
        //        matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sınava tabi öğrenicler getirilirken hata oluştu.");
        //    }
        //}


        public async Task RowSelectedHandlerSinav(Syncfusion.Blazor.Grids.RowSelectEventArgs<SinavDto> args)
        {
            SelectedSinav = args.Data;
            selectedSinavId = args.Data.Id;
            if (selectedSinavId != 0)
            {
                ApiResponseDto<List<OgrenciDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciDto>>>($"api/Ogrenci/GetOgrenciListBySinavId/{args.Data.Id}").Result;
                if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    OgrenciDtos = apiResponse.Result;
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "Sınava tabi öğrenicler getirildi");
                }
                else
                {
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sınava tabi öğrenicler getirilirken hata oluştu.");
                }
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


        public async Task CommandClickHandler(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersAcilanDto> args)
        {
            if (args.CommandColumn.Title == "Tanımlı Sınavlar")
            {
                var commandRowIndex = await DersAcGrid.GetRowIndexByPrimaryKey(args.RowData.Id); // alttaki doğru çalışıyopr bu olursa bunu yapt test edicen.
                //var commandRowIndex = DersAcGrid.CurrentViewData.ToList().IndexOf(args.RowData);
                await DersAcGrid.SelectRow(commandRowIndex);

                GetSinavsByDersAcilanId(args.RowData);
            }

        }


        public async Task CommandClickHandlerSecmeliOgr(Syncfusion.Blazor.Grids.CommandClickEventArgs<OgrenciDto> args)
        {
            if (args.CommandColumn.Title == "Ogrenciyi Ekle")
            {
                SinavKayitDto sinavKayitDto = new SinavKayitDto()
                {
                    OgrenciId = args.RowData.Id,
                    SinavId = selectedSinavId
                };
                ApiResponseDto<SinavKayitDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<SinavKayitDto>>("api/SinavKayit", sinavKayitDto);
                ogrenciSecDialogOpen = false;

                if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    args.RowData.SinavKayitId = apiResponse.Result.Id;
                    OgrenciDtos.Add(args.RowData);
                    OgrenciGrid.Refresh();
                    matToaster.Add(args.RowData.Ad + " " + args.RowData.Soyad, MatToastType.Success, "Öğrenci Kaydı gerçekleşti");
                }
                else
                {
                    matToaster.Add(args.RowData.Ad + " " + args.RowData.Soyad + " : " + apiResponse.StatusCode, MatToastType.Danger, "Öğrenci sinava kayıt edilemedi");
                }
            }
        }



        private void GetSinavsByDersAcilanId(DersAcilanDto dersAcilanDto)
        {
            ApiResponseDto<List<SinavDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<SinavDto>>>($"api/Sinav/GetSinavListByAcilanDersId/{dersAcilanDto.Id}").Result;
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                SinavDtos = apiResponse.Result;
                SelectedDersAcilans = new List<DersAcilanDto> { dersAcilanDto };
                SinavGrid.Refresh();
                OgrenciGridTemizle();
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav bilgisi getirilirken hata oluştu");
            }
        }

        public void OgrenciGridTemizle()
        {
            OgrenciDtos = new List<OgrenciDto>();
            OgrenciGrid.Refresh();
        }

        public void ActionCompletedHandler(Syncfusion.Blazor.Grids.ActionEventArgs<SinavDto> args)
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
                    Create(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                Delete(args.Data);
            }
        }


        public void OnActionBeginHandlerOgrenci(Syncfusion.Blazor.Grids.ActionEventArgs<OgrenciDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
            {
                args.Cancel = true;
                ogrenciSecDialogOpen = true;
            }
        }

        public void ActionCompletedHandlerOgrenci(Syncfusion.Blazor.Grids.ActionEventArgs<OgrenciDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                DeleteSinavKayitByOgrenci(args.Data);
            }
        }

        public async Task DeleteSinavKayitByOgrenci(OgrenciDto ogrenciDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/SinavKayit/" + ogrenciDto.SinavKayitId);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("Öğrencinin Sınav kayıdı silinmiştir", MatToastType.Success);
                    OgrenciDtos.Remove(ogrenciDto);
                    OgrenciGrid.Refresh();
                }
                else
                {
                    matToaster.Add("Öğrencinin sınav kaydı silinirken hata oluştu: " + response.StatusCode, MatToastType.Danger);
                }
                //deleteDialogOpen = false;
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Bölüm Save Failed");
            }
        }

        public async Task Create(SinavDto sinavDto)
        {

            //sinavDto.DersAcilanId = (await DersAcGrid.GetSelectedRecords()).FirstOrDefault().Id;

            if (SelectedDersAcilans.Count() == 1)
            {
                try
                {
                    sinavDto.DersAcilanId = SelectedDersAcilans.FirstOrDefault().Id;
                    sinavDto.DersAcilanIds = SelectedDersAcilans.Select(x => x.Id);
                    ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>
                    ("api/sinav/PostBulkCreate", sinavDto);
                    if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success);
                        GetSinavsByDersAcilanId(DersAcDtos.FirstOrDefault(x => x.Id == sinavDto.DersAcilanId));
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
                        SinavDtos.RemoveAll(x => x.Id == 0);
                        SinavGrid.Refresh();
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


        private List<Object> Toolbaritems = new List<Object>() { "Add", "Edit", "Delete", "ColumnChooser", new ItemModel() { Text = "Mazeret Sınavı", TooltipText = "Seçilenm Sınavın Mazereti", PrefixIcon = "e-click", Id = "Mazeret" } };
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "Mazeret")
            {
                if (selectedSinavId != 0)
                {
                    isMazeretConfirmDialogOpen = true;
                }
                else
                {
                    dialogUyariText = "Evvela bir ara sınav seçiniz.";
                    isUyariOpen = true;
                }
            }
        }

        public async Task MazeretSinavOlustur()
        {
            var mazeret = SinavDtos.FirstOrDefault(x => x.Id == selectedSinavId);
            mazeret.Id = 0;
            mazeret.MazeretiSinavId = selectedSinavId;
            mazeret.Tarih = mazeretTarihi;
            mazeret.SinavTurId = (int)SinavTurEnum.Mazeret_Sinav;
            mazeret.SablonAd = $"{SelectedSinav.SablonAd} Mazeret";
            mazeret.KisaAd = $"{SelectedSinav.KisaAd} Mazeret";
            ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/sinav", mazeret);
            isMazeretConfirmDialogOpen = false;

            GetSinavsByDersAcilanId(DersAcDtos.FirstOrDefault(x => x.Id == mazeret.DersAcilanId));
            SinavGrid.Refresh();
        }
        
        public async Task TopluButOlustur()
        {
            List<DersAcilanDto> seciliDersler = await DersAcGrid.GetSelectedRecords();
            // Büt sınavlarını oluştur finalin aynı sadece bir modalda bir tarhi girdirt.
            // Final sınavı id sini bu büte mazeret ID olarka gir.
        }

        //bool isMazeret;
        
        //public async Task SinavTurChange(ChangeEventArgs<int> args)
        //{
        //    if (args.Value == (int)SinavTurEnum.Mazeret_Sinav)
        //    {
        //        if (SelectedDersAcilans.Count>1)
        //        {
        //            isUyariOpen = true;
        //            dialogUyariText = "Birden fazla ders seçimi mevcut. Mazeret sınavları teker teker oluşturulabilir.";
        //        }


        //        isMazeret = true;

        //        OData<SinavDto> apiResponse = await Http.GetFromJsonAsync<OData<SinavDto>>($"odata/Sinavs?$select=Id,Ad&$filter=DersAcilanId eq {}");
        //    }
        //}

        //List<SinavKayitDto> mazeretSinavKayitDtos;

    }
}
