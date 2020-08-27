using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
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
    public partial class Dersler : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }


        [Parameter]
        public EventCallback<int> TabDegis { get; set; }

        SfGrid<DersDto> DersGrid;

        List<DersDto> dersDtos = new List<DersDto>();


        //List<DonemDto> donemDtos = new List<DonemDto>();
        List<DonemTipDto> donemTipDtos = new List<DonemTipDto>();

        public string[] MenuItems = new string[] { "Group", "Ungroup", "ColumnChooser", "Filter" };

        private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "900px" };

        string[] Initial = (new string[] { "DonemTipId" });

        int? donemId;

        public bool isOpsCoklama { get; set; }


        bool yerineDersDialogOpen;
        int? yerineExistProgramId;
        string yerineDersKod;
        int sonMufredatId;

        string edittekiDersKod;

        protected override void OnInitialized()
        {
            ReadDerss();

            //ApiResponseDto<List<DonemDto>> apiResponseDonem = Http.GetFromJsonAsync<ApiResponseDto<List<DonemDto>>>("api/donem/current").Result;
            //donemDtos = apiResponseDonem.Result;

            ApiResponseDto<List<DonemTipDto>> apiResponseDonem = Http.GetFromJsonAsync<ApiResponseDto<List<DonemTipDto>>>("api/donemtip").Result;
            donemTipDtos = apiResponseDonem.Result;

        }

        void ReadDerss()
        {
            try
            {
                ApiResponseDto<List<DersDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<DersDto>>>("api/ders/GetDersByMufredatId/" + appState.MufredatState.MufredatId).Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add($"{appState.MufredatState.MufredatAd} müfredatına bağlı dersler getirildi", MatToastType.Success);
                    dersDtos = apiResponse.Result;
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }

        }



        public async void ActionCompletedHandler(ActionEventArgs<DersDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                
            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Cancel)
            {
                isOpsCoklama = false;
                yerineExistProgramId = null;
            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {
                    
                    if (await Update(args.Data))
                    {
                        DersGrid.Refresh();
                    }
                    args.Cancel = true;
                    await DersGrid.CloseEdit();
                    isOpsCoklama = false;

                }
                else if (args.Action == "Add")
                {
                    //await Create(args.Data);
                    if (await Create(args.Data, false))
                    {
                        DersGrid.Refresh();
                    }
                    args.Cancel = true;
                    await DersGrid.CloseEdit();
                    isOpsCoklama = false;
                }

            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                if (await Delete(args.Data))
                {
                    DersGrid.Refresh();
                }
                args.Cancel = true;
                await DersGrid.CloseEdit();
                isOpsCoklama = false;
            }
        }

        public async Task<bool> Create(DersDto dersDto, bool isOpsCoklama)
        {
            try
            {
                dersDto.MufredatId = appState.MufredatState.MufredatId;
                dersDto.ProgramId = appState.MufredatState.ProgramId;
                dersDto.BolumId = appState.MufredatState.BolumId;
                dersDto.FakulteId = appState.MufredatState.FakulteId;

                ApiResponseDto<DersDto> apiResponse = await Http.PostJsonAsync<ApiResponseDto<DersDto>>("api/ders", dersDto);

                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                    if (!isOpsCoklama)
                    {
                        dersDtos.FirstOrDefault(x => x.Id == 0).Id = apiResponse.Result.Id;
                    }
                    ReadDerss();
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    return true;
                }
                else
                {
                    if (!isOpsCoklama)
                    {
                        dersDtos.Remove(dersDto);
                    }

                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (!isOpsCoklama)
                {
                    dersDtos.Remove(dersDto);
                    DersGrid.Refresh();
                }
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
            }
        }


        public async Task<bool> Update(DersDto dersDto)
        {
            //this updates the IsCompleted flag only
            try
            {
                if (isOpsCoklama)
                {
                    dersDto.Id = 0;
                    return await Create(dersDto, isOpsCoklama);
                }
                else
                {
                    ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/ders", dersDto);

                    if (!apiResponse.IsError)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                        return true;
                    }
                    else
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
            }
        }

        public async Task<bool> Delete(DersDto dersDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/ders/" + dersDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("", MatToastType.Success, "İşlem başarılı.");
                    dersDtos.Remove(dersDto);
                    return true;
                }
                else
                {
                    matToaster.Add("", MatToastType.Danger, "İşlem başarısız!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
            }
        }

        public string CssClass { get; set; }
        public void OnInput(InputEventArgs args)
        {
            //if (!System.Text.RegularExpressions.Regex.IsMatch(args.Value, "^[0-9]*$"))
            if (!System.Text.RegularExpressions.Regex.IsMatch(args.Value, "^[a-zA-Z0-9_.-]*$"))
            {
                CssClass = "e-error";
            }
            else
            {
                CssClass = "e-success";
            }
            this.StateHasChanged();
        }

        public void QueryCellInfoHandler(QueryCellInfoEventArgs<DersDto> args)
        {
            if (args.Data.AktifDonemdeAcik)
            {
                args.Cell.AddStyle(new string[] { "background-color:#80d192" });
            }
        }

        public async Task CommandClickHandler(CommandClickEventArgs<DersDto> args)
        {
            if (args.CommandColumn.Title == "Dersi Ac")
            {
                try
                {


                    ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/ders/CreateDersAcilansByDersId", args.RowData.Id);
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        matToaster.Add("Ders açıldı", MatToastType.Success, "İşlem başarılı.");
                        ReadDerss();
                    }
                    else
                    {
                        matToaster.Add("", MatToastType.Danger, "İşlem başarısız!");
                    }
                }
                catch (Exception ex)
                {
                    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                }
            }
            if (args.CommandColumn.Title == "Çokla")
            {
                isOpsCoklama = true;
                yerineExistProgramId = null;
                //Id sıfırla.
                //    paramtereye coklama oldugun ata
                //    insert çak. 
            }
            else if(args.CommandColumn.Type==CommandButtonType.Edit)
            {
                yerineExistProgramId = args.RowData.ProgramId;
                yerineDersKod = args.RowData.Kod;
            }


        }

        private void onChange(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            if ((bool)args.Value == false && yerineExistProgramId.HasValue)
            {
                //ApiResponseDto<MufredatDto> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<MufredatDto>>($"api/Mufredat/GetLastMufredatByProgramId/{yerineExistProgramId}").Result;
                //sonMufredatId = apiResponse.Result.Id;

                yerineDersDialogOpen = true;
            }
        }

        public async Task CommandClickHandlerDers(Syncfusion.Blazor.Grids.CommandClickEventArgs<DersDto> args)
        {
            //Dersid si ile repositoryde kayıdı alıp değiştirip programın aktif dönem müfredatında çakacaz. program göndermeli

            if (args.CommandColumn.Title == "Ders Ekle")
            {
                yerineDersKod = yerineDersKod + "(" + args.RowData.Kod + ")";
                yerineDersDialogOpen = false;
                //ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                //("api/ders", args.RowData);

                ////ApiResponseDto apiResponse = await Http.GetFromJsonAsync<ApiResponseDto>($"api/ders/AddYerineDers/{args.RowData.Id}/{yerineExistProgramId}");


                //try
                //{
                //    if (apiResponse.IsSuccessStatusCode)
                //    {
                //        DersGrid.Refresh();
                //        matToaster.Add("Yerine ders seçildi.", MatToastType.Success, "İşlem başarılı.");
                //        yerineDersDialogOpen = false;
                //    }
                //    else
                //        matToaster.Add(apiResponse.Message, MatToastType.Danger, "Hata oluştu!");
                //}
                //catch (Exception ex)
                //{
                //    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "Hata oluştu!");
                //}
            }
        }

    }
}
