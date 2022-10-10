using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private string sceneOnPlay="Level00";
    [SerializeField] private Slider gameSessonTimeSlider;
    [SerializeField] private Slider enemySpawnTimeSlider;
    [SerializeField] private Text gameSessonTimeText;
    [SerializeField] private Text enemySpawnTimeText;
    private GameSetting gameSetting;
    
    // Start is called before the first frame update
    
    void Start()
    {
        gameSetting=FindObjectOfType<GameSetting>();
    }

    // Update is called once per frame
    void Update()
    {
        gameSetting.SetGameSessonTime(gameSessonTimeSlider.value);
        gameSetting.SetenemyTimeToSpawn(enemySpawnTimeSlider.value);

        gameSessonTimeText.text="Game session time "+string.Format("{00:00}:{01:00}",
        Mathf.FloorToInt(gameSessonTimeSlider.value/60),Mathf.FloorToInt(gameSessonTimeSlider.value%60));
        
        enemySpawnTimeText.text="Enemy Spawn time "+string.Format("{00:00}:{01:00}",
        Mathf.FloorToInt(enemySpawnTimeSlider.value/60),Mathf.FloorToInt(enemySpawnTimeSlider.value%60));
        
    }
    public void Play(){
        SceneManager.LoadScene(sceneOnPlay);
    }
}
