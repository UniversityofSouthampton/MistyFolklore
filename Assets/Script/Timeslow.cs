using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeslow : MonoBehaviour
{
    public float slowdownFactor = 0.2f;

    public float slowdownLength;
    public ParticleSystem rain;
    ParticleSystem.MainModule rain_main;
    ParticleSystem.Particle[] m_Particles;
    
    // Start is called before the first frame update
    void Start()
    {
        rain_main = rain.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoSlowmotion()
    {
        //int numParticlesAlive = rain.GetParticles(m_Particles);
        //for (int i = 0; i < numParticlesAlive; i++)
        //{
            //m_Particles[i].startSize3D = new Vector3 (0.1f, 0.1f, 0.1f);
        //}
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        rain_main.startSizeY = 0.1f;
        rain_main.startSizeX = 0.1f;
        
    }

    public void TurnBack()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        rain_main.startSizeY = 1f;
    }
}
