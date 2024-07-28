using System.Threading;
using GUtils.Extensions;
using GUtilsUnity.Persistence.Data;

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
