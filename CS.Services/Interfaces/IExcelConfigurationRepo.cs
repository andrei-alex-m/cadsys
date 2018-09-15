using System;
namespace CS.Services.Interfaces
{
    public interface IExcelConfigurationRepo
    {

        void Clear(int discriminator);
        void ClearAll();
        void Save(int discriminator, string type, string fileName, string[] columnNames);
        string[] Get(int discriminator, string type);


    }
}
