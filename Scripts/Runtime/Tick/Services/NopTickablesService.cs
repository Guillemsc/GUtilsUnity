using GUtilsUnity.Tick.Enums;
using GUtilsUnity.Tick.Tickables;

namespace GUtilsUnity.Tick.Services
{
    public sealed class NopTickablesService : ITickablesService
    {
        public static readonly NopTickablesService Instance = new();

        NopTickablesService()
        {

        }

        public void Add(ITickable tickable, TickType tickType)
        {

        }

        public void Remove(ITickable tickable, TickType tickType)
        {

        }

        public void RemoveNow(ITickable tickable, TickType tickType)
        {

        }
    }
}
