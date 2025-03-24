using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Binocular : MonoBehaviour
{
    public Animator animator;
    bool isScoped = false;
    public GameObject scopeOverlay;
    public MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("IsScoped", isScoped);
            scopeOverlay.SetActive(isScoped);

            if (isScoped)
            {
                StartCoroutine(OnScope());
            }
            else {
                OnUnscope();
            }
        }
    }
    IEnumerator OnScope()
    {

        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        mr.enabled = false;
        SceneManager.LoadScene("Scene1");
        
    }

    void OnUnscope()
    {
       
        mr.enabled = true;
        SceneManager.LoadScene("SampleScene");
        scopeOverlay.SetActive(false);
    }


}
