using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.Extensions;
using UniLife.Shared;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class Sube : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        public DersAcilanDto _dersAcilanDto { get; set; } = new DersAcilanDto();



        SfDropDownList<int?, DonemDto> DropDonem;
        SfDropDownList<int?, KeyValueDto> DropFakulte;
        SfDropDownList<int?, KeyValueDto> DropBolum;
        SfDropDownList<int?, KeyValueDto> DropProgram;


        SfDropDownList<int, KeyValueDto> SubeType;


        List<DonemDto> donemDtos = new List<DonemDto>();
        List<KeyValueDto> fakulteDtos = new List<KeyValueDto>();
        List<KeyValueDto> bolumDtos = new List<KeyValueDto>();
        List<KeyValueDto> programDtos = new List<KeyValueDto>();




        Syncfusion.Blazor.Grids.SfGrid<DersAcilanDto> DersAcGrid;
        public List<DersAcilanDto> DersAcDtos = new List<DersAcilanDto>();


        Syncfusion.Blazor.Grids.SfGrid<DersAcilanDto> DersSubeGrid;
        public List<DersAcilanDto> DersSubeDtos = new List<DersAcilanDto>();



        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> OgrenciGrid;
        public List<OgrenciDto> OgrenciDtos = new List<OgrenciDto>();

        public List<DersAcilanDto> SelectedDersAcilans { get; set; } = new List<DersAcilanDto>();

        public int selectedSube { get; set; }

        bool subeDialogOpen;
        //List<OgrenciDto> SecmeliOgrenciDtos = new List<OgrenciDto>();
        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> SecmeliOgrenciGrid;

        public int subeSayisi { get; set; } = 1;
        public int subelendirmeTipi { get; set; } = 1;
        List<KeyValueDto> subeTypeDtos = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "Eşit Parçala", Id = 1 },
            new KeyValueDto() { Ad = "Tek Çift", Id = 2 },
            new KeyValueDto() { Ad = "Cinsiyet", Id = 3 }
        };

        bool isUyariOpen;
        string dialogUyariText = "";

        //protected override void OnInitialized()
        //{

        //}

        protected async override Task OnInitializedAsync()
        {
            await ReadFakultes();
            await ReadDonems();
        }

        async Task ReadFakultes()
        {
            OData<KeyValueDto> apiResponse = await Http.GetFromJsonAsync<OData<KeyValueDto>>($"odata/fakultes?$select=Id,Ad");

            if (apiResponse.Value.Count != 0)
            {
                fakulteDtos = apiResponse.Value;
                StateHasChanged();
            }
            else
            {
                matToaster.Add("", MatToastType.Danger, "fakulte getirilirken hata oluştu!");
            }
        }

        async Task ReadDonems()
        {
            ApiResponseDto<List<DonemDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DonemDto>>>("api/donem/current");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                donemDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Donem getirilirken hata oluştu!");
            }
        }


        private void FakulteToBolum(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            bolumDtos = new List<KeyValueDto>();
            programDtos = new List<KeyValueDto>();
            _dersAcilanDto.BolumId = null;
            _dersAcilanDto.ProgramId = null;
            if (_dersAcilanDto.Id == 0)
            {
                ReadBolums(args.Value).ConfigureAwait(true);
            }
            else
            {
                ReadBolums(_dersAcilanDto.FakulteId).ConfigureAwait(true);
            }
            StateHasChanged();
        }

        private void BolumToProgram(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            programDtos = new List<KeyValueDto>();
            _dersAcilanDto.ProgramId = null;
            if (_dersAcilanDto.Id == 0)
            {

                ReadPrograms(args.Value).ConfigureAwait(true);
            }
            else
            {
                ReadPrograms(_dersAcilanDto.BolumId).ConfigureAwait(true);
            }
            //StateHasChanged();
        }

        async Task ReadBolums(int? fakulteId)
        {
            OData<KeyValueDto> apiResponse;
            //apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<BolumDto>>>("api/bolum/GetBolumByFakulteIds/" + string.Join(',', fakulteId));
            apiResponse = await Http.GetFromJsonAsync<OData<KeyValueDto>>($"odata/bolums?$filter=FakulteId eq {fakulteId}&select=Id,Ad");


            if (apiResponse.Value.Count != 0)
            {
                bolumDtos = apiResponse.Value;
                StateHasChanged();
            }
            else
            {
                matToaster.Add("", MatToastType.Danger, "Bölüm getirilirken hata oluştu!");
            }
        }

        async Task ReadPrograms(int? bolumId)
        {
            OData<KeyValueDto> apiResponse;
            //apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<BolumDto>>>("api/bolum/GetBolumByFakulteIds/" + string.Join(',', fakulteId));
            apiResponse = await Http.GetFromJsonAsync<OData<KeyValueDto>>($"odata/programs?$filter=BolumId eq {bolumId}&select=Id,Ad");


            if (apiResponse.Value != null)
            {
                programDtos = apiResponse.Value;
                StateHasChanged();
            }
            else
            {
                matToaster.Add("", MatToastType.Danger, "Bölüm getirilirken hata oluştu!");
            }
        }

        async Task Refresh()
        {

            //OData<DersAcilanDto> apiResponse;
            //if (_dersAcilanDto.Sinif == 0 || _dersAcilanDto.Sinif == null)
            //{
            //    apiResponse = await Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersacilans?$filter=DonemId eq {_dersAcilanDto.DonemId} and ProgramId eq {_dersAcilanDto.ProgramId} and ProgramId eq {_dersAcilanDto.ProgramId} &$expand=Akademisyen($select=Id,Ad)");
            //}
            //else
            //{
            //    apiResponse = await Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersacilans?$filter=DonemId eq {_dersAcilanDto.DonemId} and ProgramId eq {_dersAcilanDto.ProgramId} and ProgramId eq {_dersAcilanDto.ProgramId} and Sinif eq {_dersAcilanDto.Sinif} &$expand=Akademisyen($select=Id,Ad)");
            //}

            //DersAcDtos = apiResponse.Value;
            //StateHasChanged();



            //if (apiResponse.Value != null)
            //{

            //}
            //else
            //{
            //    matToaster.Add("", MatToastType.Danger, "Bölüm getirilirken hata oluştu!");
            //}

            await GetDersAcilansByFilters();
        }


        private async Task GetDersAcilansByFilters()
        {
            SinavDersAcDto sinavDersAcDto = new SinavDersAcDto
            {
                donemId = _dersAcilanDto.DonemId,
                programId = _dersAcilanDto.ProgramId,
                dersKodu = _dersAcilanDto.Kod
            };

            //var reqURL = $"api/DersAcilan/GetDersAcilansByFilters/{_dersAcilanDto.DonemId ?? 0}/{_dersAcilanDto.ProgramId ?? 0}/{_dersAcilanDto.Sinif ?? 0}/{_dersAcilanDto.Kod ?? ""}";



            ApiResponseDto<List<DersAcilanDto>> apiResponse = await Http.PostJsonAsync<ApiResponseDto<List<DersAcilanDto>>>("api/DersAcilan/PostDersAcilansByFilters", sinavDersAcDto);
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                DersAcDtos = apiResponse.Result;
                DersAcGrid.Refresh();
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Açılan Derslerin bilgisi getirilirken hata oluştu");
            }

        }

        //public double clickedRowIndex { get; set; }
        //public void OnRecordClickHandler(Syncfusion.Blazor.Grids.RecordClickEventArgs<DersAcilanDto> args)
        //{
        //    clickedRowIndex = args.RowIndex;
        //}

        public async Task RowSelectedHandler(Syncfusion.Blazor.Grids.RowSelectEventArgs<DersAcilanDto> args)
        {
            //await DersAcGrid.ClearSelection();
            //DersAcGrid.SelectedRowIndex = args.RowIndex;

        }


        private void GetSinavsByDersAcilanId(DersAcilanDto dersAcilanDto)
        {
            ApiResponseDto<List<SinavDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<SinavDto>>>($"api/Sinav/GetSinavListByAcilanDersId/{dersAcilanDto.Id}").Result;
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {

            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav bilgisi getirilirken hata oluştu");
            }
        }


        //public void OnActionBeginHandlerOgrenci(Syncfusion.Blazor.Grids.ActionEventArgs<OgrenciDto> args)
        //{
        //    if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
        //    {
        //        args.Cancel = true;
        //        ogrenciSecDialogOpen = true;
        //    }
        //}

        public void ActionCompletedHandlerOgrenci(Syncfusion.Blazor.Grids.ActionEventArgs<OgrenciDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                DeleteSinavKayitByOgrenci(args.Data);
            }
        }

        public async Task DeleteSinavKayitByOgrenci(OgrenciDto ogrenciDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/SinavKayit/" + ogrenciDto.SinavKayitId);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("Öğrencinin Sınav kayıdı silinmiştir", MatToastType.Success);
                    OgrenciDtos.Remove(ogrenciDto);
                    OgrenciGrid.Refresh();
                }
                else
                {
                    matToaster.Add("Öğrencinin sınav kaydı silinirken hata oluştu: " + response.StatusCode, MatToastType.Danger);
                }
                //deleteDialogOpen = false;
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Bölüm Save Failed");
            }
        }


        public async Task CommandClickHandlerSecmeliOgr(Syncfusion.Blazor.Grids.CommandClickEventArgs<OgrenciDto> args)
        {
            //if (args.CommandColumn.Title == "Ogrenciyi Ekle")
            //{
            //    SubeKayitDto sinavKayitDto = new SinavKayitDto()
            //    {
            //        OgrenciId = args.RowData.Id,
            //        SinavId = selectedSube
            //    };
            //    ApiResponseDto<SinavKayitDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<SinavKayitDto>>("api/SinavKayit", sinavKayitDto);
            //    ogrenciSecDialogOpen = false;

            //    if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            //    {
            //        args.RowData.SinavKayitId = apiResponse.Result.Id;
            //        OgrenciDtos.Add(args.RowData);
            //        OgrenciGrid.Refresh();
            //        matToaster.Add(args.RowData.Ad + " " + args.RowData.Soyad, MatToastType.Success, "Öğrenci Kaydı gerçekleşti");
            //    }
            //    else
            //    {
            //        matToaster.Add(args.RowData.Ad + " " + args.RowData.Soyad + " : " + apiResponse.StatusCode, MatToastType.Danger, "Öğrenci sinava kayıt edilemedi");
            //    }
            //}
        }

        async Task DersiSubelendir()
        {
            OgrenciDtos = new List<OgrenciDto>();
            DersSubeDtos = new List<DersAcilanDto>();
            SubeliDersAcilan = new List<DersAcilanDto>();
            SubeliOgrenci = new List<OgrenciDto>();
            OgrenciGrid.Refresh();
            DersSubeGrid.Refresh();

            var selectedDersAcilan = (await DersAcGrid.GetSelectedRecords()).FirstOrDefault();

            if (subeSayisi <= 1 || (selectedDersAcilan == null))
            {
                //Diayolga uyarı : 1 den küçük ne bok yemeye...
                //yukarıdan ders seç
                isUyariOpen = true;
                dialogUyariText = "Şubelendirme sayisini 1 den büyük seçtiğinizden ve ders seçimi yaptığınızdan emin olunuz.";
            }
            else
            {
                switch (subelendirmeTipi)
                {
                    case 1:
                        //EşitBöl
                        await EsitBol(selectedDersAcilan);
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }

        }

        List<DersAcilanDto> SubeliDersAcilan = new List<DersAcilanDto>();
        List<OgrenciDto> SubeliOgrenci = new List<OgrenciDto>();

        private async Task EsitBol(DersAcilanDto dersAcilanDto)
        {
            ApiResponseDto<List<OgrenciDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciDto>>>($"api/Ogrenci/GetOgrenciListByDersAcId/{dersAcilanDto.Id}");
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {

                IEnumerable<IEnumerable<OgrenciDto>> devidedOgrencis = apiResponse.Result.Split<OgrenciDto>(subeSayisi);


                int sube = 1;
                foreach (var ogrItem in devidedOgrencis)
                {
                    dersAcilanDto.Sube = sube;
                    SubeliDersAcilan.Add(dersAcilanDto.DeepClone());
                    foreach (var ogrenci in ogrItem)
                    {
                        ogrenci.Sube = sube;
                        SubeliOgrenci.Add(ogrenci);
                    }
                    sube++;
                }

                DersSubeDtos = SubeliDersAcilan;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sınava tabi öğrenicler getirilirken hata oluştu.");
            }
        }

        public async Task RowSelectedHandlerDersSube(Syncfusion.Blazor.Grids.RowSelectEventArgs<DersAcilanDto> args)
        {
            OgrenciDtos = SubeliOgrenci.Where(x => x.Sube == args.Data.Sube).ToList();
        }

        async Task Onayla()
        {
            await CreateNewSubesAndUpdateOgrenciSubes();





            //herşey bitince açılan dersleri filtreleriyle beraer refresh ediyoruz.
            GetDersAcilansByFilters();

            //Diğer iki gridi de temizlemek lazım.
        }

        //Yeni şube yeni ders açılan demek oluyor. 1 . şube hariç diğer şubeleri ders kayıdı olarak şubeNO larla birlikte açacağız.
        //Bunlara bağlı olan öğrencileride 1. şube ders kayıdından silip bu şubelerin açılan derslerine aktaracağız.
        private async Task CreateNewSubesAndUpdateOgrenciSubes()
        {

            //List<SubeDersAcilanCreateDto> subeDersAcilanCreateDto = new List<SubeDersAcilanCreateDto>();

            SubeDersAcilanOgrenciCreateDto subeDersAcilanOgrenciCreateDto = new SubeDersAcilanOgrenciCreateDto();
            subeDersAcilanOgrenciCreateDto.DersAcilanId = SubeliDersAcilan.FirstOrDefault().Id;
            subeDersAcilanOgrenciCreateDto.Subes = SubeliDersAcilan.Select(x => x.Sube).ToList();
            subeDersAcilanOgrenciCreateDto.OgrenciIdsWithSubes = SubeliOgrenci
                .Select(c => new OgrenciSubeDto()
                {
                    OgrId = c.Id,
                    Sube = c.Sube
                }).ToList();

            ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/DersAcilan/PostCreateNewSubesAndUpdateOgrenciSubes", subeDersAcilanOgrenciCreateDto);
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                //Başarılı 
                //matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Açılan ders başarı ile şubelendirilmiştir. Lütfen açılan dersleri kontrol ediniz.");
                dialogUyariText = $"{SubeliDersAcilan.FirstOrDefault().Ad} isimli açılan ders başarı ile şubelendirilmiştir. Lütfen {SubeliDersAcilan.FirstOrDefault().Kod} kodu ile açılan dersleri kontrol ediniz.";
                isUyariOpen = true;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Açılan Derslerin bilgisi getirilirken hata oluştu");
            }
        }

    }
}
