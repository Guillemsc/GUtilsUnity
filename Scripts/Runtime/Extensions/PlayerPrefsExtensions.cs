using System;
using GUtils.Extensions;
using UnityEngine;

//FUTURE MAINTAINER: TAKE CARE WHEN UPDATING THIS. MAKE IT RETROCOMPATIBLE OR NOTIFY EVERYONE.

namespace GUtilsUnity.Extensions
{
    public static class PlayerPrefsExtensions
    {
        /// <summary>
        /// Sets a bool value for the preference identified by the given key.
        /// You can use <see cref="PlayerPrefsExtensions.GetBool(string)"/>to retrieve this value.
        /// </summary>
        public static void SetBool(string key, bool value)
        {
            var intState = Convert.ToInt32(value);
            PlayerPrefs.SetInt(key, intState);
        }

        /// <summary>
        /// Returns the value corresponding to key in the preference file if it exists.
        /// </summary>
        public static bool GetBool(string key)
        {
            var intState = PlayerPrefs.GetInt(key);
            return Convert.ToBoolean(intState);
        }

        /// <summary>
        /// Returns the value corresponding to key in the preference file if it exists.
        /// If it does not exist, it uses the default value as return.
        /// </summary>
        public static bool GetBool(string key, bool defaultValue)
        {
            var intState = PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue));
            return Convert.ToBoolean(intState);
        }

        /// <summary>
        /// Sets an enum value for the preference identified by the given key.
        /// You can use <see cref="PlayerPrefsExtensions.GetEnum{T}(string)"/>to retrieve this value.
        /// </summary>
        public static void SetEnum<T>(string key, T value) where T : Enum
        {
            //TODO: If we find a non alloc version, it'd be better
            var intState = Convert.ToInt32(value);
            PlayerPrefs.SetInt(key, intState);
        }

        /// <summary>
        /// Returns the value corresponding to key in the preference file if it exists.
        /// </summary>
        public static T GetEnum<T>(string key) where T : Enum
        {
            //TODO: If we find a non alloc version, it'd be better
            var intState = PlayerPrefs.GetInt(key);
            return (T)Enum.ToObject(typeof(T), intState);
        }

        /// <summary>
        /// Returns the value corresponding to key in the preference file if it exists.
        /// If it does not exist, it uses the default value as return.
        /// </summary>
        public static T GetEnum<T>(string key, T defaultValue) where T : Enum
        {
            //TODO: If we find a non alloc version, it'd be better
            var intState = PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue));
            return (T)Enum.ToObject(typeof(T), intState);
        }

        /// <summary>
        /// Sets a long value for the preference identified by the given key.
        /// You can use <see cref="PlayerPrefsExtensions.GetLong(string)"/>to retrieve this value.
        /// </summary>
        public static void SetLong(string key, long value)
        {
            var stringValue = value.ToString();
            PlayerPrefs.SetString(key, stringValue);
        }

        /// <summary>
        /// Returns the value corresponding to key in the preference file if it exists.
        /// </summary>
        public static long GetLong(string key)
        {
            var stringValue = PlayerPrefs.GetString(key);
            return long.Parse(stringValue);
        }

        /// <summary>
        /// Returns the value corresponding to key in the preference file if it exists.
        /// If it does not exist, it uses the default value as return.
        /// </summary>
        public static long GetLong(string key, long defaultValue)
        {
            var stringValue = PlayerPrefs.GetString(key, defaultValue.ToString());
            return long.Parse(stringValue);
        }

        public static double GetDouble(
            string key)
        {
            var stringValue = PlayerPrefs.GetString(key);
            return double.Parse(stringValue);
        }

        public static double GetDouble(
            string key,
            double defaultValue)
        {
            var stringValue = PlayerPrefs.GetString(key, defaultValue.ToString());
            return double.Parse(stringValue);
        }

        public static void SetDouble(
            string key,
            double value)
        {
            var stringValue = value.ToString();
            PlayerPrefs.SetString(key, stringValue);
        }

        public static void SetDateTime(
            string key,
            DateTime dateTime)
        {
            var timestamp = DateTimeExtensions.DateTimeToUnixTimeStamp(dateTime);
            SetDouble(key, timestamp);
        }

        public static DateTime GetDateTime(
            string key,
            DateTime defaultValue = default)
        {
            var timestamp = GetDouble(key);
            if (timestamp < 0)
                return defaultValue;

            return DateTimeExtensions.UnixTimeStampToDateTime(timestamp);
        }
    }
}
