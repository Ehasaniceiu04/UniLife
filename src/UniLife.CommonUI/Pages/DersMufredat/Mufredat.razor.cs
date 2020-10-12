using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class Mufredat : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }
        [Inject]
        public AppState appState { get; set; }


        //int? fakulteId;
        //int? bolumId;
        //int? programId;

        int? _filterFakulteId;
        public int? FilterFakulteId
        {
            get => _filterFakulteId;
            set
            {
                Refresh();
                if (_filterFakulteId == value) return;
                _filterFakulteId = value;
            }
        }
        int? _filterBolumId;
        public int? FilterBolumId
        {
            get => _filterBolumId;
            set
            {
                Refresh();
                if (_filterBolumId == value) return;
                _filterBolumId = value;
            }
        }
        int? _filterProgramId;
        public int? FilterProgramId
        {
            get => _filterProgramId;
            set
            {
                Refresh();
                if (_filterProgramId == value) return;
                _filterProgramId = value;
            }
        }

        int? ProgramValueHolder;

        string plazceHolderProgramAd = "";
        string plazceHolderBolumAd = "";
        string plazceHolderFakulteAd = "";

        string OdataQuery = "odata/Mufredats";
        //public Query totalQuery = new Query().AddParams("$expand", "program($select=Id,Ad)");
        public Query totalQuery = new Query().AddParams("$expand", "program($expand=bolum($expand=fakulte($select=Ad,Id);$select=Ad,Id);$select=Ad,Id)");



        SfGrid<MufredatDto> MufredatGrid;

        public MufredatDto mufredatDto { get; set; } = new MufredatDto();

        private List<Object> Toolbaritems = new List<Object>() { "Add", "ColumnChooser", new ItemModel() { Text = "Seçili Mufredatları Aç", TooltipText = "Click", PrefixIcon = "e-icons e-SendToBack", Id = "MufredatDerslerAc" } };

        //bool multiDialogOpen = false;

        private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "900px"};

        bool isUyariOpen;
        string dialogUyariText="";

        //List<DonemDto> donemDtos = new List<DonemDto>();
        //SfDropDownList<int?, DonemDto> DropDonem;

        List<DonemTipDto> donemTipDtos = new List<DonemTipDto>();
        SfDropDownList<int?, DonemTipDto> DropDonemTip;

        int? AçilacakMufredatlarınSecilenDonemTipIdsi;
        bool mufredatAcDialogOpen;

        bool isOpsCoklama;

        MufredatDto gridModelMufredatInfo = new MufredatDto();

        public async Task MultiplyRecord()
        {
            //Todo: Burayı odatalı girde göre yeniden yazcaz<

            //try
            //{

            //    ApiResponseDto apiResponse = await Http.GetFromJsonAsync<ApiResponseDto>("api/mufredat/Cokla/" + mufredatDto.Id);
            //    if (apiResponse.StatusCode == Status200OK)
            //    {
            //        matToaster.Add(apiResponse.Message, MatToastType.Success);
            //        ReadMufredats();
            //    }
            //    else
            //    {
            //        mufredatDtos.Remove(mufredatDto);
            //        MufredatGrid.Refresh();
            //        matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Müfredat Creation Failed");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    mufredatDtos.Remove(mufredatDto);
            //    MufredatGrid.Refresh();
            //    matToaster.Add(ex.Message, MatToastType.Danger, "Müfredat Creation Failed");
            //}
            //finally
            //{
            //    MufredatGrid.Refresh();
            //}
        }

        //Sağ Click için
        public void OnContextMenuClick(ContextMenuClickEventArgs<MufredatDto> args)
        {
            if (args.Item.Id == "copywithheader")
            {
                //this.DefaultGrid.Copy(true);
            }
        }


        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "MufredatDerslerAc")
            {
                //await ReadDonems();
                await ReadDonemTips();

                mufredatAcDialogOpen = true;
            }
        }

        public async Task MufredatiAc()
        {
            try
            {               

                ReqEntityIdWithOtherEntitiesIds reqEntityIdWithOtherEntitiesIds = new ReqEntityIdWithOtherEntitiesIds();
                reqEntityIdWithOtherEntitiesIds.EntityId = (int)AçilacakMufredatlarınSecilenDonemTipIdsi;
                reqEntityIdWithOtherEntitiesIds.OtherEntityIds = (await MufredatGrid.GetSelectedRecords()).Select(x => x.Id).ToList();
                mufredatAcDialogOpen = false;

                if (reqEntityIdWithOtherEntitiesIds.OtherEntityIds.Count()<1)
                {
                    dialogUyariText = "Bir müfredat seçimi yapmalısınız.";
                    isUyariOpen = true;
                    return;
                }

                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/mufredat/CreateDersAcilansByMufredatIds", reqEntityIdWithOtherEntitiesIds);


                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    MufredatGrid.Refresh();
                }
                else
                    matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");

            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }
        }

        public void CommandClickHandler(CommandClickEventArgs<MufredatDto> args)
        {
            try
            {
                if (args.CommandColumn.Title == "Clone")
                {
                    //mufredatDto = args.RowData;
                    //multiDialogOpen = true;

                    isOpsCoklama = true;
                }
                if (args.CommandColumn.Title == "Tanımlı Dersler")
                {

                    ApiResponseDto<MufredatStateDto> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<MufredatStateDto>>($"api/mufredat/GetMufredatState/{args.RowData.Id}").Result;
                    appState.MufredatState = apiResponse.Result;
                    TabDegis.InvokeAsync(1);

                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
            }

        }

        [Parameter]
        public EventCallback<int> TabDegis { get; set; }

        //int? tempFak;
        //int? tempBol;
        //int? tempProg;
        public async Task OnActionBeginHandler(Syncfusion.Blazor.Grids.ActionEventArgs<MufredatDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
            {
                plazceHolderProgramAd = "";
                plazceHolderBolumAd = "";
                plazceHolderFakulteAd = "";
                ProgramValueHolder = null;
                //fakulteId = null;
                //bolumId = null;

            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                //fakulteId = null;
                //bolumId = null;
                plazceHolderProgramAd = args.Data.Program.Ad; // ((UniLife.Shared.Dto.Definitions.ProgramDto)args.ForeignKeyData.Values.FirstOrDefault().FirstOrDefault()).Ad;
                plazceHolderBolumAd = args.Data.Program.Bolum.Ad;
                plazceHolderFakulteAd = args.Data.Program.Bolum.Fakulte.Ad;

                gridModelMufredatInfo.ProgramId = args.Data.Program.Id;
                //tempFak = args.Data.Program.Bolum.Fakulte.Id;
                //tempBol = args.Data.Program.Bolum.Id;
                //tempProg = args.Data.Program.Id;
                //gridModelMufredatInfo.BolumId = args.Data.Program.Bolum.Id;
                //gridModelMufredatInfo.FakulteId = args.Data.Program.Bolum.Fakulte.Id;

                await Task.Delay(100);

            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {

                if (!args.Data.ProgramId.HasValue)
                {
                    args.Data.ProgramId = ProgramValueHolder;
                }


            }

            await ActionCompletedHandler(args);

        }

        public async Task ActionCompletedHandler(ActionEventArgs<MufredatDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                ProgramValueHolder = args.Data.ProgramId;
            }
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Cancel)
            {
                isOpsCoklama = false;
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                args.Data.Program = null;
                if (args.Action == "Edit")
                {
                    if (!args.Data.ProgramId.HasValue)
                    {
                        args.Data.ProgramId = ProgramValueHolder;
                    }
                    if (await Update(args.Data))
                    {

                    MufredatGrid.Refresh();
                    }
                    args.Cancel = true;
                    await MufredatGrid.CloseEdit();
                    isOpsCoklama = false;
                }
                else if (args.Action == "Add")
                {
                    if (await Create(args.Data,false))
                    {

                    MufredatGrid.Refresh();
                    }
                    args.Cancel = true;
                    await MufredatGrid.CloseEdit();
                    isOpsCoklama = false;
                }

            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                if (await Delete(args.Data))
                {

                MufredatGrid.Refresh();
                }
                args.Cancel = true;
                await MufredatGrid.CloseEdit();
                isOpsCoklama = false;
            }
        }

        public async Task<bool> Create(MufredatDto mufredatDto,bool isOpsCoklama)
        {
            try
            {
                ApiResponseDto apiResponse;
                if (!isOpsCoklama)
                {
                    mufredatDto.Program = null;
                     apiResponse = await Http.PostJsonAsync<ApiResponseDto>
                        ("api/mufredat", mufredatDto);
                }
                else
                {
                    mufredatDto.Program = null;
                     apiResponse = await Http.PostJsonAsync<ApiResponseDto>
                        ("api/mufredat/CoklaModified", mufredatDto);
                }

                
                if (apiResponse.IsSuccessStatusCode)
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
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
            }
        }


        public async Task<bool> Update(MufredatDto mufredatDto)
        {
            try
            {

                if (isOpsCoklama)
                {
                    //mufredatDto.Id = 0;
                    return await Create(mufredatDto, isOpsCoklama);
                }
                else
                {
                    mufredatDto.Program = null;
                    ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                        ("api/mufredat", mufredatDto);

                    if (apiResponse.IsSuccessStatusCode)
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

        public async Task<bool> Delete(MufredatDto mufredatDto)
        {
            try
            {
                var apiResponse = await Http.DeleteAsync("api/mufredat/" + mufredatDto.Id);
                if (apiResponse.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("İşlem başarılı", MatToastType.Success);
                    return true;
                }
                else
                {
                    matToaster.Add("İşlem başarısız", MatToastType.Danger);
                    return false;
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                return false;
            }
        }

        //public void QueryCellInfoHandler(QueryCellInfoEventArgs<MufredatDto> args)
        //{
        //    if (args.Data.Durum==1)
        //    {
        //        args.Cell.AddStyle(new string[] { "background-color:#80d192" });
        //    }
        //}

        //public async Task OnRowSelecting(RowSelectingEventArgs<MufredatDto> args)
        //{
        //    if (args.Data.Durum ==1)
        //    {
        //        //args.Cancel = true;
        //        isUyariOpen = true;
        //        dialogUyariText = $"{args.Data.Ad} müfredatının bazı dersleri aktifleştirilmiş. Kırmızı oka basarak 'Tanımlı Derslerine' göz atabilirsiniz";
        //    }
        //}

        //async Task ReadDonems()
        //{
        //    ApiResponseDto<List<DonemDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DonemDto>>>("api/donem/current");

        //    if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
        //    {
        //        donemDtos = apiResponse.Result;
        //    }
        //    else
        //    {
        //        matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Donem getirilirken hata oluştu!");
        //    }
        //}
        async Task ReadDonemTips()
        {
            ApiResponseDto<List<DonemTipDto>> apiResponse = await Http.GetFromJsonAsync<ApiResponseDto<List<DonemTipDto>>>("api/donemtip");

            if (apiResponse.StatusCode == Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
            {
                donemTipDtos = apiResponse.Result;
            }
            else
            {
                matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Donem Tip getirilirken hata oluştu!");
            }
        }


        async Task Refresh()
        {
            totalQuery = new Query().AddParams("$expand", "program($expand=bolum($expand=fakulte($select=Ad,Id);$select=Ad,Id);$select=Ad,Id)");

            if (FilterProgramId.HasValue)
            {
                totalQuery.Where("programId", "equal", FilterProgramId);
            }
            else if (FilterBolumId.HasValue)
            {
                totalQuery.Where("program/bolumId", "equal", FilterBolumId);
            }
            else if (FilterFakulteId.HasValue)
            {
                totalQuery.Where("program/bolum/fakulteId", "equal", FilterFakulteId);
            }

            StateHasChanged();
            await Task.Delay(100);
            MufredatGrid.Refresh();


        }

        public string GetHeader(MufredatDto Value)
        {
            if (Value.Id == 0)
            {
                return "Yeni Müfredat Ekle";
            }
            else
            {
                if (isOpsCoklama)
                {
                    return $"{Value.Ad.ToString()} Müfredatını Çokla!";
                }
                else
                {
                    return $"{Value.Ad.ToString()} Müfredatını Düzenle";
                }                
            }
        }

    }
}
