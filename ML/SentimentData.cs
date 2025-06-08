using Microsoft.ML.Data;

namespace SentimentAnalysisApp.ML
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string? Text { get; set; }

        [LoadColumn(1)]
        public bool Label { get; set; }  // Necesario para el entrenamiento
    }

    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }
        public float Score { get; set; }
    }
}

