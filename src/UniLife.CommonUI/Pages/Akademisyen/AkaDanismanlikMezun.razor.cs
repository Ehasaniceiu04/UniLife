using MatBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UniLife.CommonUI.Extensions;
using UniLife.Shared.Dto;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.Dto.Definitions.Bussines;

namespace UniLife.CommonUI.Pages.Akademisyen
{
    public partial class AkaDanismanlikMezun : ComponentBase
    {
        [Inject]
        public System.Net.Http.HttpClient Http { get; set; }
        [Inject]
        public MatBlazor.IMatToaster matToaster { get; set; }

        Syncfusion.Blazor.Grids.SfGrid<OgrenciDto> mezunGrid;

        string OdataQuery = "odata/ogrencis";
        public Query totalQuery=new Query().Expand(new List<string> { "KayitNeden($select=Id,Ad)", "Danisman($select=Id,Ad)", "Program($select=Id,Ad)" });

        int OgrId;
        bool isUyariOpen;


        void AlDersler(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }
        void MufDurum(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }
        void Transkript(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;

        }
        void MezuniyetTranskript(int ogrId)
        {
            OgrId = ogrId;
            isUyariOpen = true;
        }
    }
}
