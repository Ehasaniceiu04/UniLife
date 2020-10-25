using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Syncfusion.Blazor.DropDowns;
using UniLife.Shared.Dto.Definitions;
using Syncfusion.Blazor.Data;
using System.Collections.Generic;
using Syncfusion.Blazor.Grids;
using UniLife.Shared.Dto;
using UniLife.CommonUI.Extensions;
using System.Linq;
using System;
using MatBlazor;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class SinavList : ComponentBase
    {
        [InjectAttribute]
        public System.Net.Http.HttpClient Http { get; set; }
        [InjectAttribute]
        public MatBlazor.IMatToaster matToaster { get; set; }

        int? _filterFakulteId;
        public int? FilterFakulteId
        {
            get => _filterFakulteId;
            set
            {
                if (_filterFakulteId == value) 
                {
                    KaynakChange();
                    //Refresh();
                    return;
                }
                else
                {
                    _filterFakulteId = value;
                    KaynakChange();
                    //Refresh();
                }
                
            }
        }

        int? _filterBolumId;
        public int? FilterBolumId
        {
            get => _filterBolumId;
            set
            {
                if (_filterBolumId == value)
                {
                    KaynakChange();
                    return;
                }
                else
                {
                    _filterBolumId = value;
                    KaynakChange();
                }

            }
        }
        int? _filterProgramId;
        public int? FilterProgramId
        {
            get => _filterProgramId;
            set
            {
                if (_filterProgramId == value)
                {
                    KaynakChange();
                    return;
                }
                else
                {
                    _filterProgramId = value;
                    KaynakChange();
                }

            }
        }
        int? _sinif;
        public int? Sinif
        {
            get => _sinif;
            set
            {
                if (_sinif == value)
                {
                    return;
                }
                else
                {
                    _sinif = value;
                    KaynakChange();
                }

            }
        }
        int? _donemId;
        public int? DonemId
        {
            get => _donemId;
            set
            {
                if (_donemId == value)
                {
                    return;
                }
                else
                {
                    _donemId = value;
                    KaynakChange();
                }

            }
        }

        SfDropDownList<int?, SinifDto> DropSinif;
        SfDropDownList<int?, KeyValueDto> DropDonem;

        public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();

        public Query totalQuery = new Query().Expand(new List<string> { "Dersacilan($expand=Program($select=Ad,Id);$select=Ad,Id,Kod)", "SinavTip($select=Ad,Id)" });
        string OdataQuery = "odata/sinavs";

        Syncfusion.Blazor.Grids.SfGrid<SinavDto> sinavGrid;

        bool kaynakVisible = true;

        bool sadeceYillik;
        string yillikStyle;


        private List<Object> Toolbaritems = new List<Object>() { "Edit", "Delete", "ExcelExport", "CsvExport", "PdfExport" };

        bool isUyariOpen;
        string dialogUyariText;

        protected async override Task OnInitializedAsync()
        {
            DonemId = (await appState.GetDonemState()).Id;
        }

        async Task KaynakChange()
        {
            
            kaynakVisible = false;
            Refresh();
            await Task.Delay(100);
            kaynakVisible = true;
            StateHasChanged();
        }

        async Task Refresh()
        {
            totalQuery = new Query().Expand(new List<string> { "Dersacilan($expand=Program($select=Ad,Id);$select=Ad,Id,Kod)", "SinavTip($select=Ad,Id)" });

            if (sadeceYillik == false)
            {
                totalQuery.Where("Dersacilan/donemId", "equal", DonemId);
            }
            else
            {
                totalQuery.Where("Dersacilan/IsYillik", "equal", sadeceYillik);
            }

            if (Sinif.HasValue)
            {
                totalQuery.Where("Dersacilan/sinif", "equal", Sinif);
            }

            if (FilterProgramId.HasValue)
            {
                totalQuery.Where("Dersacilan/programId", "equal", FilterProgramId);
            }
            else if (FilterBolumId.HasValue)
            {
                totalQuery.Where("Dersacilan/bolumId", "equal", FilterBolumId);
            }
            else if (FilterFakulteId.HasValue)
            {
                totalQuery.Where("Dersacilan/fakulteId", "equal", FilterFakulteId);
            }

            //sinavGrid.Refresh();
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id.Contains("delete"))
            {
                isUyariOpen = true;
                dialogUyariText = "Seçili sınavlar silinecek, emin misiniz?";
            }
        }

        async Task RemoveSinavs()
        {
            try
            {
                var selectedSinavs = await sinavGrid.GetSelectedRecords();
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>
                        ("api/sinav/BulkDelete", new IntEnumarableDto { Ids = selectedSinavs.Select(x => x.Id) });
                if (apiResponse.IsSuccessStatusCode)
                {
                    sinavGrid.Refresh();
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        private async Task OnChangeSadeceYillik(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool> args)
        {
            if (args.Checked)
            {
                yillikStyle = "color:red";
            }
            else
            {
                yillikStyle = "";
            }
            await Refresh();
        }

        //public async Task ActionCompletedHandler(ActionEventArgs<SinavDto> args)
        //{
        //    if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
        //    {
        //    }

        //}
    }
}
