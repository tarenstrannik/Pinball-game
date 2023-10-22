using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsSensor : MonoBehaviour
{
    private GameManager gameManager;
   
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Sensor");
        // Instead of destroying the projectile when it collides with an animal
        //Destroy(other.gameObject); 

        // Just deactivate the food and destroy the animal
        collision.gameObject.SetActive(false);
        gameManager.StartRound();
    }


}
