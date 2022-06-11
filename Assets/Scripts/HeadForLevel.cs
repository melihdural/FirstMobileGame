using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadForLevel : MonoBehaviour
{
    [SerializeField]
    private LevelGenerator lg;

    [SerializeField]
    private LayerMask cyl_Layer;

    private void Update()
    {
        Collider[] cyl = Physics.OverlapSphere(transform.position, 1f, cyl_Layer);

        if (cyl.Length <= 0)
        {
            lg.SpawnCylinder();   
        }
    }
}
