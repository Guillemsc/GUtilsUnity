using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    /// <summary>
    /// Generic Serializable Dictionary for Unity 2020.1 and above.
    /// Simply declare your key/value types and you're good to go - zero boilerplate.
    /// </summary>
    [Serializable]
    [MovedFrom(true, "", sourceAssembly: "JigsawPuzzle.Utils", sourceClassName: "SerializableDictionary")]
    public sealed class SerializableDictionary<TKey, TValue> :
        IDictionary<TKey, TValue>,
        IReadOnlyDictionary<TKey, TValue>,
        ISerializationCallbackReceiver
    {
        // Internal
        [FormerlySerializedAs("list")]
        [SerializeField]
        List<KeyValuePair> _list = new();

        Dictionary<TKey, int> _indexByKey = new();
        Dictionary<TKey, TValue> _dictionary = new();

#pragma warning disable 0414
        [FormerlySerializedAs("keyCollision")]
        [SerializeField, HideInInspector]
        bool _keyCollision;
#pragma warning restore 0414

        [Serializable]
        public struct KeyValuePair
        {
            public TKey Key;
            public TValue Value;

            public KeyValuePair(TKey Key, TValue Value)
            {
                this.Key = Key;
                this.Value = Value;
            }
        }

        public List<KeyValuePair> UnsafeKeyValuePairs => _list;

        // Lists are serialized natively by Unity, no custom implementation needed.
        public void OnBeforeSerialize()
        {
        }

        // Populate dictionary with pairs from _list and flag key-collisions.
        public void OnAfterDeserialize()
        {
            _dictionary.Clear();
            _indexByKey.Clear();
            _keyCollision = false;
            for (int i = 0; i < _list.Count; i++)
            {
                var key = _list[i].Key;
                if (key != null && !ContainsKey(key))
                {
                    _dictionary.Add(key, _list[i].Value);
                    _indexByKey.Add(key, i);
                }
                else
                {
                    _keyCollision = true;
                }
            }
        }

        // IDictionary
        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                _dictionary[key] = value;
                if (_indexByKey.ContainsKey(key))
                {
                    var index = _indexByKey[key];
                    _list[index] = new KeyValuePair(key, value);
                }
                else
                {
                    _list.Add(new KeyValuePair(key, value));
                    _indexByKey.Add(key, _list.Count - 1);
                }
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;

        public ICollection<TKey> Keys => _dictionary.Keys;
        public ICollection<TValue> Values => _dictionary.Values;

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            _list.Add(new KeyValuePair(key, value));
            _indexByKey.Add(key, _list.Count - 1);
        }

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        public bool Remove(TKey key)
        {
            if (_dictionary.Remove(key))
            {
                var index = _indexByKey[key];
                _list.RemoveAt(index);
                UpdateIndexLookup(index);
                _indexByKey.Remove(key);
                return true;
            }

            return false;
        }

        void UpdateIndexLookup(int removedIndex)
        {
            for (int i = removedIndex; i < _list.Count; i++)
            {
                var key = _list[i].Key;
                _indexByKey[key]--;
            }
        }

        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

        // ICollection
        public int Count => _dictionary.Count;
        public bool IsReadOnly { get; set; }

        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            Add(pair.Key, pair.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
            _list.Clear();
            _indexByKey.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            if (_dictionary.TryGetValue(pair.Key, out TValue value))
            {
                return EqualityComparer<TValue>.Default.Equals(value, pair.Value);
            }

            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentException("The array cannot be null.");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            }

            if (array.Length - arrayIndex < _dictionary.Count)
            {
                throw new ArgumentException("The destination array has fewer elements than the collection.");
            }

            foreach (var pair in _dictionary)
            {
                array[arrayIndex] = pair;
                arrayIndex++;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            TValue value;
            if (_dictionary.TryGetValue(pair.Key, out value))
            {
                bool valueMatch = EqualityComparer<TValue>.Default.Equals(value, pair.Value);
                if (valueMatch)
                {
                    return Remove(pair.Key);
                }
            }

            return false;
        }

        // IEnumerable
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
    }
}
