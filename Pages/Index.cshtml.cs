using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartNavigator.Services;
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

    public async Task OnPostAsync()
    {
        if (!string.IsNullOrWhiteSpace(Query))
        {
            Result = await _client.QueryAsync(Query);
        }
    }
}
