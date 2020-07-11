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
    public partial class OgrenciDersRapor : ComponentBase
    {

        int? programId;
        int? bolumId;
        int? fakulteId;

        private bool okTopKredi = false;
        private bool okBaska = false;
        private string stringChecked = "TopKredi";


        private bool isGridVisible = false;


        SfGrid<OgrenciDersRaporDto> OgrencilerGrid;

        string OdataQuery = "";

        public Query totalQuery = new Query();//.AddParams("$expand", "program($select=Id,Ad),KayitNeden($select=Id,Ad),OgrenimDurum($select=Id,Ad)");

        private void OnChange(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool> args)
        {
            isGridVisible = false;
        }



        private void OnChangeRadio(Syncfusion.Blazor.Buttons.ChangeArgs<string> args)
        {
            isGridVisible = false;
            stringChecked = args.Value;
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
            string OdataQueryParameters = "";
            totalQuery = new Query();

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
            if (stringChecked == "TopKredi")
            {
                //OdataQueryParameters += $"TopKredi=true,";
                totalQuery.AddParams("TopKredi", true);
            }
            else if(stringChecked == "TopAkts")
            {
                //OdataQueryParameters += $"TopAkts=true,";
                totalQuery.AddParams("TopAkts", true);

            }

            OdataQueryParameters = OdataQueryParameters.TrimEnd(',');
            //OdataQuery = $"odata/Ogrencis/GetOgrenciDers({OdataQueryParameters})";
            OdataQuery = $"odata/OgrenciDersRapors";



            isGridVisible = true;


        }
    }
}
