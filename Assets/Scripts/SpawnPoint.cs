using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject point;

    private void Start()
    {
        if (!this.gameObject.CompareTag("Enemy"))
        {
            CreatePoints();
        }
    }

    private void CreatePoints()
    {
        float radiusCyl = transform.localScale.x / 2;
        float radiusCube = point.transform.localScale.x / 2;
        float height = radiusCube + radiusCyl;

        float minRange = transform.position.z - transform.localScale.y +1.25f;
        float maxRange = transform.position.z + transform.localScale.y -1.25f;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + height, Random.Range(minRange, maxRange));
        

        Instantiate(point, pos, Quaternion.identity);

    }
}
