using CsvHelper;
using eStat.BLL.MLLogic.Models;
using System.Globalization;
using System;

namespace eStat.BLL.MLLogic.MLHelpers
{
    public static class CSVWriter
    {
        public static void Write(List<PricePredictionModel> predictionModels, string filePath)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(predictionModels);
        }
    }
}