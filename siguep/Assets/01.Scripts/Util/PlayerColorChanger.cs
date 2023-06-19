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

    public void ColorChange(int index)
    {
        renderer.material = mats[index];   
    }
}
