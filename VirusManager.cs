using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirusManager : MonoBehaviour
{
    public ActivityManager activityManager;
    public CameraController cameraController;

    public float virusMultiplier;
    float startVirusMultiplier;

    public float incubationTime;
    public float infectedTime;
    public float immunityTime;

    public int peopleInfected;

    public bool airborne;
    public bool touch;

    public float deathRate;

    public bool[] touchInfectParty;
    public bool[][] touchInfectSchool = new bool[3][];
    public bool[][] touchInfectActivity = new bool[12][];
    public bool[][] touchInfectJob = new bool[11][];
    public bool[][] touchInfectHouse = new bool[38][];

    public bool simulationStarted;
    public Slider sliderVirusMultiplier;
    public Slider sliderIncubationTime;
    public Slider sliderInfectedTime;
    public Slider sliderImmunityTime;
    public Slider sliderDeathRate;
    public Slider sliderTemperature;
    public Slider sliderHumidity;
    public TextMeshProUGUI textVirusMultiplier;
    public TextMeshProUGUI textIncubationTime;
    public TextMeshProUGUI textInfectedTime;
    public TextMeshProUGUI textImmunityTime;
    public TextMeshProUGUI textDeathRate;
    public TextMeshProUGUI textTemperature;
    public TextMeshProUGUI textHumidity;

    public GameObject airborneCheck;
    public GameObject touchCheck;

    public int totalDays;
    public int totalDeaths;
    public int totalPeopleIncubating;
    public int totalPeopleInfected;
    public int totalPeopleImmune;
    public TextMeshProUGUI textTotalDays;
    public TextMeshProUGUI textTotalDeaths;
    public TextMeshProUGUI textTotalPeopleIncubating;
    public TextMeshProUGUI textTotalPeopleInfected;
    public TextMeshProUGUI textTotalPeopleImmune;

    public DayCycle dayCycle;

    public bool useMasks;
    public float maskMultiplier;

    public int dailyDeaths;
    public int dailyInfections;
    public int dailyIncubationAndInfection;
    public int dailyImmune;

    public float virusIncreaseAtLowInfectionRate;
    public bool canCausePanic;

    public bool infiniteImmunity;
    public GameObject immunityCheck;

    public GameObject maskOn;
    public GameObject maskOff;
    public TextMeshProUGUI maskText;

    void Start () {
        startVirusMultiplier = virusMultiplier;

        touchInfectSchool[0] = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
        touchInfectSchool[1] = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
        touchInfectSchool[2] = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};

        touchInfectActivity[0] = new bool[]{false, false, false, false, false};
        touchInfectActivity[1] = new bool[]{false, false, false, false, false, false, false, false, false, false};
        touchInfectActivity[2] = new bool[]{false, false, false, false, false, false, false};
        touchInfectActivity[3] = new bool[]{false, false, false, false, false, false, false};
        touchInfectActivity[4] = new bool[]{false, false, false, false, false};
        touchInfectActivity[5] = new bool[]{false, false, false, false, false, false};
        touchInfectActivity[6] = new bool[]{false, false, false, false};
        touchInfectActivity[7] = new bool[]{false, false, false, false, false, false};

        touchInfectJob[0] = new bool[]{false, false, false, false, false};
        touchInfectJob[1] = new bool[]{false, false, false, false, false};
        touchInfectJob[2] = new bool[]{false, false, false, false, false, false};
        touchInfectJob[3] = new bool[]{false, false, false, false};
        touchInfectJob[4] = new bool[]{false, false, false};
        touchInfectJob[5] = new bool[]{false, false, false, false, false, false, false, false};
        touchInfectJob[6] = new bool[]{false, false, false ,false, false, false, false, false, false, false, false};
        touchInfectJob[7] = new bool[]{false, false, false ,false, false, false, false, false, false, false, false, false, false ,false, false, false, false, false, false, false};
        touchInfectJob[8] = new bool[]{false, false, false ,false, false, false, false, false, false};

        touchInfectHouse[0] = new bool[]{false};
        touchInfectHouse[1] = new bool[]{false};
        touchInfectHouse[2] = new bool[]{false};
        touchInfectHouse[3] = new bool[]{false};
        touchInfectHouse[4] = new bool[]{false};
        touchInfectHouse[5] = new bool[]{false};
        touchInfectHouse[6] = new bool[]{false};
        touchInfectHouse[7] = new bool[]{false, false};
        touchInfectHouse[8] = new bool[]{false, false};
        touchInfectHouse[9] = new bool[]{false, false};
        touchInfectHouse[10] = new bool[]{false, false};
        touchInfectHouse[11] = new bool[]{false, false};
        touchInfectHouse[12] = new bool[]{false, false};
        touchInfectHouse[13] = new bool[]{false, false};
        touchInfectHouse[14] = new bool[]{false, false};
        touchInfectHouse[15] = new bool[]{false, false};
        touchInfectHouse[16] = new bool[]{false, false, false};
        touchInfectHouse[17] = new bool[]{false, false, false};
        touchInfectHouse[18] = new bool[]{false, false, false};
        touchInfectHouse[19] = new bool[]{false, false, false};
        touchInfectHouse[20] = new bool[]{false, false, false};
        touchInfectHouse[21] = new bool[]{false, false, false};
        touchInfectHouse[22] = new bool[]{false, false, false};
        touchInfectHouse[23] = new bool[]{false, false, false};
        touchInfectHouse[24] = new bool[]{false, false, false};
        touchInfectHouse[25] = new bool[]{false, false, false, false};
        touchInfectHouse[26] = new bool[]{false, false, false, false};
        touchInfectHouse[27] = new bool[]{false, false, false, false};
        touchInfectHouse[28] = new bool[]{false, false, false, false};
        touchInfectHouse[29] = new bool[]{false, false, false, false};
        touchInfectHouse[30] = new bool[]{false, false, false, false};
        touchInfectHouse[31] = new bool[]{false, false, false, false};
        touchInfectHouse[32] = new bool[]{false, false, false, false};
        touchInfectHouse[33] = new bool[]{false, false, false, false};
        touchInfectHouse[34] = new bool[]{false, false, false, false};
        touchInfectHouse[35] = new bool[]{false, false, false, false};
        touchInfectHouse[36] = new bool[]{false, false, false, false};
    }

    void Update () {

        if (simulationStarted == false) {
            textVirusMultiplier.text = (Mathf.Round(sliderVirusMultiplier.value * 100.0f) * 0.01f).ToString();
            textIncubationTime.text = (Mathf.Round(sliderIncubationTime.value * 100.0f) * 0.01f).ToString() + " Days";
            textInfectedTime.text = (Mathf.Round(sliderInfectedTime.value * 100.0f) * 0.01f).ToString() + " Days";
            if (infiniteImmunity == false) {
                textImmunityTime.text = (Mathf.Round(sliderImmunityTime.value * 100.0f) * 0.01f).ToString() + " Days";
            } else {
                textImmunityTime.text = "Infinite Immunity";
            }
            textDeathRate.text = (Mathf.Round(sliderDeathRate.value * 100.0f) * 0.01f).ToString() + "%";
            textTemperature.text = (Mathf.Round(sliderTemperature.value * 10.0f) * 0.1f).ToString() + "°C";
            textHumidity.text = (Mathf.Round(sliderHumidity.value * 10.0f) * 0.1f).ToString() + "%";

            Time.timeScale = 0;
        } else {
            if (totalPeopleInfected < 0){
                totalPeopleInfected = 0;
            }
            textTotalDays.text = "Day: " + dayCycle.dayPassed.ToString();
            textTotalPeopleImmune.text = "People Immune: " + totalPeopleImmune.ToString();
            textTotalPeopleIncubating.text = "People Incubating: " + totalPeopleIncubating.ToString();
            textTotalPeopleInfected.text = "People Infected: " + totalPeopleInfected.ToString();
            textTotalDeaths.text = "Total Deaths: " + totalDeaths.ToString();

            if (canCausePanic == true) {
                if (totalPeopleIncubating == 1) {
                    virusIncreaseAtLowInfectionRate = 25;
                } else if (totalPeopleIncubating == 2) {
                    virusIncreaseAtLowInfectionRate = 15;
                } else if (totalPeopleIncubating != 0){
                    virusIncreaseAtLowInfectionRate = 1;
                }
            }

            if (totalPeopleIncubating + totalPeopleInfected > 50) {
                canCausePanic = true;
            }
        }
    }

    public void StartSimulation () {
        simulationStarted = true;
        Invoke("AbleToMoveCamera", 1);

        virusMultiplier = sliderVirusMultiplier.value;
        float temperature = sliderTemperature.value;
        float temperatureBoost = ((((temperature - 20) * -1) / 20) / 10) + 1;
        float humidity = sliderHumidity.value;
        float humidityBoost = ((((humidity - 50) * -1) / 40)/ 10) + 1;
        float originalVirusMultiplierTemp = virusMultiplier * temperatureBoost;
        float originalVirusMultiplierHumi = virusMultiplier * humidityBoost;
        virusMultiplier += (originalVirusMultiplierTemp - virusMultiplier) + (originalVirusMultiplierHumi - virusMultiplier);

        incubationTime = sliderIncubationTime.value;
        infectedTime = sliderInfectedTime.value;
        if (infiniteImmunity == false) {
            immunityTime = sliderImmunityTime.value;
        } else {
            immunityTime = 100000;
        }
        deathRate = sliderDeathRate.value;
    }

    public void AbleToMoveCamera () {
        cameraController.sliderTimeChanging = false;
    }

    public void ChangeAirborne () {
        if (airborne == true && touch == true) {
            airborne = false;
            airborneCheck.SetActive(false);
        } else if (airborne == false) {
            airborne = true;
            airborneCheck.SetActive(true);
        }
    }

    public void ChangeTouch () {
        if (touch == true && airborne == true) {
            touch = false;
            touchCheck.SetActive(false);
        } else if (touch == false) {
            touch = true;
            touchCheck.SetActive(true);
        }
    }

    public void UseMasks () {
        if (useMasks == true) {
            useMasks = false;

            maskOff.SetActive(false);
            maskOn.SetActive(true);
            maskText.text = "Use Masks";

            maskMultiplier = 1;

            activityManager.ActivateMasks(false);
        } else {
            useMasks = true;

            maskOff.SetActive(true);
            maskOn.SetActive(false);
            maskText.text = "Remove Masks";

            maskMultiplier = 0.5f;

            activityManager.ActivateMasks(true);
        }
    }

    public void CovidStats () {
        sliderVirusMultiplier.value = 0.5f;
        sliderIncubationTime.value = 6;
        sliderInfectedTime.value = 7;
        sliderImmunityTime.value = 60;
        sliderDeathRate.value = 3;
        sliderTemperature.value = 20;
        sliderHumidity.value = 50;

        touch = true;
        touchCheck.SetActive(true);
        airborne = true;
        airborneCheck.SetActive(true);
    }

    public void InfiniteImmunity () {
        if (infiniteImmunity == false) {
            infiniteImmunity = true;
            immunityCheck.SetActive(true);
        } else {
            infiniteImmunity = false;
            immunityCheck.SetActive(false);
        }
    }
}