using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeslow : MonoBehaviour
{
    public float slowdownFactor = 0.2f;

    public float slowdownLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoSlowmotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void TurnBack()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
