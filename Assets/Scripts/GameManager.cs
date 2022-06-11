using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Veriables

    [HideInInspector]
    public bool isPlayerAlive = true;
    
    public static GameManager gm;

    private GameObject player;

    [SerializeField]
    private Transform plyStartPoint;

    [SerializeField]
    private CameraController camController;

    [SerializeField]
    private float difficulty;
    
    [HideInInspector]
    public float distance;
    
    #endregion

    #region Unity
    
    private void Awake()
    {
        gm = this;
        player = GameObject.FindGameObjectWithTag("Player");
      

    }
    
    

    private void Update()
    {

        //Load Scene when player touch
            if (!isPlayerAlive)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                }

            }
                 

    
            //Set Music Controller values
            if (MusicController.musicCtrl.musicEnable == false)
            {
                MusicController.musicCtrl.GetComponent<AudioSource>().volume = 0;
                MusicController.musicCtrl.musicToggle.GetComponent<Image>().color = Color.red;
            }
            else
            {
                MusicController.musicCtrl.GetComponent<AudioSource>().volume = 1;
                MusicController.musicCtrl.musicToggle.GetComponent<Image>().color = Color.white;
            }


            //Check Player Distance
            if (player.gameObject != null)
            {
                distance = Vector3.Distance(player.transform.position, plyStartPoint.position);
                UIManager.uiM.SetDistanceValue(distance);

            }

            camController.speed += Time.timeSinceLevelLoad / 10000 * difficulty;
            camController.speed = Mathf.Clamp(camController.speed, 1, 50);
        }

    }
    #endregion

