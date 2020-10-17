using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using UniLife.Shared.Dto.Definitions;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Data;
using System.Threading.Tasks;
using UniLife.Shared.Dto;
using System.Net.Http.Json;
using System;
using MatBlazor;
using UniLife.CommonUI.Extensions;

namespace UniLife.CommonUI.Pages.Admin.OgrenciIslem.OgrenciIslemTabs
{
    public partial class DersKayit : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }



        bool isOnayli;
        SfGrid<DersAcilanDto> DersKayitGrid;

        int? seciliDonemId;

        public bool IsChecked { get; set; } = true;

        List<DonemDto> donemDtos = new List<DonemDto>();
        //SfRadioButton<string> donemRadio = new SfRadioButton<string>();


        [Parameter]
        public OgrenciDto _OgrenciDto { get; set; }

        int ogrenciProgramSure;
        List<DersAcilanDto> ListDersAcilan = new List<DersAcilanDto>();
        List<DersAcilanDto> DersKayitDtos = new List<DersAcilanDto>();


        SfTab Tab;
        public string PreviousEffect { get; set; }
        public string NextEffect { get; set; }

        SfDropDownList<int?, DonemDto> DropDonem;
        public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();

        string dialogText;
        bool dialog;

        private List<Object> Toolbaritems;
    //        = new List<Object>() {
    //        new ItemModel() { Text = "Eklenen Derslere Kayıt", TooltipText = "Click", PrefixIcon = "e-icons e-SendToBack", Id = "DersKayit" },
    //        new ItemModel() { Text = "Onayla", TooltipText = "Click", PrefixIcon = "e-icons e-SendToBack", Id = "DersOnay" },
    //        new ItemModel() { Text = "Onay Kaldır", TooltipText = "Click", PrefixIcon = "e-icons e-SendToBack", Id = "OnayKaldır", Disabled=true }
    //};



    //protected override async Task OnInitializedAsync()
    //{
    //    _OgrenciDto = appState.OgrenciState;
    //    await GetMufredatAcilanDerss();
    //    await SetOgrenciProgramSure();
    //}

    //protected override void OnInitialized()
    protected override async Task OnInitializedAsync()
        {
            if (_OgrenciDto == null)
            {
                _OgrenciDto = await appState.GetOgrenciState();
            }

            GetDonems();
            seciliDonemId = donemDtos.FirstOrDefault(x => x.Durum == true).Id;
            //GetAcilanDerssByMufredat();
            SetOgrenciProgramSure();
            SetMufredatAd();
            ReadKayitli();

            Toolbaritems = new List<object>();
            if (appState.UserNavigationLoadRole == UserRoles.Ogrenci.ToString() || UserRoles.Personel.ToString() == appState.UserNavigationLoadRole)
            {
                if (!isOnayli)
                {
                    Toolbaritems.Add(new ItemModel() { Text = "Eklenen Derslere Kayıt", TooltipText = "Click", PrefixIcon = "e-icons e-SendToBack", Id = "DersKayit" });
                }
            }
            if (appState.UserNavigationLoadRole == UserRoles.Personel.ToString() || UserRoles.Akademisyen.ToString() == appState.UserNavigationLoadRole)
            {
                if (!isOnayli)
                {
                    Toolbaritems.Add(new ItemModel() { Text = "Onayla", TooltipText = "Click", PrefixIcon = "e-icons e-SendToBack", Id = "DersOnay" });
                }
                else
                {
                    Toolbaritems.Add(new ItemModel() { Text = "Onay Kaldır", TooltipText = "Click", PrefixIcon = "e-icons e-SendToBack", Id = "OnayKaldır" });
                }
            }
        }

        void GetDonems()
        {
            ApiResponseDto<List<DonemDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DonemDto>>>("api/donem/current").Result;
            donemDtos = apiResponse.Result;

            appState.DersKayitDonemIdState = donemDtos.FirstOrDefault(x => x.Durum).Id;
        }


        int selectedItem;

        private void OnDonemChange(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            IsChecked = !IsChecked;
            appState.DersKayitDonemIdState = (int)seciliDonemId;//await donemRadio.GetSelectedValue());
        }

        //public async void onDonemChange()
        //{
        //    IsChecked = !IsChecked;
        //    appState.DersKayitDonemIdState = Convert.ToInt32(seciliDonemId);//await donemRadio.GetSelectedValue());
        //    //this.NextEffect = "FadeZoomIn";
        //    //Tab.Refresh();
        //    //if (Tab.SelectedItem == 0)
        //    //{
        //    //    await Tab.Select(Tab.SelectedItem + 1);
        //    //    await Tab.Select(Tab.SelectedItem - 1);
        //    //}
        //    //else
        //    //{
        //    //    await Tab.Select(Tab.SelectedItem - 1);
        //    //    await Tab.Select(Tab.SelectedItem + 1);
        //    //}

        //    //this.NextEffect = "SlideRightIn";
        //}



        void AcilanDersleriYenile()
        {

        }


        //void GetAcilanDerssByMufredat()
        //{
        //    ApiResponseDto<List<DersAcilanDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersacilan/GetAcilanDersByMufredatId/{_OgrenciDto.MufredatId}").Result;
        //    ListDersAcilan = apiResponse.Result;

        //    ApiResponseDto<List<DersAcilanDto>> apiResponseKayitli = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersAcilan/GetKayitliDerssByOgrenciIdDonemId/{_OgrenciDto.Id}/{appState.DersKayitDonemIdState}").Result;
        //    DersKayitDtos = apiResponseKayitli.Result;
        //}

        void SetOgrenciProgramSure()
        {
            ApiResponseDto<ProgramDto> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<ProgramDto>>($"api/program/{_OgrenciDto.ProgramId}").Result;
            ogrenciProgramSure = apiResponse.Result.NormalSure;
        }

        void SetMufredatAd()
        {
            ApiResponseDto<MufredatDto> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<MufredatDto>>($"api/mufredat/{_OgrenciDto.MufredatId}").Result;
            _OgrenciDto.MufredatAdi = apiResponse.Result.Ad;
        }


        //async Task GetMufredatAcilanDerss()
        //{
        //    ApiResponseDto<List<DersAcilanDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersacilan/GetAcilanDersByMufredatId/{_OgrenciDto.MufredatId}");
        //    ListDersAcilan = apiResponse.Result;
        //}

        //async Task SetOgrenciProgramSure()
        //{
        //    ApiResponseDto<ProgramDto> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<ProgramDto>>($"api/program/{_OgrenciDto.ProgramId}");
        //    ogrenciProgramSure = apiResponse.Result.NormalSure;
        //}

        public void OnTabSelecting(SelectingEventArgs args)
        {
            if (args.IsSwiped)
            {
                args.Cancel = true;
            }
        }

        void ReadKayitli()
        {
            try
            {
                ApiResponseDto<List<DersAcilanDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersAcilan/GetKayitliDerssByOgrenciId/{appState.OgrenciState.Id}/{appState.DersKayitDonemIdState}").Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    DersKayitDtos = apiResponse.Result.OrderBy(x => x.Kod).ToList();
                    if (DersKayitDtos.Any(x => x.IsOnayli))
                    {
                        isOnayli = true;
                    }
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        async Task Onayla()
        {
            //Kayıt olunan derslistesini al onayını ver.
            try
            {
                var kayitliDersler = await DersKayitGrid.GetCurrentViewRecords();

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/DersKayit/Onayla", kayitliDersler.Select(x => x.DersKayitId));
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    isOnayli = true;
                    StateHasChanged();
                    DersKayitGrid.Refresh();
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }

        }

        async Task OnayKaldir()
        {
            var kayitliDersler = await DersKayitGrid.GetCurrentViewRecords();
            ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/DersKayit/OnayKaldir", kayitliDersler.Select(x => x.DersKayitId));
            if (apiResponse.IsSuccessStatusCode)
            {
                matToaster.Add(apiResponse.Message, MatToastType.Success, "Onay kaldırıldı.");
                isOnayli = false;
                StateHasChanged();
                DersKayitGrid.Refresh();
            }
            else
                matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
        }

        public void QueryCellInfoHandler(QueryCellInfoEventArgs<DersAcilanDto> args)
        {
            if (!string.IsNullOrWhiteSpace(args.Data.YerineSecilenAd))
            {
                args.Cell.AddClass(new string[] { "below-35" });
                args.Cell.AddStyle(new string[] { "background-color:#cfe388" });
            }
        }

        public async Task CommandClickHandlerKayit(CommandClickEventArgs<DersAcilanDto> args)
        {
            if (args.CommandColumn.Title == "Kayıt Kaldır")
            {
                try
                {
                    var response = await Http.DeleteAsync($"api/DersKayit/DeleteByOgrId_DersId/{appState.OgrenciState.Id}/{args.RowData.Id}");

                    if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                    {
                        DersKayitDtos.RemoveAll(x => x.Id == args.RowData.Id);
                        DersKayitGrid.Refresh();
                        StateHasChanged();
                        matToaster.Add("Ders kayıdı kaldırıldı", MatToastType.Success);
                    }
                    else
                        matToaster.Add("İşlem başarısız!", MatToastType.Danger);
                }
                catch (Exception ex)
                {
                    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                }
            }
        }

        async Task KayıtOl()
        {

            try
            {
                List<DersKayitDto> ogrenciDersKayitDtos = new List<DersKayitDto>();
                foreach (var item in DersKayitDtos)
                {
                    ogrenciDersKayitDtos.Add(new DersKayitDto { DersAcilanId = item.Id, OgrenciId = appState.OgrenciState.Id, DersYerineSecilenId = item.YerineSecilenId ?? item.Id, DersYerineSecilenAd = item.YerineSecilenAd });
                }

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/DersKayit/OgrenciKayitToDerss", ogrenciDersKayitDtos);


                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    DersKayitGrid.Refresh();
                    StateHasChanged();
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }
        }

        async Task DersKayitCallBackMain(DersAcilanDto dersAcilanDto)
        {
            if (DersKayitDtos.Any(x => x.Id == dersAcilanDto.Id))
            {
                dialog = true;
                dialogText = $"'{dersAcilanDto.Kod}' kodlu, '{dersAcilanDto.Ad}' isimli ders zaten ekli!";
            }

            DersKayitDtos.Add(dersAcilanDto);
            DersKayitGrid.Refresh();
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "DersKayit")
            {
                await KayıtOl();
            }
            else if (args.Item.Id == "DersOnay")
            {
                await Onayla();
            }
            else if (args.Item.Id == "OnayKaldır")
            {
                await OnayKaldir();
            }
        }
    }
}
