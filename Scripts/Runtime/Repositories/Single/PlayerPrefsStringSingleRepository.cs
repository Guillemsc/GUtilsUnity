using System;
using UnityEngine;

namespace GUtilsUnity.Repositories
{
    /// <inheritdoc />
    public class PlayerPrefsStringSingleRepository : ISingleRepository<string>
    {
        public string PlayerPrefsKey { get; }

        public bool HasValue => PlayerPrefs.HasKey(PlayerPrefsKey);

        public PlayerPrefsStringSingleRepository(string playerPrefsKey)
        {
            PlayerPrefsKey = playerPrefsKey;
        }

        public bool Contains(string obj)
        {
            if (!TryGet(out var storedObj))
            {
                return false;
            }

            return obj.Equals(storedObj);
        }

        public bool TryGet(out string obj)
        {
            if (!HasValue)
            {
                obj = default;
                return false;
            }

            obj = PlayerPrefs.GetString(PlayerPrefsKey);
            return true;
        }

        public string Get()
        {
            return GetUnsafe();
        }

        public string GetUnsafe()
        {
            if (!TryGet(out var value))
            {
                throw new InvalidOperationException($"Player prefs does not have anything on the key {PlayerPrefsKey}");
            }

            return value;
        }

        public void Set(string value)
        {
            PlayerPrefs.SetString(PlayerPrefsKey, value);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
        }
    }
}
