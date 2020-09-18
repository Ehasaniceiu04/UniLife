using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.Admin.OgrenciIslem.OgrenciIslemTabs.DersKayitTabs
{
    public partial class SinifGrid : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }


        private Dictionary<string, object> primButton = new Dictionary<string, object>() { { "title", "Ders Seç" } };


        bool secmeliDersDialogOpen;
        List<DersAcilanDto> SecmeliDerslerDtos = new List<DersAcilanDto>();
        SfGrid<DersAcilanDto> SecmeliDersGrid;

        bool isOnayli;
        public void QueryCellInfoHandler(QueryCellInfoEventArgs<DersAcilanDto> args)
        {
            if (!string.IsNullOrWhiteSpace(args.Data.YerineSecilenAd))
            {
                args.Cell.AddClass(new string[] { "below-35" });
                args.Cell.AddStyle(new string[] { "background-color:#cfe388" });
            }
        }
        public void QueryCellInfoAcilanHandler(QueryCellInfoEventArgs<DersAcilanDto> args)
        {
            if (!args.Data.Durum)
            {
                //args.Cell.AddClass(new string[] { "below-35" });
                args.Cell.AddStyle(new string[] { "background-color:#b57f7f" });
            }
            //if (args.Data.Kayitta)
            //{
            //    args.Cell.AddStyle(new string[] { "background-color:#62734d" });
            //}
        }



        SfGrid<DersAcilanDto> DersAcilanGrid;
        SfGrid<DersAcilanDto> DersKayitGrid;

        [Parameter]
        public int Sinif { get; set; }
        public List<DersAcilanDto> DersAcilanDtos { get; set; } = new List<DersAcilanDto>();

        //[Parameter]
        public List<DersAcilanDto> DersKayitDtos { get; set; }// = new List<DersAcilanDto>();

        bool dialog;
        string dialogText = "";




        protected override void OnInitialized()
        {
            ReadKayitli();
            ReadAcilan();
        }


        void ReadKayitli()
        {
            try
            {
                ApiResponseDto<List<DersAcilanDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersAcilan/GetKayitliDerssByOgrenciId/{appState.OgrenciState.Id}/{Sinif}/{appState.DersKayitDonemIdState}").Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    DersKayitDtos = apiResponse.Result.OrderBy(x => x.Kod).ToList();
                    if (DersKayitDtos.Any(x=>x.IsOnayli))
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

        void ReadAcilan()
        {
            try
            {
                ApiResponseDto<List<DersAcilanDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersacilan/GetAcilanDerssByOgrenciId/{appState.OgrenciState.Id}/{Sinif}/{appState.DersKayitDonemIdState}").Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    //apiResponse.Result=apiResponse.Result.Where(x => DersKayitDtos.Select(x => x.Kod).Contains(x.Kod)).ToList().ForEach(x => { x.Kayitta = true; });
                    DersAcilanDtos = apiResponse.Result.OrderBy(x => x.Kod).ToList();
                    //foreach (var item in DersAcilanDtos)
                    //{
                    //    if (DersKayitDtos.Any(x=>x.Kod == item.Kod))
                    //    {
                    //        item.Kayitta = true;
                    //    }
                    //}
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }

        }


        public void CommandClickHandler(CommandClickEventArgs<DersAcilanDto> args)
        {
            // oka bastıkmı varsa zaten uyarı veriyoruz onu vermeye devam edecek
            // eğer atma başrılı olursa üsttegi giridn satırı boyayacaz
            // ilk açılıştada alttaki gidin idlerini üsttekilerde aratacaz varsa boyayacaz



            if (args.CommandColumn.Title == "Kayıt Ol")
            {

                if (args.RowData.Durum == false)
                {
                    dialogText = $"'{args.RowData.Kod}' kodlu, '{args.RowData.Ad}' isimli ders açılmadı.";
                    dialog = true;
                    return;
                }

                if (DersKayitDtos.Any(x => x.Id == args.RowData.Id))
                {
                    dialog = true;
                    dialogText = $"'{args.RowData.Kod}' kodlu, '{args.RowData.Ad}' isimli ders zaten ekli!";
                }
                else
                {
                    DersKayitDtos.Add(args.RowData);
                    DersKayitGrid.Refresh();
                }
            }

            if (args.CommandColumn.Title == "Seçmeli Al")
            {
                if (DersKayitDtos.Any(x => x.Id == args.RowData.Id))
                {
                    dialog = true;
                    dialogText = $"{args.RowData.Kod} kodlu, {args.RowData.Ad} isimli ders yerine seçim zaten yapılmış!";
                }
                else
                {
                    secmeliDersDialogOpen = true;
                    //Modal içinde girden seçmeli ders seçtiriyoruz.
                    //ApiResponseDto<List<DersAcilanDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersAcilan/ByZorunlu/{false}").Result;

                    OData<DersAcilanDto> apiResponse = Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersAcilans?$filter=Zorunlu eq false and Sinif eq '{args.RowData.Sinif}'").Result;

                    SecmeliDerslerDtos = apiResponse.Value;
                    foreach (var item in SecmeliDerslerDtos)
                    {
                        item.YerineSecilenAd = args.RowData.Ad;
                        item.YerineSecilenId = args.RowData.Id;
                    }
                }
            }
            //DersKayitGrid.Refresh();

        }


        public async Task CommandClickSecmeliAl(DersAcilanDto args)
        {
            if (DersKayitDtos.Any(x => x.Id == args.Id))
            {
                dialog = true;
                dialogText = $"{args.Kod} kodlu, {args.Ad} isimli ders yerine seçim zaten yapılmış!";
            }
            else
            {
                try
                {
                    secmeliDersDialogOpen = true;
                    //Modal içinde girden seçmeli ders seçtiriyoruz.
                    //ApiResponseDto<List<DersAcilanDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersAcilan/ByZorunlu/{false}");
                    OData<DersAcilanDto> apiResponse = await Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersAcilans?$filter=Zorunlu eq false and SecmeliKodu eq '{args.SecmeliKodu}' and Sinif eq {args.Sinif}");

                    if (apiResponse.Value.Count() > 0)
                    {
                        SecmeliDerslerDtos = apiResponse.Value;
                        foreach (var item in SecmeliDerslerDtos)
                        {
                            item.YerineSecilenAd = args.Ad;
                            item.YerineSecilenId = args.Id;
                        }
                    }
                    else
                        matToaster.Add("", MatToastType.Danger, "İşlem başarısız!");
                }
                catch (Exception ex)
                {
                    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                }




                //secmeliDersDialogOpen = true;
                ////Modal içinde girden seçmeli ders seçtiriyoruz.
                ////ApiResponseDto<List<DersAcilanDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersAcilan/ByZorunlu/{false}");
                //OData<DersAcilanDto> apiResponse = await Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersAcilans?$filter=Zorunlu eq false and SecmeliKodu eq '{args.SecmeliKodu}'");
                //SecmeliDerslerDtos = apiResponse.Value;
                //foreach (var item in SecmeliDerslerDtos)
                //{
                //    item.YerineSecilenAd = args.Ad;
                //    item.YerineSecilenId = args.Id;
                //}
            }

        }

        public async Task CommandClickHandlerSecmeli(CommandClickEventArgs<DersAcilanDto> args)
        {
            secmeliDersDialogOpen = false;
            if (args.CommandColumn.Title == "Kayıt Ol Secmeli")
            {
                DersKayitDtos.Add(args.RowData);
                DersKayitGrid.Refresh();
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
                        matToaster.Add("Ders kayıdı kaldırıldı", MatToastType.Success);
                        DersKayitDtos.RemoveAll(x => x.Id == args.RowData.Id);
                        DersKayitGrid.Refresh();
                        StateHasChanged();
                        matToaster.Add("İşlem başarılı.", MatToastType.Success);
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

        async Task Onayla()
        {
            //Kayıt olunan derslistesini al onayını ver.
            try
            {
                var kayitliDersler = await DersKayitGrid.GetCurrentViewRecords();

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/DersKayit/Onayla", kayitliDersler.Select(x=>x.DersKayitId));
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    isOnayli = true;
                    DersAcilanGrid.Refresh();
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
            //TODO
        }
    }
}
