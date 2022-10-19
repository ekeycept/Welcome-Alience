using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unlocking : MonoBehaviour
{
    public GameObject Enemies;
    public GameObject nextlevel;

    public void Update()
    {


        if (Enemies.transform.childCount == 0)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;

            if (currentLevel >= PlayerPrefs.GetInt("levelIsUnlocked"))
            {
                PlayerPrefs.SetInt("levelIsUnlocked", currentLevel + 1);
            }

            nextlevel.SetActive(true);

            Debug.Log("LEVEL" + PlayerPrefs.GetInt("levelIsUnlocked") + " UNLOCKED");
        }
    }

    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int newlevel = currentLevel + 1;
        SceneManager.LoadScene(newlevel);
        if (Enemies = null)
        {
            

            if (currentLevel >= PlayerPrefs.GetInt("levelIsUnlocked"))
            {
                PlayerPrefs.SetInt("levelIsUnlocked", currentLevel + 1);
            }

            nextlevel.SetActive(true);

            Debug.Log("LEVEL" + PlayerPrefs.GetInt("levelIsUnlocked") + " UNLOCKED");
        }
    }
}


