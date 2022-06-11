using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Point : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis = new Vector3(0, 0, 0);

    [SerializeField]
    private LayerMask playerLayer;

    private PlayerManager pm;

    [SerializeField]
    private Color collactableColor, nonCollactableColor;

    [SerializeField]
    private AudioClip pickupCoin;

    [SerializeField]

    

    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }
    

    private void Update()
    {
        transform.Rotate(axis * Time.deltaTime);

        bool isTouchToPlayer = Physics.CheckSphere(transform.position, 1f, playerLayer);

        if (pm.isCollectable)
        {
            axis.y = 270;
            GetComponent<MeshRenderer>().material.color = collactableColor;
            



            if (isTouchToPlayer)
            {
                
                pm.IncreaseHealth(2.0f);
                Camera.main.GetComponent<AudioSource>().PlayOneShot(pickupCoin, 0.5f);
                
                Destroy(this.gameObject);
            }
        }
        else
        {
            axis.y = 45;
            GetComponent<MeshRenderer>().material.color = nonCollactableColor;
        }


    }
}
