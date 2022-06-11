using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{

    #region Veraibles
    
    [HideInInspector]
    public static MusicController musicCtrl;
    
    [HideInInspector]
    public bool musicEnable;

    [SerializeField]
    private AudioSource music;
    
    public GameObject musicToggle;

    #endregion

    #region Unity
    
    private void Awake()
    {
        musicCtrl = this;
        musicEnable = IntToBool(PlayerPrefs.GetInt("musicEnable"));

    }
    
    #endregion

    #region Functions 
    
    public void ToggleMusic()
    {

        if (!musicEnable)
        {
            //Enable Music  
            music.Play();
            musicEnable = true;
            musicToggle.GetComponent<Image>().color = Color.white;
            PlayerPrefs.SetInt("musicEnable", BoolToInt(true));


        }
        else
        {
            //Disable Music
            music.Pause();
            musicEnable = false;
            musicToggle.GetComponent<Image>().color = Color.red;
            PlayerPrefs.SetInt("musicEnable", BoolToInt(false));

        }

    }

    
    //Convert to values that belongs to musicEnable for PlayerPrefs
    private int BoolToInt(bool value)
    {
        if (value)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
    private bool IntToBool(int value)
    {
        if (value != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    #endregion

}
