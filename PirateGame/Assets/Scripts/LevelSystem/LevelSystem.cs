using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged; 
    public event EventHandler OnLevelChanged;
    private int level;
    private float experience;
    private float experienceToNextLevel;
    private int maxLevel=0;

    public LevelSystem(){
        level=0;
        experience=0;
        experienceToNextLevel=100f;
    }
    public void AddExperience(float amount){
        if(!IsMaxLevel()){
            experience+=amount;
        while(!IsMaxLevel()&&experience>=experienceToNextLevel){
            level++;
            experience-=experienceToNextLevel;
            experienceToNextLevel*=1.5f;
            if(OnLevelChanged!=null) OnLevelChanged(this,EventArgs.Empty);
            }
        }
        if(OnExperienceChanged!=null) OnExperienceChanged(this,EventArgs.Empty);
    }
    public int GetLevelNumber(){
        return level;
    }
   public float GetExperienceNormalized(){
    if(IsMaxLevel())
        return 1f;

    return experience/experienceToNextLevel;
   }
   public float GetExperience(){
    return experience;
   }
   public float GetExperienceToNextLevel(){
    return experienceToNextLevel;
   }
   public bool IsMaxLevel(){
    if(maxLevel!=0){
        if(level>maxLevel){
            Debug.Log(level);
            return true;
            }
        }
         return false;
    }
    public void SetMaxLevel(int maxLevel){
        this.maxLevel=maxLevel;
    }
}
