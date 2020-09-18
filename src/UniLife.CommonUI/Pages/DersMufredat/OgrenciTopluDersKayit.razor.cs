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
using UniLife.CommonUI.Pages.Definitions.Tabs;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class OgrenciTopluDersKayit : ComponentBase
    {
        [InjectAttribute]
        public System.Net.Http.HttpClient Http { get; set; }
        [InjectAttribute]
        public MatBlazor.IMatToaster matToaster { get; set; }

        bool hedefVisible=true;
        bool kaynakVisible= false;
        
        string OdataQuery = "odata/ogrencis";
        public Query totalQuery = new Query();

        
        string OdataQueryDers = "odata/dersacilans";
        public Query totalQueryDers = new Query();

        public SfGrid<OgrenciDto> OgrGrid;

        public SfGrid<DersAcilanDto> dersAcGrid;

        List<DersAcilanDto> dersAcilanDtos = new List<DersAcilanDto>();

        int? _programId;
        public int? ProgramId
        {
            get => _programId;
            set
            {
                ProgramId2 = value;
                KaynakChange();
                if (_programId == value) return;
                _programId = value;
            }
        }

        int? _bolumId;
        public int? BolumId
        {
            get => _bolumId;
            set
            {
                BolumId2 = value;
                KaynakChange();
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
                FakulteId2 = value;
                KaynakChange();
                if (_fakulteId == value) return;
                _fakulteId = value;
            }
        }
        int? _mufredatId;
        public int? MufredatId
        {
            get => _mufredatId;
            set
            {
                MufredatId2 = value;
                KaynakChange();
                if (_mufredatId == value) return;
                _mufredatId = value;
            }
        }
        public DateTime? StartValue { get; set; }
        public DateTime? EndValue { get; set; }

        int? _programId2;
        public int? ProgramId2
        {
            get => _programId2;
            set
            {
                //HedefChange();
                if (_programId2 == value) return;
                _programId2 = value;
            }
        }

        int? _bolumId2;
        public int? BolumId2
        {
            get => _bolumId2;
            set
            {
                //HedefChange();
                if (_bolumId2 == value) return;
                _bolumId2 = value;
            }
        }
        int? _fakulteId2;
        public int? FakulteId2
        {
            get => _fakulteId2;
            set
            {
                //HedefChange();
                if (_fakulteId2 == value) return;
                _fakulteId2 = value;
            }
        }
        int? _mufredatId2;
        public int? MufredatId2
        {
            get => _mufredatId2;
            set
            {
                HedefChange();
                if (_mufredatId2 == value) return;
                _mufredatId2 = value;
            }
        }

        int? donemId;
        int? kayitNeden;

        string dialogUyariText;
        bool isUyariOpen;

        bool ogrenciDersKayitDialogOpen;

        protected override async Task OnInitializedAsync()
        {
            donemId = (await appState.GetDonemState()).Id;
        }

        async Task Kayit()
        {
            try
            {
                HedefKaynakDto hedefKaynakDto = new HedefKaynakDto
                {
                    HedefIdList = (await dersAcGrid.GetSelectedRecords()).Select(x => x.Id).ToList(),
                    KaynakIdList = (await OgrGrid.GetSelectedRecords()).Select(x => x.Id).ToList()
                };
                if (hedefKaynakDto.HedefIdList.Count<1 || hedefKaynakDto.KaynakIdList.Count<1)
                {
                    dialogUyariText = "Öğrenci ve ders seçimlerini yaptığınızdan emin olunuz.";
                    isUyariOpen = true;
                }

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/derskayit/HedefKaynakOgrDersKayit", hedefKaynakDto);

                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    //await HedefChange();
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        async Task Refresh()
        {
            totalQuery = new Query();

            if (StartValue.HasValue && EndValue.HasValue)
            {
                var ColPre = new WhereFilter();
                List<WhereFilter> Predicate = new List<WhereFilter>();
                Predicate.Add(new WhereFilter()
                {
                    Field = "KayitTarih",
                    value = StartValue,
                    Operator = "greaterthanorequal",
                    IgnoreCase = true
                });
                Predicate.Add(new WhereFilter()
                {
                    Field = "KayitTarih",
                    value = EndValue,
                    Operator = "lessthanorequal",
                    IgnoreCase = true
                });
                ColPre = WhereFilter.And(Predicate);
                totalQuery = new Query().Where(ColPre);
            }

            if (kayitNeden.HasValue)
            {
                totalQuery.Where("kayitNedenId", "equal", kayitNeden);
            }

            if (MufredatId.HasValue)
            {
                totalQuery.Where("mufredatId", "equal", MufredatId);
            }
            else if (ProgramId.HasValue)
            {
                totalQuery.Where("programId", "equal", ProgramId);
            }
            else if (BolumId.HasValue)
            {
                totalQuery.Where("bolumId", "equal", BolumId);
            }
            else if (FakulteId.HasValue)
            {
                totalQuery.Where("fakulteId", "equal", FakulteId);
            }
        }
        async Task RefreshHedef()
        {
            ApiResponseDto<List<DersAcilanDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/DersAcilan/GetDersAcilansByMufredat/{MufredatId2}/{donemId}").Result;

            dersAcilanDtos = apiResponse.Result;
            dersAcGrid.Refresh();
            //totalQueryDers = new Query();

            //if (donemId.HasValue)
            //{
            //    totalQueryDers.Where("donemId", "equal", donemId);
            //}

            //if (MufredatId2.HasValue)
            //{
            //    totalQueryDers.Where("mufredatId", "equal", MufredatId2);
            //}
            //else if (ProgramId2.HasValue)
            //{
            //    totalQueryDers.Where("programId", "equal", ProgramId2);
            //}
            //else if (BolumId2.HasValue)
            //{
            //    totalQueryDers.Where("bolumId", "equal", BolumId2);
            //}
            //else if (FakulteId2.HasValue)
            //{
            //    totalQueryDers.Where("fakulteId", "equal", FakulteId2);
            //}
        }


        OgrenciDto secilenOgr;
        public async Task CommandClickedKDers(Syncfusion.Blazor.Grids.CommandClickEventArgs<OgrenciDto> args)
        {
            if (args.CommandColumn.Title == "Kayıtlı Dersleri")
            {
                secilenOgr = args.RowData;
                ogrenciDersKayitDialogOpen = true;
            }
        }

        async Task KaynakChange()
        {
            kaynakVisible = false;
            await Task.Delay(100);
            kaynakVisible = true;
            await Refresh();
            StateHasChanged();
        }

        async Task HedefChange()
        {
            //hedefVisible = false;
            //await Task.Delay(100);
            //hedefVisible = true;

            await RefreshHedef();
            StateHasChanged();
        }

    }
}
