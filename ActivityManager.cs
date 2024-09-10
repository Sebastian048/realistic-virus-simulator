using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityManager : MonoBehaviour
{
    public bool[] testLocation0;
    public bool[] testLocation1;
    public bool[] testLocation2;
    public bool[] testLocation3;
    public bool[] testLocation4;
    public bool[] testLocation5;
    public bool[] testLocation6;
    public bool[] testLocation7;
    public bool[] testLocation8;
    public bool[] testLocation9;
    public bool[] testLocation10;
    public bool[] testLocation11;

    Transform person;

    public Transform[] randomWayPoints;

    public Transform[][] activityLocations = new Transform[8][];
    public Transform[] location0;
    public Transform[] location1;
    public Transform[] location2;
    public Transform[] location3;
    public Transform[] location4;
    public Transform[] location5;
    public Transform[] location6;
    public Transform[] location7;
    public bool[][] boolLocations = new bool[8][];
    public Person[][] playerMovementActivity = new Person[8][];

    public Transform[][] houseLocations = new Transform[37][];
    public Transform[] houseLocation0;
    public Transform[] houseLocation1;
    public Transform[] houseLocation2;
    public Transform[] houseLocation3;
    public Transform[] houseLocation4;
    public Transform[] houseLocation5;
    public Transform[] houseLocation6;
    public Transform[] houseLocation7;
    public Transform[] houseLocation8;
    public Transform[] houseLocation9;
    public Transform[] houseLocation10;
    public Transform[] houseLocation11;
    public Transform[] houseLocation12;
    public Transform[] houseLocation13;
    public Transform[] houseLocation14;
    public Transform[] houseLocation15;
    public Transform[] houseLocation16;
    public Transform[] houseLocation17;
    public Transform[] houseLocation18;
    public Transform[] houseLocation19;
    public Transform[] houseLocation20;
    public Transform[] houseLocation21;
    public Transform[] houseLocation22;
    public Transform[] houseLocation23;
    public Transform[] houseLocation24;
    public Transform[] houseLocation25;
    public Transform[] houseLocation26;
    public Transform[] houseLocation27;
    public Transform[] houseLocation28;
    public Transform[] houseLocation29;
    public Transform[] houseLocation30;
    public Transform[] houseLocation31;
    public Transform[] houseLocation32;
    public Transform[] houseLocation33;
    public Transform[] houseLocation34;
    public Transform[] houseLocation35;
    public Transform[] houseLocation36;
    public bool[][] boolHouseLocations = new bool[37][];
    public Person[][] playerMovementHouse = new Person[37][];

    public Transform[][] schoolLocations = new Transform[3][];
    public Transform[] highSchoolLocations;
    public Transform[] primarySchoolLocations;
    public Transform[] kinderSchoolLocations;
    public bool[][] boolSchoolLocations = new bool[3][];
    public Person[][] playerMovementSchool = new Person[3][];

    public Transform[] jobLocations;

    public int playerNum;
    public bool reset;

    public bool[] schoolPeriods;
    public bool[] checkSchoolPeriods;

    public bool[] housePeriods;
    public bool[] checkHousePeriods;

    public LightingManager lightingManager;

    public Transform playerParent;
    public List<Transform> playerList;
    public List<Transform> childList;
    public List<Transform> adultList;


    public bool[] jobPeriods;
    public bool[] checkJobPeriods;
    public bool[][] boolJobLocations = new bool[9][];
    public Transform[][] randomJobLocations = new Transform[9][];
    public Transform[] jobLocation0;
    public Transform[] jobLocation1;
    public Transform[] jobLocation2;
    public Transform[] jobLocation3;
    public Transform[] jobLocation4;
    public Transform[] jobLocation5;
    public Transform[] jobLocation6;
    public Transform[] jobLocation7;
    public Transform[] jobLocation8;
    public Person[][] playerMovementJob = new Person[9][];


    public Person[] playerMovementActivityTest0;
    public Person[] playerMovementActivityTest1;
    public Person[] playerMovementActivityTest2;
    public Person[] playerMovementActivityTest3;
    public Person[] playerMovementActivityTest4;
    public Person[] playerMovementActivityTest5;
    public Person[] playerMovementSchoolTest0;
    public Person[] playerMovementSchoolTest1;
    public Person[] playerMovementSchoolTest2;
    public Person[] playerMovementJobTest0;
    public Person[] playerMovementJobTest1;
    public Person[] playerMovementJobTest2;
    public Person[] playerMovementJobTest3;
    public Person[] playerMovementJobTest4;
    public Person[] playerMovementJobTest5;
    public Person[] playerMovementJobTest6;
    public Person[] playerMovementHouseTest0;
    public Person[] playerMovementHouseTest1;
    public Person[] playerMovementHouseTest2;
    public Person[] playerMovementHouseTest3;
    public Person[] playerMovementHouseTest4;
    public Person[] playerMovementHouseTest5;
    public Person[] playerMovementHouseTest6;
    public Person[] playerMovementHouseTest7;
    public Person[] playerMovementHouseTest8;
    public Person[] playerMovementHouseTest9;
    public Person[] playerMovementHouseTest10;
    public Person[] playerMovementHouseTest11;
    public Person[] playerMovementHouseTest12;
    public Person[] playerMovementHouseTest13;
    public Person[] playerMovementHouseTest14;
    public Person[] playerMovementHouseTest15;
    public Person[] playerMovementHouseTest16;
    public Person[] playerMovementHouseTest17;



    public Transform[] partyLocations;
    public bool[] boolPartyLocations;
    public Person[] playerMovementParty;

    public Transform[] hospitalLocations;
    public bool[] boolHospitalLocations;

    public DayCycle dayCycle;

    public float RNaught;
    public TextMeshProUGUI textRNaught;

    public VirusManager virusManager;
    
    public bool resetHousePeriods;

    public bool jobsOpen;

    void Start () {
        boolSchoolLocations[0] = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
        boolSchoolLocations[1] = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
        boolSchoolLocations[2] = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
        schoolLocations[0] = kinderSchoolLocations;
        schoolLocations[1] = primarySchoolLocations;
        schoolLocations[2] = highSchoolLocations;
        playerMovementSchool[0] = new Person[]{null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
        playerMovementSchool[1] = new Person[]{null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
        playerMovementSchool[2] = new Person[]{null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};

        activityLocations[0] = location0;
        activityLocations[1] = location1;
        activityLocations[2] = location2;
        activityLocations[3] = location3;
        activityLocations[4] = location4;
        activityLocations[5] = location5;
        activityLocations[6] = location6;
        activityLocations[7] = location7;
        boolLocations[0] = new bool[]{false, false, false, false, false};
        boolLocations[1] = new bool[]{false, false, false, false, false, false, false, false, false, false};
        boolLocations[2] = new bool[]{false, false, false, false, false, false, false};
        boolLocations[3] = new bool[]{false, false, false, false, false, false, false};
        boolLocations[4] = new bool[]{false, false, false, false, false};
        boolLocations[5] = new bool[]{false, false, false, false, false, false};
        boolLocations[6] = new bool[]{false, false, false, false};
        boolLocations[7] = new bool[]{false, false, false, false, false, false};
        playerMovementActivity[0] = new Person[]{null, null, null, null, null};
        playerMovementActivity[1] = new Person[]{null, null, null, null, null, null, null, null, null, null};
        playerMovementActivity[2] = new Person[]{null, null, null, null, null, null, null};
        playerMovementActivity[3] = new Person[]{null, null, null, null, null, null, null};
        playerMovementActivity[4] = new Person[]{null, null, null, null, null};
        playerMovementActivity[5] = new Person[]{null, null, null, null, null, null};
        playerMovementActivity[6] = new Person[]{null, null, null, null};
        playerMovementActivity[7] = new Person[]{null, null, null, null, null, null};

        


        randomJobLocations[0] = jobLocation0;
        randomJobLocations[1] = jobLocation1;
        randomJobLocations[2] = jobLocation2;
        randomJobLocations[3] = jobLocation3;
        randomJobLocations[4] = jobLocation4;
        randomJobLocations[5] = jobLocation5;
        randomJobLocations[6] = jobLocation6;
        randomJobLocations[7] = jobLocation7;
        randomJobLocations[8] = jobLocation8;
        boolJobLocations[0] = new bool[]{false, false, false, false, false};
        boolJobLocations[1] = new bool[]{false, false, false, false, false};
        boolJobLocations[2] = new bool[]{false, false, false, false, false, false};
        boolJobLocations[3] = new bool[]{false, false, false, false};
        boolJobLocations[4] = new bool[]{false, false, false};
        boolJobLocations[5] = new bool[]{false, false, false, false, false, false, false, false};
        boolJobLocations[6] = new bool[]{false, false, false, false, false, false, false, false, false, false, false};
        boolJobLocations[7] = new bool[]{false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
        boolJobLocations[8] = new bool[]{false, false, false, false, false, false, false, false, false};
        playerMovementJob[0] = new Person[]{null, null, null, null, null};
        playerMovementJob[1] = new Person[]{null, null, null, null, null};
        playerMovementJob[2] = new Person[]{null, null, null, null, null, null};
        playerMovementJob[3] = new Person[]{null, null, null, null};
        playerMovementJob[4] = new Person[]{null, null, null};
        playerMovementJob[5] = new Person[]{null, null, null, null, null, null, null, null};
        playerMovementJob[6] = new Person[]{null, null, null, null, null, null, null, null, null, null, null};
        playerMovementJob[7] = new Person[]{null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null};
        playerMovementJob[8] = new Person[]{null, null, null, null, null, null, null, null, null};
        
        houseLocations[0] = houseLocation0;
        houseLocations[1] = houseLocation1;
        houseLocations[2] = houseLocation2;
        houseLocations[3] = houseLocation3;
        houseLocations[4] = houseLocation4;
        houseLocations[5] = houseLocation5;
        houseLocations[6] = houseLocation6;
        houseLocations[7] = houseLocation7;
        houseLocations[8] = houseLocation8;
        houseLocations[9] = houseLocation9;
        houseLocations[10] = houseLocation10;
        houseLocations[11] = houseLocation11;
        houseLocations[12] = houseLocation12;
        houseLocations[13] = houseLocation13;
        houseLocations[14] = houseLocation14;
        houseLocations[15] = houseLocation15;
        houseLocations[16] = houseLocation16;
        houseLocations[17] = houseLocation17;
        houseLocations[18] = houseLocation18;
        houseLocations[19] = houseLocation19;
        houseLocations[20] = houseLocation20;
        houseLocations[21] = houseLocation21;
        houseLocations[22] = houseLocation22;
        houseLocations[23] = houseLocation23;
        houseLocations[24] = houseLocation24;
        houseLocations[25] = houseLocation25;
        houseLocations[26] = houseLocation26;
        houseLocations[27] = houseLocation27;
        houseLocations[28] = houseLocation28;
        houseLocations[29] = houseLocation29;
        houseLocations[30] = houseLocation30;
        houseLocations[31] = houseLocation31;
        houseLocations[32] = houseLocation32;
        houseLocations[33] = houseLocation33;
        houseLocations[34] = houseLocation34;
        houseLocations[35] = houseLocation35;
        houseLocations[36] = houseLocation36;
        playerMovementHouse[0] = new Person[]{null};
        playerMovementHouse[1] = new Person[]{null};
        playerMovementHouse[2] = new Person[]{null};
        playerMovementHouse[3] = new Person[]{null};
        playerMovementHouse[4] = new Person[]{null};
        playerMovementHouse[5] = new Person[]{null};
        playerMovementHouse[6] = new Person[]{null};
        playerMovementHouse[7] = new Person[]{null, null};
        playerMovementHouse[8] = new Person[]{null, null};
        playerMovementHouse[9] = new Person[]{null, null};
        playerMovementHouse[10] = new Person[]{null, null};
        playerMovementHouse[11] = new Person[]{null, null};
        playerMovementHouse[12] = new Person[]{null, null};
        playerMovementHouse[13] = new Person[]{null, null};
        playerMovementHouse[14] = new Person[]{null, null};
        playerMovementHouse[15] = new Person[]{null, null};
        playerMovementHouse[16] = new Person[]{null, null, null};
        playerMovementHouse[17] = new Person[]{null, null, null};
        playerMovementHouse[18] = new Person[]{null, null, null};
        playerMovementHouse[19] = new Person[]{null, null, null};
        playerMovementHouse[20] = new Person[]{null, null, null};
        playerMovementHouse[21] = new Person[]{null, null, null};
        playerMovementHouse[22] = new Person[]{null, null, null};
        playerMovementHouse[23] = new Person[]{null, null, null};
        playerMovementHouse[24] = new Person[]{null, null, null};
        playerMovementHouse[25] = new Person[]{null, null, null, null};
        playerMovementHouse[26] = new Person[]{null, null, null, null};
        playerMovementHouse[27] = new Person[]{null, null, null, null};
        playerMovementHouse[28] = new Person[]{null, null, null, null};
        playerMovementHouse[29] = new Person[]{null, null, null, null};
        playerMovementHouse[30] = new Person[]{null, null, null, null};
        playerMovementHouse[31] = new Person[]{null, null, null, null};
        playerMovementHouse[32] = new Person[]{null, null, null, null};
        playerMovementHouse[33] = new Person[]{null, null, null, null};
        playerMovementHouse[34] = new Person[]{null, null, null, null};
        playerMovementHouse[35] = new Person[]{null, null, null, null};
        playerMovementHouse[36] = new Person[]{null, null, null, null};
        boolHouseLocations[0] = new bool[]{false};
        boolHouseLocations[1] = new bool[]{false};
        boolHouseLocations[2] = new bool[]{false};
        boolHouseLocations[3] = new bool[]{false};
        boolHouseLocations[4] = new bool[]{false};
        boolHouseLocations[5] = new bool[]{false};
        boolHouseLocations[6] = new bool[]{false};
        boolHouseLocations[7] = new bool[]{false, false};
        boolHouseLocations[8] = new bool[]{false, false};
        boolHouseLocations[9] = new bool[]{false, false};
        boolHouseLocations[10] = new bool[]{false, false};
        boolHouseLocations[11] = new bool[]{false, false};
        boolHouseLocations[12] = new bool[]{false, false};
        boolHouseLocations[13] = new bool[]{false, false};
        boolHouseLocations[14] = new bool[]{false, false};
        boolHouseLocations[15] = new bool[]{false, false};
        boolHouseLocations[16] = new bool[]{false, false, false};
        boolHouseLocations[17] = new bool[]{false, false, false};
        boolHouseLocations[18] = new bool[]{false, false, false};
        boolHouseLocations[19] = new bool[]{false, false, false};
        boolHouseLocations[20] = new bool[]{false, false, false};
        boolHouseLocations[21] = new bool[]{false, false, false};
        boolHouseLocations[22] = new bool[]{false, false, false};
        boolHouseLocations[23] = new bool[]{false, false, false};
        boolHouseLocations[24] = new bool[]{false, false, false};
        boolHouseLocations[25] = new bool[]{false, false, false, false};
        boolHouseLocations[26] = new bool[]{false, false, false, false};
        boolHouseLocations[27] = new bool[]{false, false, false, false};
        boolHouseLocations[28] = new bool[]{false, false, false, false};
        boolHouseLocations[29] = new bool[]{false, false, false, false};
        boolHouseLocations[30] = new bool[]{false, false, false, false};
        boolHouseLocations[31] = new bool[]{false, false, false, false};
        boolHouseLocations[32] = new bool[]{false, false, false, false};
        boolHouseLocations[33] = new bool[]{false, false, false, false};
        boolHouseLocations[34] = new bool[]{false, false, false, false};
        boolHouseLocations[35] = new bool[]{false, false, false, false};
        boolHouseLocations[36] = new bool[]{false, false, false, false};

        playerList = GetChildren(playerParent);

        int totalPlayersCounted = 0;
        for (int i = 0; i < houseLocations.Length; i++) {
            for (int t = 0; t < houseLocations[i].Length; t++) {
                if (totalPlayersCounted >= playerList.Count) {
                    break;
                }
                playerList[totalPlayersCounted].GetComponent<Movement>().houseInt = i;
                playerList[totalPlayersCounted].GetComponent<Movement>().locationInHouseInt = t;

                totalPlayersCounted += 1;
            }
        }
 
        Invoke("AssignGroup", 0.1f);
    }

    void Update () {
        if (lightingManager.TimeOfDay > 8 && lightingManager.TimeOfDay < 8.5f) {
            for(int i = 0; i < housePeriods.Length; i++) {
                housePeriods[i] = false;
                checkHousePeriods[i] = false;
            }
        }


        if ((lightingManager.TimeOfDay > 0 && lightingManager.TimeOfDay < 2) && dayCycle.inHouse == true) {
            housePeriods[0] = true;
        } else if ((lightingManager.TimeOfDay > 2 && lightingManager.TimeOfDay < 4) && dayCycle.inHouse == true){
            housePeriods[1] = true;
        } else if ((lightingManager.TimeOfDay > 4 && lightingManager.TimeOfDay < 6) && dayCycle.inHouse == true) {
            housePeriods[2] = true;
        } else if ((lightingManager.TimeOfDay > 6 && lightingManager.TimeOfDay < 7) && dayCycle.inHouse == true) {
            housePeriods[3] = true;
        }
        if (housePeriods[0] == true || housePeriods[1] == true || housePeriods[2] == true || housePeriods[3] == true){
            for (int i = 0; i < boolHouseLocations.Length; i++) {
                for (int t = 0; t < boolHouseLocations[i].Length; t++) {
                    boolHouseLocations[i][t] = false;
                }
            }
            for(int i = 0; i < housePeriods.Length; i++) {
                if (housePeriods[i] == true && checkHousePeriods[i] == false) {
                    checkHousePeriods[i] = true;
                    for (int t = 0; t < playerList.Count; t++) {
                        if (playerList[t] != null) {
                            playerList[t].GetComponent<Movement>().ChangeHouseLocation();
                        }
                    }
                }
            }
        }

        
        if (dayCycle.dayOfTheWeek != 5 && dayCycle.dayOfTheWeek != 6) {

        if (lightingManager.TimeOfDay > 9 && schoolPeriods[0] == false) {
            schoolPeriods[0] = true;
        } else if (lightingManager.TimeOfDay > 11 && schoolPeriods[1] == false){
            schoolPeriods[1] = true;
        } else if (lightingManager.TimeOfDay > 13 && schoolPeriods[2] == false) {
            schoolPeriods[2] = true;
        } else {
            for(int i = 0; i < schoolPeriods.Length; i++) {
                if (schoolPeriods[i] == true && checkSchoolPeriods[i] == false) {
                    checkSchoolPeriods[i] = true;
                    for (int t = 0; t < childList.Count; t++) {
                        if (childList[t] != null) {
                            childList[t].GetComponent<Movement>().alreadyAtSchool = false;
                        }
                    }
                }
            }
        }
        
        if (dayCycle.inWork == true) {
        if (lightingManager.TimeOfDay > 11 && jobPeriods[0] == false) {
            jobPeriods[0] = true;
        } else if (lightingManager.TimeOfDay > 13 && jobPeriods[1] == false){
            jobPeriods[1] = true;
        } else if (lightingManager.TimeOfDay > 15 && jobPeriods[2] == false) {
            jobPeriods[2] = true;
        } else {
            for(int i = 0; i < jobPeriods.Length; i++) {
                if (jobPeriods[i] == true && checkJobPeriods[i] == false) {
                    checkJobPeriods[i] = true;
                    for (int t = 0; t < adultList.Count; t++) {
                        if (adultList[t] != null) {
                            int changeJobLocation = Random.Range(0, boolJobLocations[adultList[t].GetComponent<Movement>().jobGroup].Length);
                            int breakTheThing = 0;
                            while(boolJobLocations[adultList[t].GetComponent<Movement>().jobGroup][changeJobLocation] == true) {
                                changeJobLocation = Random.Range(0, boolJobLocations[adultList[t].GetComponent<Movement>().jobGroup].Length);
                                breakTheThing += 1;
                                if (breakTheThing > 50) {
                                    break;
                                }
                            }  
                            boolJobLocations[adultList[t].GetComponent<Movement>().jobGroup][changeJobLocation] = true;
                            adultList[t].GetComponent<Movement>().currentJobLocation = changeJobLocation;
                            if (adultList[t].gameObject.activeSelf == true) {
                                adultList[t].GetComponent<Movement>().navMeshAgent.SetDestination(randomJobLocations[adultList[t].GetComponent<Movement>().jobGroup][changeJobLocation].position);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < boolJobLocations.Length; i++) {
                for (int t = 0; t < boolJobLocations[i].Length; t++) {
                    boolJobLocations[i][t] = false;
                }
            }
        }
        }
        
        }


        if (dayCycle.boolShutDownSchool == true) {
            for(int i = 0; i < playerMovementSchool.Length; i++) {
                for(int t = 0; t < playerMovementSchool[i].Length; t++) {
                    playerMovementSchool[i][t] = null;
                }
            }
        }
        if (dayCycle.boolShutDownWork == true) {
            for(int i = 0; i < playerMovementJob.Length; i++) {
                for(int t = 0; t < playerMovementJob[i].Length; t++) {
                    playerMovementJob[i][t] = null;
                }
            }
        }
    }

    List<Transform> GetChildren(Transform parent) {
        List<Transform> children = new List<Transform>();

        foreach(Transform child in parent) {
            children.Add(child.GetChild(0));
        }

        return children;
    }

    public void AssignGroup () {
        for (int i = 0; i < playerList.Count; i++) {
            int personAge = playerList[i].gameObject.GetComponent<Person>().age;

            if (personAge <= 18) {
                childList.Add(playerList[i]);
            } else if (personAge > 18 && personAge <= 64) {
                adultList.Add(playerList[i]);
            }
        }

        int[] addingJobPlaces = new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0};
        //change when adding job places
        for (int i = 0; i < adultList.Count; i++) {
            adultList[i].GetComponent<Movement>().jobNumber = i;
            if (i >= 0 && i <= 4) {
                adultList[i].GetComponent<Movement>().jobGroup = 0;
            } else if (i > 4 && i <= 9) {
                adultList[i].GetComponent<Movement>().jobGroup = 1;
            } else if (i > 9 && i <= 15) {
                adultList[i].GetComponent<Movement>().jobGroup = 2;
            } else if (i > 15 && i <= 19) {
                adultList[i].GetComponent<Movement>().jobGroup = 3;
            } else if (i > 19 && i <= 22) {
                adultList[i].GetComponent<Movement>().jobGroup = 4;
            } else if (i > 22 && i <= 30) {
                adultList[i].GetComponent<Movement>().jobGroup = 5;
            } else if (i > 30 && i <= 41) {
                adultList[i].GetComponent<Movement>().jobGroup = 6;
            } else if (i > 41 && i <= 61) {
                adultList[i].GetComponent<Movement>().jobGroup = 7;
            } else if (i > 61 && i <= 70) {
                adultList[i].GetComponent<Movement>().jobGroup = 8;
            }

            playerMovementJob[adultList[i].GetComponent<Movement>().jobGroup][addingJobPlaces[adultList[i].GetComponent<Movement>().jobGroup]] = adultList[i].GetComponent<Person>();
            addingJobPlaces[adultList[i].GetComponent<Movement>().jobGroup] += 1;
        }
    }


    public void ResetLocations () {
        if (reset == false) {
            for (int i = 0; i < schoolLocations.Length; i++) {
                for (int t = 0; t < schoolLocations[i].Length; t++) {
                    boolSchoolLocations[i][t] = false;
                }
            }

            for (int i = 0; i < schoolPeriods.Length; i++) {
                schoolPeriods[i] = false;
                checkSchoolPeriods[i] = false;
            }

            for (int i = 0; i < jobPeriods.Length; i++) {
                jobPeriods[i] = false;
                checkJobPeriods[i] = false;
            }

            for (int i = 0; i < boolLocations.Length; i++) {
                for (int t = 0; t < boolLocations[i].Length; t++) {
                    boolLocations[i][t] = false;
                }
            }

            for (int i = 0; i < boolPartyLocations.Length; i++) {
                boolPartyLocations[i] = false;
            }

            reset = true;
        }
    }


    public void EndSimulation () {
        float divideBy = 100;
        RNaught = 0;

        for (int i = 0; i < playerList.Count; i++) {
            if (playerList[i].GetComponent<Person>().timesInfected != 0) {
                float personRNaught = playerList[i].GetComponent<Person>().myRNaught / playerList[i].GetComponent<Person>().timesInfected;
                personRNaught = personRNaught * (playerList[i].GetComponent<Person>().finalAverageVirusMultiplication / playerList[i].GetComponent<Person>().timesInfected);
                RNaught += personRNaught;
            } else {
                divideBy -= 1;
            }
        }

        RNaught = RNaught / divideBy;

        RNaught = Mathf.Round(RNaught * 100.0f) * 0.01f;

        textRNaught.text = "RNaught: " +  RNaught.ToString();
    }


    public void ActivateMasks (bool masks) {
        for (int i = 0; i < playerList.Count; i++) {
            playerList[i].GetComponent<Person>().masks.SetActive(masks);
        }
    }

    public void OpenJobs (bool open) {
        if (open == false) {
            for (int i = 0; i < adultList.Count; i++) {
                adultList[i].GetComponent<Movement>().alreadyAtWork = false;
                adultList[i].GetComponent<Movement>().atHomeBecauseOfConditions = false;
            }
        } else {
            for (int i = 0; i < adultList.Count; i++) {
                adultList[i].GetComponent<Movement>().atHomeBecauseOfConditions = true;
            }
        }
    }

    public void OpenSchools (bool open) {
        if (open == false) {
            for (int i = 0; i < childList.Count; i++) {
                childList[i].GetComponent<Movement>().atHomeBecauseOfConditions = false;
            }
        } else {
            for (int i = 0; i < childList.Count; i++) {
                childList[i].GetComponent<Movement>().atHomeBecauseOfConditions = true;
            }
        }
    }
}