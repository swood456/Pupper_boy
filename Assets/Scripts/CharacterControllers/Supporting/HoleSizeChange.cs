﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSizeChange : MonoBehaviour {
    [HideInInspector] private float amountDug = 0.0f;
    public float AmountDug { get; }
    public float maxAmountDug = 1.0f;
    public Vector3 maxSize = new Vector3(1.4f, 1.0f, 1.4f);
    public float decayWait = 5.0f;
    public float decayTime = 15.0f;
    [Tooltip("Scaled with objects y size")]public float decayMoveDis = 2.0f;
    public GameObject snowImpact;


    public float getPercentDug() {
        return amountDug / maxAmountDug;
    }

    //sets the size to a specific scale or to a spefic percent
    public void SetSize(Vector3 size) {
        transform.localScale = size;
    }
    public void SetSize(float size) {
        SetSize(size * maxSize);
    }

    //expands the hole from an optional start size to an end size over a given duration. Optional callback when completed
    public void Expand(float addAmountDug, float duration, System.Action onComplete = null) {
        amountDug += addAmountDug;
        Expand(maxSize * amountDug/maxAmountDug, duration, onComplete);
    }
    public void Expand(Vector3 endSize, float duration, System.Action onComplete = null) {
        Expand(transform.localScale, endSize, duration, onComplete);
    }
    public void Expand(Vector3 startSize, Vector3 endSize, float duration, System.Action onComplete = null) {
        StartCoroutine(GrowHole(startSize, endSize, duration, onComplete));
    }

    //shrinks a hole down
    public void Decay() {
        Decay(decayTime);
    }
    public void Decay(float duration, bool destroyAtEnd = true, System.Action onComplete = null) {
        if(snowImpact != null)
            snowImpact.SetActive(false);
        StartCoroutine(ShrinkHole(duration, destroyAtEnd, onComplete));
    }

    //Coroutine to grow the hole
    IEnumerator GrowHole(Vector3 startSize, Vector3 endSize, float duration, System.Action onComplete) {
        //initial setting
        transform.localScale = startSize;
        float startTime = Time.time;

        //expand over time
        while (startTime + duration > Time.time) {
            transform.localScale = Vector3.Lerp(startSize, endSize, (Time.time - startTime) / duration);
            yield return new WaitForEndOfFrame();
        }

        //post settings
        transform.localScale = endSize;
        onComplete?.Invoke();
    }

    //slowly returns a hole to the earth
    IEnumerator ShrinkHole(float duration, bool destroyAtEnd = true, System.Action onComplete = null) {
        yield return new WaitForSeconds(decayWait);
        //initial setting
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + transform.up * -decayMoveDis * transform.localScale.y;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.y * 0.5f, transform.localScale.z * 1.5f);
        float startTime = Time.time;

        //remove hole over time
        while (startTime + duration > Time.time) {
            transform.position = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime) / duration);
            transform.localScale = Vector3.Lerp(startScale, endScale, (Time.time - startTime) / duration);
            yield return new WaitForEndOfFrame();
        }

        //post settings
        transform.position = endPosition;
        transform.localScale = endScale;
        onComplete?.Invoke();
        if (destroyAtEnd) {
            Destroy(gameObject);
        }
    }
}
