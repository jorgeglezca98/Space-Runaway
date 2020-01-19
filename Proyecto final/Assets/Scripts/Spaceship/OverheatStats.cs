using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheatStats
{

    private float attackModeTime = 5f;
    private bool attackMode = false;
    private float playerOverheat = 0f;
    private float maxOverheat = 100f;
    private float overheatWaitTime = 5f;

    private bool isCoolingDown = false;
    private float cooldownStartTime;
    
    // The amount of overheat the weapon produces everytime it shots.
    public float overheatIncrement = 3f;
    // The amount of overheat the weapon cools down everytime it shots.
    public float overheatDecrement = 0.75f;
    // The amount of time in seconds the weapon get disabled when the
    // maximum overheat is achieved.
    public float maxOverheatPenalizationTime = 3f;

    public float getCooldownStartTime()
    {
        return cooldownStartTime;
    }

    public void setCooldownStartTime(float time)
    {
        cooldownStartTime = time;
    }

    public float getOverheat()
    {
        return playerOverheat;
    }

    public float getMaxOverheat()
    {
        return maxOverheat;
    }

    public void setOverheat(float overheat)
    {
        if (overheat > 0f)
            playerOverheat = overheat;
        else
            playerOverheat = 0f;
    }

    public bool isAttacking()
    {
        return attackMode;
    }

    public void setAttackMode(bool attacking)
    {
        attackMode = attacking;
    }

    public float getAttackModeTime()
    {
        return attackModeTime;
    }

    public void setAttackModeTime(float time)
    {
        attackModeTime = time;
    }

    public bool getIsCoolingDown()
    {
        return isCoolingDown;
    }

    public void setIsCoolingDown(bool isCoolingDown)
    {
        this.isCoolingDown = isCoolingDown;
    }
}