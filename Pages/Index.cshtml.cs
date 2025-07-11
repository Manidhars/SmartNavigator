using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartNavigator.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly AiClient _client;

    public IndexModel(AiClient client)
    {
        _client = client;
    }

    [BindProperty]
    public string? Query { get; set; }

    public string? Result { get; set; }

    public string? MainUrl { get; set; }

    public List<string>? RelatedUrls { get; set; }

    public async Task OnPostAsync()
    {
        if (!string.IsNullOrWhiteSpace(Query))
        {
            Result = await _client.QueryAsync(Query);

            if (!string.IsNullOrWhiteSpace(Result))
            {
                var matches = Regex.Matches(Result, @"https?://\S+");
                if (matches.Count > 0)
                {
                    MainUrl = matches[0].Value;
                    RelatedUrls = matches.Cast<Match>().Skip(1).Select(m => m.Value).ToList();
                }
            }
        }
    }
}
