using UnityEngine;
using UnityEditor;

public class PreventCulledLOD : EditorWindow
{
    [MenuItem("Tools/Prevent LOD Culling (Smart Fix)")]
    public static void FixAllLODCulling()
    {
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        int fixedCount = 0;

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            LODGroup lodGroup = prefab.GetComponentInChildren<LODGroup>();
            if (lodGroup == null) continue;

            LOD[] lods = lodGroup.GetLODs();
            if (lods.Length == 0) continue;

            // שים את הערך הכי נמוך האפשרי ל־LOD האחרון כדי למנוע Culled
            float minThreshold = 0.0001f;
            if (lods[lods.Length - 1].screenRelativeTransitionHeight < minThreshold)
                continue; // כבר תוקן בעבר

            lods[lods.Length - 1].screenRelativeTransitionHeight = minThreshold;

            lodGroup.SetLODs(lods);
            lodGroup.RecalculateBounds();
            EditorUtility.SetDirty(prefab);
            PrefabUtility.SavePrefabAsset(prefab);

            fixedCount++;
        }

        Debug.Log($"✅ Patched {fixedCount} prefabs to prevent LOD culling.");
    }
}
