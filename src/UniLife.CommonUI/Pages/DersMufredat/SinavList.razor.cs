using Microsoft.AspNetCore.Components;

namespace UniLife.CommonUI.Pages.DersMufredat
{
    public partial class SinavList : ComponentBase
    {
        [InjectAttribute]
        public System.Net.Http.HttpClient Http { get; set; }
        [InjectAttribute]
        public MatBlazor.IMatToaster matToaster { get; set; }

    }
}
