using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject finalScreen;
    public int score{get;set;}
    float timer=60f;
    void Awake(){
        Time.timeScale=1;
    }
    void Start()
    {
        if(GameSetting.instance!=null)
        timer=GameSetting.instance.GetGameSessonTime();
    }

    // Update is called once per frame
    void Update()
    {
       timerText.text=TimeToString(timer);
       timer-=Time.deltaTime;
       if(timer<=0){
        FinalScreen();
        timer=0;
       }
    }
    private string TimeToString(float seconds){
        return string.Format("{00:00}:{01:00}",Mathf.FloorToInt(seconds/60),Mathf.FloorToInt(seconds%60));
    }
    public void FinalScreen(){
        scoreText.text="Total Score: "+score;
        Time.timeScale=0;
        finalScreen.SetActive(true);
        }
     public void Play(){
        Time.timeScale=1;
        SceneManager.LoadScene("Level00");
    }
    public void MainMenu(){
        Time.timeScale=1;
        SceneManager.LoadScene("Menu");
    }
}
