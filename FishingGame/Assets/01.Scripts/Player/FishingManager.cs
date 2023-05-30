using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
public class FishingManager : ActionBase
{
    //[SerializeField] private FishingBlockSO _fishing;
    public List<Data> datas = new List<Data>();

    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject pointer;
    [SerializeField] private Material[] mats;
    private List<GameObject>preList = new List<GameObject>();
    [SerializeField] private float _waitingTime;

    private bool isSuccess = false;

    private int aimCount;
    private int playCount;

    private void Start() {
        Fishing();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            for(int i = 0; i < datas.Count; ++i){
                Destroy(preList[i]);
            }
            preList.Clear();
            Fishing();
        }
    }

    [ContextMenu("Fishing")]
    public void Fishing() 
    {
        StartCoroutine(StartFishing(datas.Count, _waitingTime));
    }

    private IEnumerator StartFishing(int count, float time){
        float percent = 0;
        print("Enter");
        for(int i = 0; i < count; ++i){
            percent = UnityEngine.Random.Range(0f, 100f);

            Data data = new Data(true, datas[i].percent);
            if(percent <= datas[i].percent){
                data.isCorrecting = true;
                datas[i].SetData(data);
            }
            else{
                data.isCorrecting = false;
                datas[i].SetData(data);
            }
        }

        int correctingCnt = 0;
        foreach(Data data in datas){
            if(data.isCorrecting) correctingCnt++;
        }
        if(correctingCnt == 0){
            print($"correct data is null");
            int randNum = Random.Range(0, datas.Count);
            datas[randNum].isCorrecting = true;
        }

        foreach(Data f in datas){
            if(f.isCorrecting){
                aimCount++;
            }
        }

        print("Setting");
        for(int i = 0; i < datas.Count; ++i){
            GameObject obj = Instantiate(prefab, new Vector3(i * 2, 0, 0), Quaternion.identity);
            obj.GetComponent<MeshRenderer>().material = datas[i].isCorrecting == true? mats[0] : mats[1];
            preList.Add(obj);
        }

        yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.H));
        print("game enter");
        for(int i = 0; i < count; ++i){
            print("play Game");
            pointer.transform.position = preList[i].transform.position + Vector3.up;
            if(Input.GetKeyDown(KeyCode.Space)){
                if(datas[i].isCorrecting){
                    playCount++;
                }
                else break;
            }
            yield return new WaitForSeconds(time);
        }

        isSuccess = playCount == aimCount;
        print(isSuccess);
        playCount = aimCount = 0;
    }
}
