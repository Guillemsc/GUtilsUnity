using System.Threading.Tasks;
using GUtilsUnity.TimeSlicing.Awaiting;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Work
{
    public sealed class EditorWorkProgressReporter
    {
        readonly TimeSlicingAwaiter _timeSlicingAwaiter = TimeSlicingAwaiter.FromStarted(TimeSlicingConstants.TargetMsFor120Fps);

        readonly string _workName;
        readonly float _progressPerUnit;
        readonly float _baseProgress;

        public EditorWorkProgressReporter(
            string workName,
            float progressPerUnit,
            float baseProgress
            )
        {
            _workName = workName;
            _progressPerUnit = progressPerUnit;
            _baseProgress = baseProgress;
        }

        public ValueTask Report(string name, float progress)
        {
            float newProgress = _baseProgress + (Mathf.Clamp(progress, 0f, 1f) * _progressPerUnit);

            EditorUtility.DisplayProgressBar(_workName, name, newProgress);

            return _timeSlicingAwaiter.TryTimeSlice();
        }
    }
}
