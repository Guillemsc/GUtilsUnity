using System;
using GUtilsUnity.Json.Extensions;
using Newtonsoft.Json;
using UnityEngine;

namespace GUtilsUnity.Json.ScriptableObjects
{
    /// <summary>
    /// Inherit from this class to create a scriptable object that will generate a
    /// Json for you with the data values. This Json can be then copy pasted and used anywhere you want.
    /// </summary>
    /// <typeparam name="TData">The type of the data to be serialized.</typeparam>
    public abstract class JsonGeneratorScriptableObject<TData> : BaseJsonGeneratorScriptableObject
    {
        static readonly JsonSerializerSettings JsonSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        static readonly JsonSerializerSettings JsonWithoutIndentationSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None,
        };

        [SerializeField] TData _data;
        [SerializeField, HideInInspector] string _generatedJson;

        public override void GenerateJson()
        {
            try
            {
                _generatedJson = JsonConvert.SerializeObject(_data, JsonSettings);
            }
            catch (Exception ex)
            {
                _generatedJson = ex.Message;
            }
        }

        public override string GenerateCopyJson(bool indented)
        {
            try
            {
                JsonSerializerSettings settings = indented ? JsonSettings : JsonWithoutIndentationSettings;

                string json = JsonConvert.SerializeObject(_data, settings);

                return JsonExtensions.AddEscapeCharacters(json);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
