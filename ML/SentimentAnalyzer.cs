using Microsoft.ML;
using Microsoft.ML.Data;

namespace SentimentAnalysisApp.ML
{
    public class SentimentAnalyzer
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentAnalyzer()
        {
            _mlContext = new MLContext();

            var trainingData = new[]
            {
                new SentimentData { Text = "Me encanta este producto", Label = true },
                new SentimentData { Text = "Este servicio es horrible", Label = false },
                new SentimentData { Text = "Estoy muy satisfecho", Label = true },
                new SentimentData { Text = "Es una pérdida de dinero", Label = false },
                new SentimentData { Text = "Excelente atención", Label = true },
                new SentimentData { Text = "No me gustó para nada", Label = false },
                new SentimentData { Text = "Pésimo, no me gusto para nada", Label = false },
                new SentimentData { Text = "Bien, lindo producto", Label = true },
                new SentimentData { Text = "Mal, feo producto", Label = false }
            };

            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(dataView);

            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        }

        public SentimentPrediction Predict(string text)
        {
            var input = new SentimentData { Text = text };
            return _predictionEngine.Predict(input);
        }
    }
}

