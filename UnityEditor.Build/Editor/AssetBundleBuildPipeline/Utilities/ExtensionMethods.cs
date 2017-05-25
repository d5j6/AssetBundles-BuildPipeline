﻿using System.Collections.Generic;
using UnityEditor.Experimental.Build.AssetBundle;

namespace UnityEditor.Build.Utilities
{
    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty<T> (this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static void Swap<T>(this IList<T> array, int index1, int index2)
        {
            var t = array[index2];
            array[index2] = array[index1];
            array[index1] = t;
        }

        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            var t = array[index2];
            array[index2] = array[index1];
            array[index1] = t;
        }

        public static BuildInput Merge(this BuildInput input, BuildInput input2)
        {
            var inputHashMap = new HashSet<GUID>();

            foreach(var def in input.definitions)
                foreach(var asset in def.explicitAssets)
                    if(!inputHashMap.Contains(asset.asset))
                        inputHashMap.Add(asset.asset);

            var defList = new List<BuildInput.Definition>(input.definitions);

            foreach(var def in input2.definitions)
                foreach(var asset in def.explicitAssets)
                    if(!inputHashMap.Contains(asset.asset))
                        defList.Add(def);


            var mergedInput = new BuildInput();
            mergedInput.definitions = defList.ToArray();

            return mergedInput;
        }
    }
}
