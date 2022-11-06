using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSystemAnimated 
{
    public event EventHandler OnExperienceChanged; 
    public event EventHandler OnLevelChanged;
    private LevelSystem levelSystem;
    private bool isAnimating;
    private int level;
    private float experience;
    private float experienceToNextLevel;
    private float updateTime;
    private float updateTimeMax=.015f;
    public LevelSystemAnimated(LevelSystem levelSystem){
        SetLevelSystem(levelSystem);
    }
    public void SetLevelSystem(LevelSystem levelSystem){
        this.levelSystem=levelSystem;

        level=levelSystem.GetLevelNumber();
        experience=levelSystem.GetExperience();
        experienceToNextLevel=levelSystem.GetExperienceToNextLevel();

        levelSystem.OnExperienceChanged+=LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged+=LevelSystem_OnLevelChanged;
    }
     private void LevelSystem_OnExperienceChanged(object sender,System.EventArgs e){
        isAnimating=true;
    }
      private void LevelSystem_OnLevelChanged(object sender,System.EventArgs e){
        isAnimating=true;
    }
    public void Update() {
        if(isAnimating){
            updateTime+=Time.deltaTime;
            while(updateTime>updateTimeMax){
                updateTime-=updateTimeMax;
                UpdateAddExperience();
            }
        }
        Debug.Log(level+" "+experience);
    }
    private void UpdateAddExperience(){
        if(level<levelSystem.GetLevelNumber()){
                    AddExperience();
                }else{
                    if(experience<levelSystem.GetExperience()){
                        AddExperience();
                    }else isAnimating=false;
                }
    }
    private void AddExperience(){
        experience++;
        if(experience>=experienceToNextLevel&&!levelSystem.IsMaxLevel()){
            level++;
            experienceToNextLevel*=1.5f;
            experience=0;
            if(OnLevelChanged!=null) OnLevelChanged(this,EventArgs.Empty);
        }
        if(OnExperienceChanged!=null) OnExperienceChanged(this,EventArgs.Empty);
    }
    public int GetLevelNumber(){
        return level;
    }
    public float GetExperienceNormalized(){
    if(levelSystem.IsMaxLevel())
    return 1f;
    return experience/experienceToNextLevel;
   }
}
