using Microsoft.AspNetCore.Mvc;
using SentimentAnalysisApp.ML;
using SentimentAnalysisApp.Models;
using System.Text.Json;

namespace SentimentAnalysisApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly SentimentAnalyzer _analyzer = new();

        [HttpGet]
        public IActionResult Index()
        {
            var model = new SentimentViewModel();

            if (TempData.ContainsKey("History"))
            {
                var json = TempData["History"] as string;
                model.Results = JsonSerializer.Deserialize<List<SentimentResult>>(json) ?? new List<SentimentResult>();
                TempData.Keep("History"); // mantener datos para siguiente petición
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string newOpinion)
        {
            var result = _analyzer.Predict(newOpinion);
            var resultItem = new SentimentResult
            {
                Opinion = newOpinion,
                Prediction = result.Prediction ? "Positiva" : "Negativa",
                Probability = result.Probability
            };

            var history = new List<SentimentResult>();

            if (TempData.ContainsKey("History"))
            {
                var json = TempData["History"] as string;
                history = JsonSerializer.Deserialize<List<SentimentResult>>(json) ?? new List<SentimentResult>();
            }

            history.Insert(0, resultItem); // insertar al inicio
            TempData["History"] = JsonSerializer.Serialize(history);
            TempData.Keep("History");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearHistorial()
        {
            HttpContext.Session.Remove("Historial");

            var model = new SentimentViewModel
            {
                Results = new List<SentimentResult>() // Devuelve una lista vacía explícitamente
            };

            return View("Index", model); // No uses RedirectToAction aquí, porque no pasas modelo
        }
    }
}


