using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

[System.Serializable]
public enum ARROW_TYPE{
    UP = 0,
    DOWN,
    LEFT,
    RIGHT,
};
public class FishingManager : ActionBase
{
    [SerializeField] private float time;
    [SerializeField] private int cnt;

    [SerializeField] private Queue<ARROW_TYPE> at = new Queue<ARROW_TYPE>();

    private void Start() {
        Fishing();
    }

    [ContextMenu("Fishing")]
    public void Fishing() 
    {
        StartCoroutine(StartFishing(time));
    }

    KeyCode key;

    private IEnumerator StartFishing(float time){

        for(int i = 0; i < cnt; ++i){
            at.Enqueue((ARROW_TYPE)Random.Range(0,cnt));
        }

        while(at.Count!=0){
            if(Input.GetKeyDown(KeyCode.UpArrow) && at.Peek() == ARROW_TYPE.UP){
                print("up");
                at.Dequeue();
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) && at.Peek() == ARROW_TYPE.DOWN){

                print("down");
                at.Dequeue();
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow) && at.Peek() == ARROW_TYPE.LEFT){

                print("left");
                at.Dequeue();
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow) && at.Peek() == ARROW_TYPE.RIGHT){

                print("right");
                at.Dequeue();
            }
            else{
                continue;
            }
            yield return new WaitForSeconds(time);
            
        }
    }
}
