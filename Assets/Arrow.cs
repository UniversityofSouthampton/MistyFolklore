using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //public Transform prefArrow;
    private Enemy enmy_script;

    private Rigidbody rb_arrow;
    // Start is called before the first frame update
    
    void Awake()
    {
        //prefArrow = this.transform.parent;
         rb_arrow = GetComponent<Rigidbody>();
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
            rb_arrow.isKinematic = true;
        }
    }
}
