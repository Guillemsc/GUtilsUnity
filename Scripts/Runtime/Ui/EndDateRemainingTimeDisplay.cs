using System;
using GUtilsUnity.Extensions;
using GUtilsUnity.Serialization.SerializableTypes;
using TMPro;
using UnityEngine;

namespace GUtilsUnity.Ui
{
    public class EndDateRemainingTimeDisplay : MonoBehaviour
    {
        public SerializableDateTime EndDateTime;
        public TMP_Text Text;

        void OnEnable()
        {
            SetRemainingTimeString();
        }

        public void Update()
        {
            SetRemainingTimeString();
        }

        void SetRemainingTimeString()
        {
            var currentTime = DateTimeExtensions.GetCurrentDateTimeByKind(EndDateTime.DateTimeKind);
            var delta = EndDateTime - currentTime;
            delta = TimeSpanExtensions.Max(delta, TimeSpan.Zero);
            Text.SetText(delta.ToStringWithMostRelevantPairDHMS());
        }
    }
}
