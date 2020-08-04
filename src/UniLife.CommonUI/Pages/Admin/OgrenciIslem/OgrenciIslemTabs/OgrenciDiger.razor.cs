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
using Syncfusion.Blazor.Grids;

namespace UniLife.CommonUI.Pages.Admin.OgrenciIslem.OgrenciIslemTabs
{
    public partial class OgrenciDiger : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        

        public string Visible { get; set; } = "visible";//"hidden";
        public double[] CellSpacing = { 5, 5 };
        public int Columns = 2;
        public double AspectRatio = 2;//100 / 85;

        [Parameter]
        public OgrenciDto _OgrenciDto { get; set; } = new OgrenciDto();


        


        protected override async Task OnInitializedAsync()
        {
            try
            {
                ApiResponseDto apiGecis = await Http.GetFromJsonAsync<ApiResponseDto>("api/ogrgecis");
                ApiResponseDto apiCeza = await Http.GetFromJsonAsync<ApiResponseDto>("api/ogrceza");
                ApiResponseDto apiDondur = await Http.GetFromJsonAsync<ApiResponseDto>("api/ogrdondur");
                ApiResponseDto apiStaj = await Http.GetFromJsonAsync<ApiResponseDto>("api/ogrstaj");
                ApiResponseDto apiTez = await Http.GetFromJsonAsync<ApiResponseDto>("api/ogrtez");

                gecisDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<OgrGecisDto[]>(apiGecis.Result.ToString()).ToList<OgrGecisDto>(); 
                cezaDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<OgrCezaDto[]>(apiGecis.Result.ToString()).ToList<OgrCezaDto>(); 
                dondurDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<OgrDondurDto[]>(apiGecis.Result.ToString()).ToList<OgrDondurDto>();
                stajDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<OgrStajDto[]>(apiGecis.Result.ToString()).ToList<OgrStajDto>();
                tezDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<OgrTezDto[]>(apiGecis.Result.ToString()).ToList<OgrTezDto>();
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }
        }


        #region GECIS

        SfGrid<OgrGecisDto> GecisGrid;

        List<OgrGecisDto> gecisDtos = new List<OgrGecisDto>();

