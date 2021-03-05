﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.Dto.Definitions;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.DropDowns;
using UniLife.Shared.Dto;
using MatBlazor;
using UniLife.CommonUI.Extensions;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace UniLife.CommonUI.Pages.MazuniyetDiploma
{
    public partial class Mezuniyet : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        int? ProgramId;
        int? BolumId;
        int? FakulteId;

        bool AGNOKontrol;
        bool KrediKontrol;
        bool AKTSKontrol;
        bool StajKontrol;
        bool ZorunluDersKontrol;
        bool SecemeliDersKontrol;
        bool BasarisizDersKontrol;
        bool HazirlikKontrol;

        bool isUyariOpen;
        bool isDialogUyariOpen;
        int OgrId;
        string dialogText;

        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> mezunGrid;

        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> mezunOnayGrid;

        List<OgrenciDto> ogrenciMezunDtos = new List<OgrenciDto>();
        

        string OdataQuery = "odata/ogrencis/OgrMezuniyet";
        public Query totalQuery;

        SfDropDownList<int?, DonemDto> DropDonem;
        int? donemId;
        public DateTime? MezuniyetTarih { get; set; }

        public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();

        private List<Object> Toolbaritems = new List<Object>() { new ItemModel() { Text = "Onaya Gönder", TooltipText = "Onaya Gönder", PrefixIcon = "e-click", Id = "OnayaGonder" } };

        async Task Refresh()
        {
            totalQuery = new Query();

                totalQuery.AddParams("agno", AGNOKontrol);
                totalQuery.AddParams("kredi", KrediKontrol);
                totalQuery.AddParams("akts", AKTSKontrol);
                totalQuery.AddParams("staj", StajKontrol);
                totalQuery.AddParams("zders", ZorunluDersKontrol);
                totalQuery.AddParams("sders", SecemeliDersKontrol);
                totalQuery.AddParams("bders", BasarisizDersKontrol);
                totalQuery.AddParams("hazirlik", HazirlikKontrol);

            totalQuery.Expand(new List<string> { "KayitNeden($select=Id,Ad)", "Danisman($select=Id,Ad)", "Program($select=Id,Ad)" });

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

            //isGridVisible = true;
            StateHasChanged();
            await Task.Delay(100);
            mezunGrid.Refresh();

            if (donemId!=0 && MezuniyetTarih.HasValue)
            {
                await MezunOnayLoad();
            }
        }

        async Task MezunOnayLoad()
        {
            ApiResponseDto<List<OgrenciDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciDto>>>($"api/ogrenci/GetOgrenciOnay/{FakulteId}/{BolumId}/{ProgramId}/{donemId}");

            if (apiResponse.StatusCode == StatusCodes.Status200OK)
            {
                ogrenciMezunDtos = apiResponse.Result;
                //mezunOnayGrid.Refresh();
                //StateHasChanged();
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Bölüm getirilirken hata oluştu!");
            }
        }

        async Task AlDersler(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }
        async Task MufDurum(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }
        async Task Transkript(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;

        }
        async Task MezuniyetTranskript(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "OnayaGonder")
            {

                if (!MezuniyetTarih.HasValue)
                {
                    dialogText = "Mezuniyet tarihi seçmelisiniz!";
                    isDialogUyariOpen = true;
                    return;
                }
                if (donemId ==0)
                {
                    dialogText = "Liste dönemi seçmelisiniz!";
                    isDialogUyariOpen = true;
                    return;
                }

                var selectedOgr = await mezunGrid.GetSelectedRecords();
                
                foreach (var item in selectedOgr) 
                {
                    item.MezuniyetTarih = MezuniyetTarih;
                    item.DanismanOnay = false;
                }

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/Ogrenci/UpdateOgrenciOnayBekle", selectedOgr.Select(x => x.Id));
                if (apiResponse.IsSuccessStatusCode)
                {
                    ogrenciMezunDtos = selectedOgr;
                    mezunOnayGrid.Refresh();
                    matToaster.Add("Onaya gönderildi", MatToastType.Success, "İşlem başarılı.");

                }
                else
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                }

                

                

            }
        }

        async Task DonemChange()
        {
            if (donemId.HasValue)
            {
                MezunOnayLoad();
            }
        }
    }
}
