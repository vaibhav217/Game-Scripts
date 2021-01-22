﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    //public ProjectileGun gunScript;
    //public Rigidbody rb;
    //public BoxCollider coll;
    public Transform player, gunContainer;
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {

        //Setup
        if (!equipped)
        {
            //gunScript.enabled = false;
           // rb.isKinematic = false;
           // coll.isTrigger = false;
        }
        if (equipped)
        {
           // gunScript.enabled = true;
           // rb.isKinematic = true;
            //coll.isTrigger = true;
            slotFull = true;
        }
    }


    public void Equip() {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        
    }

    public void Throw()
    {
        if (equipped) Drop();
    }
    private void Update()
    {
        //Check if player is in range and "E" is pressed
        
        
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(new Vector3(-48.846f, 78.973f, -175.743f));
        //transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
       // rb.isKinematic = true;
       // coll.isTrigger = true;

        //Enable script
       // gunScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
      // rb.isKinematic = false;
       // coll.isTrigger = false;

        //Gun carries momentum of player
       // rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
     //   rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
     //   rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
       // rb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
       // gunScript.enabled = false;
    }
}
