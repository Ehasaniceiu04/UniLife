using MatBlazor;
using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using Syncfusion.Blazor.DropDowns;
using System.ComponentModel;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class Sinavlar : ComponentBase
    {
        [InjectAttribute]
        public System.Net.Http.HttpClient Http { get; set; }
        [InjectAttribute]
        public MatBlazor.IMatToaster matToaster { get; set; }

        public DersAcilanDto _dersAcilanDto { get; set; } = new DersAcilanDto();



        SfDropDownList<int?, DonemDto> DropDonem;
        SfDropDownList<int?, KeyValueDto> DropFakulte;
        SfDropDownList<int?, KeyValueDto> DropBolum;
        SfDropDownList<int?, KeyValueDto> DropProgram;
        SfDropDownList<int?, SinifDto> DropSinif;



        List<DonemDto> donemDtos = new List<DonemDto>();
        List<KeyValueDto> fakulteDtos = new List<KeyValueDto>();
        List<KeyValueDto> bolumDtos = new List<KeyValueDto>();
        List<KeyValueDto> programDtos = new List<KeyValueDto>();

        List<SinavTipDto> sinavTipDtos= new List<SinavTipDto>();
        List<SinavTurDto> sinavTurDtos = new List<SinavTurDto>();

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


        Syncfusion.Blazor.Grids.SfGrid<DersAcilanDto> DersAcGrid;
        public List<DersAcilanDto> DersAcDtos = new List<DersAcilanDto>();

        Syncfusion.Blazor.Grids.SfGrid<SinavDto> SinavGrid;
        public List<SinavDto> SinavDtos = new List<SinavDto>();

        //protected override void OnInitialized()
        //{

        //}

        protected async override Task OnInitializedAsync()
        {
            await ReadFakultes();
            await ReadDonems();
            await ReadSinavTip();
            await ReadSinavTur();
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

        async Task ReadSinavTip()
        {
            ApiResponseDto<List<SinavTipDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<SinavTipDto>>>("api/Keyvalues/GetSinavTip");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                sinavTipDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav tipleri getirilirken hata oluştu!");
            }
        }

        async Task ReadSinavTur()
        {
            ApiResponseDto<List<SinavTurDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<SinavTurDto>>>("api/Keyvalues/GetSinavTur");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                sinavTurDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Sinav turleri getirilirken hata oluştu!");
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
        void SinifChange()
        {
            _dersAcilanDto.Sinif = DropSinif.Value;
        }

        async Task Refresh()
        {

            OData<DersAcilanDto> apiResponse;
            //apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<BolumDto>>>("api/bolum/GetBolumByFakulteIds/" + string.Join(',', fakulteId));
            if (_dersAcilanDto.Sinif == 0 || _dersAcilanDto.Sinif==null)
            {
                apiResponse = await Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersacilans?$filter=DonemId eq {_dersAcilanDto.DonemId} and ProgramId eq {_dersAcilanDto.ProgramId} and ProgramId eq {_dersAcilanDto.ProgramId} &$expand=Akademisyen($select=Id,Ad)");
            }
            else
            {
                apiResponse = await Http.GetFromJsonAsync<OData<DersAcilanDto>>($"odata/dersacilans?$filter=DonemId eq {_dersAcilanDto.DonemId} and ProgramId eq {_dersAcilanDto.ProgramId} and ProgramId eq {_dersAcilanDto.ProgramId} and Sinif eq {_dersAcilanDto.Sinif} &$expand=Akademisyen($select=Id,Ad)");
            }

            DersAcDtos = apiResponse.Value;
            StateHasChanged();

            if (apiResponse.Value != null)
            {
                
            }
            else
            {
                matToaster.Add("", MatToastType.Danger, "Bölüm getirilirken hata oluştu!");
            }
        }

        public void CommandClickHandler(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersAcilanDto> args)
        {
            if (args.CommandColumn.Title == "Tanımlı Sınavlar")
            {
                ApiResponseDto<List<SinavDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<SinavDto>>>($"api/Sinav/GetSinavListByAcilanDersId/{args.RowData.Id}").Result;

            }
        }


    }
}
