using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepTimer
{
    private int stepsTimed = 0;
    private int goalSteps;
    private float totalTime = 0;
    private float currentStepStartTime;
    private float currentStepEndTime;
    public float CurrentStepTime { get => (currentStepEndTime - currentStepStartTime) * 1000; }
    public float Mean { get => totalTime / stepsTimed; }

    public StepTimer(int goalSteps)
    {
        this.goalSteps = goalSteps;
    }

    public void StartStep()
    {
        currentStepStartTime = Time.realtimeSinceStartup;
    }

    public void EndStep()
    {
        currentStepEndTime = Time.realtimeSinceStartup;
        totalTime += CurrentStepTime;
        stepsTimed++;

        if (stepsTimed == goalSteps) LogCurrentMean();
    }

    public void LogCurrentMean()
    {
        Debug.Log($"Steps meassured: {stepsTimed} \n Mean of times: {Mean} ms");
    }
}
