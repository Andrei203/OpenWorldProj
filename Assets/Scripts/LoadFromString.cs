using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadFromString : MonoBehaviour
{
    public MeshFilter loadTo;
    public string file = "cow.obj";

    public int currentLOD = -1;
    /*private void Start()
    {
        ReadString();
    }*/
    public void UpdateLOD(int lod)
    {
        if (lod != currentLOD)
        {
            currentLOD = lod;
            if (currentLOD >= 0)
                Load();
            else
                Unload();
        }
    }
    
    public void WriteString()
    {
        string path = Application.streamingAssetsPath + "/" + file;

        StreamWriter writer = new StreamWriter(path, true);
        
        writer.WriteLine("Test");
        writer.Close();

        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    [ContextMenu("Load OBJ")]
    public void Load()
    {
        string path = Application.streamingAssetsPath + "/" + file;

        StreamReader reader = new StreamReader(path);
        string line;
        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        while ((line = reader.ReadLine()) != null)
        {
            string[] split = line.Split(' ');
            switch (split[0])
            {
                case "v": // Vertex
                    Vector3 vertex = new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));
                    vertices.Add(vertex);
                    break;
                case "f": // Index
                    List<int> theseIndexes = new List<int>();
                    for (int i = 1; i < split.Length; i++)
                    {
                        theseIndexes.Add(int.Parse(split[i].Split('/')[0]) - 1);
                    }
                    indices.AddRange(Triangulate(theseIndexes));
                    break;
            }
        }
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.RecalculateNormals();
        loadTo.sharedMesh = mesh;

        reader.Close();
    }

    [ContextMenu("Unload OBJ")]
    public void Unload()
    {
        //Mesh mesh = loadTo.sharedMesh;
        loadTo.sharedMesh = null;
        //Resources.UnloadAsset(mesh);
    }

    public List<int> Triangulate(List<int> _indices)
    {
        List<int> triangulated = new List<int>();
        for (int i = 2; i < _indices.Count; i++)
        {
            triangulated.AddRange(new []
            {
                _indices[0],
                _indices[i-1],
                _indices[i]
            });
        }
        return triangulated;
    }

}
