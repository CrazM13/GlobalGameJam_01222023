using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Nibbleable_Object : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool canNibble = true;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] GameObject manager;
    [SerializeField] List<ParticleSystem> PE;
    public UnityEvent OnNibbleStart { get; set; } = new UnityEvent();
    public UnityEvent OnNibbleContinue { get; set; } = new UnityEvent();
    public UnityEvent OnNibbleEnd { get; set; } = new UnityEvent();

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
            if (currentHealth == maxHealth)
            {
                OnNibbleStart.Invoke();
            }
            else if (currentHealth < maxHealth && currentHealth > 0)
            {
                OnNibbleContinue.Invoke();
            }
            currentHealth = currentHealth - attack;
            if (currentHealth <= 0)
            {
                OnNibbleEnd.Invoke();
                if(PE.Count > 0) {
                    for (int i = 0; i < PE.Count; i++)
                    {
                        PE[i].Play();
                    }
                }
                canNibble = false;
				manager.GetComponent<Plant_Manager>().removePlant(this.gameObject);
				Object.Destroy(this.gameObject, 1);
            }
        }
    }
}
