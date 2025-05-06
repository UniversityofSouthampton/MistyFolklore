using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator anim_transition;
    public Scene scene1;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && Pickup.isHolding == false)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                StartCoroutine(LoadLevel(0));
            }
            else{
                StartCoroutine(LoadLevel(1));
            }
        }
    }

     IEnumerator LoadLevel(int levelindex)
    {
        anim_transition.SetTrigger("Start");
       
        yield return new WaitForSeconds(1.5f);
         Destroy(cube);
        SceneManager.LoadScene(levelindex);
        
    }
}
