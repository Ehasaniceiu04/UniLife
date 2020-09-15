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
        int? _filterMufredatId;
        public int? FilterMufredatId
        {
            get => _filterMufredatId;
            set
            {
                if (_filterMufredatId == value)
                {
                    KaynakChange();
                    return;
                }
                else
                {
                    _filterMufredatId = value;
                    KaynakChange();
                }

            }
        }

        int? sinif;
        int? donemId;

        SfDropDownList<int?, SinifDto> DropSinif;
        SfDropDownList<int?, KeyValueDto> DropDonem;

        public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();

        public Query totalQuery = new Query().Expand(new List<string> { "Dersacilan($expand=Program($select=Ad,Id);$select=Ad,Id,Kod)" });
        string OdataQuery = "odata/sinavs";

        Syncfusion.Blazor.Grids.SfGrid<SinavDto> sinavGrid;

        bool kaynakVisible = true;
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
            totalQuery = new Query().Expand(new List<string> { "Dersacilan($expand=Program($select=Ad,Id);$select=Ad,Id,Kod)" });
            
            if (donemId.HasValue)
            {
                totalQuery.Where("Dersacilan/donemId", "equal", donemId);
            }
            if (sinif.HasValue)
            {
                totalQuery.Where("Dersacilan/sinif", "equal", sinif);
            }

            if (FilterMufredatId.HasValue)
            {
                totalQuery.Where("Dersacilan/mufredatId", "equal", FilterMufredatId);
            }
            else if (FilterProgramId.HasValue)
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
        }

        //public async Task ActionCompletedHandler(ActionEventArgs<SinavDto> args)
        //{
        //    if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
        //    {
        //    }

        //}
    }
}
