using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{   [SerializeField, Range(10f,100f)] private float speed=10f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float damege=30f;
    private Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity=transform.up*speed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()!=null){
            other.GetComponent<Player>().Damege(damege);
        }else if(other.GetComponent<Enemy>()!=null){
            other.GetComponent<Enemy>().Damege(damege);
        }
        Instantiate(explosion,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
