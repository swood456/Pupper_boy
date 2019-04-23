﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {


    //when a player enters, they can now interact with the object
    public virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<DogController>().AddObject(this);
        }
    }

    //when the player leaves, theyre no longer able to interact
    public virtual void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<DogController>().RemoveObject(this);
        }
    }
    
    //overridable interact function
    public virtual void OnInteract() {

    }


}
