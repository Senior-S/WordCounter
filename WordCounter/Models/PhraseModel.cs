using System.ComponentModel.DataAnnotations;

namespace WordCounter.Models;

public class PhraseModel
{
    [Required]
    public string Phrase { get; set; } = string.Empty;

    public Dictionary<string, int> WordCount { get; set; } = new();
}
