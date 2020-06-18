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
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{UriHelper.BaseUri}api/servermethods/MonthlyStats")));

            return await response.ReadAsync<Stats>();
        }

        public async Task<IEnumerable<RevenueByCompany>> RevenueByCompany()
        {
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{UriHelper.BaseUri}api/servermethods/RevenueByCompany")));

            return await response.ReadAsync<IEnumerable<RevenueByCompany>>();
        }

        public async Task<IEnumerable<RevenueByEmployee>> RevenueByEmployee()
        {
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{UriHelper.BaseUri}api/servermethods/RevenueByEmployee")));

            return await response.ReadAsync<IEnumerable<RevenueByEmployee>>();
        }

        public async Task<IEnumerable<RevenueByMonth>> RevenueByMonth()
        {
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{UriHelper.BaseUri}api/servermethods/RevenueByMonth")));

            return await response.ReadAsync<IEnumerable<RevenueByMonth>>();
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

    public class RevenueByCompany
    {
        public string Company { get; set; }
        public decimal Revenue { get; set; }
    }

    public class RevenueByEmployee
    {
        public string Employee { get; set; }
        public decimal Revenue { get; set; }
    }

    public class RevenueByMonth
    {
        public DateTime Month { get; set; }
        public decimal Revenue { get; set; }
    }
}
