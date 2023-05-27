using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{   [SerializeField] private Transform[] spawns;
    [SerializeField] private GameObject enemy;
    [SerializeField,Range(0f,60f)] private float timeToSpawn=10f;
    private bool canSpawn=true;
    // Start is called before the first frame update
    private void Awake() {
        
        foreach (Transform item in spawns)
        {
            item.GetComponent<SpriteRenderer>().enabled=false;
        }
    }
    
    void Start()
    {
        if(GameSetting.instance!=null)
            timeToSpawn=GameSetting.instance.GetEnemyTimeToSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn){
            Instantiate(enemy,spawns[Random.Range(0,spawns.Length)].position,Quaternion.identity).GetComponent<Enemy>().SetRandowType();
            canSpawn=false;
            StartCoroutine(Wait(timeToSpawn));
        }
    }
    private IEnumerator Wait(float _seconds){
        yield return new WaitForSeconds(_seconds);
        canSpawn=true;
    }
}
