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
        private bool okOgrDurum = false;

        private bool isGridVisible = false; 


        SfGrid<OgrenciDto> OgrencilerGrid;

        string OdataQuery = "odata/Ogrencis";

        public Query totalQuery = new Query();//.AddParams("$expand", "program($select=Id,Ad),KayitNeden($select=Id,Ad),OgrenimDurum($select=Id,Ad)");

        private void OnChange(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool> args)
        {
            isGridVisible = false;
        }


        async Task Refresh()
        {
            totalQuery = new Query();
            totalQuery.Expand(new List<string> { "Program($select=Id,Ad)" });

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
            if (okKayitNeden)
            {
                //expandQueryString += "KayitNeden($select=Id,Ad),";
                totalQuery.Expand(new List<string> { "KayitNeden($select=Id,Ad)" });
                okKayitNeden = true;
            }
            if (okOgrDurum)
            {
                //expandQueryString += "KayitNeden($select=Id,Ad),";
                totalQuery.Expand(new List<string> { "OgrenimDurum($select=Id,Ad)" });
                okOgrDurum = true;
            }

            isGridVisible = true;
            //expandQueryString = expandQueryString.TrimEnd(',');

            //if (!string.IsNullOrWhiteSpace(expandQueryString))
            //{
            //    totalQuery.AddParams("$expand", expandQueryString);
            //}


        }
    }
}
