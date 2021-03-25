using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
namespace UniLife.CommonUI.Pages.Akademisyen
{
    public partial class AkaDanismanlikMezunOlmus: ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }
        [Inject]
        public AppState appState { get; set; }

        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> mezunGrid;

        string OdataQuery = "odata/ogrencis";
        public Query totalQuery = new Query().Expand(new List<string> { "KayitNeden($select=Id,Ad)", "Danisman($select=Id,Ad)", "Program($select=Id,Ad)" });

        int akademisyenId;

        protected override async Task OnInitializedAsync()
        {
            akademisyenId = (await appState.GetAkademisyenState()).Id;

            totalQuery.Where("DanismanId", "equal", akademisyenId).Where("MezunOnay", "greaterthan", (int)MezunOnayDurum.DanismanOnayinda);

        }
    }
}
