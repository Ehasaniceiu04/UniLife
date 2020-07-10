using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.DataModels;
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


        SfGrid<Ogrenci> OgrencilerGrid;

        string OdataQuery = "odata/Ogrencis";

        public Query totalQuery = new Query();//.AddParams("$expand", "program($select=Id,Ad),KayitNeden($select=Id,Ad),OgrenimDurum($select=Id,Ad)");

        private void OnChange(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool> args)
        {
            isGridVisible = false;
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Excel Export")
            {
                await this.OgrencilerGrid.ExcelExport();

                ////Export current page
                //ExcelExportProperties ExportProperties = new ExcelExportProperties();
                //ExportProperties.ExportType = ExportType.CurrentPage;
                //await this.OgrencilerGrid.ExcelExport(ExportProperties);
            }
            if (args.Item.Id == "OgrencilerGrid_pdfexport")
            {
                await this.OgrencilerGrid.PdfExport();
            }
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
                totalQuery.Expand(new List<string> { "KayitNeden($select=Id,Ad)" });
                okKayitNeden = true;
            }
            if (okOgrDurum)
            {
                totalQuery.Expand(new List<string> { "OgrenimDurum($select=Id,Ad)" });
                okOgrDurum = true;
            }

            isGridVisible = true;


        }
    }
}
