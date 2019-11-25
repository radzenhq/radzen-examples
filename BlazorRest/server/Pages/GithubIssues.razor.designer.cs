using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Blazor.Pages
{
    public partial class GithubIssuesComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected RadzenGrid<GitHubIssue> grid0;

        IEnumerable<GitHubIssue> _githubIssues;
        protected IEnumerable<GitHubIssue> githubIssues
        {
            get
            {
                return _githubIssues;
            }
            set
            {
                if(_githubIssues != value)
                {
                    _githubIssues = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            Load();
        }

        protected async void Load()
        {
            var getIssuesResult = await GetIssues($"aspnet/AspNetCore");
            githubIssues = getIssuesResult;
        }
    }
}
