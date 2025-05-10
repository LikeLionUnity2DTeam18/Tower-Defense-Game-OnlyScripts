using UnityEngine;

public class MissingScriptRuntimeScanner : MonoBehaviour
{
    void Start()
    {
        var allGOs = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var go in allGOs)
        {
            var comps = go.GetComponents<MonoBehaviour>();
            for (int i = 0; i < comps.Length; i++)
            {
                if (comps[i] == null)
                    Debug.LogWarning($"Missing script on: {GetFullPath(go)}", go);
            }
        }
    }

    string GetFullPath(GameObject go)
    {
        return go.transform.parent == null
            ? go.name
            : GetFullPath(go.transform.parent.gameObject) + "/" + go.name;
    }
}