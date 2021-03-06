using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Data;
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


        bool isUyariOpen;
        string dialogUyariText;

        SfGrid<DersDto> DersGrid;

        List<DersDto> dersDtos = new List<DersDto>();


        //List<DonemDto> donemDtos = new List<DonemDto>();

        public string[] MenuItems = new string[] { "Group", "Ungroup", "ColumnChooser", "Filter" };

        private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "1200px" };

        string[] Initial = (new string[] { "Sinif", "DonemTipId" });

        int? donemId;

        public bool isOpsCoklama { get; set; }


        bool yerineDersDialogOpen;
        static int? yerineExistProgramId;
        string yerineDersKod;
        string yerineDersAd;
        double yerineDersKredi;
        int yerineDersAkts;
        int yerineDersId;
        int sonMufredatId;

        string edittekiDersKod;

        public Query totalQuery = new Query();

        string OdataQuery = "odata/Derss";

        SfTextBox DialogKod;

        DersDto dersDtoContext=new DersDto();

        bool isShowYillik;
        protected override void OnInitialized()
        {
            ReadDerss();

            //ApiResponseDto<List<DonemDto>> apiResponseDonem = Http.GetFromJsonAsync<ApiResponseDto<List<DonemDto>>>("api/donem/current").Result;
            //donemDtos = apiResponseDonem.Result;


        }

        void ReadDerss()
        {
            try
            {
                ApiResponseDto<List<DersDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersDto>>>("api/ders/GetDersByMufredatId/" + appState.MufredatState.MufredatId).Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add($"{appState.MufredatState.MufredatAd} müfredatına bağlı dersler getirildi", MatToastType.Success);
                    dersDtos = apiResponse.Result;
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }

        }


        public async void ActionBeginHandler(ActionEventArgs<DersDto> args)
        {
            if (args.Data !=null && args.Data.DonemTipId == (int)DonemTipEnum.Güz_Dönemi)
            {
                isShowYillik = true;
            }
            else
            {
                isShowYillik = false;
            }
        }

        public async void ActionCompletedHandler(ActionEventArgs<DersDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                if (!args.Data.IsYillik)
                {
                    dersDtoContext.IsYillik = false;
                }
                dersiAktifledi = null;
            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Cancel)
            {
                dersiAktifledi = null;
                isOpsCoklama = false;
                yerineExistProgramId = null;
            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {

                    if (await Update(args.Data))
                    {
                        DersGrid.Refresh();
                    }
                    args.Cancel = true;
                    await DersGrid.CloseEdit();
                    isOpsCoklama = false;


                }
                else if (args.Action == "Add")
                {
                    //await Create(args.Data);
                    if (await Create(args.Data, false))
                    {
                        DersGrid.Refresh();
                    }
                    args.Cancel = true;
                    await DersGrid.CloseEdit();
                    isOpsCoklama = false;
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                if (await Delete(args.Data))
                {
                    DersGrid.Refresh();
                }
                args.Cancel = true;
                await DersGrid.CloseEdit();
                isOpsCoklama = false;
            }
        }

        public async Task<bool> Create(DersDto dersDto, bool isOpsCoklama)
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
                    if (!isOpsCoklama)
                    {
                        dersDtos.FirstOrDefault(x => x.Id == 0).Id = apiResponse.Result.Id;
                    }
                    ReadDerss();
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    return true;
                }
                else
                {
                    if (!isOpsCoklama)
                    {
                        dersDtos.Remove(dersDto);
                    }

                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (!isOpsCoklama)
                {
                    dersDtos.Remove(dersDto);
                    DersGrid.Refresh();
                }
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
            }
        }


        public async Task<bool> Update(DersDto dersDto)
        {
            //this updates the IsCompleted flag only
            try
            {
                if (isOpsCoklama)
                {
                    dersDto.Id = 0;
                    return await Create(dersDto, isOpsCoklama);
                }
                else
                {



                    ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/ders", dersDto);

                    if (!apiResponse.IsError)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                        if (dersDto.Durum == false)
                        {
                            yerineDersDialogOpen = true;
                        }
                        if (dersiAktifledi.HasValue)
                        {
                            if ((bool)dersiAktifledi == true)
                            {
                                DeleteExistKancas();
                            }
                        }
                        return true;
                    }
                    else
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
            }
        }

        public async Task<bool> Delete(DersDto dersDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/ders/" + dersDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("", MatToastType.Success, "İşlem başarılı.");
                    dersDtos.Remove(dersDto);
                    return true;
                }
                else
                {
                    matToaster.Add("", MatToastType.Danger, "İşlem başarısız!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
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

        //public void QueryCellInfoHandler(QueryCellInfoEventArgs<DersDto> args)
        //{
        //    if (args.Data.AktifDonemdeAcik)
        //    {
        //        args.Cell.AddStyle(new string[] { "background-color:#80d192" });
        //    }
        //}

        public async Task CommandClickHandler(CommandClickEventArgs<DersDto> args)
        {
            if (args.CommandColumn.Title == "Dersi Ac")
            {
                if (args.RowData.Durum == false)
                {
                    dialogUyariText = "Bu ders aktif değil.";
                    isUyariOpen = true;
                    return;
                }
                try
                {


                    ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/ders/CreateDersAcilansByDersId", args.RowData.Id);
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        matToaster.Add("Ders açıldı", MatToastType.Success, "İşlem başarılı.");
                        ReadDerss();
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
            if (args.CommandColumn.Title == "Çokla")
            {
                isOpsCoklama = true;
                yerineExistProgramId = null;
                //Id sıfırla.
                //    paramtereye coklama oldugun ata
                //    insert çak. 
            }
            else if (args.CommandColumn.Type == CommandButtonType.Edit)
            {
                yerineExistProgramId = args.RowData.ProgramId;
                yerineDersKod = args.RowData.Kod;
                yerineDersId = args.RowData.Id;
                yerineDersAd = args.RowData.Ad;
                yerineDersAkts = args.RowData.Akts ?? 0;
                yerineDersKredi = args.RowData.Kredi;
            }


        }

        bool? dersiAktifledi;
        private void onChange(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            try
            {
                if ((bool)args.Value == false && yerineExistProgramId.HasValue)
                {
                    dersiAktifledi = false;
                    //ApiResponseDto<MufredatDto> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<MufredatDto>>($"api/Mufredat/GetLastMufredatByProgramId/{yerineExistProgramId}").Result;
                    //sonMufredatId = apiResponse.Result.Id;


                }
                else if ((bool)args.Value == true && yerineDersId != 0)
                {
                    dersiAktifledi = true;
                    //DeleteExistKancas();
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        private void DeleteExistKancas()
        {
            try
            {
                ApiResponseDto<MufredatDto> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<MufredatDto>>($"api/ders/DeleteExistKancas/{yerineDersId}").Result;
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "Bağlı dersleri silindi.");
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Bağlı dersleri silerken oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Bağlı dersleri silerken oluştu!");
            }

        }

        async Task CommandClickHandlerDers(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersDto> args)
        {
            if (args.CommandColumn.Title == "Ders Ekle")
            {
                try
                {
                    //Virgüllü çözüm
                    //////args.RowData.EskiMufBagliDersId = args.RowData.EskiMufBagliDersId + "," + yerineDersId + ",";
                    //////args.RowData.EskiMufBagliDersId.Replace(",,", ",");

                    //////args.RowData.Mufredat = null;
                    //////ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>("api/ders", args.RowData);

                    //////if (apiResponse.IsSuccessStatusCode)
                    //////{
                    //////    //DersGrid.Refresh();
                    //////    matToaster.Add("Yerine ders seçildi. Kayıt edip kapatabilirsiniz.", MatToastType.Success, "İşlem başarılı.");
                    //////}
                    //////else
                    //////    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                    ///
                    DersKancaDto dersKancaDto = new DersKancaDto()
                    {
                        AktifMufredatDersId = args.RowData.Id,
                        PasifMufredatDersId = yerineDersId,
                        PasifMufredatAkts = yerineDersAkts,
                        PasifMufredatDersAd = yerineDersAd,
                        PasifMufredatDersKod = yerineDersKod,
                        PasifMufredatKredi = yerineDersKredi,
                        AktifMufredatAkts = (int)args.RowData.Akts,
                        AktifMufredatDersAd = args.RowData.Ad,
                        AktifMufredatDersKod = args.RowData.Kod,
                        AktifMufredatKredi = args.RowData.Kredi
                    };


                    ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/derskanca/YeniKanca", dersKancaDto);
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        //dersDto.KancalananDersAd = args.RowData.Ad;
                        matToaster.Add("Yerine ders seçildi.", MatToastType.Success, "İşlem başarılı.");
                        StateHasChanged();
                        ReadDerss();
                    }
                    else
                        matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");

                }
                catch (Exception ex)

                {
                    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
                }
                finally
                {
                    yerineDersDialogOpen = false;
                }
            }
        }

        async Task KodChanged(ChangedEventArgs args)
        {
            try
            {
                if (this.dersDtoContext.Kod != null)
                {
                    OData<DersDto> apiResponse = await Http.GetFromJsonAsync<OData<DersDto>>($"odata/derss?$filter=Kod eq '{args.Value.ToUpper()}' and MufredatId eq {appState.MufredatState.MufredatId}");
                    if (apiResponse.Value.Count > 0)
                    {
                        matToaster.Add($"{args.Value.ToUpper()} kod bilgisi {appState.MufredatState.MufredatAd} da zaten mevcut!", MatToastType.Danger, "Hata oluştu!");
                        this.dersDtoContext.Kod = null;
                    }
                    //else
                    //{
                    //    this.dersDtoContext.Kod = this.dersDtoContext.Kod.ToUpper();
                    //    this.StateHasChanged();
                    //}
                }

            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }

        }


        public void QueryCellInfoHandler(QueryCellInfoEventArgs<DersDto> args)
        {
            if (!args.Data.Durum)
            {
                args.Cell.AddStyle(new string[] { "background-color:#f2ab96" });
            }


        }


        private void OnDonemTipValueChage(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?, KeyValueDto> args)
        {
            if ((int)DonemTipEnum.Güz_Dönemi == args.Value)
            {
                isShowYillik = true;
            }
            else
            {
                isShowYillik = false;
            }
        }


    }
}
