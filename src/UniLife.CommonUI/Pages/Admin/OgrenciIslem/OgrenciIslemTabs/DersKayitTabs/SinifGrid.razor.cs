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
        

        [Parameter]
        public int Sinif { get; set; }
        public List<DersAcilanDto> DersAcilanDtos { get; set; } = new List<DersAcilanDto>();


        bool dialog;
        string dialogText = "";


        [Parameter]
        public EventCallback<DersAcilanDto> DersKayitCallBack { get; set; }
        [Parameter]
        public bool IsOnayli { get; set; }


        protected override void OnInitialized()
        {
            
            ReadAcilan();
        }


        

        void ReadAcilan()
        {
            try
            {
                ApiResponseDto<List<DersAcilanDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersacilan/GetAcilanDerssByOgrenciId/{appState.OgrenciState.Id}/{Sinif}/{appState.DersKayitDonemIdState}").Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    DersAcilanDtos = apiResponse.Result.OrderBy(x => x.Kod).ToList();
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }

        }


        public async Task CommandClickHandler(CommandClickEventArgs<DersAcilanDto> args)
        {
            // oka bastıkmı varsa zaten uyarı veriyoruz onu vermeye devam edecek
            // eğer atma başrılı olursa üsttegi giridn satırı boyayacaz
            // ilk açılıştada alttaki gidin idlerini üsttekilerde aratacaz varsa boyayacaz



            if (args.CommandColumn.Title == "Kayıt")
            {

                if (args.RowData.Durum == false)
                {
                    dialogText = $"'{args.RowData.Kod}' kodlu, '{args.RowData.Ad}' isimli ders açılmadı.";
                    dialog = true;
                    return;
                }

                //if (DersKayitDtos.Any(x => x.Id == args.RowData.Id))
                //{
                //    dialog = true;
                //    dialogText = $"'{args.RowData.Kod}' kodlu, '{args.RowData.Ad}' isimli ders zaten ekli!";
                //}
                //else
                //{
                    await DersKayitCallBack.InvokeAsync(args.RowData);
                //}
            }

            if (args.CommandColumn.Title == "Seçmeli Al")
            {
                //if (DersKayitDtos.Any(x => x.Id == args.RowData.Id))
                //{
                //    dialog = true;
                //    dialogText = $"{args.RowData.Kod} kodlu, {args.RowData.Ad} isimli ders yerine seçim zaten yapılmış!";
                //}
                //else
                //{
                    
                    //Modal içinde girden seçmeli ders seçtiriyoruz.
                    //ApiResponseDto<List<DersAcilanDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersAcilan/ByZorunlu/{false}").Result;

                    OData<DersAcilanDto> apiResponse = Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersAcilans?$filter=Zorunlu eq false and Sinif eq '{args.RowData.Sinif}'").Result;

                    SecmeliDerslerDtos = apiResponse.Value;
                    foreach (var item in SecmeliDerslerDtos)
                    {
                        item.YerineSecilenAd = args.RowData.Ad;
                        item.YerineSecilenId = args.RowData.Id;
                    }
                secmeliDersDialogOpen = true;
                //}
            }
            //DersKayitGrid.Refresh();

        }


        public async Task CommandClickSecmeliAl(DersAcilanDto args)
        {
            //if (DersKayitDtos.Any(x => x.Id == args.Id))
            //{
            //    dialog = true;
            //    dialogText = $"{args.Kod} kodlu, {args.Ad} isimli ders yerine seçim zaten yapılmış!";
            //}
            //else
            //{
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
            //}

        }

        public async Task CommandClickHandlerSecmeli(CommandClickEventArgs<DersAcilanDto> args)
        {
            secmeliDersDialogOpen = false;
            if (args.CommandColumn.Title == "Kayıt Ol Secmeli")
            {
                await DersKayitCallBack.InvokeAsync(args.RowData);
            }
        }



       



        

        

        
    }
}
