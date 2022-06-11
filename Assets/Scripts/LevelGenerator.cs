using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    
    #region Veriables

    #region SerializeFields

    [Header("Cylinder Attributes")]
    
    [Tooltip("Default cylinder prefabs for instantiate")]
    [SerializeField]
    private GameObject cylinder;

    [Tooltip("Minimum radius for cylinder size")]
    [SerializeField]
    private float minRadius;
    
    [Tooltip("Maximum radius for cylinder size")]
    [SerializeField]
    private float maxRadius;
    
    [Header("Enemy Cylinder Attributes")]
    
    [SerializeField]
    private Color enemy_cylinder;
    
    #endregion
    
    private GameObject previous_cylinder;

    #endregion
    
    #region Function
    private float FindRadius(float minR, float maxR)
    {
        float radius = Random.Range(minR, maxR);

        if (previous_cylinder != null)
        {
            while (Mathf.Abs(radius - previous_cylinder.transform.localScale.x) < 0.4f)
            {
                radius = Random.Range(minR, maxR);
            }
        }

        return radius;

    }

    public void SpawnCylinder()
    {      
            //Random Cylinder Scale
            float radius = FindRadius(minRadius, maxRadius);
            float height = Random.Range(1f, 5f);

            //Apply Cylinder Scale to Prefabs
            cylinder.transform.localScale = new Vector3(radius, height, radius);
            
            //Instantiate First Cylinder
            if (previous_cylinder == null)
            {
                previous_cylinder = Instantiate(cylinder, Vector3.zero, Quaternion.identity);
            }
            
            //Instantiate All Other Cylinders
            else
            {
                float spawnPoint = previous_cylinder.transform.position.z + previous_cylinder.transform.localScale.y + cylinder.transform.localScale.y;
                previous_cylinder = Instantiate(cylinder, new Vector3(0,0,spawnPoint), Quaternion.identity);

                if (Random.value < 0.1f)
                {
                    previous_cylinder.GetComponent<Renderer>().material.color = enemy_cylinder;
                    previous_cylinder.tag = "Enemy";
                }
            }
            
            //Rotate Cylinder
            previous_cylinder.transform.Rotate(90, 0, 0);    


        
    }
    
    #endregion

    
} 
