using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using UniLife.Shared.Dto.Definitions;
using Syncfusion.Blazor.Navigations;

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

        SfTab Tab;
        int selectedItem;

        public void OnTabSelecting(SelectingEventArgs args)
        {
            if (args.IsSwiped)
            {
                args.Cancel = true;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            //try
            //{
            //    ApiResponseDto<List<OgrenciNotlarDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<OgrenciNotlarDto>>>("api/sinavKayit/GetOgrenciNotlar/" + appState.OgrenciState.Id);

            //    if (apiResponse.IsSuccessStatusCode)
            //    {
            //        matToaster.Add($"{appState.OgrenciState.Ad} 'nin not bilgileri getirildi.", MatToastType.Success);
            //        ogrenciNotlarDtos = apiResponse.Result;
            //    }
            //    else
            //    {
            //        ogrenciNotlarDtos = new List<OgrenciNotlarDto>();
            //        matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            //}
        }
    }
}
