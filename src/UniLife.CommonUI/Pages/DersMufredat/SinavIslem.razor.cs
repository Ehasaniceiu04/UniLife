using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class SinavIslem : ComponentBase
    {
        [InjectAttribute]
        public System.Net.Http.HttpClient Http { get; set; }
        [InjectAttribute]
        public MatBlazor.IMatToaster matToaster { get; set; }

        public DersAcilanDto _dersAcilanDto { get; set; } = new DersAcilanDto();



        SfDropDownList<int?, DonemDto> DropDonem;
        SfDropDownList<int?, SinifDto> DropSinif;



        List<DonemDto> donemDtos = new List<DonemDto>();

        List<SinavTipDto> sinavTipDtos = new List<SinavTipDto>();
        List<SinavTurDto> sinavTurDtos = new List<SinavTurDto>();

        List<SinavDto> mazeretSinavDtos = new List<SinavDto>();



        Syncfusion.Blazor.Grids.SfGrid<DersAcilanForSinav> DersAcGrid;

        Syncfusion.Blazor.Grids.SfGrid<SinavDto> SinavGrid = new SfGrid<SinavDto>();
        public List<SinavDto> SinavDtos = new List<SinavDto>();


        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> OgrenciGrid;
        public List<OgrenciDto> OgrenciDtos = new List<OgrenciDto>();

        public List<DersAcilanForSinav> SelectedDersAcilans { get; set; } = new List<DersAcilanForSinav>();

        public int selectedSinavId { get; set; }
        public SinavDto SelectedSinav { get; set; }


        bool ogrenciSecDialogOpen;
        //List<OgrenciDto> SecmeliOgrenciDtos = new List<OgrenciDto>();
        Syncfusion.Blazor.Grids.SfGrid<DersKayitDto> SecmeliOgrenciGrid;

        string dialogUyariText;
        bool isUyariOpen;
        //protected override void OnInitialized()
        //{

        //}

        string mazeretConfirmDialogTitle;
        bool isMazeretConfirmDialogOpen;
        DateTime mazeretTarihi;

        private DialogSettings DialogParams = new DialogSettings { MinHeight = "300px", Width = "400px", };


        bool isShowSinavs = true;

        bool OgrGridShow = true;

        public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();
        int? _programId;
        public int? ProgramId
        {
            get => _programId;
            set
            {
                Refresh();
                if (_programId == value) return;
                _programId = value;
            }
        }
        int? _bolumId;
        public int? BolumId
        {
            get => _bolumId;
            set
            {
                Refresh();
                if (_bolumId == value) return;
                _bolumId = value;
            }
        }
        int? _fakulteId;
        public int? FakulteId
        {
            get => _fakulteId;
            set
            {
                Refresh();
                if (_fakulteId == value) return;
                _fakulteId = value;
            }
        }
        int? _donemId;
        public int? DonemId
        {
            get => _donemId;
            set
            {
                Refresh();
                if (_donemId == value) return;
                _donemId = value;
            }
        }
        public Query totalQuery;// = new Query().Expand(new List<string> { "program($select=Id,Ad)" });

        public Query totalQueryOgr;

        bool sadeceYillik;
        string yillikStyle;

        protected async override Task OnInitializedAsync()
        {
            await ReadDonems();
            await ReadSinavTip();
            await ReadSinavTur();

            DonemId = (await appState.GetDonemState()).Id;
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }


        async Task ReadDonems()
        {
            ApiResponseDto<List<DonemDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DonemDto>>>("api/donem/current");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                donemDtos = apiResponse.Result;
                DonemId = donemDtos.FirstOrDefault(x => x.Durum == true).Id;
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
                sinavTipDtos = apiResponse.Result.Where(x => x.Id != (int)SinavTipEnum.Mazeret).ToList();
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


        void SinifChange()
        {
            _dersAcilanDto.Sinif = DropSinif.Value;
        }


        async Task SinavGridTemizle()
        {
            SinavDtos = new List<SinavDto>();
            SinavGrid.Refresh();
        }


        public async Task RowSelectedHandlerSinav(Syncfusion.Blazor.Grids.RowSelectEventArgs<SinavDto> args)
        {
            await Task.Delay(100);
            SelectedSinav = args.Data;
            selectedSinavId = args.Data.Id;
            if (selectedSinavId != 0)
            {
                await OgrenciGridRefresh(args.Data.Id);
            }

        }

        async Task SinavGridRefresh()
        {
            var selectedDersAcilan = (await DersAcGrid.GetSelectedRecords()).FirstOrDefault();


            GetSinavsByDersAcilanId(selectedDersAcilan);
        }

        async Task OgrenciGridRefresh(int sinavId)
        {
            ApiResponseDto<List<OgrenciDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciDto>>>($"api/Ogrenci/GetOgrenciListBySinavId/{sinavId}").Result;
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                OgrenciDtos = apiResponse.Result;
                matToaster.Add(apiResponse.Message, MatToastType.Success, "Sınava tabi öğrenicler getirildi");
                //OgrGridShow = false;
                //await Task.Delay(100);

                //OgrenciGrid.Refresh();

                //base.InvokeAsync(StateHasChanged);
                //OgrGridShow = true;

                StateHasChanged();

            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sınava tabi öğrenicler getirilirken hata oluştu.");
            }
        }

        public async Task RowSelectedHandler(Syncfusion.Blazor.Grids.RowSelectEventArgs<DersAcilanForSinav> args)
        {
            List<DersAcilanForSinav> seciliDersler = await DersAcGrid.GetSelectedRecords();
            if (seciliDersler.Count > 1)
            {
                CokDersSecili(seciliDersler);
            }
        }




        public void CokDersSecili(List<DersAcilanForSinav> args)
        {
            SelectedDersAcilans = args;
            SinavDtos = new List<SinavDto>();

        }


        public void CommandClickHandler(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersAcilanForSinav> args)
        {
            if (args.CommandColumn.Title == "Tanımlı Sınavlar")
            {
                var commandRowIndex = DersAcGrid.GetRowIndexByPrimaryKey(args.RowData.Id).Result; // alttaki doğru çalışıyopr bu olursa bunu yapt test edicen.
                //var commandRowIndex = DersAcGrid.CurrentViewData.ToList().IndexOf(args.RowData);
                DersAcGrid.SelectRow(commandRowIndex);


                //DersAcGrid.SelectRow(clickedRowIndex);
                GetSinavsByDersAcilanId(args.RowData);
                StateHasChanged();
            }

        }

        async Task Remove()
        {
            SinavDtos = new List<SinavDto>();
            isShowSinavs = false;
            SinavGrid.Refresh();
            isShowSinavs = true;
        }


        public async Task CommandClickHandlerSecmeliOgr(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersKayitDto> args)
        {
            if (args.CommandColumn.Title == "Ogrenciyi Ekle")
            {
                if (OgrenciDtos.Any(x => x.Id == args.RowData.OgrenciId))
                {
                    dialogUyariText = "Bu öğrenci seçili sınava zaten ekli.";
                    isUyariOpen = true;
                }
                else
                {
                    SinavKayitDto sinavKayitDto = new SinavKayitDto()
                    {
                        OgrenciId = args.RowData.Ogrenci.Id,
                        SinavId = selectedSinavId,
                        MazeretiSinavKayitId = SelectedSinav.MazeretiSinavId,
                    };
                    ApiResponseDto<SinavKayitDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<SinavKayitDto>>("api/SinavKayit", sinavKayitDto);
                    ogrenciSecDialogOpen = false;

                    if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                    {
                        //args.RowData.SinavKayitId = apiResponse.Result.Id;
                        //OgrenciDtos.Add(args.RowData);
                        OgrenciGridRefresh(selectedSinavId);
                        OgrenciGrid.Refresh();
                        matToaster.Add(args.RowData.Ogrenci.Ad + " " + args.RowData.Ogrenci.Soyad, MatToastType.Success, "Öğrenci Kaydı gerçekleşti");
                    }
                    else
                    {
                        matToaster.Add(args.RowData.Ogrenci.Ad + " " + args.RowData.Ogrenci.Soyad + " : " + apiResponse.StatusCode, MatToastType.Danger, "Öğrenci sinava kayıt edilemedi");
                    }
                }
            }
        }



        private void GetSinavsByDersAcilanId(DersAcilanForSinav dersAcilanDto)
        {
            ApiResponseDto<List<SinavDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<SinavDto>>>($"api/Sinav/GetSinavListByAcilanDersId/{dersAcilanDto.Id}").Result;
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                SinavDtos = apiResponse.Result;
                SelectedDersAcilans = new List<DersAcilanForSinav> { dersAcilanDto };
                OgrenciGridTemizle();
                if (SinavDtos.Count < 1)
                {
                    isShowSinavs = false;
                }
                else
                {
                    isShowSinavs = true;
                }
                StateHasChanged();
            }
            else
            {
                SinavDtos = new List<SinavDto>();
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav bilgisi getirilirken hata oluştu");
            }
        }

        public void OgrenciGridTemizle()
        {
            OgrenciDtos = new List<OgrenciDto>();
            //OgrenciGrid.Refresh();
        }

        
        public void ActionCompletedHandler(Syncfusion.Blazor.Grids.ActionEventArgs<SinavDto> args)
        {
            butEnabled = true;
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
                    isShowSinavs = true;
                    Create(args.Data);
                }
            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                Delete(args.Data);
            }
        }


        string ogrenciAddOdata;
        public void OnActionBeginHandlerOgrenci(Syncfusion.Blazor.Grids.ActionEventArgs<OgrenciDto> args)
        {
            


            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
            {
                ogrenciAddOdata = $"odata/derskayits?$expand=Ogrenci($select=Id,Ad,Soyad,Ogrno)&$select=Id,OgrenciId";
                totalQueryOgr = new Query();
                totalQueryOgr.Where("dersAcilanId", "equal", SelectedSinav.DersAcilanId);
                if (SelectedSinav.SinavTipId == (int)SinavTipEnum.But)
                {
                    ogrenciAddOdata = $"odata/derskayits?$expand=Ogrenci($select=Id,Ad,Soyad,Ogrno)&$filter=dersAcilanId eq {SelectedSinav.DersAcilanId} and (GecDurum eq false or HarfNot eq 'DC')&$select=Id,OgrenciId";
                }
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
                    //OgrenciDtos.Remove(ogrenciDto);
                    //OgrenciGrid.Refresh();

                    await OgrenciGridRefresh(selectedSinavId);
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
                        GetSinavsByDersAcilanId(SelectedDersAcilans.FirstOrDefault(x => x.Id == sinavDto.DersAcilanId));
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


        private List<Object> Toolbaritems = new List<Object>() { "Add", "Edit", "Delete", "ColumnChooser", new ItemModel() { Text = "Mazeret Sınavı", TooltipText = "Seçilenm Sınavın Mazereti", PrefixIcon = "e-icons e-Signature", Id = "Mazeret" } };


        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            isShowSinavs = true;
            if (args.Item.Id == "Mazeret")
            {
                if (selectedSinavId != 0)
                {
                    if (SelectedSinav.IsYayinli)
                    {
                        isMazeretConfirmDialogOpen = true;
                    }
                    else
                    {
                        dialogUyariText = "Mazeret sınavı oluşturmak için önce sınav sonucunun yayınlanması lazım.";
                        isUyariOpen = true;
                    }
                }
                else
                {
                    dialogUyariText = "Evvela bir ara sınav seçiniz.";
                    isUyariOpen = true;
                }
            }
        }

        //private List<Object> ToolbarDersitems = new List<Object>() { new ItemModel() { Text = "Toplu Büt Oluştur", TooltipText = "Seçilen dersler için büt sınavı oluşturur.", PrefixIcon = "e-icons e-Signature", Id = "TopluBut" } };
        //public async Task ToolbarDersClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        //{
        //    if (args.Item.Id == "TopluBut")
        //    {
        //        TopluButOlustur();
        //    }
        //}


        public async Task MazeretSinavOlustur()
        {
            var mazeret = SinavDtos.FirstOrDefault(x => x.Id == selectedSinavId);
            mazeret.Id = 0;
            mazeret.MazeretiSinavId = selectedSinavId;
            mazeret.Tarih = mazeretTarihi;
            mazeret.SinavTurId = (int)SinavTurEnum.Mazeret_Sinav;
            mazeret.SinavTipId = (int)SinavTipEnum.Mazeret;
            mazeret.SablonAd = $"{SelectedSinav.SablonAd} Mazeret";
            mazeret.KisaAd = $"{SelectedSinav.KisaAd} Mazeret";
            ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/sinav", mazeret);
            isMazeretConfirmDialogOpen = false;

            SinavGridRefresh();
        }

        //public async Task TopluButOlustur()
        //{
        //    List<DersAcilanDto> seciliDersler = await DersAcGrid.GetSelectedRecords();
        //    // Büt sınavlarını oluştur finalin aynı sadece bir modalda bir tarhi girdirt.
        //    // Final sınavı id sini bu büte mazeret ID olarka gir.
        //}

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

        async Task Refresh()
        {
            totalQuery = new Query();
            //totalQuery.Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)" });
            //totalQuery.Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)", "Donem($select=Id,Ad)", "bolum($expand=fakulte($select=Ad,Id);$select=Ad,Id)" });
            await Task.Delay(100);

            if (sadeceYillik == false)
            {
                totalQuery.Where("donemId", "equal", DonemId);
            }
            else
            {
                totalQuery.Where("IsYillik", "equal", sadeceYillik);
            }

            if (ProgramId.HasValue)
            {
                totalQuery.Where("programId", "equal", ProgramId);
            }
            else if (BolumId.HasValue)
            {
                totalQuery.Where("bolumId", "equal", BolumId);
            }
            else if (FakulteId.HasValue)
            {
                totalQuery.Where("fakulteId", "equal", FakulteId);
            }

            StateHasChanged();
            await Task.Delay(100);
            DersAcGrid.Refresh();


        }


        SfDropDownList<int?, SinavTipDto> sinavTipDrop;
        SinavDto sinavInfo;//= new SinavDto();
        bool butEnabled = true;
        async Task SinavTipHandler(ChangeEventArgs<int?, SinavTipDto> args)
        {
            
            if (args.Value == (int?)SinavTipEnum.But)
            {
                SinavDto selectedSinavTip = SinavDtos.FirstOrDefault(x => x.SinavTipId == (int)SinavTipEnum.Final);
                if (selectedSinavTip == null)
                {
                    dialogUyariText = "Büt oluşturmak için dersin bir finalinin olması lazım.";
                    isUyariOpen = true;
                    await sinavTipDrop.Clear();
                }
                else
                {
                    butEnabled = false;
                    sinavInfo.EtkiOran = selectedSinavTip.EtkiOran;
                }
            }
        }

        async Task OnValueSelect(Syncfusion.Blazor.DropDowns.SelectEventArgs<SinavTipDto> args)
        {
            if (args.ItemData.Id == (int?)SinavTipEnum.But)
            {
                SinavDto selectedSinavTip = SinavDtos.FirstOrDefault(x => x.SinavTipId == (int)SinavTipEnum.Final);
                if (selectedSinavTip == null)
                {
                    dialogUyariText = "Büt oluşturmak için dersin bir finalinin olması lazım.";
                    isUyariOpen = true;
                    await sinavTipDrop.Clear();
                }
                else
                {
                    butEnabled = false;
                    sinavInfo.EtkiOran = selectedSinavTip.EtkiOran;
                }
            }
        }

        private async Task OnChangeSadeceYillik(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool> args)
        {
            if (args.Checked)
            {
                yillikStyle = "color:red";
            }
            else
            {
                yillikStyle = "";
            }
            await Refresh();
        }
    }
}
