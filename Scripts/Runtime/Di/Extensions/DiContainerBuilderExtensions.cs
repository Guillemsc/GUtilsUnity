using System;
using System.Collections.Generic;
using GUtils.Di.Builder;
using GUtils.Di.Container;
using GUtils.Di.Installers;
using GUtils.Disposing.Disposables;
using UnityEngine;

namespace GUtilsUnity.Di.Extensions
{
    public static class DiContainerBuilderExtensions
    {
        public static void InstallDefaultOrReparent<T>(this IDiContainerBuilder builder, T defaultPrefab, Transform pivot)
            where T : MonoBehaviour, IInstaller
        {
            if (builder.TryGetBinding<T>(out var diBindingActionBuilder))
            {
                builder.WhenBuild(c =>
                {
                    var instance = c.Resolve<T>();
                    instance.transform.SetParent(pivot, false);
                });
            }
            else
            {
                builder.Install(UnityEngine.Object.Instantiate(defaultPrefab, pivot, false));
            }
        }
    }
}
