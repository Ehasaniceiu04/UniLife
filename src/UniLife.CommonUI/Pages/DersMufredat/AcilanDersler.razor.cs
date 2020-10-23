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
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using System.Drawing;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class AcilanDersler : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        int? _programId;
        public int? ProgramId
        {
            get => _programId;
            set
            {
                if (_programId == value)
                {
                    Refresh();
                    return;
                }
                else
                {
                    _programId = value;
                    Refresh();
                }

            }
        }
        int? _bolumId;
        public int? BolumId
        {
            get => _bolumId;
            set
            {
                if (_bolumId == value)
                {
                    Refresh();
                    return;
                }
                else
                {
                    _bolumId = value;
                    Refresh();
                }
            }
        }
        int? _fakulteId;
        public int? FakulteId
        {
            get => _fakulteId;
            set
            {
                if (_fakulteId == value)
                {
                    Refresh();
                    return;
                }
                else
                {
                    _fakulteId = value;
                    Refresh();
                }
            }
        }

        int? _donemId;
        public int? DonemId
        {
            get => _donemId;
            set
            {
                if (_donemId == value)
                {
                    Refresh();
                    return;
                }
                else
                {
                    _donemId = value;
                    Refresh();
                }

            }
        }

        int? tempProgramId;
        int? tempBolumId;
        int? tempFakulteId;

        string OdataQuery= "odata/dersacilans";
        //string OdataQuery = "odata/DersAcilansExt";

        //public Query totalQuery = new Query().Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)", "Donem($select=Id,Ad)", "Bolum($select=Id,Ad)", "Fakulte($select=Id,Ad)" });
        public Query totalQuery;//= new Query().Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)", "Donem($select=Id,Ad)", "bolum($expand=fakulte($select=Ad,Id);$select=Ad,Id)" });


        bool akademisyenDialogOpen;
        SfGrid<AkademisyenDto> AkademisyenGrid;

        bool programDialogOpen;
        bool isProgramSecildi;
        bool isGridVisible;

        Syncfusion.Blazor.Grids.SfGrid<DersAcilanDto> DersAcilanGrid;

        List<DersAcilanDto> dersAcilanDtos { get; set; }

        List<ProgramDto> programDtos { get; set; }
        List<AkademisyenDto> akademisyenDtos { get; set; }

        public Query Query = new Query().Select(new List<string> { "Ad", "Id" }).Take(6).RequiresCount();

        SfDropDownList<int, KeyValueDto> DropAltKotaUyg;
        SfDropDownList<int, KeyValueDto> DropBolDisKotaUyg;
        List<KeyValueDto> DropKotaUygDtos = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "Hayır", Id = 0 },
            new KeyValueDto() { Ad = "Evet", Id = 1 },
            new KeyValueDto() { Ad = "Evet(Referans dersleri bolum içi değerlendir)", Id = 2 },
            new KeyValueDto() { Ad = "Evet(Genel Kontenjandan Al-Bloke Koy)", Id = 3 },
            new KeyValueDto() { Ad = "Evet(Referans dersleri bolum içi değerlendir)(Genel Kontenjandan Al-Bloke Koy)", Id = 4 }
        };


        private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "1100px" };

        //public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).Take(6).RequiresCount();
        public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();
        SfDropDownList<int?, DonemDto> DropDonem;


        public bool isOpsCoklama { get; set; }

        DersAcilanDto selectedRecord;

        bool isKayitliShow;

        bool isUyariOpen;
        string dialogUyariText;

        bool isShowKayOgrSay;
        bool sadeceYillik;
        string yillikStyle;
        //public void Change(@Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        //{
        //    if (args.Value == null)
        //    {
        //        DersAcilanGrid.ClearFiltering();
        //    }
        //    else
        //    {
        //        //startswith
        //        DersAcilanGrid.FilterByColumn("ProgramId", "equal", 1);
        //    }
        //}

        protected override async Task OnInitializedAsync()
        {
            DonemId = (await appState.GetDonemState()).Id;
        }

        public async Task OnActionBeginHandler(ActionEventArgs<DersAcilanDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
            {
                //plazceHolderProgramAd = "";
                //ProgramValueHolder = null;
                //fakulteId = null;
                //bolumId = null;

            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                tempProgramId = null;
                tempBolumId = null;
                tempFakulteId = null;
                isProgramSecildi = false;
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (isProgramSecildi)
                {
                    args.Data.ProgramId = tempProgramId;
                    args.Data.BolumId = tempBolumId;
                    args.Data.FakulteId = tempFakulteId;
                }
            }

            await ActionCompletedHandler(args);

        }


        public async Task ActionCompletedHandler(ActionEventArgs<DersAcilanDto> args)
        {
            try
            {
                if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
                {

                }
                if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
                {
                    if (args.Action == "Edit")
                    {
                        if (await Update(args.Data))
                        {
                            DersAcilanGrid.Refresh();
                        }
                        args.Cancel = true;
                        await DersAcilanGrid.CloseEdit();
                        isOpsCoklama = false;
                    }
                    else if (args.Action == "Add")
                    {
                        if (await Create(args.Data))
                        {
                            DersAcilanGrid.Refresh();
                        }
                        args.Cancel = true;
                        await DersAcilanGrid.CloseEdit();
                        isOpsCoklama = false;
                    }

                }
                if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
                {
                    if (await Delete(args.Data))
                    {

                        DersAcilanGrid.Refresh();
                    }
                    args.Cancel = true;
                    await DersAcilanGrid.CloseEdit();
                    isOpsCoklama = false;
                }

            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }



        }

        public async Task<bool> Create(DersAcilanDto dersAcilanDto)
        {
            try
            {
                dersAcilanDto.Donem = null;
                dersAcilanDto.Akademisyen = null;
                dersAcilanDto.Program = null;
                dersAcilanDto.Bolum = null;

                ApiResponseDto<DersAcilanDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<DersAcilanDto>>
                    ("api/dersAcilan", dersAcilanDto);
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                    return true;
                }
                else
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
            }
        }

        public async Task<bool> Update(DersAcilanDto dersAcilanDto)
        {
            try
            {
                if (isOpsCoklama)
                {
                    dersAcilanDto.Id = 0;
                    return await Create(dersAcilanDto);
                }
                else
                {
                    dersAcilanDto.Program = null;
                    dersAcilanDto.Bolum = null;
                    dersAcilanDto.Fakulte = null;
                    dersAcilanDto.Akademisyen = null;
                    dersAcilanDto.Donem = null;
                    ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                        ("api/dersAcilan", dersAcilanDto);

                    if (apiResponse.IsSuccessStatusCode)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
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

        public async Task<bool> Delete(DersAcilanDto dersAcilanDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/dersAcilan/" + dersAcilanDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("Açılan Ders Deleted", MatToastType.Success);
                    return true;
                }
                else
                {
                    matToaster.Add("Açılan Ders Delete Failed: " + response.StatusCode, MatToastType.Danger);
                    return false;
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Açılan Ders Save Failed");
                return false;
            }
        }

        async Task Refresh()
        {
            //await Task.Delay(100);
            totalQuery = new Query().Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)", "Donem($select=Id,Ad)", "bolum($expand=fakulte($select=Ad,Id);$select=Ad,Id)" }); ;
            //totalQuery.Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)" });


            if (sadeceYillik==false)
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
            isGridVisible = true;
            StateHasChanged();
            await Task.Delay(100);
            DersAcilanGrid.Refresh();

            
        }

        public async Task CommandClickHandlerAkademisyen(Syncfusion.Blazor.Grids.CommandClickEventArgs<AkademisyenDto> args)
        {
            if (args.CommandColumn.Title == "Akademisyen Ekle")
            {
                ApiResponseDto apiResponse = await Http.GetFromJsonAsync<ApiResponseDto>($"api/dersacilan/UpdateDersAcilanAkademsiyen/{AkademisyeninAtanacagiDersAcId}/{args.RowData.Id}");
                akademisyenDialogOpen = false;

                if (apiResponse.IsSuccessStatusCode)
                {
                    DersAcilanGrid.Refresh();
                    matToaster.Add(args.RowData.Ad + " " + args.RowData.Soyad, MatToastType.Success, "Akademisyen kaydı güncellendi");
                }
                else
                {
                    matToaster.Add(args.RowData.Ad + " " + args.RowData.Soyad + " : " + apiResponse.StatusCode, MatToastType.Danger, "Akademisyen kayıt edilemedi");
                }
            }
        }

        int AkademisyeninAtanacagiDersAcId;
        public async Task AkademisyenAta(int dersAcilanId)
        {
            akademisyenDialogOpen = true;
            AkademisyeninAtanacagiDersAcId = dersAcilanId;
        }

        public async Task CommandClickHandler(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersAcilanDto> args)
        {
            if (args.CommandColumn.Title == "Çokla")
            {
                isOpsCoklama = true;
                //Id sıfırla.
                //    paramtereye coklama oldugun ata
                //    insert çak. 
            }
            if (args.CommandColumn.Title == "Akademisyen")
            {
                AkademisyenAta(args.RowData.Id);
            }

        }

        public void ValueChange(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool> args)
        {
            DersAcilanGrid.FilterByColumn("Zorunlu", "equal", args.Checked);
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Kayıtlı Öğrenciler")
            {
                if (selectedRecord != null)
                {
                    isKayitliShow = true;
                }
                else
                {
                    dialogUyariText = "Önce bir öğrenci seçiniz.";
                    isUyariOpen = true;
                }
            }
        }

        public async Task OnRowSelecting(RowSelectingEventArgs<DersAcilanDto> args)
        {
            selectedRecord = args.Data;
        }
        //public void CheckTemiz()
        //{
        //    DersAcilanGrid.ClearFiltering("Zorunlu");
        //}

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
        private void OnChangeOrgSayi(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool> args)
        {

        }
        
    }
}
