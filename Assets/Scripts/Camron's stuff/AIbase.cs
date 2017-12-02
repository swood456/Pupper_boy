﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIbase : MonoBehaviour {

    public GameObject[] Dialog;
    private GameObject Player;
    public Interactable.Tag ToyTag;
    

    private bool inRange = false;
    private GameObject activeIcon;
    

    //Triggers when the player enters the range
    public virtual void OnInRange() {
        Display(Dialog[0]);
    }
    //Trigger when the player leaves range
    public virtual void OnExitRange() {
        EndDisplay();
    }

    public virtual void ToyInRange() {
        EndDisplay();
        Display(Dialog[1]);
    }

    //Trigger when the player barks while in rage
    public virtual void OnBark() {

    }
    //Displays the thought bubble
    public void Display(GameObject icon) {
        EndDisplay();
        icon.SetActive(true);
        activeIcon = icon;
    }
    //hides the icon
    public void EndDisplay() {
        if (activeIcon != null) {
            activeIcon.SetActive(false);
        }
    }

    // Update is called once per frame
    public virtual void Update () {
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            inRange = true;
            OnInRange();
        } else if (col.gameObject.GetComponent<Interactable>() != null && col.gameObject.GetComponent<Interactable>().hasTag(ToyTag)) {
            ToyInRange();
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            inRange = false;
            OnExitRange();
        }
    }

    // Use this for initialization
    public virtual void Start() {
        foreach (GameObject i in Dialog) {
            i.SetActive(false);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
    }

}
