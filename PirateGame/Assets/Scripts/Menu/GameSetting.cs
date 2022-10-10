using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{   public static GameSetting instance;
    private float gameSessonTime=60f;
    private float enemyTimeToSpawn=5f;
    private void Awake() {
        if(instance==null){
          instance=this;
        }else{
          if(instance!=this){
            Destroy(gameObject);
          }
        }
     DontDestroyOnLoad(gameObject);    
    }
    public void SetGameSessonTime(float seconds){
      gameSessonTime=seconds;
    }
    public void SetenemyTimeToSpawn(float seconds){
      enemyTimeToSpawn=seconds;
    }
    public float GetGameSessonTime(){
      return gameSessonTime;
    }
    public float GetEnemyTimeToSpawn(){
      return enemyTimeToSpawn;
    }
}
