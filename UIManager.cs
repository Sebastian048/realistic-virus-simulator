using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int virusStateToShow;
    public float playerHealthToShow;

    public TextMeshProUGUI textPersonNumber;
    public TextMeshProUGUI textVirusState;
    public TextMeshProUGUI textPlayerHealth;
    public TextMeshProUGUI textAge;
    public TextMeshProUGUI textImmunitySystem;
    public TextMeshProUGUI textChronicDisease;

    float timer = 1;

    public bool startUpdating;

    public bool followPlayer;
    public Transform player;

    public GameObject helpSection;
    public string[] tittleInfo;
    public string[] infoText;
    public TextMeshProUGUI helpText;
    public TextMeshProUGUI tittleText;

    public TextMeshProUGUI dayOfTheWeekText;
    public string[] daysOfTheWeek;

    public int personAge;
    public float personImmuneSystem;
    public bool personChronicDisease;
    public int personNumber;

    public GameObject playerInfo;
    public GameObject playerInfoOff;

    public Person personScript;

    void Update () {
        if (followPlayer == true && player != null) {
            transform.position = player.position;
        }



        if (startUpdating == true) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                textAge.text = "Age: " + personScript.age.ToString();
                textImmunitySystem.text = "Immune System Strength: " + personScript.immuneSystemStrength.ToString() + "/1000";
                textPersonNumber.text = "Person " + personScript.movement.playerNumber;
           
                if (personScript.hasChronicDisease == true) {
                    textChronicDisease.text = "Has Chronic Disease";
                } else {
                    textChronicDisease.text = "doesn't have Chronic Disease";
                }

                virusStateToShow = personScript.state;
                if (virusStateToShow == 0) {
                    textVirusState.text = "Virus State: " + "Vulnerable";
                } else if (virusStateToShow == 1) {
                    textVirusState.text = "Virus State: " + "Incubating";
                } else if (virusStateToShow == 2) {
                    textVirusState.text = "Virus State: " + "Infected";
                } else if (virusStateToShow == 3) {
                    textVirusState.text = "Virus State: " + "Immune";
                }

                playerHealthToShow = personScript.virusState;
                if (playerHealthToShow >= 500) {
                    textPlayerHealth.text = "Player Health: " + "Good";
                } else if (playerHealthToShow >= 450) {
                    textPlayerHealth.text = "Player Health: " + "Average";
                } else if (playerHealthToShow >= 350) {
                    textPlayerHealth.text = "Player Health: " + "Weak";
                } else if (playerHealthToShow >= 250) {
                    textPlayerHealth.text = "Player Health: " + "Poor";
                } else if (playerHealthToShow >= 150) {
                    textPlayerHealth.text = "Player Health: " + "Debilitated";
                } else if (playerHealthToShow >= 75) {
                    textPlayerHealth.text = "Player Health: " + "Critical";
                } else {
                    textPlayerHealth.text = "Player Health: " + "Dying";
                }

            
            timer = 1;
            }
        }
    }

    public void GetPlayerInfo (Person currentPersonScript) {
        Debug.Log("worked thing");
        playerInfo.SetActive(true);
        playerInfoOff.SetActive(false);

        personScript = currentPersonScript;

        timer = 0;

        startUpdating = true;
    }

    public void HelpButton (int infoType) {
        helpSection.SetActive(true);
        helpText.text = infoText[infoType];
        tittleText.text = tittleInfo[infoType];
    }

    public void SelectDay (int currentDay) {
        dayOfTheWeekText.text = daysOfTheWeek[currentDay];
    }
}
