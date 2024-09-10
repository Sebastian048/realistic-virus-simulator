using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayCycle : MonoBehaviour
{
    public LightingManager dayTime;
    public VirusManager virusManager;
    public UIManager uiManager;

    public bool inHouse;
    public bool inSchool;
    public bool inWork;

    public Slider timeSlider;

    public bool resetValues;
    public ActivityManager activityManager;

    public int dayOfTheWeek;
    public bool alreadyUpdatedDay;

    public float sleepTime;

    public bool paused;

    public bool boolShutDownSchool;
    public bool boolShutDownWork;
    public bool boolQuarantine;

    public int dayPassed;
    public bool[] sixHoursPassed;

    public TextMeshProUGUI speedMultiplierText;

    public bool changedDayOfTheWeek;

    public int realDayOfTheWeek;

    public GameObject schoolOn;
    public GameObject schoolOff;
    public TextMeshProUGUI schoolText;
    public GameObject workOn;
    public GameObject workOff;
    public TextMeshProUGUI workText;
    public GameObject quarantineOn;
    public GameObject quarantineOff;
    public TextMeshProUGUI quarantineText;

    public bool realPause;

    void Update () {
        if (dayTime.TimeOfDay < 8 || dayTime.TimeOfDay > 22) {
            inHouse = true;
        } else {
            inHouse = false;
        }

        if (dayTime.TimeOfDay > 8 && dayTime.TimeOfDay < 15) {
            inSchool = true;
        } else {
            inSchool = false;
        }

        if (dayTime.TimeOfDay > 8 && dayTime.TimeOfDay < 17) {
            inWork = true;
        } else {
            inWork = false;
        }

        if (dayOfTheWeek == 5 || dayOfTheWeek == 6) {
            inSchool = false;
            inWork = false;
        }

        if (dayTime.TimeOfDay > 23 && dayTime.TimeOfDay < 24) {
            activityManager.reset = false;
        } else if (dayTime.TimeOfDay > 3 && dayTime.TimeOfDay < 4) {
            activityManager.ResetLocations();
        }

        if ((dayTime.TimeOfDay > 3 && dayTime.TimeOfDay < 4) && alreadyUpdatedDay == false) {
            if (dayOfTheWeek == 6) {
                dayOfTheWeek = 0;
            } else {
                dayOfTheWeek += 1;
            }
            dayPassed += 1;
            //Debug.Log("New day");
            virusManager.dailyIncubationAndInfection = virusManager.totalPeopleIncubating + virusManager.totalPeopleInfected;
            virusManager.dailyImmune = virusManager.totalPeopleImmune;
            Debug.Log("Day " + dayPassed.ToString() + " Deaths: " + virusManager.dailyDeaths.ToString());
            Debug.Log("Day " + dayPassed.ToString() + " Infections: " + virusManager.dailyInfections.ToString());
            Debug.Log("Day " + dayPassed.ToString() + " Incubations and Infection: " + virusManager.dailyIncubationAndInfection.ToString());
            Debug.Log("Day " + dayPassed.ToString() + " Immune: " + virusManager.dailyImmune.ToString());
            virusManager.dailyDeaths = 0;
            virusManager.dailyInfections = 0;
            virusManager.dailyIncubationAndInfection = 0;
            virusManager.dailyImmune = 0;
            alreadyUpdatedDay = true;
        } else if (inHouse == false && alreadyUpdatedDay == true) {
            alreadyUpdatedDay = false;
        }

        if ((dayTime.TimeOfDay > 0 && dayTime.TimeOfDay < 1) && changedDayOfTheWeek == false) {
            if (realDayOfTheWeek == 6) {
                realDayOfTheWeek = 0;
            } else {
                realDayOfTheWeek += 1;
            }

            uiManager.SelectDay(realDayOfTheWeek);

            changedDayOfTheWeek = true;
        }
        if (dayTime.TimeOfDay > 23) {
            changedDayOfTheWeek = false;
        }

        if (paused == true) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = timeSlider.value;
        }

        if (boolQuarantine == true) {
            inWork = false;
            inSchool = false;
            inHouse = true;
        }

        speedMultiplierText.text = (Mathf.Round(timeSlider.value * 100.0f) * 0.01f).ToString() + "x";

        if (sixHoursPassed[0] == false && (dayTime.TimeOfDay > 0 && dayTime.TimeOfDay < 6)) {
            sixHoursPassed[0] = true;
            sixHoursPassed[1] = false;
            activityManager.EndSimulation();
        } else if (sixHoursPassed[1] == false && (dayTime.TimeOfDay > 6 && dayTime.TimeOfDay < 12)) {
            sixHoursPassed[1] = true;
            sixHoursPassed[2] = false;
            activityManager.EndSimulation();
        } else if (sixHoursPassed[2] == false && (dayTime.TimeOfDay > 12 && dayTime.TimeOfDay < 18)) {
            sixHoursPassed[2] = true;
            sixHoursPassed[3] = false;
            activityManager.EndSimulation();
        } else if (sixHoursPassed[3] == false && (dayTime.TimeOfDay > 18 && dayTime.TimeOfDay < 24)) {
            sixHoursPassed[3] = true;
            sixHoursPassed[0] = false;
            activityManager.EndSimulation();
        }
    }

    public void Pause (bool actualPause) {
        if (actualPause == true) {
            if (realPause == false) {
                realPause = true;
            } else {
                realPause = false;
            }
        }

        if (realPause == true && actualPause == true && paused == true) {
            paused = false;
        } else if (paused == false || realPause == false) {
            if (paused == false) {
                paused = true;
            } else if (realPause == false && paused == true) {
                paused = false;
            }
        }
    }

    public void ShutDownSchool () {
        if (boolShutDownSchool == false) {
            boolShutDownSchool = true;
            schoolOff.SetActive(false);
            schoolOn.SetActive(true);
            activityManager.OpenSchools(boolShutDownSchool);
            schoolText.text = "Open Schools";
        } else {
            boolShutDownSchool = false;
            schoolOff.SetActive(true);
            schoolOn.SetActive(false);
            activityManager.OpenSchools(boolShutDownSchool);
            schoolText.text = "Close Schools";
        }
    }

    public void ShutDownWork () {
        if (boolShutDownWork == false) {
            boolShutDownWork = true;
            workOff.SetActive(false);
            workOn.SetActive(true);
            activityManager.OpenJobs(boolShutDownWork);
            workText.text = "Open Jobs";
        } else {
            boolShutDownWork = false;
            workOff.SetActive(true);
            workOn.SetActive(false);
            activityManager.OpenJobs(boolShutDownWork);
            workText.text = "Close Jobs";
        }
    }

    public void Quarantine () {
        if (boolQuarantine == false) {
            boolQuarantine = true;
            quarantineOff.SetActive(false);
            quarantineOn.SetActive(true);
            quarantineText.text = "Lift Quarantine";
        } else {
            boolQuarantine = false;
            quarantineOff.SetActive(true);
            quarantineOn.SetActive(false);
            quarantineText.text = "Quarantine";
        }
    }
}
