using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.Dto.Definitions;
using Syncfusion.Blazor.Data;


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

        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> mezunGrid;

        string OdataQuery = "odata/ogrencis/OgrMezuniyet";
        public Query totalQuery;


        async Task Refresh()
        {
            totalQuery = new Query();

            if (AGNOKontrol)
            {
                totalQuery.AddParams("agno", AGNOKontrol);
            }
            if (KrediKontrol)
            {
                totalQuery.AddParams("kredi", KrediKontrol);
            }
            if (AKTSKontrol)
            {
                totalQuery.AddParams("akts", AKTSKontrol);
            }
            if (StajKontrol)
            {
                totalQuery.AddParams("staj", StajKontrol);
            }
            if (ZorunluDersKontrol)
            {
                totalQuery.AddParams("zders", ZorunluDersKontrol);
            }
            if (SecemeliDersKontrol)
            {
                totalQuery.AddParams("sders", SecemeliDersKontrol);
            }
            if (BasarisizDersKontrol)
            {
                totalQuery.AddParams("bders", BasarisizDersKontrol);
            }
            if (HazirlikKontrol)
            {
                totalQuery.AddParams("hazirlik", HazirlikKontrol);
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

            //isGridVisible = true;
            StateHasChanged();
            await Task.Delay(100);
            mezunGrid.Refresh();
        }

    }
}
