using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace BlazorCrmWasm.Pages
{
    public partial class HomeComponent
    {
        [Inject]
        HttpClient Http { get; set; }

        public async Task<Stats> MonthlyStats()
        {
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{UriHelper.BaseUri}api/servermethods/monthlystats")));

            return await response.ReadAsync<Stats>();
        }
    }

    public class Stats
    {
        public DateTime Month { get; set; }
        public decimal Revenue { get; set; }
        public int Opportunities { get; set; }
        public decimal AverageDealSize { get; set; }
        public double Ratio { get; set; }
    }
}
