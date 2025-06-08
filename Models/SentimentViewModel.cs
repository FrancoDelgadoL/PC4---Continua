namespace SentimentAnalysisApp.Models
{
    public class SentimentResult
    {
        public string? Opinion { get; set; }
        public string? Prediction { get; set; }
        public float Probability { get; set; }
    }

    public class SentimentViewModel
    {
        public string? NewOpinion { get; set; }
        public List<SentimentResult> Results { get; set; } = new();
    }
}
