using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float boundX=15f;  
    [SerializeField] private float boundY=15f;
    [Header("Bounds")]
    [SerializeField] private float minX,maxX,minY,maxY;
    [Header("Target")]
    [SerializeField] private Transform target;

    int t=0;
    public void Start(){
        if(!target)
        target=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

   private void LateUpdate(){
       if(target!=null)
        FollowTarget(target);
       }
   private void FollowTarget(Transform target){
    Vector3 delta=Vector3.zero;
     
       float deltaX= target.position.x-this.transform.position.x;
       if(deltaX > boundX||deltaX<-boundX){
        t=1;
           if(transform.position.x<target.position.x){
               delta.x=deltaX-boundX;
           }else
           delta.x=deltaX+boundX;
       }else t=0;
 
         float deltaY= target.position.y-this.transform.position.y;
       if(deltaY > boundY||deltaY<-boundY){
        t=1;
           if(transform.position.y<target.position.y){
               delta.y=deltaY-boundY;
           }else
           delta.y=deltaY+boundY;
       }else t=0;

    transform.position=Vector3.Lerp(transform.position,new Vector3(target.position.x,
    target.position.y,this.transform.position.z),t*Time.deltaTime);
    transform.position=new Vector3(Mathf.Clamp(target.position.x,minX,maxX),Mathf.Clamp(target.position.y,minY,maxY),transform.position.z);
   }
}
