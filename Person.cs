using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    public int state;

    public bool hasVirusAtStart;

    public Material[] mats;

    public Transform player;

    public int age;
    public int ageGroup;

    public Movement movement;
    public ActivityManager activityManager;

    public float infectTime;
    float startInfectTime;

    public DayCycle dayCycle;

    public bool atSchool;
    public bool atWork;
    public bool doingActivity;
    public bool atHouse;
    public bool atParty;
    public bool walkingAround;

    public VirusManager virusManager;

    public float immuneSystemStrength;
    public bool hasChronicDisease;
    public float immuneSystemAdvantage;

    public bool asymptomatic;
    public float timeToCheckVirus;
    float startTimeToCheckVirus;
    public float timeHavingTheVirus;

    public float virusState;
    public float virusInfectedTimeRandom;

    public bool willDie;
    public float deathChance;
    public bool getBetter;

    public float randomDeathSpeed;
    public float randomVirusSpeed;

    public float touchInfectTime;
    float startTouchInfectTime;

    public Transform touchVirusCube;
    public bool stationary;
    public bool stationaryCheck;
    public Rigidbody rb;

    public UIManager uiManager;

    public int peakAge;
    
    public int numberOfPeople = 100;

    public float myRNaught;
    public int timesInfected;
    public float averageVirusMultiplication;
    public float finalAverageVirusMultiplication;

    public GameObject masks;

    public bool startedVirus;

    private float GenerateRandomAge()
    {
        float rand = Random.Range(0f, 1f);
        if (rand <= 0.5f)
        {
            return Mathf.Lerp(0, peakAge, Random.Range(0f, 1f));
        }
        else
        {
            return Mathf.Lerp(peakAge, 100, 1 - Mathf.Sqrt((1 - rand) * 2));
        }
    }

    void Start () {
        infectTime = 2;
        startInfectTime = infectTime;
        timeToCheckVirus = 5;
        startTimeToCheckVirus = timeToCheckVirus;

        startTouchInfectTime = touchInfectTime;

        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();
        player.GetComponent<Renderer>().material = mats[state];

        // Generate a random age
        age = Mathf.RoundToInt(GenerateRandomAge());

        if (age <= 5) {
            ageGroup = 0;
        } else if (age <= 11) {
            ageGroup = 1;
        }else if (age <= 18) {
            ageGroup = 2;
        } else if (age <= 64) {
            ageGroup = 3;
        } else {
            ageGroup = 4;
        }

        
        if (age <= 0) {
            immuneSystemStrength = Random.Range(50, 150);
        } else if (age >= 1 && age <= 5) {
            immuneSystemStrength = Random.Range(100, 300);
        } else if (age >= 6 && age <= 12) {
            immuneSystemStrength = Random.Range(200, 400);
        } else if (age >= 13 && age <= 19) {
            immuneSystemStrength = Random.Range(400, 700);
        } else if (age >= 20 && age <= 30) {
            immuneSystemStrength = Random.Range(300, 600);
        } else if (age >= 31 && age <= 40) {
            immuneSystemStrength = Random.Range(250, 550);
        } else if (age >= 41 && age <= 50) {
            immuneSystemStrength = Random.Range(200, 500);
        } else if (age >= 51 && age <= 65) {
            immuneSystemStrength = Random.Range(150, 400);
        } else if (age >= 66 && age <= 80) {
            immuneSystemStrength = Random.Range(100, 300);
        } else if (age >= 81 && age <= 90) {
            immuneSystemStrength = Random.Range(50, 200);
        } else if (age >= 91) {
            immuneSystemStrength = Random.Range(25, 100);
        }

        if (Random.Range(0, 10) <= 5) {
            hasChronicDisease = true;
            immuneSystemStrength -= 100;

            if (immuneSystemStrength < 0) {
                immuneSystemStrength = 0;
            }

            deathChance = 1 - immuneSystemStrength / 1000;

            deathChance += 0.25f;
        } else {
            deathChance = 1 - immuneSystemStrength / 1000;
        }
        deathChance = deathChance * 1.5f;

        immuneSystemAdvantage = immuneSystemStrength / 1000 + 1;

        if (0 == Random.Range (0, 50) || hasVirusAtStart == true) {
            startedVirus = true;
        }
    }

    void Update () {
        atSchool = ageGroup <= 2 && dayCycle.inSchool == true;
        atWork = ageGroup == 3 && dayCycle.inWork == true;
        doingActivity = movement.doingActivity == true && movement.restingAtHouse == false && movement.isPartying == false;
        atHouse = (dayCycle.inHouse == true && movement.isPartying == false) || movement.restingAtHouse == true;
        atParty = movement.isPartying == true;

        if (atSchool == false && atWork == false && doingActivity == false && atHouse == false && atParty == false) {
            walkingAround = true;
        } else {
            walkingAround = false;
        }


        if (walkingAround == false){
            infectTime -= Time.deltaTime;
            if (infectTime < 0 && state == 0) {
                averageVirusMultiplication = 0;

                float infectChances = SearchForInfectedPeople();
                infectChances = (float)infectChances * virusManager.virusMultiplier - 1;
                bool willBeInfected = false;
 
                if (virusManager.airborne == true && virusManager.touch == true) {
                    if (infectChances >= Random.Range(0, 1000) || CheckForTouchVirus()) {
                        willBeInfected = true;
                        finalAverageVirusMultiplication += averageVirusMultiplication;
                    }
                } else if (virusManager.airborne == true && virusManager.touch == false) {
                    if (infectChances >= Random.Range(0, 1000)) {
                        willBeInfected = true;
                        finalAverageVirusMultiplication += averageVirusMultiplication;
                    }
                } else if (virusManager.airborne == false && virusManager.touch == true) {
                    if (CheckForTouchVirus()) {
                        willBeInfected = true;
                        finalAverageVirusMultiplication += averageVirusMultiplication;
                    }
                }

                if (startedVirus == true) {
                    willBeInfected = true;
                    startedVirus = false;
                }
                
                if (willBeInfected == true) {
                    virusManager.totalPeopleIncubating += 1;
                    virusManager.dailyInfections += 1;
                    AddRNaught();

                    state = 1;
                    timesInfected += 1;

                    if (doingActivity == true) {
                        Debug.Log("Infected at Activity");
                    } else if (atWork == true) {
                        Debug.Log("Infected at Work");
                    } else if (atSchool == true) {
                        Debug.Log("Infected at School");
                    } else if (atHouse == true) {
                        Debug.Log("Infected at House");
                    } else if (atParty == true) {
                        Debug.Log("Infected at Party");
                    }

                    StartCoroutine(StartInfection(Random.Range(virusManager.incubationTime * 0.75f, virusManager.incubationTime * 1.25f)));
                    player.GetComponent<Renderer>().material = mats[1];

                    if (virusManager.deathRate * deathChance >= Random.Range(0, 100)) {
                        willDie = true;
                        randomDeathSpeed = Random.Range(1, 2f);
                    } else {
                        randomVirusSpeed = Random.Range(0.5f, 1.5f);

                        if (200 * immuneSystemAdvantage >= Random.Range(0, 1000)) {
                            asymptomatic = true;
                        }
                    }
                }

                infectTime = startInfectTime;
            }


            if (virusManager.touch == true) {
                touchInfectTime -= Time.deltaTime;
                if (walkingAround == false && touchInfectTime < 0 && (state == 1 || state == 2)) {
                    if (virusManager.virusMultiplier * 10 > Random.Range(0, 100)) {
                        AssignTouchVirusBools();
                    }

                    touchInfectTime = startTouchInfectTime;
                }
            }
        }

        if ((timeHavingTheVirus < 0 || virusState < 0) && willDie == true && state != 1) {
            virusManager.totalPeopleInfected -= 1;
            virusManager.totalDeaths += 1;
            virusManager.dailyDeaths += 1;
            activityManager.boolHospitalLocations[movement.lastSpaceAtHospital] = false;
            this.gameObject.SetActive(false);
        }

        timeToCheckVirus -= Time.deltaTime;
        if (state == 2 && willDie == true && timeToCheckVirus < 0) {

            virusState -= (((1000 - immuneSystemStrength) / (virusInfectedTimeRandom * 300)) * 5 * randomDeathSpeed);

            timeToCheckVirus = startTimeToCheckVirus;
        } else if (state == 2 && timeToCheckVirus < 0) {
            if (asymptomatic == false) {
                    float hospitalBoost = 1;
                    if (movement.atHospital == true) {
                        hospitalBoost = 1.5f;
                    }

                    if (getBetter == false) {
                        virusState -= ((1000 - immuneSystemStrength) / (virusInfectedTimeRandom * 300)) * 5 * randomVirusSpeed * hospitalBoost;
                    } else {
                        virusState += ((1000 - immuneSystemStrength) / (virusInfectedTimeRandom * 300)) * 5 * randomVirusSpeed * hospitalBoost;
                    }
            } else {
                if (timeToCheckVirus < 0) {
                    if (0 == Random.Range(0,2)) {
                        virusState += ((1000 - immuneSystemStrength) / (virusInfectedTimeRandom * 300)) * 5  * randomVirusSpeed;
                    } else {
                        virusState -= ((1000 - immuneSystemStrength) / (virusInfectedTimeRandom * 300)) * 5  * randomVirusSpeed;
                    }
                }
            }

            timeToCheckVirus = startTimeToCheckVirus;
        }

        timeHavingTheVirus -= Time.deltaTime;

        if (timeHavingTheVirus < (virusInfectedTimeRandom * 300) / 2) {
            getBetter = true;
        }

        
        if ((rb.velocity.x == 0 && rb.velocity.z == 0) && stationaryCheck == false) {
            stationary = true;
        } else if (rb.velocity.x == 0 && rb.velocity.z == 0) {
            stationary = false;
        }

        if (rb.velocity.x != 0 && rb.velocity.z != 0) {
            stationaryCheck = false;
        }
    }

    public float SearchForInfectedPeople () {
        float infectedChance = 0;

        float totalInfectedPeople = 0;

        float peopleAtActivity = 0;


        if (doingActivity == true) {
            for (int i = 0; i < activityManager.playerMovementActivity[movement.currentActivity].Length; i++) {
                if (activityManager.playerMovementActivity[movement.currentActivity][i] != null) {
                    peopleAtActivity += 1;

                    if (activityManager.playerMovementActivity[movement.currentActivity][i].state == 1 || activityManager.playerMovementActivity[movement.currentActivity][i].state == 2) {
                        totalInfectedPeople += 1;
                    }
                }
            }

            float activityMultiplier = 3;
            if (dayCycle.boolShutDownWork == true || dayCycle.boolShutDownSchool == true) {
                activityMultiplier = 0.75f;
            }

            infectedChance = (((totalInfectedPeople / peopleAtActivity) * activityMultiplier) * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate;
            averageVirusMultiplication += 3;
        } else if (atSchool == true) {
            if (dayCycle.boolShutDownSchool == false) {
            for (int i = 0; i < activityManager.playerMovementSchool[ageGroup].Length; i++) {
                if (activityManager.playerMovementSchool[ageGroup][i] != null) {
                    peopleAtActivity += 1;

                    if (activityManager.playerMovementSchool[ageGroup][i].state == 1 || activityManager.playerMovementSchool[ageGroup][i].state == 2) {
                        totalInfectedPeople += 1;
                    }
                }
            }
            }

            infectedChance = (((totalInfectedPeople / peopleAtActivity) * (2 * 2)) * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate;
            averageVirusMultiplication += (2 * 2);
        } else if (atWork == true) {
            if (dayCycle.boolShutDownWork == false) {
            for (int i = 0; i < activityManager.playerMovementJob[movement.jobGroup].Length; i++) {
                if (activityManager.playerMovementJob[movement.jobGroup][i] != null) {
                    peopleAtActivity += 1;

                    if (activityManager.playerMovementJob[movement.jobGroup][i].state == 1 || activityManager.playerMovementJob[movement.jobGroup][i].state == 2) {
                        totalInfectedPeople += 1;
                    }
                }
            }
            }

            infectedChance = (((totalInfectedPeople / peopleAtActivity) * 0.5f) * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate;
            averageVirusMultiplication += 0.5f;
        } else if (atParty == true) {
            for (int i = 0; i < activityManager.playerMovementParty.Length; i++) {
                if (activityManager.playerMovementParty[i] != null) {
                    peopleAtActivity += 1;

                    if (activityManager.playerMovementParty[i].state == 1 || activityManager.playerMovementParty[i].state == 2) {
                        totalInfectedPeople += 1;
                    }
                }
            }

            float partyMultiplier = 10;
            if (dayCycle.boolShutDownWork == true || dayCycle.boolShutDownSchool == true) {
                partyMultiplier = 2;
            }

            infectedChance = (((totalInfectedPeople / peopleAtActivity) * partyMultiplier) * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate;
            averageVirusMultiplication += 1;
        } else if (atHouse == true) {
            for (int i = 0; i < activityManager.playerMovementHouse[movement.houseInt].Length; i++) {
                if (activityManager.playerMovementHouse[movement.houseInt][i] != null) {
                    peopleAtActivity += 1;

                    if (activityManager.playerMovementHouse[movement.houseInt][i].state == 1 || activityManager.playerMovementHouse[movement.houseInt][i].state == 2) {
                        totalInfectedPeople += 1;
                    }
                }
            }

            float houseMultiplier = 1;
            if (dayCycle.boolShutDownWork == true || dayCycle.boolShutDownSchool == true) {
                houseMultiplier = 0.05f;
            }

            infectedChance = (((totalInfectedPeople / peopleAtActivity) * houseMultiplier) * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate;
            averageVirusMultiplication += 1;
        }

        return infectedChance;
    }

    IEnumerator StartInfection(float timeToIncubate) {
        yield return new WaitForSeconds(timeToIncubate * 300);

        virusInfectedTimeRandom = Random.Range((virusManager.infectedTime * 0.75f) / immuneSystemAdvantage, virusManager.infectedTime * 1.25f);
        StartCoroutine(StartImmunity(virusInfectedTimeRandom));
        state = 2;
        virusManager.totalPeopleIncubating -= 1;
        virusManager.totalPeopleInfected += 1;
        virusState = 500;
        timeHavingTheVirus = virusInfectedTimeRandom * 300;
        player.GetComponent<Renderer>().material = mats[2];
        getBetter = false;
    }

    IEnumerator StartImmunity(float timeToBeInfected) {
        yield return new WaitForSeconds(timeToBeInfected * 300);

        state = 3;
        virusManager.totalPeopleInfected -= 1;
        virusManager.totalPeopleImmune += 1;
        player.GetComponent<Renderer>().material = mats[3];
        StartCoroutine(StartNormal(Random.Range(virusManager.immunityTime * 0.75f * immuneSystemAdvantage, virusManager.immunityTime * 1.25f)));
        asymptomatic = false;
    }

    IEnumerator StartNormal(float timeToBeImmune) {
        yield return new WaitForSeconds(timeToBeImmune * 300);

        state = 0;
        virusState = 500;
        virusManager.totalPeopleImmune -= 1;
        player.GetComponent<Renderer>().material = mats[0];
    }

    public void AssignTouchVirusBools () {
        int activityToCancel = 0;
        int firstNum = 0;
        int secondNum = 0;
        Transform touchVirusCubeGameObject = null;
        bool occupied = false;

        if (doingActivity == true) {
            if (virusManager.touchInfectActivity[movement.currentActivity][movement.currentPlaceInActivity] == false) {
                virusManager.touchInfectActivity[movement.currentActivity][movement.currentPlaceInActivity] = true;

                touchVirusCubeGameObject = Instantiate(touchVirusCube, new Vector3(activityManager.activityLocations[movement.currentActivity][movement.currentPlaceInActivity].position.x , 3, activityManager.activityLocations[movement.currentActivity][movement.currentPlaceInActivity].position.z), new Quaternion(0, 0, 0, 0));
            } else {
                occupied = true;
            }
            activityToCancel = 0;
            firstNum = movement.currentActivity;
            secondNum = movement.currentPlaceInActivity;
        } else if (atSchool == true) {
            if (dayCycle.boolShutDownSchool == false) {
                if (virusManager.touchInfectSchool[ageGroup][movement.randomSchoolSeat] == false) {
                    virusManager.touchInfectSchool[ageGroup][movement.randomSchoolSeat] = true;

                    touchVirusCubeGameObject = Instantiate(touchVirusCube, new Vector3(activityManager.schoolLocations[ageGroup][movement.randomSchoolSeat].position.x , 3, activityManager.schoolLocations[ageGroup][movement.randomSchoolSeat].position.z), new Quaternion(0, 0, 0, 0));
                } else {
                    occupied = true;
                }
                activityToCancel = 1;
                firstNum = ageGroup;
                secondNum = movement.randomSchoolSeat;
            } else {
                virusManager.touchInfectSchool[ageGroup][movement.randomSchoolSeat] = false;
                occupied = true;
            }
        } else if (atWork == true) {
            if (dayCycle.boolShutDownWork == false) {
                if (virusManager.touchInfectJob[movement.jobGroup][movement.currentJobLocation] == false) {
                    virusManager.touchInfectJob[movement.jobGroup][movement.currentJobLocation] = true;

                    touchVirusCubeGameObject = Instantiate(touchVirusCube, new Vector3(activityManager.randomJobLocations[movement.jobGroup][movement.currentJobLocation].position.x , 3, activityManager.randomJobLocations[movement.jobGroup][movement.currentJobLocation].position.z), new Quaternion(0, 0, 0, 0));
                } else {
                    occupied = true;
                }
                activityToCancel = 2;
                firstNum = movement.jobGroup;
                secondNum = movement.currentJobLocation;
            } else {
                virusManager.touchInfectJob[movement.jobGroup][movement.currentJobLocation] = false;
                occupied = true;
            }
        } else if (atParty == true) {
            if (virusManager.touchInfectParty[movement.saveCheckSpaceParty] == false) {
                virusManager.touchInfectParty[movement.saveCheckSpaceParty] = true;

                touchVirusCubeGameObject = Instantiate(touchVirusCube, new Vector3(activityManager.partyLocations[movement.saveCheckSpaceParty].position.x , 3, activityManager.partyLocations[movement.saveCheckSpaceParty].position.z), new Quaternion(0, 0, 0, 0));
            } else {
                occupied = true;
            }
            activityToCancel = 3;
            firstNum = movement.saveCheckSpaceParty;
        } else if (atHouse == true) {
            int placeInDaHouse = 0;
            if (movement.changeHouseLocation == false) {
                placeInDaHouse = movement.locationInHouseInt;
            } else {
                placeInDaHouse = movement.locationInHouseInt;
            }
            if (virusManager.touchInfectHouse[movement.houseInt][placeInDaHouse] == false) {
                virusManager.touchInfectHouse[movement.houseInt][placeInDaHouse] = true;

                touchVirusCubeGameObject = Instantiate(touchVirusCube, new Vector3(activityManager.houseLocations[movement.houseInt][placeInDaHouse].position.x , 3, activityManager.houseLocations[movement.houseInt][placeInDaHouse].position.z), new Quaternion(0, 0, 0, 0));
            } else {
                occupied = true;
            }
            virusManager.touchInfectHouse[movement.houseInt][placeInDaHouse] = true;
            activityToCancel = 4;
            firstNum = movement.houseInt;
            secondNum = placeInDaHouse;
        }
 
        if (occupied == false) {
            StartCoroutine(DestroyTouchVirus(touchVirusCubeGameObject, activityToCancel, firstNum, secondNum));
        }
    }
    IEnumerator DestroyTouchVirus (Transform touchVirusThing, int activityToCancel, int firstNum, int secondNum) {
        yield return new WaitForSeconds(300);

        if (touchVirusThing.gameObject != null) {
            Destroy(touchVirusThing.gameObject);
        }
        if (activityToCancel == 0) {
            virusManager.touchInfectActivity[firstNum][secondNum] = false;
        } else if (activityToCancel == 1) {
            virusManager.touchInfectSchool[firstNum][secondNum] = false;
        } else if (activityToCancel == 2) {
            virusManager.touchInfectJob[firstNum][secondNum] = false;
        } else if (activityToCancel == 3) {
            virusManager.touchInfectParty[firstNum] = false;
        } else if (activityToCancel == 4) {
            virusManager.touchInfectHouse[firstNum][secondNum] = false;
        }
    }

    public bool CheckForTouchVirus () {

            if (doingActivity == true) {
                float activityMultiplier = 3;
                if (virusManager.touchInfectActivity[movement.currentActivity][movement.currentPlaceInActivity] == true && stationary == true) {
                    stationaryCheck = true;

                    if (dayCycle.boolShutDownWork == true || dayCycle.boolShutDownSchool == true) {
                        activityMultiplier = 0.75f;
                    }   

                    if (((virusManager.virusMultiplier * activityMultiplier) * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate >= Random.Range(0, 100)) {
                        return true;
                    }
                }

                averageVirusMultiplication += activityMultiplier;
            } else if (atSchool == true) {
                if (dayCycle.boolShutDownSchool == false) {
                if (virusManager.touchInfectSchool[ageGroup][movement.randomSchoolSeat] == true && stationary == true) {
                    stationaryCheck = true;
                    if (((virusManager.virusMultiplier * (2 * 2)) * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate >= Random.Range(0, 100)) {
                        return true;
                    }
                }
                }

                averageVirusMultiplication += (2 * 2);
            } else if (atWork == true) {
                if (dayCycle.boolShutDownWork == false) {
                if (virusManager.touchInfectJob[movement.jobGroup][movement.currentJobLocation] == true && stationary == true) {
                    stationaryCheck = true;
                    if ((virusManager.virusMultiplier * (0.5f) * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate >= Random.Range(0, 100)) {
                        return true;
                    }
                }
                }

                averageVirusMultiplication += 0.5f;
            } else if (atParty == true) {
                float partyMultiplier = 10;
                if (virusManager.touchInfectParty[movement.saveCheckSpaceParty] == true && stationary == true) {
                    stationaryCheck = true;

                    if (dayCycle.boolShutDownWork == true || dayCycle.boolShutDownSchool == true) {
                        partyMultiplier = 2;
                    }


                    if ((virusManager.virusMultiplier * partyMultiplier * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate >= Random.Range(0, 100)) {
                        return true;
                    }
                }

                averageVirusMultiplication += partyMultiplier;
            } else if (atHouse == true) {
                int placeInDaHouse = 0;
                if (movement.changeHouseLocation == false) {
                    placeInDaHouse = movement.locationInHouseInt;
                } else {
                    placeInDaHouse = movement.fakeLocationInHouseInt;
                }
                if (virusManager.touchInfectHouse[movement.houseInt][placeInDaHouse] == true && stationary == true) {
                    stationaryCheck = true;

                    float houseMultiplier = 1;
                    if (dayCycle.boolShutDownWork == true || dayCycle.boolShutDownSchool == true) {
                        houseMultiplier = 0.01f;
                    }

                    if ((virusManager.virusMultiplier * houseMultiplier * virusManager.maskMultiplier * 10) * virusManager.virusIncreaseAtLowInfectionRate >= Random.Range(0, 100)) {
                        return true;
                    }
                }

                averageVirusMultiplication += 1;
        }

        return false;
    }

    public void SendInfoToShow () {
        uiManager.GetPlayerInfo(gameObject.GetComponent<Person>());
    }

    public void AddRNaught () {
        float totalPeopleToAddRNaught = 0;

        if (atWork == true) {
            for(int i = 0; i < activityManager.playerMovementJob[movement.jobGroup].Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementJob[movement.jobGroup][i] != null) {
                    playerState = activityManager.playerMovementJob[movement.jobGroup][i].state;

                    if (playerState == 1 || playerState == 2) {
                        totalPeopleToAddRNaught += 1;
                    }
                }
            }
            Debug.Log(totalPeopleToAddRNaught);

            for(int i = 0; i < activityManager.playerMovementJob[movement.jobGroup].Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementJob[movement.jobGroup][i] != null) {
                    playerState = activityManager.playerMovementJob[movement.jobGroup][i].state;

                    if (playerState == 1 || playerState == 2) {
                        activityManager.playerMovementJob[movement.jobGroup][i].myRNaught = activityManager.playerMovementJob[movement.jobGroup][i].myRNaught + (float)(1 / totalPeopleToAddRNaught);
                    }
                }
            }
        } else if (atHouse == true) {
            for(int i = 0; i < activityManager.playerMovementHouse[movement.houseInt].Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementHouse[movement.houseInt][i] != null) {
                    playerState = activityManager.playerMovementHouse[movement.houseInt][i].state;

                    if (playerState == 1 || playerState == 2) {
                        totalPeopleToAddRNaught += 1;
                    }
                }
            }

            for (int i = 0; i < activityManager.playerMovementHouse[movement.houseInt].Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementHouse[movement.houseInt][i] != null) {
                    playerState = activityManager.playerMovementHouse[movement.houseInt][i].state;

                    if (playerState == 1 || playerState == 2) {
                        activityManager.playerMovementHouse[movement.houseInt][i].myRNaught = activityManager.playerMovementHouse[movement.houseInt][i].myRNaught + (float)(1 / totalPeopleToAddRNaught);
                    }
                }
            }
        } else if (doingActivity == true) {
            for(int i = 0; i < activityManager.playerMovementActivity[movement.currentActivity].Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementActivity[movement.currentActivity][i] != null) {
                    playerState = activityManager.playerMovementActivity[movement.currentActivity][i].state;

                    if (playerState == 1 || playerState == 2) {
                        totalPeopleToAddRNaught += 1;
                    }
                }
            }

            Debug.Log(totalPeopleToAddRNaught);
            
            for (int i = 0; i < activityManager.playerMovementActivity[movement.currentActivity].Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementActivity[movement.currentActivity][i] != null) {
                    playerState = activityManager.playerMovementActivity[movement.currentActivity][i].state;

                    if (playerState == 1 || playerState == 2) {
                        activityManager.playerMovementActivity[movement.currentActivity][i].myRNaught = activityManager.playerMovementActivity[movement.currentActivity][i].myRNaught + (float)(1 / totalPeopleToAddRNaught);
                    }
                }
            }

        } else if (atSchool == true) {
            for(int i = 0; i < activityManager.playerMovementSchool[ageGroup].Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementSchool[ageGroup][i] != null) {
                    playerState = activityManager.playerMovementSchool[ageGroup][i].state;

                    if (playerState == 1 || playerState == 2) {
                        totalPeopleToAddRNaught += 1;
                    }
                }
            }

            Debug.Log(totalPeopleToAddRNaught);
            
            for (int i = 0; i < activityManager.playerMovementSchool[ageGroup].Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementSchool[ageGroup][i] != null) {
                    playerState = activityManager.playerMovementSchool[ageGroup][i].state;

                    if (playerState == 1 || playerState == 2) {
                        activityManager.playerMovementSchool[ageGroup][i].myRNaught = activityManager.playerMovementSchool[ageGroup][i].myRNaught + (float)(1 / totalPeopleToAddRNaught);
                    }
                }
            }
        } else if (atParty == true) {
            for(int i = 0; i < activityManager.playerMovementParty.Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementParty[i] != null) {
                    playerState = activityManager.playerMovementParty[i].state;

                    if (playerState == 1 || playerState == 2) {
                        totalPeopleToAddRNaught += 1;
                    }
                }
            }

            Debug.Log(totalPeopleToAddRNaught);
            
            for (int i = 0; i < activityManager.playerMovementParty.Length; i++) {
                int playerState = 0;
                if (activityManager.playerMovementParty[i] != null) {
                    playerState = activityManager.playerMovementParty[i].state;

                    if (playerState == 1 || playerState == 2) {
                        activityManager.playerMovementParty[i].myRNaught = activityManager.playerMovementParty[i].myRNaught + (float)(1 / totalPeopleToAddRNaught);
                    }
                }
            }
        }
    }
}
