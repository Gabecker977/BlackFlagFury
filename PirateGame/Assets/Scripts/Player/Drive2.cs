using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// A very simplistic car driving on the x-z plane.
[RequireComponent(typeof(Rigidbody2D))]
public class Drive2 : MonoBehaviour
{
    [SerializeField,Range(10f,100f)]private float speed = 10.0f;
    [SerializeField,Range(10f,100f)]private float rotationSpeed = 100.0f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform singleShootTransform;
    [SerializeField] private Transform[] tripleShootTransfor;
    private Rigidbody2D rb;
    private void Start() {
        rb=GetComponent<Rigidbody2D>();
    }
    void CalculateAngle(){
        Vector3 tankForward=transform.up;
      //  Vector3 fuelDir=fuel.transform.position-transform.position;

       // Debug.DrawRay(this.transform.position,tankForward,Color.green,2);
       // Debug.DrawRay(this.transform.position,fuelDir,Color.red,2);

       // float dot=tankForward.x * fuelDir.x+tankForward.y*fuelDir.y;
      //  float angle= Mathf.Acos(dot/(tankForward.magnitude*fuelDir.magnitude));
        //Debug.Log("Angle: "+angle*Mathf.Rad2Deg);
       /// Debug.Log("Unity Angle: "+Vector3.Angle(tankForward,fuelDir));
      //  int crossWise=1;

     //   if(Cross(tankForward,fuelDir).z<0){
    //        crossWise=-1;
    //    }
       // this.transform.Rotate(0,0,angle*Mathf.Rad2Deg*crossWise*rSpeed);
    }

  /*  Vector3 Cross(Vector3 v,Vector3 w){
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.x * w.z - v.z * w.x;
        float zMult = v.x * w.y - v.y * w.x;
        return new Vector3(xMult,yMult,zMult);
    }*/
   /* void AutoPilot(){
        CalculateAngle();
        
        Vector3 dir=fuel.transform.position-transform.position;
        transform.position+=transform.up*speed*Time.deltaTime;
    }*/
  /*  float CalculateDistance(Transform obg){
        //By calculations
        float distance=Mathf.Sqrt(Mathf.Pow(obg.position.x-transform.position.x,2)+Mathf.Pow(obg.position.y-transform.position.y,2));
         //By the method Vector3.Distance()
        Vector3 fuelPos=new Vector3(fuel.transform.position.x,fuel.transform.position.y,0);
        Vector3 tankPos=new Vector3(transform.position.x,transform.position.y,0);
        float uDistance=Vector3.Distance(fuelPos,tankPos);
        //By the magnitude
        Vector3 tankTofuel=fuelPos-tankPos;

       // Debug.Log("Distance: "+distance);
       // Debug.Log("Udistance: "+uDistance);
       // Debug.Log("Mdistance: "+tankTofuel.magnitude);
       return distance;
    }*/

    void Update()
    {

        float translation = -Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

       
        //translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        //transform.Translate(0, -translation, 0);
        rb.velocity=transform.up*translation;

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);
        //Single shoot
        if(Input.GetKeyUp(KeyCode.Space)){
            Instantiate(bullet,singleShootTransform.position,singleShootTransform.rotation);
        }

    }
    private IEnumerator Wait(){
        yield return new WaitForSeconds(2f);

    }
}