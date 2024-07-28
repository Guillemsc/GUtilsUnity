using GUtils.Loading.Loadables;
using GUtilsUnity.Persistence.Services;
using GUtilsUnity.Saving.Saveables;

namespace GUtilsUnity.Persistence.Data
{
    /// <summary>
    /// Represents an interface for an object that can be used by the <see cref="IPersistenceService"/>
    /// to load and save data from it.
    /// </summary>
    public interface IPersistenceData : ILoadable, ISaveable
    {

    }
}
