using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    [SerializeField]
    private string _tag; //loai
  
    public float slowDown; // can nang
  
    public int dollars; // cong tien
    // Start is called before the first frame update


    void Awake(){
        this.tag = _tag;
    }
}
