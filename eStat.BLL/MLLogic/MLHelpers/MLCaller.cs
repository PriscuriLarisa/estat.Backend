using CsvHelper.Configuration;
using CsvHelper;
using eStat.BLL.MLLogic.MLHelpers.Interfaces;
using eStat.DAL.Migrations;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using eStat.BLL.MLLogic.Models;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Dynamic;
using System.Reflection.PortableExecutable;
using eStat.Library.Models;

namespace eStat.BLL.MLLogic.MLHelpers
{
    public static class MLCaller
    {
        private const string TRAIN_SCRIPT_PATH = "..\\eStat.BLL\\MLLogic\\SolutionItems\\Scripts\\price_prediction_train.py";
        private const string PREDICT_SCRIPT_PATH = "..\\eStat.BLL\\MLLogic\\SolutionItems\\Scripts\\price_prediction_predict.py";
        private const string PYTHON_EXE_PATH = "..\\eStat.BLL\\MLLogic\\SolutionItems\\Scripts\\Scripts\\python.exe";

        public static void RunMLPredictionAlgorithm()
        {
            string fileName = PREDICT_SCRIPT_PATH;
            string python = PYTHON_EXE_PATH;

            Process psi = new Process();
            psi.StartInfo = new ProcessStartInfo(python, fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };
            psi.Start();

            string output = psi.StandardOutput.ReadToEnd();
            string error = psi.StandardError.ReadToEnd();
            Console.WriteLine(error);

            psi.WaitForExit();
            int result = psi.ExitCode;
        }

        public static void RunMLTrainingAlgorithm()
        {
            string fileName = TRAIN_SCRIPT_PATH;
            string python = PYTHON_EXE_PATH;

            Process psi = new Process();
            psi.StartInfo = new ProcessStartInfo(python, fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true
            };
            psi.Start();

            string output = psi.StandardOutput.ReadToEnd();
            string error = psi.StandardError.ReadToEnd();
            Console.WriteLine(error);

            psi.WaitForExit();
            int result = psi.ExitCode;
        }
    }
}
