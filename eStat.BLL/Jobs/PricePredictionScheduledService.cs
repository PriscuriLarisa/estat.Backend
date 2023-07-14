using eStat.BLL.MLLogic.Interfaces;
using eStat.BLL.MLLogic.MLHelpers;
using eStat.BLL.MLLogic.MLHelpers.Interfaces;
using Microsoft.Extensions.Hosting;

namespace eStat.BLL.Jobs
{
    public class PricePredictionScheduledService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly int _frequency = 1;
        private readonly IPredicitonDataGenerator _predictionDataGenerator;
        private readonly IPricePredictionHandler _predictionDataHandler;

        public PricePredictionScheduledService(IPredicitonDataGenerator predictionDataGenerator, IPricePredictionHandler predictionDatahandler)
        {
            _predictionDataGenerator = predictionDataGenerator;
            _predictionDataHandler = predictionDatahandler;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(MyTask, null, TimeSpan.Zero,
                TimeSpan.FromHours(_frequency));
            return Task.CompletedTask;
        }

        public async void MyTask(object state)
        {
            //_predictionDataGenerator.GenerateInitialDatasetFromDatabase();
            //_predictionDataGenerator.GenerateData();
            //MLCaller.RunMLPredictionAlgorithm();
            //List<Library.Models.PricePrediction> pricePredictions = PricePredictionCSVReader.GetListOfPricePredictions();
            //_predictionDataHandler.HandlePricePredictions(pricePredictions);
            //MLCaller.RunMLTrainingAlgorithm();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
