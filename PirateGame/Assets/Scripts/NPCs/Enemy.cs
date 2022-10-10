using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   [Header("Enemy Paremetres")]
    [SerializeField] private Type enemyType;
    [SerializeField,Range(1f,100f)] private float life=100f; 
    [SerializeField] private Transform enemyTarget;
	[SerializeField] private float speed = 0.5f;
	[SerializeField] private float accuracy = 1.0f;
    [SerializeField] private float rotationSpeed=0.1f;
    [SerializeField] private GameObject explosion;
    [Header("Atack paremetres")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField,Range(0.1f,2f)] private float timeToShoot=1f;
    [SerializeField] private GameObject deathEffect;


    private enum Type{Chaser, Shooter}
    private bool canShoot=true;
	void Start () {
		if(enemyTarget==null)
            enemyTarget=GameObject.FindGameObjectWithTag("Player").transform;
	}
    private void Update() {
        if(life<=0){
            FindObjectOfType<UI>().score++;
            Instantiate(deathEffect,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
	void LateUpdate () {
		if(enemyTarget!=null){
            Vector3 lookAtTarget = new Vector3(enemyTarget.position.x,enemyTarget.position.y, 
			transform.position.z);
        Vector3 direction=lookAtTarget-this.transform.position;
        CalculateAngle(enemyTarget);

		if(enemyType.Equals(Type.Chaser)){
            Chase(enemyTarget);
        }
        else if(enemyType.Equals(Type.Shooter)){
            if(Vector3.Distance(transform.position,enemyTarget.position) > accuracy)
               Chase(enemyTarget);
            else
                ShooterBehaviour();
            }
        }
    }
    void CalculateAngle(Transform target){
        Vector3 forward=-transform.up;
        Vector3 targetDir=target.transform.position-transform.position;
        float dot=forward.x * targetDir.x+forward.y*targetDir.y;
        int crossWise=1;
        if(Cross(forward,targetDir).z<0){
            crossWise=-1;
        }
        this.transform.Rotate(0,0,Vector3.Angle(forward,targetDir)*crossWise*rotationSpeed);
    }
    Vector3 Cross(Vector3 v,Vector3 w){
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.x * w.z - v.z * w.x;
        float zMult = v.x * w.y - v.y * w.x;
        return new Vector3(xMult,yMult,zMult);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag=="Player"&&enemyType.Equals(Type.Chaser)){
        Instantiate(explosion,transform.position,Quaternion.identity);
        Destroy(gameObject);
        other.collider.GetComponent<Player>().Damege(50f);
        }
    }
    private void Chase(Transform target){
        
        GetComponent<Rigidbody2D>().velocity=-transform.up*speed;
    }
    private void ShooterBehaviour(){
        if(canShoot){
            SingleShoot();
            canShoot=false;
            StartCoroutine(Wait(timeToShoot));
        }
    }
    
    private void SingleShoot()=>Instantiate(bullet,bulletSpawnPosition.position,bulletSpawnPosition.rotation);
    private IEnumerator Wait(float _seconds){
        yield return new WaitForSeconds(_seconds);
        canShoot=true;
    }
    public void Damege(float damege){
        life-=damege;
    }
    public void SetRandowType(){
        if(Random.Range(0,2)==0)
        enemyType=Type.Chaser;
        else
        enemyType=Type.Shooter;
    }
}
