using System;
using System.Collections.Generic;
using System.Linq;
using GUtilsUnity.Serialization.SerializableTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace GUtilsUnity.Repositories
{
    public interface IUpdateKeys
    {
        void UpdateKeysFromValues();
    }

    /// <inheritdoc cref="IKeyValueRepository{T, Y}" />
    [MovedFrom(true, "JigsawPuzzle.Features.Utils.Repositories", "JigsawPuzzle.Utils", "ScriptableObjectKeyValueRepository")]
    public class ScriptableObjectKeyValueRepository<TId, TObject> : ScriptableObject, IKeyValueRepository<TId, TObject>, IUpdateKeys
    {
        public SerializableDictionary<TId, TObject> _values = new ();

        public IEnumerable<KeyValuePair<TId, TObject>> Items => _values;
        public IEnumerable<TId> Keys => _values.Keys;
        public IEnumerable<TObject> Values => _values.Values;

        public int Count => _values.Count;

        public bool Contains(TId key)
        {
            return _values.ContainsKey(key);
        }

        public TObject Get(TId key)
        {
            return _values[key];
        }

        public bool TryGet(TId key, out TObject value)
        {
            return _values.TryGetValue(key, out value);
        }

        public void Add(TId key, TObject value)
        {
            _values.Add(key, value);
        }

        public void Set(TId id, TObject value)
        {
            _values[id] = value;
        }

        public bool Remove(TId key)
        {
            return _values.Remove(key);
        }

        public void Clear()
        {
            _values.Clear();
        }

        public void UpdateKeysFromValues()
        {
            var func = GetKeyFromValue();
            if (func == null)
            {
                UnityEngine.Debug.LogError("Tried to update keys from values, but it is not implemented");
                return;
            }

            var values = _values.UnsafeKeyValuePairs.ToList();
            var initialCount = _values.Count;

            _values.Clear();

            foreach (var value in values)
            {
                TId id = default;
                try
                {
                    id = func.Invoke(value.Value);
                }
                catch {}

                _values.Add(id, value.Value);
            }

            if (_values.Count != initialCount)
            {
                UnityEngine.Debug.LogError("Some values were lost because they had the same ids");
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public virtual Func<TObject, TId> GetKeyFromValue()
        {
            return null;
        }
    }
}
