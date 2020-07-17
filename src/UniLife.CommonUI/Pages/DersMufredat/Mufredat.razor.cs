using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
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


        int? fakulteId;
        int? bolumId;
        //int? programId;
        int? ProgramValueHolder;

        string plazceHolderProgramAd = "";

        string OdataQuery = "odata/Mufredats";
        public Query totalQuery = new Query().AddParams("$expand", "program($select=Id,Ad)");



        SfGrid<MufredatDto> MufredatGrid;

        public MufredatDto mufredatDto { get; set; } = new MufredatDto(); // Holds Mufredat being actively modified or created
                                                                          //[CascadingParameter]
                                                                          //public MufredatDto SelectedMufredat { get; set; }


        //List<MufredatDto> mufredatDtos = new List<MufredatDto>();
        //List<ProgramDto> programDtos = new List<ProgramDto>();

        private List<Object> Toolbaritems = new List<Object>() { "Add", "Search", "Paste", "Copy", "ColumnChooser", new ItemModel() { Text = "Dersleri Aç", TooltipText = "Click", PrefixIcon = "e-icons e-DoubleArrowRight", Id = "DersleriAc" } };

        bool multiDialogOpen = false;

        private DialogSettings DialogParams = new DialogSettings { MinHeight = "400px", Width = "900px" };


        protected override void OnInitialized()
        {
            //ReadMufredats();
            //ApiResponseDto<List<ProgramDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<ProgramDto>>>("api/program").Result;
            //programDtos = apiResponse.Result;
        }

        //void ReadMufredats()
        //{

        //    ApiResponseDto<List<MufredatDto>> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<List<MufredatDto>>>("api/mufredat").Result;

        //    if (apiResponse.StatusCode == Status200OK)
        //    {
        //        matToaster.Add(apiResponse.Message, MatToastType.Success, "Müfredatlar getirildi");
        //        mufredatDtos = apiResponse.Result;
        //    }
        //    else
        //    {
        //        matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Müfredat bilgisi getirilirken hata oluştu!");
        //    }
        //}

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
            if (args.Item.Id == "DersleriAc")
            {
                try
                {
                    IntEnumarableDto intEnumarableDto = new IntEnumarableDto();
                    intEnumarableDto.EnumerableList = (await MufredatGrid.GetSelectedRecords()).Select(x => x.Id);

                    ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>("api/mufredat/CreateDersAcilansByMufredatIds", intEnumarableDto);


                    if (apiResponse.IsSuccessStatusCode)
                    {
                        matToaster.Add(apiResponse.Message, MatToastType.Success, "İşlem başarılı.");
                    }
                    else
                        matToaster.Add(apiResponse.Message, MatToastType.Danger, "İşlem başarısız!");
                    
                }
                catch (Exception ex)
                {
                    matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger, "İşlem başarısız!");
                }

            }
        }

        public void CommandClickHandler(CommandClickEventArgs<MufredatDto> args)
        {
            //Perform your custom command button click operation here. And also with the value in “args” you can differentiate the buttons, if having multiple custom command buttons.
            try
            {
                if (args.CommandColumn.Title == "Clone")
                {
                    mufredatDto = args.RowData;
                    multiDialogOpen = true;
                }
                if (args.CommandColumn.Title == "DersOlus")
                {

                    ApiResponseDto<MufredatStateDto> apiResponse = Http.GetFromJsonAsync<ApiResponseDto<MufredatStateDto>>($"api/mufredat/GetMufredatState/{args.RowData.Id}").Result;
                    appState.MufredatState = apiResponse.Result;
                    TabDegis.InvokeAsync(1);

                }
            }
            catch (Exception)
            {

            }
            
        }

        [Parameter]
        public EventCallback<int> TabDegis { get; set; }


        public async Task OnActionBeginHandler(Syncfusion.Blazor.Grids.ActionEventArgs<MufredatDto> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
            {
                plazceHolderProgramAd = "";
                ProgramValueHolder = null;
                fakulteId = null;
                bolumId = null;

                //await ActionCompletedHandler(args);
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                fakulteId = null;
                bolumId = null;
                plazceHolderProgramAd = args.Data.Program.Ad; // ((UniLife.Shared.Dto.Definitions.ProgramDto)args.ForeignKeyData.Values.FirstOrDefault().FirstOrDefault()).Ad;

                //await ActionCompletedHandler(args);
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
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                if (args.Action == "Edit")
                {
                    if (!args.Data.ProgramId.HasValue)
                    {
                        args.Data.ProgramId = ProgramValueHolder;
                    }
                    await Update(args.Data);
                    MufredatGrid.Refresh();
                    args.Cancel = true;
                    await MufredatGrid.CloseEdit();
                }
                else if (args.Action == "Add")
                {
                    //args.Data.ProgramId = programId;
                    await Create(args.Data);
                    MufredatGrid.Refresh();
                    args.Cancel = true;
                    await MufredatGrid.CloseEdit();
                }

            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                await Delete(args.Data);
                MufredatGrid.Refresh();
                args.Cancel = true;
                await MufredatGrid.CloseEdit();
            }
        }

        public async Task Create(MufredatDto mufredatDto)
        {
            try
            {
                mufredatDto.Program = null;
                ApiResponseDto apiResponse = await Http.PostJsonAsync<ApiResponseDto>
                    ("api/mufredat", mufredatDto);
                if (apiResponse.IsSuccessStatusCode)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                    var recordedDto = Newtonsoft.Json.JsonConvert.DeserializeObject<MufredatDto>(apiResponse.Result.ToString());

                    //mufredatDtos.FirstOrDefault(x => x.Id == 0).Id = recordedDto.Id;
                    //MufredatGrid.Refresh();
                }
                else
                {
                    //mufredatDtos.Remove(mufredatDto);
                    //MufredatGrid.Refresh();
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Müfredat Creation Failed");
                }
            }
            catch (Exception ex)
            {
                //mufredatDtos.Remove(mufredatDto);
                MufredatGrid.Refresh();
                matToaster.Add(ex.Message, MatToastType.Danger, "Müfredat Creation Failed");
            }
        }


        public async Task Update(MufredatDto mufredatDto)
        {
            //this updates the IsCompleted flag only
            try
            {
                ApiResponseDto apiResponse = await Http.PutJsonAsync<ApiResponseDto>
                    ("api/mufredat", mufredatDto);

                if (!apiResponse.IsError)
                {
                    matToaster.Add(apiResponse.Message, MatToastType.Success);
                }
                else
                {
                    matToaster.Add(apiResponse.Message + " : " + apiResponse.StatusCode, MatToastType.Danger, "Müfredat Save Failed");
                    //update failed gridi boz !
                }
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Müfredat Save Failed");
                //update failed gridi boz !
            }
        }

        public async Task Delete(MufredatDto mufredatDto)
        {
            try
            {
                var response = await Http.DeleteAsync("api/mufredat/" + mufredatDto.Id);
                if (response.StatusCode == (System.Net.HttpStatusCode)Microsoft.AspNetCore.Http.StatusCodes.Status200OK)
                {
                    matToaster.Add("Müfredat Deleted", MatToastType.Success);
                    //mufredatDtos.Remove(mufredatDto);
                }
                else
                {
                    matToaster.Add("Müfredat Delete Failed: " + response.StatusCode, MatToastType.Danger);
                }
                //deleteDialogOpen = false;
            }
            catch (Exception ex)
            {
                matToaster.Add(ex.Message, MatToastType.Danger, "Universite Save Failed");
            }
        }

    }
}
