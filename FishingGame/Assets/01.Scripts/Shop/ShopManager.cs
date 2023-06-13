using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class ShopManager : MonoBehaviour
{
    public void Buying(){

    }
    public void Selling(FISH_TYPE fs){
        switch(fs){
            case FISH_TYPE.Infected_Mackerel :
                break;
            case FISH_TYPE.Mackerel :
                break;
            case FISH_TYPE.Infected_Salmon :
                break;
            case FISH_TYPE.Salmon :
                break;
            case FISH_TYPE.Infected_Shark :
                break;
            case FISH_TYPE.Shark :
                break;
            default:
                break;
        }
    }
}
