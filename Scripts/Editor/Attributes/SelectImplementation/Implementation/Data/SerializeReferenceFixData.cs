using System.Collections.Generic;

namespace GUtilsUnity.Attributes.SelectImplementation.Data
{
    public static class SerializeReferenceFixData
    {
        public static readonly HashSet<string> HasUpdated = new();
        public static readonly HashSet<long> ActiveManagedReferences = new();
    }
}
