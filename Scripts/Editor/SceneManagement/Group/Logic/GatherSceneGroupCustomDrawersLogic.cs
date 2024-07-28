using System;
using System.Collections.Generic;
using GUtils.Extensions;
using GUtilsUnity.Extensions;
using GUtilsUnity.SceneManagement.Group.CustomDrawers;
using GUtilsUnity.SceneManagement.Group.Data;

namespace GUtilsUnity.SceneManagement.Group.Logic
{
    public static class GatherSceneGroupCustomDrawersLogic
    {
        public static void Execute(ToolData toolData)
        {
            toolData.SceneGroupCustomDrawer.Clear();

            List<Type> types = ReflectionExtensions.GetInheritedTypes(typeof(ISceneGroupCustomDrawer));

            foreach (Type type in types)
            {
                bool hasDefaultConstructor = type.GetConstructor(Type.EmptyTypes) != null;

                if (!hasDefaultConstructor)
                {
                    continue;
                }

                ISceneGroupCustomDrawer customDrawer = Activator.CreateInstance(type) as ISceneGroupCustomDrawer;

                toolData.SceneGroupCustomDrawer.Add(customDrawer);
            }
        }
    }
}
