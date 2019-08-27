using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace Blazor.Pages
{
    public partial class GithubIssuesComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

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

        protected override async Task OnInitializedAsync()
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
