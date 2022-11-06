using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{   [Header("Timer")]
    [SerializeField] private Text timerText;
    [Header("Score")]
    [SerializeField] private Text scoreText;
    [Header("FinalScreen")]
    [SerializeField] private GameObject finalScreen;
    [Header("Level Window")]
    [SerializeField] private Text levelText;
    [SerializeField] private Slider levelBar;
    [Header("Pause Window")]
    [SerializeField] private GameObject pauseWindow;
    private bool isPaused=false;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated; 
    public int score{get;set;}
    float timer=60f;
    void Awake(){
        Time.timeScale=1;
    }
    void Start()
    {
        if(GameSetting.instance!=null)
        timer=GameSetting.instance.GetGameSessonTime();
       SetExperienceBar(0);
        SetLevelNumber(0);
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
    private void SetExperienceBar(float experience){
        levelBar.value=experience;
    }
    private void SetLevelNumber(int level){
        levelText.text="Level "+(level+1);
    }
    public void SetLevelSystem(LevelSystem newLevelSystem){
        levelSystem=newLevelSystem;
    }
     public void SetLevelSystemAnimated(LevelSystemAnimated newLevelSystem){
        levelSystemAnimated=newLevelSystem;

        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBar(levelSystemAnimated.GetExperienceNormalized());
        levelSystemAnimated.OnExperienceChanged+=LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged+=LevelSystemAnimated_OnLevelChanged;
    }
    private void LevelSystemAnimated_OnLevelChanged(object sender,System.EventArgs e){
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
    }
    private void LevelSystemAnimated_OnExperienceChanged(object sender,System.EventArgs e){
        SetExperienceBar(levelSystemAnimated.GetExperienceNormalized());
    }

    private string TimeToString(float seconds){
        return string.Format("{00:00}:{01:00}",Mathf.FloorToInt(seconds/60),Mathf.FloorToInt(seconds%60));
    }
    public void PauseAction(){
        if(!isPaused){
            Pause();
        }else 
            Resume();
    }
    private void Pause(){
        isPaused=true;
        Time.timeScale=0;
        pauseWindow.SetActive(true);
    }
    private void Resume(){
         isPaused=false;
        Time.timeScale=1;
        pauseWindow.SetActive(false);
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
