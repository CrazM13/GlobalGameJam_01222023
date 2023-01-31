using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plant_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> plants;
    [SerializeField] bool survivingPlants;
    void Start()
    {
        plants = new List<GameObject>(UnityEngine.GameObject.FindGameObjectsWithTag("Plant"));
        if (plants.Count > 0)
        {
            survivingPlants = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (plants.Count <= 0)
        {
            survivingPlants = false;
        }
        checkIfWon();
    }

    void checkIfWon()
    {
        if (survivingPlants == false)
        {
            ServiceLocator.SceneManager.LoadSceneByName("WinScene");
        }
    }

    public void removePlant(GameObject gameObject)
    {
        plants.Remove(gameObject);
    }
    public int remainingPlantCount()
    {
        return plants.Count;
    }
}
