using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Data.Core
{
    public interface IDataAccess
    {
        // LoadData: Devuelve una lista de tipo T (ej: List<Room>)
        Task<IEnumerable<T>> LoadData<T>(string storedProcedure, object parameters, string connectionId = "DefaultConnection");

        // SaveData: Ejecuta un comando (Insert, Update, Delete)
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "DefaultConnection");
    }
}