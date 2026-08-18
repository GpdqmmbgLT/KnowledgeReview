using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityLifetime : MonoBehaviour
{
    public GameObject newObj;
    void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    void Awake()
    {
        Debug.Log("Awake");
    }
    void Start()
    {
        Debug.Log("Start");
    }
    void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }

    void Update()
    {
        Debug.Log("Update");
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
