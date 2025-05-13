using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //public Transform prefArrow;
    private Enemy enmy_script;
    // Start is called before the first frame update
    
    void Awake()
    {
        //prefArrow = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            this.transform.SetParent(other.gameObject.transform);
            enmy_script = other.gameObject.GetComponent<Enemy>();
            enmy_script.RagdollModeOn();
            //Rigidbody rb_arrow = prefArrow.GetComponent<Rigidbody>();
            //rb_arrow.isKinematic = true;
        }
    }
}
