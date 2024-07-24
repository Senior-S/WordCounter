using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WordCounter.Models;

namespace WordCounter.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(PhraseModel model)
    {
        if (model.Phrase == null || model.Phrase.Length < 1) return View();

        Dictionary<string, int> splittedWords = model.Phrase.ToLower().Split(' ').Select(CleanWord).GroupBy(c => c.ToLower())
            .Select(c => new KeyValuePair<string, int>(c.Key, c.Count())).OrderByDescending(c => c.Value).ToDictionary();
        model.WordCount = splittedWords;

        return View(model);
    }

    string CleanWord(string input)
    {
        const string pattern = @"[^a-zÀ-ÿ\u00f1]";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(input);

        return new string(input.Where(c => !matches.Any(m => m.Value[0] == c)).ToArray());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
