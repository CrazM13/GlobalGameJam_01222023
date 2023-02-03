using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nibbleable_Object : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool canNibble = true;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] GameObject manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            canNibble = false;
            manager.GetComponent<Plant_Manager>().removePlant(this.gameObject);
            Object.Destroy(this.gameObject, 1);
        }
    }

    public void gettingNibbled(int attack)
    {
        if (canNibble == true)
        {
            currentHealth = currentHealth - attack;
            if (currentHealth <= 0)
            {
                canNibble = false;
                this.gameObject.SetActive(false);
				manager.GetComponent<Plant_Manager>().removePlant(this.gameObject);
				Object.Destroy(this.gameObject, 1);
            }
        }
    }
}
