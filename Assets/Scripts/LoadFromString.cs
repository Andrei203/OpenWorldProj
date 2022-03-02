using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadFromString : MonoBehaviour
{
    public MeshFilter loadTo;
    public string file = "cow.obj";
    private void Start()
    {
        ReadString();
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

    public void ReadString()
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
                    for (int i = 1; i < split.Length; i++)
                    {
                        indices.Add(int.Parse(split[i]) - 1);
                    }
                    break;
            }
        }
        Debug.Log($"{vertices.Capacity} vertices loaded");
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.RecalculateNormals();
        loadTo.sharedMesh = mesh;

        reader.Close();
    }

}
