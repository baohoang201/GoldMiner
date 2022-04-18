using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pod : MonoBehaviour
{

    public Text txtScore;
 
   
    int score = 0;
    public enum PodState{
        ROTATION,//start
        SHOOT,//press
        REWIND// return
    }

    #region Serialize
    [SerializeField]
   
    private float _rotationSpeed = 0.2f;
    [SerializeField]
    private float _speed;
    #endregion
     private float _angle;

     private Vector3 _Origin;
    PodState podState = PodState.ROTATION;
    private float _slowDown;
    private Transform _Rod;
    private bool _flagRod;
    private int _dollars;
   


    void OnTriggerEnter2D(Collider2D col){

        //da cham
        if(_flagRod) return;

        //huy khi ve rotation
        _flagRod = true;

        _Rod = col.transform;
        //chuyen trang thai
        _Rod.SetParent(transform); // keo ve
        _slowDown = _Rod.GetComponent<Rod>().slowDown;
          podState = PodState.REWIND;
        _dollars += _Rod.GetComponent<Rod>().dollars;
    }

    void Start(){
         txtScore = GameObject.Find("Score").GetComponent<Text>();
 
    }
    void Awake(){
        _Origin = transform.position;// vi tri ban dau
       
    }
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        switch(podState){
            case PodState.ROTATION:
            if(Input.GetKeyDown(KeyCode.Space))
                podState = PodState.SHOOT;
                _angle += _rotationSpeed;// tang goc quay

            

            //rotation 80 -> -80
            if (_angle >60 || _angle<-60)
                    _rotationSpeed *= -1;// doi chieu
                   
                     transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            
           
            break;

             case PodState.SHOOT:
            //press Space to Shoot
            // down follow 
            //return if col with gold or limtetd
            // podState = rewind

            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            if(Mathf.Abs(transform.position.x) >9  || transform.position.y < -4.5)
            
            //change status
            podState = PodState.REWIND;
            break;


             case PodState.REWIND:
                transform.Translate(Vector3.up * (_speed - _slowDown) * Time.deltaTime);
           

                //dua ve so nguyen
                if(Mathf.Floor(transform.position.x)  == Mathf.Floor(_Origin.x ) && Mathf.Floor(transform.position.y)  == Mathf.Floor(_Origin.y ))
                {

                       
                        if(_Rod != null){
                            _slowDown = 0;//reset slow
                            _flagRod = false;
                            //cong diem
                            // score+=10;
                            // txtScore.text = "$ " +score.ToString();
                            setDollar(_dollars);
                           
                             Destroy(_Rod.gameObject);

                        }
                      
                        podState = PodState.ROTATION;
                        transform.position = _Origin;
                }
                    

                
            break;

        }
        
    }

    private void setDollar(int dollars){
        txtScore.text = string.Format("$ {0}", dollars);

    }

    
}
