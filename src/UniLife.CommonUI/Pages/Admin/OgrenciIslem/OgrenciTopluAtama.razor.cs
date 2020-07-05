using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto;
using MatBlazor;
using System.Net.Http.Json;
using Syncfusion.Blazor.Navigations;

namespace UniLife.CommonUI.Pages.Admin.OgrenciIslem
{
    public partial class OgrenciTopluAtama : ComponentBase
    {

        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        SfDropDownList<int?, KeyValueDto> DropFakulte;
        SfDropDownList<int?, KeyValueDto> DropBolum;
        SfDropDownList<int?, KeyValueDto> DropProgram;
        SfDropDownList<int?, SinifDto> DropSinif;
        SfDropDownList<int?, KeyValueDto> DropKayitNeden;
        SfDropDownList<int?, KeyValueDto> DropOgrDurum;
        SfDropDownList<int?, KeyValueDto> DropCinsiyet;        




        List<KeyValueDto> fakulteDtos = new List<KeyValueDto>();
        List<KeyValueDto> bolumDtos = new List<KeyValueDto>();
        List<KeyValueDto> programDtos = new List<KeyValueDto>();
        List<KeyValueDto> kayitNedenDtos = new List<KeyValueDto>();
        List<KeyValueDto> ogrenimDurumDtos = new List<KeyValueDto>();
        List<SinifDto> sinifDtos = new List<SinifDto>
        {
            new SinifDto() { Ad = "Tümü", Id = 0 },
            new SinifDto() { Ad = "1", Id = 1 },
            new SinifDto() { Ad = "2", Id = 2 },
            new SinifDto() { Ad = "3", Id = 3 },
            new SinifDto() { Ad = "4", Id = 4 },
            new SinifDto() { Ad = "5", Id = 5 },
            new SinifDto() { Ad = "6", Id = 6 },
            new SinifDto() { Ad = "7", Id = 7 },
            new SinifDto() { Ad = "8", Id = 8 },
            new SinifDto() { Ad = "9", Id = 9 },
        };
        List<KeyValueDto> cinsiyetDtos = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "Kadın", Id = 0 },
            new KeyValueDto() { Ad = "Erkek", Id = 1 }
        };

        public int DanismanTip { get; set; }
        List<KeyValueDto> danismanDtos = new List<KeyValueDto>
        {
            new KeyValueDto() { Ad = "ilk Danişman", Id = 1 },
            new KeyValueDto() { Ad = "ikinci Danişman", Id = 2 }
        };

        public ReqOgrTopAtaDto reqOgrTopAtaDto { get; set; } = new ReqOgrTopAtaDto();

        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> OgrencilerGrid;

        
        string OdataQuery = "";
        bool isTopGridVisible;



        SfTab Tab;
        public void TabCreate()
        {
            Tab.EnableTab(0, true);
            Tab.EnableTab(1, false);
            Tab.EnableTab(2, false);
        }

        public void OnTabSelecting(SelectingEventArgs args)
        {
            if (args.IsSwiped)
            {
                args.Cancel = true;
            }
        }


        protected async override Task OnInitializedAsync()
        {
            //ReadDersliks();
            //ReadDerlikRezervs();

            await ReadFakultes();
            await ReadKayitNedens();
            await ReadOgrenimDurums();
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

        async Task ReadKayitNedens()
        {
            ApiResponseDto<List<KeyValueDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<KeyValueDto>>>("api/KeyValues/GetKayitNeden");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                kayitNedenDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Kayit Neden bilgisi getirilirken hata oluştu!");
            }
        }
        async Task ReadOgrenimDurums()
        {
            ApiResponseDto<List<KeyValueDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<KeyValueDto>>>("api/ogrenimDurum");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                ogrenimDurumDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Öğrenim Durum bilgisi getirilirken hata oluştu!");
            }
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

        private void FakulteToBolum(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            bolumDtos = new List<KeyValueDto>();
            programDtos = new List<KeyValueDto>();
            reqOgrTopAtaDto.BolumId = null;
            reqOgrTopAtaDto.ProgramId = null;
            //if (reqOgrTopAtaDto.Id == 0)
            //{
            ReadBolums(args.Value).ConfigureAwait(true);
            //}
            //else
            //{
            //    ReadBolums(reqOgrTopAtaDto.FakulteId).ConfigureAwait(true);
            //}
            StateHasChanged();
        }
        private void BolumToProgram(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            programDtos = new List<KeyValueDto>();
            reqOgrTopAtaDto.ProgramId = null;
            //if (_dersAcilanDto.Id == 0)
            //{

            ReadPrograms(args.Value).ConfigureAwait(true);
            //}
            //else
            //{
            //    ReadPrograms(_dersAcilanDto.BolumId).ConfigureAwait(true);
            //}
            //StateHasChanged();
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
                matToaster.Add("", MatToastType.Danger, "Program getirilirken hata oluştu!");
            }
        }

        async Task Refresh()
        {
            string OdataQueryParameters="";
            
            if (reqOgrTopAtaDto.FakulteId.HasValue)
            {
                OdataQueryParameters = $"FakulteId={reqOgrTopAtaDto.FakulteId},";
            }
            if (reqOgrTopAtaDto.BolumId.HasValue)
            {
                OdataQueryParameters = $"BolumId={reqOgrTopAtaDto.BolumId},";
            }
            if (reqOgrTopAtaDto.ProgramId.HasValue)
            {
                OdataQueryParameters = $"ProgramId={reqOgrTopAtaDto.ProgramId},";
            }
            if (reqOgrTopAtaDto.KayitNedenId.HasValue)
            {
                OdataQueryParameters = $"KayitNedenId={reqOgrTopAtaDto.KayitNedenId},";
            }
            if (reqOgrTopAtaDto.OgrenimDurumId.HasValue)
            {
                OdataQueryParameters = $"OgrenimDurumId={reqOgrTopAtaDto.OgrenimDurumId},";
            }
            if (reqOgrTopAtaDto.Sinif.HasValue)
            {
                OdataQueryParameters = $"Sinif={reqOgrTopAtaDto.Sinif},";
            }
            if (reqOgrTopAtaDto.Cinsiyet.HasValue)
            {
                OdataQueryParameters = $"Cinsiyet={reqOgrTopAtaDto.Cinsiyet},";
            }


            OdataQueryParameters =OdataQueryParameters.TrimEnd(',');
            OdataQuery = $"odata/Ogrencis/GetTopAta({OdataQueryParameters})";
            isTopGridVisible = true;
        }

    }
}
