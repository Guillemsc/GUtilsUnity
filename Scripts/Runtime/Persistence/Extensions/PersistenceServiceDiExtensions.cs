using GUtilsUnity.Di.Builder;
using GUtilsUnity.Di.Container;
using GUtilsUnity.Persistence.Data;
using GUtilsUnity.Persistence.Services;

namespace GUtilsUnity.Persistence.Extensions
{
    public static class PersistenceServiceDiExtensions
    {
        public static IDiBindingActionBuilder<T> FromPersistenceService<T>(
            this IDiBindingBuilder<T> diBindingBuilder
            ) where T : IPersistenceData
        {
            T FromFunction(IDiResolveContainer resolveContainer)
            {
                IPersistenceService persistenceService = resolveContainer.Resolve<IPersistenceService>();
                return persistenceService.Get<T>();
            }

            return diBindingBuilder.FromFunction(FromFunction);
        }
    }
}
