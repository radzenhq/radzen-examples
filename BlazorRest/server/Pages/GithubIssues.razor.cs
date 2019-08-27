using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Blazor.Pages
{
  public class GitHubIssue
  {
    [JsonPropertyName("html_url")]
    public string Url { get; set; }

    public string Title { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime Created { get; set; }
  }

  public partial class GithubIssuesComponent
  {
    [Inject]
    public HttpClient HttpClient { get; set; }

    public async Task<IEnumerable<GitHubIssue>> GetIssues(string repo)
    {
      var message = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/repos/{repo}/issues?state=open&sort=created&direction=desc");
      message.Headers.Add("Accept", "application/vnd.github.v3+json");
      message.Headers.Add("User-Agent", "HttpClient");

      var response = await HttpClient.SendAsync(message);

      response.EnsureSuccessStatusCode();

      var json = await response.Content.ReadAsStringAsync();

      return JsonSerializer.Deserialize<IEnumerable<GitHubIssue>>(json, new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
      });
    }
  }
}
