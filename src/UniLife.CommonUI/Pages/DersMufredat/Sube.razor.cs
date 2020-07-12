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
using Syncfusion.Blazor.Navigations;

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
        SfDropDownList<string, KeyValueStringDto> ogrSubeler;



        List<DonemDto> donemDtos = new List<DonemDto>();
        List<KeyValueDto> fakulteDtos = new List<KeyValueDto>();
        List<KeyValueDto> bolumDtos = new List<KeyValueDto>();
        List<KeyValueDto> programDtos = new List<KeyValueDto>();




        Syncfusion.Blazor.Grids.SfGrid<SubeDersAcilanDto> DersAcGrid;
        public List<SubeDersAcilanDto> DersAcDtos = new List<SubeDersAcilanDto>();


        Syncfusion.Blazor.Grids.SfGrid<DersAcilanDto> DersSubeGrid;
        public List<DersAcilanDto> DersSubeDtos = new List<DersAcilanDto>();



        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> OgrenciGrid;
        public List<OgrenciDto> OgrenciDtos = new List<OgrenciDto>();

        public List<DersAcilanDto> SelectedDersAcilans { get; set; } = new List<DersAcilanDto>();

        public int selectedSube { get; set; }

        bool subeDialogOpen;
        //List<OgrenciDto> SecmeliOgrenciDtos = new List<OgrenciDto>();
        Syncfusion.Blazor.Grids.SfGrid<AkademisyenDto> AkademisyenGrid;

        public int subeSayisi { get; set; } = 1;
        public int subelendirmeTipi { get; set; } = 1;
        List<KeyValueDto> subeTypeDtos = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "Eşit Parçala", Id = 1 },
            new KeyValueDto() { Ad = "Tek Çift", Id = 2 },
            new KeyValueDto() { Ad = "Cinsiyet", Id = 3 }
        };

        List<KeyValueStringDto> OgrSubelerDto = new List<KeyValueStringDto> { new KeyValueStringDto() { Ad = "1", Id = "1" } };

        //{
        //    new KeyValueDto() { Ad = "Eşit Parçala", Id = 1 },
        //    new KeyValueDto() { Ad = "Tek Çift", Id = 2 },
        //    new KeyValueDto() { Ad = "Cinsiyet", Id = 3 }
        //};

        bool isUyariOpen;
        string dialogUyariText = "";

        int selectedDersAcilanId;
        double selectedDersAcilanRowIndex;
        DersAcilanDto selectedDersAcilanSube;

        bool SubelendirAllow = false;
        //Şubelendir buttonuna basınca sağdaki gridler varklı davranmalı.
        //eğer sadece satır seçildiyse Grid normal şubeleri getirmeli.
        bool IsStateSubelendir;

        string subeChangeNumber;

        bool akademisyenDialogOpen;

        bool SubeTasi;


        private List<Object> Toolbaritems = new List<Object>() { new ItemModel() { Text = "Şubeleri Temizle", TooltipText = "Click", Id = "SubeTemizle" } };

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

            SubelendirAllow = false;
            await GetDersAcilansByFilters();
        }


        private async Task GetDersAcilansByFilters()
        {
            SinavDersAcDto sinavDersAcDto = new SinavDersAcDto
            {
                donemId = _dersAcilanDto.DonemId,
                programId = _dersAcilanDto.ProgramId,
                dersKodu = _dersAcilanDto.Kod,
                SubeGroup = true
            };

            //var reqURL = $"api/DersAcilan/GetDersAcilansByFilters/{_dersAcilanDto.DonemId ?? 0}/{_dersAcilanDto.ProgramId ?? 0}/{_dersAcilanDto.Sinif ?? 0}/{_dersAcilanDto.Kod ?? ""}";



            ApiResponseDto<List<SubeDersAcilanDto>> apiResponse = await Http.PostJsonAsync<ApiResponseDto<List<SubeDersAcilanDto>>>("api/DersAcilan/PostDersAcilansByFilters", sinavDersAcDto);
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

        public async Task RowSelectedHandler(Syncfusion.Blazor.Grids.RowSelectEventArgs<SubeDersAcilanDto> args)
        {
            IsStateSubelendir = false;
            DersSubeDtos = new List<DersAcilanDto>();
            OgrenciDtos = new List<OgrenciDto>();
            SubeliDersAcilan = new List<DersAcilanDto>();
            SubeliOgrenci = new List<OgrenciDto>();
            //Şubelendirmeye bir kere izin veriyoruz.
            //OgrSubelerDto = new List<KeyValueDto> { new KeyValueDto() { IntValue = 1, Id = 1 } };

            if (args.Data.SubeCount == 1)
            {
                SubelendirAllow = true;
                await DersSubeGrid.EnableToolbarItems(new List<string>() { "SubeTemizle" }, false);
            }
            else
            {
                SubelendirAllow = false;
                //Toolbaritems = new List<Object>() { new ItemModel() { Text = "Şubeleri Temizle", TooltipText = "Click", Id = "SubeTemizle" } };

                await DersSubeGrid.EnableToolbarItems(new List<string>() { "SubeTemizle" },true);
            }
            StateHasChanged();

            DersSubeDtos = await GetDersAcilanSubelerByDersKod(args.Data.Kod);
            DersSubeGrid.Refresh();
            selectedDersAcilanId = DersSubeDtos.FirstOrDefault().Id;
            selectedDersAcilanRowIndex = args.RowIndex;


            //await DersAcGrid.ClearSelection();
            //DersAcGrid.SelectedRowIndex = args.RowIndex;
            OgrSubelerDto = new List<KeyValueStringDto>();
            for (int i = 1; i < DersSubeDtos.Count + 1; i++)
            {
                OgrSubelerDto.Add(new KeyValueStringDto() { Ad = i.ToString(), Id = i.ToString() });
            }
            StateHasChanged();
        }


        private async Task<List<DersAcilanDto>> GetDersAcilanSubelerByDersKod(string dersKod)
        {
            ApiResponseDto<List<DersAcilanDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DersAcilanDto>>>($"api/dersacilan/GetDersAcilanSubelerByDersKod/{dersKod}");
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                return apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav bilgisi getirilirken hata oluştu");
                return null;
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


        public async Task CommandClickHandlerSube(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersAcilanDto> args)
        {
            if (args.CommandColumn.Title == "Öğretmen Ata")
            {
                akademisyenDialogOpen = true;
            }
        }

        async Task DersiSubelendir()
        {
            IsStateSubelendir = true;
            

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
                OgrenciDtos = new List<OgrenciDto>();
                DersSubeDtos = new List<DersAcilanDto>();
                SubeliDersAcilan = new List<DersAcilanDto>();
                SubeliOgrenci = new List<OgrenciDto>();
                OgrenciGrid.Refresh();
                DersSubeGrid.Refresh();

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

                //await Onayla();
            }

        }

        List<DersAcilanDto> SubeliDersAcilan = new List<DersAcilanDto>();
        List<OgrenciDto> SubeliOgrenci = new List<OgrenciDto>();

        private async Task EsitBol(SubeDersAcilanDto subeDersAcilanDto)
        {
            ApiResponseDto<DersAcilanDto> apiResponseDers = await Http.GetFromJsonAsync<ApiResponseDto<DersAcilanDto>>($"api/dersAcilan/GetDersAcilanSpecByDersAcId/{selectedDersAcilanId}");



            ApiResponseDto<List<OgrenciDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciDto>>>($"api/Ogrenci/GetOgrenciListByDersAcId/{selectedDersAcilanId}");
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK
                && apiResponseDers.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {

                IEnumerable<IEnumerable<OgrenciDto>> devidedOgrencis = apiResponse.Result.Split<OgrenciDto>(subeSayisi);

                var dersAcilan = apiResponseDers.Result;
                int sube = 1;
                foreach (var ogrItem in devidedOgrencis)
                {
                    dersAcilan.Sube = sube;
                    dersAcilan.OgrCount = ogrItem.Count();
                    SubeliDersAcilan.Add(dersAcilan.DeepClone());
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
            selectedDersAcilanSube = args.Data;
            if (IsStateSubelendir)
            {
                OgrenciDtos = SubeliOgrenci.Where(x => x.Sube == args.Data.Sube).ToList();
                OgrenciGrid.Refresh();

            }
            else
            {
                OgrenciDtos = await GetOgrencisByDersAcilanId(args.Data);
                OgrenciGrid.Refresh();
            }

        }


        private async Task<List<OgrenciDto>> GetOgrencisByDersAcilanId(DersAcilanDto dersAcilanDto)
        {
            ApiResponseDto<List<OgrenciDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciDto>>>($"api/Ogrenci/GetOgrenciListByDersAcId/{dersAcilanDto.Id}");
            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                return apiResponse.Result;

            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav bilgisi getirilirken hata oluştu");
                return null;
            }
        }

        async Task Onayla()
        {
            IsStateSubelendir = false;
            await CreateNewSubesAndUpdateOgrenciSubes();





            //herşey bitince açılan dersleri filtreleriyle beraer refresh ediyoruz.
            GetDersAcilansByFilters();

            //Diğer iki gridi de temizlemek lazım.
            DersAcGrid.SelectRow(selectedDersAcilanRowIndex);

            Toolbaritems = new List<Object>() { new ItemModel() { Text = "Şubeleri Temizle", TooltipText = "Click", Id = "SubeTemizle" } };
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


        public async Task CommandClickHandlerAkademisyen(Syncfusion.Blazor.Grids.CommandClickEventArgs<AkademisyenDto> args)
        {
            if (args.CommandColumn.Title == "Akademisyen Ekle")
            {
                ApiResponseDto apiResponse = await Http.GetFromJsonAsync<ApiResponseDto>($"api/dersacilan/UpdateDersAcilanAkademsiyen/{selectedDersAcilanSube.Id}/{args.RowData.Id}" );
                akademisyenDialogOpen = false;

                if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    DersSubeDtos = await GetDersAcilanSubelerByDersKod(selectedDersAcilanSube.Kod);
                    DersSubeGrid.Refresh();
                    matToaster.Add(args.RowData.Ad + " " + args.RowData.Soyad, MatToastType.Success, "Akademisyen kaydı güncellendi");
                }
                else
                {
                    matToaster.Add(args.RowData.Ad + " " + args.RowData.Soyad + " : " + apiResponse.StatusCode, MatToastType.Danger, "Akademisyen kayıt edilemedi");
                }
            }
        }

        public async Task OnActionBeginHandlerSube(Syncfusion.Blazor.Grids.ActionEventArgs<DersAcilanDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "edit")
                {
                    akademisyenDialogOpen = true;
                }

            }
        }

        async Task SeciliOgrTasi()
        { 
            var selectedOrg = (await OgrenciGrid.GetSelectedRecords()).FirstOrDefault();
            if (selectedOrg == null)
            {
                isUyariOpen = true;
                dialogUyariText = "Lütfen bir öğrenci seçimi yapınız";
            }
            else
            {
                int selectedSube = Convert.ToInt32(ogrSubeler.Value);
                if (selectedSube == selectedDersAcilanSube.Sube)
                {
                    dialogUyariText = $"Seçilen öğrencinin şube numarası zaten {selectedSube}";
                    isUyariOpen = true;
                }
                else
                {
                    PutUpdateOgrencisDersKayitsDto putUpdateOgrencisDersKayitsDto = new PutUpdateOgrencisDersKayitsDto();
                    putUpdateOgrencisDersKayitsDto.SelectedDersAcilanId = selectedDersAcilanSube.Id;
                    putUpdateOgrencisDersKayitsDto.PointedDersAcilanId = DersSubeDtos.Where(x => x.Sube == selectedSube).FirstOrDefault().Id;
                    putUpdateOgrencisDersKayitsDto.OgrenciIds =(await OgrenciGrid.GetSelectedRecords()).Select(x=>x.Id);


                    ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>("api/derskayit/PutUpdateOgrencisDersKayits", putUpdateOgrencisDersKayitsDto);
                    akademisyenDialogOpen = false;

                    if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                    {
                        //OgrenciDtos = await GetOgrencisByDersAcilanId(selectedDersAcilanSube);
                        //OgrenciGrid.Refresh();
                        OgrenciDtos = new List<OgrenciDto>();

                        DersSubeDtos = await GetDersAcilanSubelerByDersKod(selectedDersAcilanSube.Kod);
                        DersSubeGrid.Refresh();

                        matToaster.Add($"Seçilen öğrencilerin şubeleri {selectedSube} olarak değiştirilmiştir.", MatToastType.Success, "Şube değişikliği başarılı");
                    }
                    else
                    {
                        matToaster.Add("Seçilen öğrencilerin şubeleri değiştirilemedi " + apiResponse.StatusCode, MatToastType.Danger, "Şube değişikliği başarısız oldu!");
                    }
                }

                
            }
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "SubeTemizle")
            {
                List<int> silinecekDersAcilanIds = DersSubeDtos.Where(x=>x.Sube!=1).Select(x => x.Id).ToList();
                if (silinecekDersAcilanIds.Count()<1)
                {
                    dialogUyariText = "Birden fazla şube mevcut değil.";
                    isUyariOpen = true;
                    return;
                }

                int baseDersAcilanId = DersSubeDtos.FirstOrDefault(x => x.Sube == 1).Id;

                ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds = new ReqEntityIdWithOtherEntitiesIds();
                reqEntityIdWithOtherEntitiesIds.EntityId = baseDersAcilanId;
                reqEntityIdWithOtherEntitiesIds.OtherEntityIds = silinecekDersAcilanIds;

                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>("api/derskayit/PutUpdateOgrencisDersKayitsDeleteExSubes", reqEntityIdWithOtherEntitiesIds);
                if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    DersAcGrid.SelectRow(selectedDersAcilanRowIndex);
                    DersSubeGrid.Refresh();
                    OgrenciGrid.Refresh();
                    matToaster.Add($"Dersin tüm öğrencileri 1. şubeye aktarılmıştır.", MatToastType.Success, "Şube değişikliği başarılı");
                    await Refresh();
                    StateHasChanged();
                }
                else
                {
                    matToaster.Add("Sube silem işleminde hata oluştu." + apiResponse.StatusCode, MatToastType.Danger, "Hata!");
                }

            }
        }


    }
}
