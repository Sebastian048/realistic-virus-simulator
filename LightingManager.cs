using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    public float lightRotation;

    [SerializeField, Range(0, 24)] public float TimeOfDay;

    public TextMeshProUGUI timeText;
    public float minutes;
    public float checkMinutesDecimals;
    public float checkMinutes;
    public float replaceDot;


    private void Update()
    {
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime / 12.5f;
            TimeOfDay %= 24; //to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
        
        minutes = TimeOfDay % 1;
        minutes = minutes * 60;
        timeText.text = (Mathf.FloorToInt(TimeOfDay) + (Mathf.Round(minutes) / 100)).ToString();

        checkMinutesDecimals = Mathf.Round(minutes) / 100;
        checkMinutes = float.Parse(timeText.text);
        checkMinutes = checkMinutes % 1;
        checkMinutes = Mathf.Round(checkMinutes * 10.0f) * 0.1f;
        if (checkMinutes == checkMinutesDecimals) {
            timeText.text = timeText.text + "0";
        }

        timeText.text = timeText.text.Replace( ".", ":" ) ;
    }


    private void UpdateLighting(float timePercent)
    {
        DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
    }
}