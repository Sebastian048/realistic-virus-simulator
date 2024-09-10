using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public int playerNumber;
    public int jobNumber;
    public int jobGroup;

    public float checkForWayPointTime;
    float startCheckForWayPointTime;

    public NavMeshAgent navMeshAgent;

    public ActivityManager activityManager;
    public float activityTime;
    float startActivityTime;
    public bool doingActivity;
    public int currentActivity;
    public int currentPlaceInActivity;

    public DayCycle dayCycle;
    public LightingManager lightingManager;

    Person person;

    public int randomSchoolSeat;

    public bool alreadyAtSchool;
    public bool alreadyAtWork;
    public bool restingAtHouse;

    public bool isPartying;
    public float partyMoveTime;
    public float startPartyMoveTime;
    public int saveCheckSpaceParty;

    public int saveChangedActivityPlace;

    public int houseInt;
    public int locationInHouseInt;

    public bool atHospital;
    public int lastSpaceAtHospital;

    public int currentJobLocation;

    public bool changeHouseLocation;

    public int fakeHouseInt;
    public int fakeLocationInHouseInt;

    public bool atHomeBecauseOfConditions;

    void Start () {
        activityTime = 5;
        startActivityTime = activityTime;
        partyMoveTime = 5;
        startPartyMoveTime = partyMoveTime;

        person = GetComponent<Person>();

        Physics.IgnoreLayerCollision(6, 6, true);

        playerNumber = activityManager.playerNum;
        activityManager.playerNum += 1;

    }

    void Update () {
        if (person.state != 2 || person.asymptomatic == true) {
            bool canMove = true;

            if (atHomeBecauseOfConditions == true) {
                if (person.ageGroup == 3 && dayCycle.inWork == true) {
                    navMeshAgent.SetDestination(activityManager.houseLocations[houseInt][locationInHouseInt].position);
                    activityManager.playerMovementHouse[houseInt][locationInHouseInt] = GetComponent<Person>();
                    canMove = false;
                } else if (dayCycle.inSchool == true) {
                    navMeshAgent.SetDestination(activityManager.houseLocations[houseInt][locationInHouseInt].position);
                    activityManager.playerMovementHouse[houseInt][locationInHouseInt] = GetComponent<Person>();
                    canMove = false;
                }
            }
            
            if (canMove == true) {
                Move();
            }
        } else {
            if (person.virusState <= 150) {
                AssignHospital();
            } else {
                navMeshAgent.SetDestination(activityManager.houseLocations[houseInt][locationInHouseInt].position);
                activityManager.playerMovementHouse[houseInt][locationInHouseInt] = GetComponent<Person>();
            }
        }

        if (person.virusState > 150 && atHospital == true) {
            activityManager.boolHospitalLocations[lastSpaceAtHospital] = false;
            atHospital = false;
        }
    }

    public void Move () {
        activityTime -= Time.deltaTime;

        if (dayCycle.inHouse == true && isPartying == false) {
            if (changeHouseLocation == false) {
                navMeshAgent.SetDestination(activityManager.houseLocations[houseInt][locationInHouseInt].position);
                activityManager.playerMovementHouse[houseInt][locationInHouseInt] = GetComponent<Person>();
            } else {
                navMeshAgent.SetDestination(activityManager.houseLocations[houseInt][fakeLocationInHouseInt].position);
            }
        }

        if (isPartying == true) {
            partyMoveTime -= Time.deltaTime;

            if (partyMoveTime < 0) {
                partyMoveTime = startPartyMoveTime;

                AssignLocationInActivity();
            }
        }

        if (person.age >= 18 && person.age <= 30 && lightingManager.TimeOfDay >= 22 && isPartying == false && (dayCycle.dayOfTheWeek == 4 || dayCycle.dayOfTheWeek == 5)) {
            AssignLocationInActivity();
        }

        if (((person.age <= 18 && dayCycle.inSchool == true) || (person.age > 18 && person.age <= 64 && dayCycle.inWork == true))) {
            
            if (person.age <= 18) { 
                AssignSchool();     
            } else if (person.age <= 64) {
                if (dayCycle.inWork == true && alreadyAtWork == false) {
                    alreadyAtWork = true;
                    navMeshAgent.SetDestination(activityManager.jobLocations[jobNumber].position);
                }
            }
            

        } else if (dayCycle.inHouse == false && isPartying == false) {

            if (activityTime < 0) {

                if (doingActivity == false) {
                    int randomNum = Random.Range(0, 10);
                    if (randomNum == 0 || randomNum == 1 || randomNum == 2 || randomNum == 6) {
                        if ((dayCycle.boolShutDownWork == true && person.ageGroup == 3)! || (dayCycle.boolShutDownSchool == true && person.ageGroup <= 2)!) {
                            AssignLocationInActivity();
                        }
                    } else if (((randomNum == 3) && person.age <= 18) || ((randomNum == 3 || randomNum == 4) && (person.age > 18 && person.age <= 64)) || ((randomNum == 3 || randomNum == 4 || randomNum == 5) && person.age > 64)) {
                        navMeshAgent.SetDestination(activityManager.houseLocations[houseInt][locationInHouseInt].position);
                        doingActivity = true;
                        restingAtHouse = true;
                        activityTime = startActivityTime * 10;
                        activityManager.playerMovementActivity[currentActivity][saveChangedActivityPlace] = null;
                        activityManager.playerMovementHouse[houseInt][locationInHouseInt] = GetComponent<Person>();
                        StartCoroutine(DoingActivityBool(startActivityTime * 10));
                    } else {
                        //if there is no activity
                        activityTime = startActivityTime;
                    }
                }
            }   


        checkForWayPointTime -= Time.deltaTime;

        if (doingActivity == false && checkForWayPointTime < 0) {
            if (!navMeshAgent.pathPending){
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f){
                        ChangeWayPoint();
                        checkForWayPointTime = startCheckForWayPointTime;
                    }
                }
            }
        }

    }

        if (dayCycle.inSchool == false && dayCycle.inWork == false) {
            alreadyAtWork = false;
            alreadyAtSchool = false;
        }

        if (isPartying == true && (lightingManager.TimeOfDay > 2 && lightingManager.TimeOfDay < 4)) {
            isPartying = false;

            doingActivity = false;
        }

        if ((restingAtHouse == false && dayCycle.inHouse == false) || isPartying == true) {
            activityManager.playerMovementHouse[houseInt][locationInHouseInt] = null;
        }
    }

    public void ChangeWayPoint() {
        navMeshAgent.SetDestination(activityManager.randomWayPoints[Random.Range(0, activityManager.randomWayPoints.Length)].position);
    }

    public void AssignLocationInActivity () {
        if (person.age >= 18 && person.age <= 30 && ((lightingManager.TimeOfDay >= 22 || lightingManager.TimeOfDay < 2) && (dayCycle.dayOfTheWeek == 4 || dayCycle.dayOfTheWeek == 5))) {
            if (isPartying == true) {
                activityManager.boolPartyLocations[saveCheckSpaceParty] = false;
            }
            int checkSpaceParty = Random.Range(0, activityManager.partyLocations.Length);
            isPartying = true;
            int amountOfLoops = 0;
            while (activityManager.boolPartyLocations[checkSpaceParty] == true) {
                checkSpaceParty = Random.Range(0, activityManager.partyLocations.Length);

                amountOfLoops += 1;
                if (amountOfLoops > 50) {
                    break;
                }
            }

            saveCheckSpaceParty = checkSpaceParty;
            activityManager.boolPartyLocations[checkSpaceParty] = true;
            if (doingActivity == false) {
                activityManager.playerMovementParty[checkSpaceParty] = GetComponent<Person>();
            }
            doingActivity = true;
            navMeshAgent.SetDestination(activityManager.partyLocations[checkSpaceParty].position);
        } else {
            doingActivity = true;
            activityTime = startActivityTime * 5;
            currentActivity = Random.Range(0, activityManager.activityLocations.Length);
            int checkSpace = Random.Range(0, activityManager.boolLocations[currentActivity].Length);
            int amountOfLoops = 0;
            while (activityManager.boolLocations[currentActivity][checkSpace] == true) {
                if (amountOfLoops > activityManager.activityLocations[currentActivity].Length * 2) {
                    currentActivity = Random.Range(0, activityManager.activityLocations.Length);
                }
                checkSpace = Random.Range(0, activityManager.activityLocations[currentActivity].Length);
            
                amountOfLoops += 1;
                if (amountOfLoops > 100) {
                    break;
                }
            }

            //all the activities are equal to true
            StartCoroutine(DoingActivityBool(startActivityTime * 5));
            activityManager.boolLocations[currentActivity][checkSpace] = true;
            activityManager.playerMovementActivity[currentActivity][checkSpace] = GetComponent<Person>();
            saveChangedActivityPlace = checkSpace;
            currentPlaceInActivity = checkSpace;
            navMeshAgent.SetDestination(activityManager.activityLocations[currentActivity][checkSpace].position);
            StartCoroutine(ChangeActivityLocations(currentActivity, checkSpace));
        }
    }
    
    IEnumerator StopPartyingBool(float changeBoolTime) {
        yield return new WaitForSeconds(changeBoolTime); 
        doingActivity = false;
    }

    IEnumerator DoingActivityBool(float changeBoolTime) {
        yield return new WaitForSeconds(changeBoolTime); 
        doingActivity = false;

        if (restingAtHouse == true) {
            activityManager.playerMovementHouse[houseInt][locationInHouseInt] = null;
            restingAtHouse = false;
        }
    }

    IEnumerator ChangeBoolLocations(int activity, int space) {
        yield return new WaitForSeconds(startActivityTime * 2.4f); 
        activityManager.boolLocations[activity][space] = false;
        if (activityManager.playerMovementActivity[activity][saveChangedActivityPlace] != null) {
            activityManager.playerMovementActivity[activity][saveChangedActivityPlace] = null;
        }
    }
    IEnumerator ChangeActivityLocations(int activity, int space) {
        yield return new WaitForSeconds(startActivityTime * 2.4f); 

        int checkSpace = Random.Range(0, activityManager.boolLocations[activity].Length);


        bool childAtSchool = person.age <= 18 && dayCycle.inSchool == true;
        bool adultAtWork = person.age > 18 && person.age <= 64 && dayCycle.inWork == true;
        bool inTheHouse = dayCycle.inHouse == true;
        
        if (((person.age <= 18 && dayCycle.inSchool == true) || (person.age > 18 && person.age <= 64 && dayCycle.inWork == true) || dayCycle.inHouse == true || isPartying == true) == false) {
            bool canChangePlace = false;

            for (int i = 0; i < activityManager.boolLocations[activity].Length; i++) {
                if (activityManager.boolLocations[activity][i] == false) {
                    canChangePlace = true;
                }
            }

            if (canChangePlace == true) {
                activityManager.boolLocations[activity][space] = false;

                int amountOfLoops = 0;
                while (activityManager.boolLocations[activity][checkSpace] == true) {
                    checkSpace = Random.Range(0, activityManager.boolLocations[activity].Length);
            
                    amountOfLoops += 1;
                    if (amountOfLoops > 50) {
                        break;
                    }
                }
                activityManager.boolLocations[activity][checkSpace] = true;
                currentPlaceInActivity = checkSpace;
                navMeshAgent.SetDestination(activityManager.activityLocations[activity][checkSpace].position);
                StartCoroutine(ChangeBoolLocations(activity, checkSpace));
            } else {
                currentPlaceInActivity = space;
                StartCoroutine(ChangeBoolLocations(activity, space));
            }
        } else {
            activityManager.boolLocations[activity][space] = false;
        }
    }

    public void AssignSchool () {
        if (dayCycle.inSchool == true && alreadyAtSchool == false) {
            activityManager.boolSchoolLocations[person.ageGroup][randomSchoolSeat] = false;
            activityManager.playerMovementSchool[person.ageGroup][randomSchoolSeat] = null;

            randomSchoolSeat = Random.Range(0, activityManager.schoolLocations[person.ageGroup].Length);

            int amountOfLoops = 0;
            while(activityManager.boolSchoolLocations[person.ageGroup][randomSchoolSeat] == true) {
                randomSchoolSeat = Random.Range(0, activityManager.schoolLocations[person.ageGroup].Length);

                amountOfLoops += 1;
                if (amountOfLoops > 50) {
                    break;
                }
            }

            alreadyAtSchool = true;
            activityManager.boolSchoolLocations[person.ageGroup][randomSchoolSeat] = true;
            activityManager.playerMovementSchool[person.ageGroup][randomSchoolSeat] = GetComponent<Person>();
            navMeshAgent.SetDestination(activityManager.schoolLocations[person.ageGroup][randomSchoolSeat].position);
        }
    }

    public void AssignHospital () {
        bool spaceAtHospital = false;
        if (atHospital == false) {
            for (int i = 0; i < activityManager.hospitalLocations.Length; i++) {
                if (activityManager.boolHospitalLocations[i] == false) {
                    spaceAtHospital = true;
                }
            }

            if (spaceAtHospital == true) {
                int checkSpaceHospital = Random.Range(0, activityManager.hospitalLocations.Length);
                atHospital = true;
                int amountOfLoops = 0;
                while (activityManager.boolHospitalLocations[checkSpaceHospital] == true) {
                    checkSpaceHospital = Random.Range(0, activityManager.hospitalLocations.Length);

                    amountOfLoops += 1;
                    if (amountOfLoops > 50) {
                        break;
                    }
                }
                
                lastSpaceAtHospital = checkSpaceHospital;
                activityManager.boolHospitalLocations[checkSpaceHospital] = true;
                navMeshAgent.SetDestination(activityManager.hospitalLocations[checkSpaceHospital].position);
            }
        }
    }

    public void ChangeHouseLocation () {
        changeHouseLocation = true;
        float amountOfLoops = 0;
        fakeLocationInHouseInt = Random.Range(0, activityManager.houseLocations[houseInt].Length);
        while (activityManager.boolHouseLocations[houseInt][fakeLocationInHouseInt] == true) {
            fakeLocationInHouseInt = Random.Range(0, activityManager.houseLocations[houseInt].Length);

            amountOfLoops += 1;
            if (amountOfLoops > 10) {
                break;
            }
        }
        activityManager.boolHouseLocations[houseInt][fakeLocationInHouseInt] = true;
    }
}