        public async Task ActionCompletedGecis(ActionEventArgs<OgrGecisDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {
                    await UpdateGecis(args.Data);
                }
                else if (args.Action == "Add")
                {
                    await CreateGecis(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await DeleteGecis(args.Data);
            }
        }

        public async Task CreateGecis(OgrGecisDto ogrGecisDto)
        {
            try
            {
                ogrGecisDto.OgrenciId = _OgrenciDto.Id;
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/ogrGecis", ogrGecisDto);
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    gecisDtos.Remove(ogrGecisDto);
                    GecisGrid.Refresh();
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                gecisDtos.Remove(ogrGecisDto);
                GecisGrid.Refresh();
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        public async Task UpdateGecis(OgrGecisDto ogrGecisDto)
        {
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/ogrgecis", ogrGecisDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                    //update failed gridi boz !
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
                //update failed gridi boz !
            }
        }

        public async Task DeleteGecis(OgrGecisDto ogrGecisDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/ogrGecis/" + ogrGecisDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("", MatToastType.Success, "İşlem başarılı.");
                    gecisDtos.Remove(ogrGecisDto);
                }
                else
                {
                    matToaster.Add("", MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        #endregion

        async Task Kaydet(string PostType)
        {
            //try
            //{
            //    _OgrenciDigerDto.OgrenciId = _OgrenciDto.Id;
            //    ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>($"api/ogrenciDiger/{PostType}", _OgrenciDigerDto);

            //    if (apiResponse.IsSuccessStatusCode)
            //    {
            //        matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
            //    }
            //    else
            //        matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
            //}
            //catch (Exception ex)
            //{
            //    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            //}
        }


        #region Dondur

        SfGrid<OgrDondurDto> DondurGrid;

        List<OgrDondurDto> dondurDtos = new List<OgrDondurDto>();

        public async Task ActionCompletedDondur(ActionEventArgs<OgrDondurDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {
                    await UpdateDondur(args.Data);
                }
                else if (args.Action == "Add")
                {
                    await CreateDondur(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await DeleteDondur(args.Data);
            }
        }

        public async Task CreateDondur(OgrDondurDto ogrDondurDto)
        {
            try
            {
                ogrDondurDto.OgrenciId = _OgrenciDto.Id;
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/ogrDondur", ogrDondurDto);
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    dondurDtos.Remove(ogrDondurDto);
                    DondurGrid.Refresh();
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                dondurDtos.Remove(ogrDondurDto);
                DondurGrid.Refresh();
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        public async Task UpdateDondur(OgrDondurDto ogrDondurDto)
        {
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/ogrdondur", ogrDondurDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                    //update failed gridi boz !
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
                //update failed gridi boz !
            }
        }

        public async Task DeleteDondur(OgrDondurDto ogrDondurDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/ogrDondur/" + ogrDondurDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("", MatToastType.Success, "İşlem başarılı.");
                    dondurDtos.Remove(ogrDondurDto);
                }
                else
                {
                    matToaster.Add("", MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        #endregion




        #region Ceza

        SfGrid<OgrCezaDto> CezaGrid;

        List<OgrCezaDto> cezaDtos = new List<OgrCezaDto>();

        public async Task ActionCompletedCeza(ActionEventArgs<OgrCezaDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {
                    await UpdateCeza(args.Data);
                }
                else if (args.Action == "Add")
                {
                    await CreateCeza(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await DeleteCeza(args.Data);
            }
        }

        public async Task CreateCeza(OgrCezaDto ogrCezaDto)
        {
            try
            {
                ogrCezaDto.OgrenciId = _OgrenciDto.Id;
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/ogrCeza", ogrCezaDto);
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    cezaDtos.Remove(ogrCezaDto);
                    CezaGrid.Refresh();
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                cezaDtos.Remove(ogrCezaDto);
                CezaGrid.Refresh();
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        public async Task UpdateCeza(OgrCezaDto ogrCezaDto)
        {
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/ogrceza", ogrCezaDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                    //update failed gridi boz !
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
                //update failed gridi boz !
            }
        }

        public async Task DeleteCeza(OgrCezaDto ogrCezaDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/ogrCeza/" + ogrCezaDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("", MatToastType.Success, "İşlem başarılı.");
                    cezaDtos.Remove(ogrCezaDto);
                }
                else
                {
                    matToaster.Add("", MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        #endregion





        #region Staj

        SfGrid<OgrStajDto> StajGrid;

        List<OgrStajDto> stajDtos = new List<OgrStajDto>();

        public async Task ActionCompletedStaj(ActionEventArgs<OgrStajDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {
                    await UpdateStaj(args.Data);
                }
                else if (args.Action == "Add")
                {
                    await CreateStaj(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await DeleteStaj(args.Data);
            }
        }

        public async Task CreateStaj(OgrStajDto ogrStajDto)
        {
            try
            {
                ogrStajDto.OgrenciId = _OgrenciDto.Id;
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/ogrStaj", ogrStajDto);
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    stajDtos.Remove(ogrStajDto);
                    StajGrid.Refresh();
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                stajDtos.Remove(ogrStajDto);
                StajGrid.Refresh();
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        public async Task UpdateStaj(OgrStajDto ogrStajDto)
        {
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/ogrstaj", ogrStajDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                    //update failed gridi boz !
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
                //update failed gridi boz !
            }
        }

        public async Task DeleteStaj(OgrStajDto ogrStajDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/ogrStaj/" + ogrStajDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("", MatToastType.Success, "İşlem başarılı.");
                    stajDtos.Remove(ogrStajDto);
                }
                else
                {
                    matToaster.Add("", MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        #endregion




        #region Tez

        SfGrid<OgrTezDto> TezGrid;

        List<OgrTezDto> tezDtos = new List<OgrTezDto>();

        public async Task ActionCompletedTez(ActionEventArgs<OgrTezDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {
                    await UpdateTez(args.Data);
                }
                else if (args.Action == "Add")
                {
                    await CreateTez(args.Data);
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await DeleteTez(args.Data);
            }
        }

        public async Task CreateTez(OgrTezDto ogrTezDto)
        {
            try
            {
                ogrTezDto.OgrenciId = _OgrenciDto.Id;
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/ogrTez", ogrTezDto);
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    tezDtos.Remove(ogrTezDto);
                    TezGrid.Refresh();
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                tezDtos.Remove(ogrTezDto);
                TezGrid.Refresh();
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        public async Task UpdateTez(OgrTezDto ogrTezDto)
        {
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/ogrtez", ogrTezDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                }
                else
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                    //update failed gridi boz !
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
                //update failed gridi boz !
            }
        }

        public async Task DeleteTez(OgrTezDto ogrTezDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/ogrTez/" + ogrTezDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("", MatToastType.Success, "İşlem başarılı.");
                    tezDtos.Remove(ogrTezDto);
                }
                else
                {
                    matToaster.Add("", MatToastType.Danger, "Hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
            }
        }

        #endregion

    }
}
