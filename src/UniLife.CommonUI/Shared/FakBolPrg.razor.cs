using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Shared
{
    public partial class FakBolPrg : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        SfDropDownList<int?, KeyValueDto> DropFakulte;
        SfDropDownList<int?, KeyValueDto> DropBolum;
        SfDropDownList<int?, KeyValueDto> DropProgram;
        List<KeyValueDto> fakulteDtos = new List<KeyValueDto>();
        List<KeyValueDto> bolumDtos = new List<KeyValueDto>();
        List<KeyValueDto> programDtos = new List<KeyValueDto>();



        private int? _fakValue;
        [Parameter]
        public int? FakulteId
        {
            get => _fakValue;
            set
            {
                if (_fakValue == value) return;
                _fakValue = value;
                FakulteIdChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public EventCallback<int?> FakulteIdChanged { get; set; }


        private int? _prgValue;
        [Parameter]
        public int? ProgramId
        {
            get => _prgValue;
            set
            {
                if (_prgValue == value) return;
                _prgValue = value;
                ProgramIdChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public EventCallback<int?> ProgramIdChanged { get; set; }
        [Parameter]
        public string ProgramPlaceHolder { get; set; }


        private int? _bolValue;
        [Parameter]
        public int? BolumId
        {
            get => _bolValue;
            set
            {
                if (_bolValue == value) return;
                _bolValue = value;
                BolumIdChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public EventCallback<int?> BolumIdChanged { get; set; }

        [Parameter]
        public bool ProgramHide { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ReadFakultes();
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

        private void FakulteToBolum(Syncfusion.Blazor.DropDowns.ChangeEventArgs<int?> args)
        {
            bolumDtos = new List<KeyValueDto>();
            programDtos = new List<KeyValueDto>();
            BolumId = null;
            ProgramId = null;
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
            ProgramId = null;

            ReadPrograms(args.Value).ConfigureAwait(true);

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
    }
}
