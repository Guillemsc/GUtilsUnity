using System.Threading;
using GUtilsUnity.Persistence.Data;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.Persistence.Extensions
{
    public static class PersistenceDataExtensions
    {
        public static void SaveAsync(this IPersistenceData persistenceData)
        {
            persistenceData.Save(CancellationToken.None).RunAsync();
        }
    }
}
