using eStat.BLL.MLLogic.Models;

namespace eStat.BLL.MLLogic.Interfaces
{
    public interface IPredicitonDataGenerator
    {
        void GenerateInitialDataset();
        void GenerateInitialDatasetFromDatabase();
        void GenerateData();
    }
}