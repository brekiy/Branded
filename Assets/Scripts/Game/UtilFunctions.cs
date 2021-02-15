using System.Collections;
using UnityEngine;

public static class UtilFunctions {
    public static bool LayerMaskContains(int layerMask, int targetLayer) {
        return layerMask == (layerMask | (1 << targetLayer));
    }
}
