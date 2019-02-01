using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVitals : MonoBehaviour
{
    /// <summary>
    /// This script assumes the player by default weighs around 80kg
    /// </summary>

    public float bodyTemp = 37;         // Celsius
    [SerializeField] float bodyTempLossRate = 0f;

    public float calories = 2000;       // Calories
    [SerializeField] float caloryLossRate = 0f;

    public float heartRate = 80;          // Beats per minute

    public float hydration = 60000;       // Mililiter
    [SerializeField] float hydrationLossRate = 0f;

    //public float fatigue;               // Percentages

    public float insulation;            // Percentages


    private void Start()
    {
        InvokeRepeating("HungerCalculation", 0, 1f);
        InvokeRepeating("BodyTemperatureCalculation", 0, 1f);
        InvokeRepeating("HydrationCalculation", 0, 1f);
    }

    void Update()
    {
        calories -= (caloryLossRate * Time.deltaTime);
        bodyTemp -= (bodyTempLossRate * Time.deltaTime);
        hydration -= (hydrationLossRate * Time.deltaTime);
    }

    private void HungerCalculation()
    {
        Debug.Log("Updating hunger");

        #region Body Temperature Loss Calculation
        float a = 0;
        if (bodyTemp > 31.5f && bodyTemp <= 37f)
            a = 0.025f;
        else if (bodyTemp > 26f && bodyTemp <= 31.5f)
            a = 0.039f;
        else if (bodyTemp > 19f && bodyTemp <= 26f)
            a = 0.053f;
        else if (bodyTemp <= 19f)
            a = 0.064f;
        #endregion

        #region Calory-count Loss Calculation
        float b = 0;
        if (calories > 1200 && calories <= 2000)
            b = 0f;
        else if (calories > 600 && calories <= 1200)
            b = 0.014f;
        else if (calories > 0 && calories <= 600)
            b = 0.028f;
        else if (calories == 0f)
            b = 0.042f;

        #endregion

        #region Formulas

        float c = (0.08f * (heartRate / 60));   //Heartrate calory decrease

        #endregion

        caloryLossRate = a + b + c;
    }

    private void BodyTemperatureCalculation()
    {
        Debug.Log("Updating Body Temperature");

        #region Formulas

        float a = (0.0011f * (heartRate / 60));   //Heartrate temperature increase

        #endregion

        float b = 0;
        if (calories <= 0)
            b = 0.15f;

        float c = 0;
        if (WorldStatus.Temperature == 0)
            c = 0.105f;
        else if (WorldStatus.Temperature < 0 && WorldStatus.Temperature >= -10)
            c = 0.14f;
        else if (WorldStatus.Temperature < -10 && WorldStatus.Temperature >= -20)
            c = 0.17f;
        else if (WorldStatus.Temperature < -20 && WorldStatus.Temperature >= -30)
            c = 0.19f;

        float x = ( b + c ) - a;
        bodyTempLossRate = x - (x * (insulation / 100));
    }

    private void HydrationCalculation()
    {
        Debug.Log("Updating Hydration");

        #region Body Temperature Loss Calculation
        float a = 0;
        if (bodyTemp > 31.5f && bodyTemp <= 37f)
            a = 0.27f;
        else if (bodyTemp > 26f && bodyTemp <= 31.5f)
            a = 0.29f;
        else if (bodyTemp > 19f && bodyTemp <= 26f)
            a = 0.32f;
        else if (bodyTemp <= 19f)
            a = 0.35f;
        #endregion

        hydrationLossRate = a;
    }
}