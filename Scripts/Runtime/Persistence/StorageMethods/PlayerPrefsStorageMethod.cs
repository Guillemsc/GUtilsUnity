using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.DiscriminatedUnions;
using GUtilsUnity.Optionals;
using GUtilsUnity.Persistence.StorageMethods;
using GUtilsUnity.Types;
using UnityEngine;

namespace GUtilsUnity.Persistence.Methods
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="IStorageMethod"/> where data gets saved as <see cref="PlayerPrefs"/>.
    /// </summary>
    public sealed class PlayerPrefsStorageMethod : IStorageMethod
    {
        public static readonly PlayerPrefsStorageMethod Instance = new();

        PlayerPrefsStorageMethod()
        {

        }

        public Task<Optional<ErrorMessage>> Save(string localPath, string data, CancellationToken cancellationToken)
        {
            PlayerPrefs.SetString(localPath, data);
            PlayerPrefs.Save();

            return Task.FromResult(Optional<ErrorMessage>.None);
        }

        public Task<OneOf<string, ErrorMessage>> Load(string localPath, CancellationToken cancellationToken)
        {
            bool found = PlayerPrefs.HasKey(localPath);

            if (!found)
            {
                OneOf<string, ErrorMessage> oneOfError = new ErrorMessage(
                    $"Key {localPath} could not be found on PlayerPrefs"
                    );
                return Task.FromResult(oneOfError);
            }

            string finalString = PlayerPrefs.GetString(localPath);
            OneOf<string, ErrorMessage> oneOfResult = finalString;
            return Task.FromResult(oneOfResult);
        }

        public static void ClearAllStoredData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        public static void ClearStoredData(string localPath)
        {
            PlayerPrefs.DeleteKey(localPath);
            PlayerPrefs.Save();
        }
    }
}
