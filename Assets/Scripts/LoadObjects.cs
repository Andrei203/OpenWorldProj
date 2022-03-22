using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class LoadObjects : MonoBehaviour
{
    [Serializable]
    public struct ObjectInstance
    {
        public string guid;
        public GameObject prefab;
    }
    public string file = "cow.obj";
    public List<ObjectInstance> instances = new List<ObjectInstance>();
    private List<GameObject> features = new List<GameObject>();


    [ContextMenu("Save to file")]
    public void SaveToFile()
    {
        string output = "";
        List<Transform> toDelete = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (PrefabUtility.GetPrefabAssetType(child.gameObject) != PrefabAssetType.NotAPrefab)
            {
                GameObject prefabObject = PrefabUtility.GetCorrespondingObjectFromSource(child.gameObject);
                string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(prefabObject));
                output += $"{guid} {child.position.x} {child.position.y} {child.position.z}\n";
                bool hasInstance = false;
                foreach (ObjectInstance instance in instances)
                {
                    if (instance.guid == guid)
                    {
                        hasInstance = true;
                    }
                }
                if (!hasInstance) instances.Add(new ObjectInstance{guid = guid, prefab = prefabObject});
                toDelete.Add(child);
            }
        }
        StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/" + file);
        writer.Write(output);
        writer.Close();
        foreach (Transform delete in toDelete)
        {
            DestroyImmediate(delete.gameObject);
        }
    }
    
    [ContextMenu("Load from file")]
    public void LoadFromFile()
    {
        string path = Application.streamingAssetsPath + "/" + file;
        if (!File.Exists(path)) return;
        StreamReader reader = new StreamReader(path);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] split = line.Split(' ');
            foreach (ObjectInstance instance in instances)
            {
                if (instance.guid == split[0])
                {
                    Vector3 vertex = new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));
                    GameObject go = null;
                    if (Application.isPlaying)
                    {
                        go = Instantiate(instance.prefab, vertex, Quaternion.identity, transform);
                    }
                    else
                    {
                        go = (GameObject) PrefabUtility.InstantiatePrefab(instance.prefab, transform);
                        go.transform.position = vertex;
                    }
                    features.Add(go);
                    break;
                }
            }
        }
    }
}
