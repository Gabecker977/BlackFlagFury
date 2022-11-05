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
    private LevelSystem levelSystem; 
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

        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBar(levelSystem.GetExperienceNormalized());
        levelSystem.OnExperienceChanged+=LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged+=LevelSystem_OnLevelChanged;
    }
    private void LevelSystem_OnLevelChanged(object sender,System.EventArgs e){
        SetLevelNumber(levelSystem.GetLevelNumber());
    }
    private void LevelSystem_OnExperienceChanged(object sender,System.EventArgs e){
        SetExperienceBar(levelSystem.GetExperienceNormalized());
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
