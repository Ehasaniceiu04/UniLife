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

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class AcilanDersler : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        int? programId;
        int? bolumId;
        int? fakulteId;
        int? donemId;

        int? tempProgramId;
        int? tempBolumId;
        int? tempFakulteId;

        string OdataQuery = "odata/dersacilans";
        public Query totalQuery = new Query().Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)", "Donem($select=Id,Ad)" });
        bool isGridVisible = true;

        bool akademisyenDialogOpen;
        SfGrid<AkademisyenDto> AkademisyenGrid;

        bool programDialogOpen;
        bool isProgramSecildi;


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

        public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).Take(6).RequiresCount();
        SfDropDownList<int?, DonemDto> DropDonem;


        public bool isOpsCoklama { get; set; }
        public void Change(@Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            if (args.Value == null)
            {
                DersAcilanGrid.ClearFiltering();
            }
            else
            {
                //startswith
                DersAcilanGrid.FilterByColumn("ProgramId", "equal", 1);
            }
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
                dersAcilanDto.Program= null;

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
            totalQuery = new Query();
            totalQuery.Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)" });

            if (donemId.HasValue)
            {
                totalQuery.Where("donemId", "equal", donemId);
            }

            if (programId.HasValue)
            {
                totalQuery.Where("programId", "equal", programId);
            }
            else if (bolumId.HasValue)
            {
                totalQuery.Where("bolumId", "equal", bolumId);
            }
            else if (fakulteId.HasValue)
            {
                totalQuery.Where("fakulteId", "equal", fakulteId);
            }


            isGridVisible = true;


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

        }

    }
}
