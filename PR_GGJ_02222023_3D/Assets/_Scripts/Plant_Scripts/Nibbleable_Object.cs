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
    [SerializeField] List<ParticleSystem> PE;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        currentHealth = maxHealth;
        for(int i = 0; i < this.transform.childCount; i++)
        {
            PE.Add(this.transform.GetChild(i).GetComponent<ParticleSystem>());
        }
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            for(int i = 0; i < PE.Count; i++)
            {
                PE[i].Play();
            }
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
