using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void Restart () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);;
    }
}
