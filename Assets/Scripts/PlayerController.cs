using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; 
    public bool GotPowerUp;
    public bool GotWeapon;
    public bool GotExplosion; 
     Rigidbody playerRb; 
     GameObject focalpoint; 
     public float PowerUpStrength = 20.0f; 
     public float Health = 3;
     public GameObject PowerupIndicator; 
     public GameObject PowerupIndicator2; 

    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalpoint = GameObject.Find("focus point");
    }

    // Update is called once per frame
    void Update()
    {
      float vertical = Input.GetAxis("Vertical"); 
      playerRb.AddForce(focalpoint.transform.forward * speed * vertical); 
      PowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
      PowerupIndicator2.transform.position = transform.position + new Vector3(0, -0.5f, 0);

      ///// health system/////
      if(transform.position.y < -5)
      {
        if(Health > 0)
        {
          transform.position = new Vector3(0,0,0);
          Health--; 
        }
        else
        {
          Debug.Log("Game over! you suck lil nigga");
        }
        ///////WEAPON UP SYSTEM/////////// 
        if (Input.GetKeyDown(KeyCode.Space))
        {
          if(GotExplosion == true)
          {
            Debug.Log("kaboom");
          }
        }
      }
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Power Up"))
      {
        GotPowerUp = true;
        Destroy(other.gameObject);
        StartCoroutine(PowerupCountdownLoop());
        PowerupIndicator.gameObject.SetActive(true);  
      }
      
      if (other.CompareTag("Explosion Power Up"))
      {
        GotExplosion= true; 
        Destroy(other.gameObject);
        PowerupIndicator2.gameObject.SetActive(true); 
        StartCoroutine(ExplosionCountdownLoop());
      }
    }
     
     IEnumerator PowerupCountdownLoop()
     {
        yield return new WaitForSeconds(7);
        GotPowerUp = false;
        PowerupIndicator.gameObject.SetActive(false);  
     }   

     IEnumerator ExplosionCountdownLoop()
     {
        yield return new WaitForSeconds(7);
        GotExplosion= false;
        PowerupIndicator2.gameObject.SetActive(false);  
     }

    void OnCollisionEnter(Collision collision)
    {
      Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
      Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
      
      if (collision.gameObject.CompareTag("Enemy") && GotPowerUp)
      {
        Debug.Log("Collided with" + collision.gameObject.name + "with power set to" + GotPowerUp);
        enemyRigidbody.AddForce(awayFromPlayer * PowerUpStrength, ForceMode.Impulse);
      }
    }
}
