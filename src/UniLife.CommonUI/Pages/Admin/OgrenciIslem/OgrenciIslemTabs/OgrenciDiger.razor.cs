using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using UniLife.Shared.Dto.Definitions;
using Syncfusion.Blazor.Navigations;
using UniLife.Shared.Dto;
using System.Net.Http.Json;
using MatBlazor;
using UniLife.CommonUI.Extensions;

namespace UniLife.CommonUI.Pages.Admin.OgrenciIslem.OgrenciIslemTabs
{
    public partial class OgrenciDiger : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        [Parameter]
        public OgrenciDto _OgrenciDto { get; set; } = new OgrenciDto();

        public OgrenciDigerDto _OgrenciDigerDto { get; set; } = new OgrenciDigerDto();

        public string Visible { get; set; } = "visible";//"hidden";
        public double[] CellSpacing = { 5, 5 };
        public int Columns = 2;
        public double AspectRatio = 2;//100 / 85;
        public void OnTabSelecting(SelectingEventArgs args)
        {
            if (args.IsSwiped)
            {
                args.Cancel = true;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ApiResponseDto<OgrenciDigerDto> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<OgrenciDigerDto>>("api/OgrenciDiger/GetOgrDigerByOgrId/" + _OgrenciDto.Id);


                if (apiResponse.IsSuccessStatusCode)
                {
                    _OgrenciDigerDto = apiResponse.Result?? new OgrenciDigerDto();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }
        }

        async Task Kaydet(string PostType)
        {
            try
            {
                _OgrenciDigerDto.OgrenciId = _OgrenciDto.Id;
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>($"api/ogrenciDiger/{PostType}", _OgrenciDigerDto);

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
