using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChanger : MonoBehaviour
{
    private SkinnedMeshRenderer renderer; 

    public List<Material> mats = new List<Material>();

    private void Awake()
    {
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void ColorChange()
    {
        renderer.material = mats[Random.Range(0, 3)];   
    }
}
