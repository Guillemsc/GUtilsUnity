using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    [Serializable]
    public struct SerializableGuid : ISerializationCallbackReceiver
    {
        [FormerlySerializedAs("serializedGuid")]
        [SerializeField] string _serializedGuid;
#pragma warning disable 414
        //It is used on an editor
        [SerializeField] bool _locked;
#pragma warning restore 414

        Guid _guid;

        public SerializableGuid(Guid guid)
        {
            _serializedGuid = null;
            _locked = false;
            _guid = guid;
        }

        public override bool Equals(object obj)
        {
            return obj is SerializableGuid guid && _guid.Equals(guid._guid);
        }

        public override int GetHashCode()
        {
            return -1324198676 + _guid.GetHashCode();
        }

        public void OnAfterDeserialize()
        {
            if(!Guid.TryParse(_serializedGuid, out var guid))
            {
                _guid = Guid.Empty;
                UnityEngine.Debug.LogWarning($"Attempted to parse invalid GUID string '{_serializedGuid}'. GUID will set to System.Guid.Empty");
                return;
            }

            _guid = guid;
        }

        public void OnBeforeSerialize()
        {
            _serializedGuid = _guid.ToString();
        }

        public override string ToString() => _guid.ToString();

        public static bool operator ==(SerializableGuid a, SerializableGuid b) => a._guid == b._guid;
        public static bool operator !=(SerializableGuid a, SerializableGuid b) => a._guid != b._guid;
        public static implicit operator SerializableGuid(Guid guid) => new(guid);
        public static implicit operator Guid(SerializableGuid serializable) => serializable._guid;
        public static implicit operator SerializableGuid(string serializedGuid) => new(Guid.Parse(serializedGuid));
        public static implicit operator string(SerializableGuid serializedGuid) => serializedGuid.ToString();
    }
}
