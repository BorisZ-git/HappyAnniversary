using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Supporting
{
    public static class Utils
    {
        /// <summary>
        /// Compare equals of Layers
        /// </summary>
        /// <param name="layer">collision layer</param>
        /// <param name="layerMask">compare layer</param>
        /// <returns>comparison result</returns>
        public static bool IsInLayer(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}

