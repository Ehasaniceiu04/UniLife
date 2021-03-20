using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.MazuniyetDiploma
{
    public partial class Diploma: ComponentBase
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

        int? _kayitNedenId;
        public int? KayitNedenId
        {
            get => _kayitNedenId;
            set
            {
                if (_kayitNedenId == value) return;
                else
                {
                    _kayitNedenId = value;
                    Refresh();
                }
            }
        }

        int? _ogrenimDurumId;
        public int? OgrenimDurumId
        {
            get => _ogrenimDurumId;
            set
            {
                if (_ogrenimDurumId == value) return;
                else
                {
                    _ogrenimDurumId = value;
                    Refresh();
                }
            }
        }

        int? _ogrenimTurId;
        public int? OgrenimTurId
        {
            get => _ogrenimTurId;
            set
            {
                if (_ogrenimTurId == value) return;
                else
                {
                    _ogrenimTurId = value;
                    Refresh();
                }
            }
        }

        SfDropDownList<int?, KeyValueDto> DropKNe;
        SfDropDownList<int?, KeyValueDto> DropOgrDurum;
        SfDropDownList<int?, KeyValueDto> DropOgrTur;

        string OdataQuery = "odata/ogrencis";
        public Query totalQuery = new Query();

        public SfGrid<OgrenciDto> diplomaGrid;
        

        public Query AdIdQuery = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();


        async Task Refresh()
        {
            totalQuery = new Query();


            totalQuery.Expand(new List<string> { "Program($select=Id,Ad)" });

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


            if (OgrenimTurId.HasValue)
            {
                totalQuery.Where("ogrenimturId", "equal", OgrenimTurId);
            }
            if (OgrenimDurumId.HasValue)
            {
                totalQuery.Where("ogrenimDurumId", "equal", OgrenimDurumId);
            }
            if (KayitNedenId.HasValue)
            {
                totalQuery.Where("kayitNedenId", "equal", KayitNedenId);
            }

            //totalQuery.Where("MezunOnay", "greaterthan", 2);
            totalQuery.Where("MezunOnay", "lessthan", 2);


            StateHasChanged();
            await Task.Delay(100);
            diplomaGrid.Refresh();
        }

        async Task Yazdir()
        {

        }

    }
}
