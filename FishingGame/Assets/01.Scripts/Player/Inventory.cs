using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class Inventory : MonoBehaviour
{
    public List<FISH_TYPE> inventory;

    public void Adder(FISH_TYPE fs){
        inventory.Add(fs);
    }

    public void Substract(FISH_TYPE fs){
        inventory.Remove(fs);
    }
}
