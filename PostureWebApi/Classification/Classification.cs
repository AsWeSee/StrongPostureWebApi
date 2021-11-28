using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using PostureWebApi.JsonModels;

//public class LearnPointFloat
//{
//    [LoadColumn(0, 13), VectorType(14), ColumnName("Points")]
//    public float[] PointsVector { get; set; }

//    [LoadColumn(14)]
//    public bool isGood { get; set; }
//}
public class LearnPrediction
{

    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }

    public float Probability { get; set; }

    public float Score { get; set; }
}
namespace PostureWebApi.Classification
{
    public class Classification
    {
        private List<LearnPoint> inMemoryCollection;
        private int trainCount = 0;
        private MLContext mlContext;
        private IDataView dataview;
        private PredictionEngine<LearnPoint, LearnPrediction> predictionFunction;

        public Classification()
        {
            Console.WriteLine("Classification start");
            mlContext = new MLContext();

            ResetTrainData();
        }

        public void ResetTrainData()
        {
            Console.WriteLine("ResetTrainData");
            inMemoryCollection = new List<LearnPoint>();
            trainCount = 0;
        }

        public string AddPointsAndReTrain(LearnPoint newPoints)
        {
            Console.WriteLine("AddPointsAndReTrain");
            inMemoryCollection.Add(newPoints);
            if (inMemoryCollection.Count >= 10 * (trainCount + 1))
            {
                Console.WriteLine($"train {trainCount}");
                trainCount++;
                dataview = LoadData(mlContext);
                ITransformer model = BuildAndTrainModel(mlContext, dataview);
                predictionFunction = mlContext.Model.CreatePredictionEngine<LearnPoint, LearnPrediction>(model);

                return "train num " + trainCount;
            }
            else
            {
                return "points added: " + inMemoryCollection.Count;
            }
        }

        IDataView LoadData(MLContext mlContext)
        {
            Console.WriteLine("=============== Load Data ===============");
            return mlContext.Data.LoadFromEnumerable(inMemoryCollection);
        }

        static ITransformer BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet)
        {
            Console.WriteLine("=============== Build Model ===============");
            var estimator = mlContext.Transforms.Concatenate("Features", new[] { "Points" })
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "isGood", featureColumnName: "Features", maximumNumberOfIterations: 100));

            Console.WriteLine("=============== Train Model ===============");
            var model = estimator.Fit(splitTrainSet);
            Console.WriteLine("=============== End of training ===============");

            return model;
        }

        public LearnPrediction Predict(LearnPoint point)
        {
            if (trainCount == 0)
            {
                return new LearnPrediction()
                {
                    Prediction = false,
                    Score = -2
                };
            }
            Console.WriteLine($"Predict . Point {point.Points[0]} {point.Points[1]}");

            LearnPrediction resultPrediction = predictionFunction.Predict(point);
            
            Console.WriteLine();
            Console.WriteLine($"Score: {resultPrediction.Score} | Prediction: {(Convert.ToBoolean(resultPrediction.Prediction) ? "Positive" : "Negative")} | Probability: {resultPrediction.Probability} ");
            

            return resultPrediction;
        }
    }
}
