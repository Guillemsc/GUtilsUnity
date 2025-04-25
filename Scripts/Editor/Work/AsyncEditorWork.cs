using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GUtilsUnity.Extensions;
using UnityEditor;
using GUtils.Extensions;

namespace GUtilsUnity.Work
{
    public sealed class AsyncEditorWork
    {
        public static readonly AsyncEditorWork Instance = new();

        readonly List<Func<EditorWorkProgressReporter, Task>> _workUnits = new();

        public bool Working { get; private set; }

        AsyncEditorWork()
        {

        }

        public void Add(Func<EditorWorkProgressReporter, Task> function)
        {
            if (Working)
            {
                return;
            }

            _workUnits.Add(function);
        }

        public void Add(Action action)
        {
            if (Working)
            {
                return;
            }

            Task Function(EditorWorkProgressReporter progressReporter)
            {
                action.Invoke();
                return Task.CompletedTask;
            }

            _workUnits.Add(Function);
        }

        public void Run(string workName)
        {
            Execute(workName).FireAndForget();
        }

        async Task Execute(string workName)
        {
            if (Working)
            {
                return;
            }

            Working = true;

            float progressPerUnit = GUtils.Extensions.MathExtensions.Divide(1f, _workUnits.Count);

            for (int i = 0; i < _workUnits.Count; ++i)
            {
                Func<EditorWorkProgressReporter, Task> workUnit = _workUnits[i];

                float baseProgress = progressPerUnit * i;

                EditorWorkProgressReporter editorWorkProgressReporter = new(workName, progressPerUnit, baseProgress);

                try
                {
                    await workUnit.Invoke(editorWorkProgressReporter);
                }
                catch (Exception exception)
                {
                    UnityEngine.Debug.LogError(exception);
                    break;
                }

                await Task.Yield();
            }

            await Task.Yield();

            _workUnits.Clear();

            EditorUtility.ClearProgressBar();

            Working = false;
        }
    }
}
