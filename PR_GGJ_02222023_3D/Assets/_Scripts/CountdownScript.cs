using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountdownScript : MonoBehaviour
{

    [SerializeField] private Text wiText;
    [SerializeField] private float mainTimer;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = mainTimer; 
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            wiText.text = timer.ToString("F");
        }
        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            wiText.text = "0.00";
            timer = 0.0f;
        }
    }

    public void RestBtn()
    {
        timer = mainTimer;
        canCount = true;
        doOnce=false;
    }

    void GameOver()
    {
        //Load a new Scene
    }
                    
}
