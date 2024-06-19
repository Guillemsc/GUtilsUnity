using System;
using System.Collections.Generic;
using GUtilsUnity.Extensions;
using GUtilsUnity.SceneManagement.Group.CustomDrawers;
using GUtilsUnity.SceneManagement.Group.Data;

namespace GUtilsUnity.SceneManagement.Group.Logic
{
    public static class GatherSceneEntryCustomDrawersLogic
    {
        public static void Execute(ToolData toolData)
        {
            toolData.SceneEntryCustomDrawers.Clear();

            List<Type> types = ReflectionExtensions.GetInheritedTypes(typeof(ISceneEntryCustomDrawer));

            foreach(Type type in types)
            {
                bool hasDefaultConstructor = type.GetConstructor(Type.EmptyTypes) != null;

                if(!hasDefaultConstructor)
                {
                    continue;
                }

                ISceneEntryCustomDrawer customDrawer = Activator.CreateInstance(type) as ISceneEntryCustomDrawer;

                toolData.SceneEntryCustomDrawers.Add(customDrawer);
            }
        }
    }
}
