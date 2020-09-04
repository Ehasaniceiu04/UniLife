using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class OgrenciDersAktarim : ComponentBase
    {
        [InjectAttribute]
        public System.Net.Http.HttpClient Http { get; set; }
        [InjectAttribute]
        public MatBlazor.IMatToaster matToaster { get; set; }

        public DersAcilanDto _dersAcilanDto { get; set; } = new DersAcilanDto();

        SfDropDownList<int?, KeyValueDto> DropDers2;
        SfDropDownList<int?, KeyValueDto> DropDers;

        SfDropDownList<int?, KeyValueDto> DropDonem2;
        SfDropDownList<int?, KeyValueDto> DropDonem;
        SfDropDownList<int?, KeyValueDto> DropFakulte;
        SfDropDownList<int?, KeyValueDto> DropBolum;
        SfDropDownList<int?, KeyValueDto> DropProgram;

        public Query donemQuery = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();

        string OdataDersKaynakQuery;//= $"odata/dersacilans?$filter=programId eq {ProgramId}";
        //public Query dersQuery = new Query()
        //    .Expand(new List<string> { "program($select=Id,Ad)", "Akademisyen($select=Id,Ad)", "Donem($select=Id,Ad)", "bolum($expand=fakulte($select=Ad,Id);$select=Ad,Id)" })
        //    .Select(new List<string> { "Id", "Ad" }).RequiresCount();
        public Query ders2Query = new Query().Select(new List<string> { "Id", "Ad" }).RequiresCount();

        int? _programId;
        public int? ProgramId
        {
            get => _programId;
            set
            {
                ProgramId2 = value;
                if (_programId == value) return;
                _programId = value;
            }
        }
        //public int? ProgramId
        //{
        //    get => _programId;
        //    set
        //    {
        //        if (_programId.HasValue)
        //        {
        //            OdataDersKaynakQuery = $"odata/dersacilans?$filter=programId eq {_programId}";
        //            DropDers.Refresh();
        //        }
        //        else
        //        {
        //            OdataDersKaynakQuery = null;
        //            //DropDers.DataSource = null;
        //            DropDers.Clear();
        //            DropDers.Refresh();
        //        }
        //        //StateHasChanged();
        //        base.InvokeAsync(StateHasChanged);
        //        //DropDers.Refresh();
        //        if (_programId == value) return;
        //        _programId = value;
        //    }
        //}

        int? _bolumId;
        public int? BolumId
        {
            get => _bolumId;
            set
            {
                bolumId2 = value;
                if (_bolumId == value) return;
                _bolumId = value;
            }
        }

        int? _fakulteId;
        public int? FakulteId
        {
            get => _fakulteId;
            set
            {
                fakulteId2 = value;
                if (_fakulteId == value) return;
                _fakulteId = value;
            }
        }
        int? _donemId;
        public int? DonemId
        {
            get => _donemId;
            set
            {
                donemId2 = value;
                if (_donemId == value) return;
                _donemId = value;
            }
        }
        int? dersId;

        int? _sinif;
        public int? Sinif
        {
            get => _sinif;
            set
            {
                sinif2 = value;
                if (_sinif == value) return;
                _sinif = value;
            }
        }

        int? _programId2;
        public int? ProgramId2
        {
            get => _programId2;
            set
            {
                ProgramId2ChangedHandler(value);
                if (_programId2 == value) return;
                _programId2 = value;
            }
        }

        int? bolumId2;
        int? fakulteId2;
        int? sinif2;
        int? donemId2;
        int? dersId2;

        bool aktarimConfirmDialogOpen;
        string aktarimConfirmDialogTitle="Öğrenci Ders Aktarımı";

        //List<SinifDto> sinifDtos = SinifDto.SinifDtos;
        SfDropDownList<int?, SinifDto> DropSinif;
        SfDropDownList<int?, SinifDto> DropSinif2;

        SfGrid<DersKayitDto> ogrGrid;
        SfGrid<DersKayitDto> ogrHedefGrid;
        
        bool kaynakVisible=true;
        bool hedefVisible = true;
        protected async override Task OnInitializedAsync()
        {

        }
        

        async Task AktarimYap()
        {

        }

        async Task KaynakChange()
        {
            kaynakVisible = false;
            await Task.Delay(100);
            kaynakVisible = true;
        }

        async Task HedefChange()
        {
            hedefVisible = false;
            await Task.Delay(100);
            hedefVisible = true;
        }

        async Task ProgramIdChangedHandler(int? programId)
        {
            if (programId.HasValue)
            {
                kaynakVisible = false;
                ProgramId = null;
                dersId = null;
                await Task.Delay(100);
                ProgramId = programId;
                kaynakVisible = true;
            }
            else
            {
                ProgramId = null;
                dersId = null;
                kaynakVisible = false;
                await Task.Delay(100);
                kaynakVisible = true;
            }
        }
        async Task ProgramId2ChangedHandler(int? programId2)
        {
            if (programId2.HasValue)
            {
                hedefVisible = false;
                //_programId2 = null;
                dersId2 = null;
                await Task.Delay(100);
                //_programId2 = programId2;
                hedefVisible = true;
            }
            else
            {
                //_programId2 = null;
                dersId2 = null;
                hedefVisible = false;
                await Task.Delay(100);
                hedefVisible = true;
            }
        }

        async Task Tasi()
        {
            //var kaynakOgrenciIDs = (await ogrGrid.GetSelectedRecords()).Select(x=>x.OgrenciId);
            //var hedefOgrenciIDs = (await ogrHedefGrid.GetCurrentViewRecords()).Select(x => x.OgrenciId);

            try
            {
                HedefKaynakDto hedefKaynakDto = new HedefKaynakDto
                {
                    HedefId = (int)dersId2,
                    KaynakId = (int)dersId,
                    HedefIdList = (await ogrHedefGrid.GetCurrentViewRecords()).Select(x => x.OgrenciId).ToList(),
                    KaynakIdList = (await ogrGrid.GetSelectedRecords()).Select(x => x.OgrenciId).ToList()
                };

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/derskayit/HedefKaynakOgrAktar", hedefKaynakDto);

                if (apiResponse.IsSuccessStatusCode)
                {
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
}
