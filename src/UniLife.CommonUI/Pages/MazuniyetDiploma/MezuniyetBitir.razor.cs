using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.MazuniyetDiploma
{
    public partial class MezuniyetBitir : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        int? _filterFakulteId;
        public int? FilterFakulteId
        {
            get => _filterFakulteId;
            set
            {
                if (_filterFakulteId == value) return;
                else
                {
                    _filterFakulteId = value;
                    Refresh();
                }
            }
        }
        int? _filterBolumId;
        public int? FilterBolumId
        {
            get => _filterBolumId;
            set
            {
                if (_filterBolumId == value) return;
                else
                {
                    _filterBolumId = value;
                    Refresh();
                }
            }
        }
        int? _filterProgramId;
        public int? FilterProgramId
        {
            get => _filterProgramId;
            set
            {
                if (_filterProgramId == value) return;
                else
                {
                    _filterProgramId = value;
                    Refresh();
                }
            }
        }


        bool isDialogUyariOpen;
        string dialogText;

        string OdataQuery = "odata/ogrencis";
        public Query totalQuery;
        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> mezunBitirGrid;

        SfDropDownList<int?, KeyValueDto> DropDiplomaTip;
        int? DiplomaTip;

        DateTime? MezuniyetTarih;


        public void CommandClickHandler(CommandClickEventArgs<OgrenciDto> args)
        {
            if (args.CommandColumn.Title == "Bitir")
            {

            }
        }



        async Task Refresh()
        {
            totalQuery = new Query();


            totalQuery.Expand(new List<string> { "KayitNeden($select=Id,Ad)", "Danisman($select=Id,Ad)", "Program($select=Id,Ad)" });

            if (FilterProgramId.HasValue)
            {
                totalQuery.Where("programId", "equal", FilterProgramId);
            }
            else if (FilterBolumId.HasValue)
            {
                totalQuery.Where("bolumId", "equal", FilterBolumId);
            }
            else if (FilterFakulteId.HasValue)
            {
                totalQuery.Where("fakulteId", "equal", FilterFakulteId);
            }

            //totalQuery.Where("MezunOnay", "greaterthan", 2);
            totalQuery.Where("MezunOnay", "lessthan", 2);


            //isGridVisible = true;
            StateHasChanged();
            await Task.Delay(100);
            mezunBitirGrid.Refresh();
        }

        async Task MezunBas()
        {
            if (!DiplomaTip.HasValue)
            {
                dialogText = "Diploma tipi seçmelisiniz!";
                isDialogUyariOpen = true;
                return;
            }
            if (!MezuniyetTarih.HasValue)
            {
                dialogText = "Mezuniyet tarihi seçmelisiniz!";
                isDialogUyariOpen = true;
                return;
            }

            //seçililerin diploması basılacak.
        }

    }
}
