using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anima : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    string status = "status";
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            animator.SetInteger(status, 1);
        }else{
            animator.SetInteger(status, 0);
        }
        
    }


    
}
