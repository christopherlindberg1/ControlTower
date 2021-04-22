using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    interface ITextFileDataAccess
    {
        T LoadData<T>(string filePath);

        void SaveData<T>(string filePath, T objectToSave);
    }
}
