using GUtils.Optionals;

namespace GUtilsUnity.Repositories.Extensions
{
    public static class ReadOnlySingleRepositoryExtensions
    {
        public static Optional<TObject> GetOptional<TObject>(this IReadOnlySingleRepository<TObject> repository)
        {
            bool hasValue = repository.TryGet(out TObject value);

            return hasValue ? value : Optional<TObject>.None;
        }
    }
}
