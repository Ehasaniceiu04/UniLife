using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using UniLife.Shared.Dto.Definitions;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;

namespace UniLife.CommonUI.Pages.Admin.Raporlar
{
    public partial class OgrenciRapor : ComponentBase
    {

        int? programId;
        int? bolumId;
        int? fakulteId;

        private bool okKayitNeden = false;

        SfGrid<OgrenciDto> OgrencilerGrid;

        string OdataQuery = "odata/Ogrencis";

        public Query totalQuery = new Query();//.AddParams("$expand", "program($select=Id,Ad),KayitNeden($select=Id,Ad),OgrenimDurum($select=Id,Ad)");

        

        async Task Refresh()
        {
            string filterQueryString = "";
            string expandQueryString = "program($select=Id,Ad),";

            if (programId.HasValue)
            {
                filterQueryString += $"programId eq {programId} and ";
            }
            else if (bolumId.HasValue)
            {
                filterQueryString += $"bolumId eq {bolumId} and ";
            }
            else if (fakulteId.HasValue)
            {
                filterQueryString = $"fakulteId eq {fakulteId} and ";
            }
            if (okKayitNeden)
            {
                expandQueryString += "KayitNeden($select=Id,Ad),";
            }

            expandQueryString = expandQueryString.TrimEnd(',');
            filterQueryString = filterQueryString.TrimEnd('a', 'n', 'd', ' ');

            if (!string.IsNullOrWhiteSpace(filterQueryString))
            {
                totalQuery.AddParams("$filter", filterQueryString);
            }
            if (!string.IsNullOrWhiteSpace(expandQueryString))
            {
                totalQuery.AddParams("$expand", expandQueryString);
            }


        }
    }
}
