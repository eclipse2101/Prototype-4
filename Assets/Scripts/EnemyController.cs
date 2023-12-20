using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed; 
    Rigidbody badGuyRb; 
    GameObject player; 
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        badGuyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        badGuyRb.AddForce( lookDirection * speed);
    }
}
