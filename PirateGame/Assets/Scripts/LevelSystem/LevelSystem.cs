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

    public LevelSystem(){
        level=0;
        experience=0;
        experienceToNextLevel=100f;
    }
    public void AddExperience(float amount){
        experience+=amount;
        while(experience>=experienceToNextLevel){
            level++;
            experience-=experienceToNextLevel;
            experienceToNextLevel*=1.5f;
            if(OnLevelChanged!=null) OnLevelChanged(this,EventArgs.Empty);
        }
        if(OnExperienceChanged!=null) OnExperienceChanged(this,EventArgs.Empty);
    }
    public int GetLevelNumber(){
        return level;
    }
   public float GetExperienceNormalized(){
    return experience/experienceToNextLevel;
   }
   public float GetExperience(){
    return experience;
   }
   public float GetExperienceToNextLevel(){
    return experienceToNextLevel;
   }
}
