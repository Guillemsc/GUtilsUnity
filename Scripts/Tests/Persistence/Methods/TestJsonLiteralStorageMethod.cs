using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.DiscriminatedUnions;
using GUtilsUnity.Optionals;
using GUtilsUnity.Persistence.StorageMethods;
using GUtilsUnity.Types;

namespace GUtilsUnity.Persistence.Tests.Methods
{
    public sealed class TestJsonLiteralStorageMethod : IStorageMethod
    {
        readonly string _jsonString;

        public TestJsonLiteralStorageMethod(string jsonString)
        {
            _jsonString = jsonString;
        }

        public Task<Optional<ErrorMessage>> Save(string localPath, string data, CancellationToken cancellationToken)
        {
            return Task.FromResult(Optional<ErrorMessage>.None);
        }

        public Task<OneOf<string, ErrorMessage>> Load(string localPath, CancellationToken cancellationToken)
        {
            return Task.FromResult(OneOf<string, ErrorMessage>.Of(_jsonString));
        }
    }
}
