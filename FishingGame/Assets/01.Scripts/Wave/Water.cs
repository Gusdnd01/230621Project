using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Water : MonoBehaviour
{
    private MeshFilter mf;

    private void Awake() {
        mf = GetComponent<MeshFilter>();
    }

    private void Update() {
        Vector3[] vertices = mf.mesh.vertices;
        for(int i = 0; i < vertices.Length; i++){
            vertices[i].y = WaveManager.Instance.GetWaveHeight(transform.position.x + vertices[i].x) + WaveManager.Instance.GetWaveHeight(transform.position.x + vertices[i].z);
        }
        mf.mesh.vertices = vertices;
        mf.mesh.RecalculateNormals();
    }
}
