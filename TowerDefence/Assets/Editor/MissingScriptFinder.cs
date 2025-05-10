#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;

public class MissingScriptFinder
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    private static void FindMissing()
    {
        Debug.Log("tool");
        GameObject[] allGOs = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var go in allGOs)
        {
            var comps = go.GetComponents<MonoBehaviour>();
            for (int i = 0; i < comps.Length; i++)
            {
                if (comps[i] == null)
                    Debug.Log($"Missing script on \"{go.name}\"", go);
            }
        }
    }
}
#endif