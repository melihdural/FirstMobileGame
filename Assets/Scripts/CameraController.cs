using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public float speed = 1.0f;
   private void Update()
   {
      transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
   }
}
