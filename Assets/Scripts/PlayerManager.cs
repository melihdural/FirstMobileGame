using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class PlayerManager : MonoBehaviour
{

    #region Constant Values
    // Default Scale Ratio
    private const float size_scalel = 0.28f;

    //OverlapSphere's virtual radius
    private const float checkerRadius = 0.18f;

    //Offset for touching the enemy cylinder
    private const float offset = 0.05f;
    
    #endregion

    #region Variables

    public static PlayerManager pm;

    #region SerializedFields

    //Default Scale
    [SerializeField]
    private Vector3 default_scale = new Vector3(1, 1, 1);
    
    //LayerMask that OverlapSphere Function Recognize to Cylinder Layer 
    [SerializeField]
    private LayerMask cylinderLayer;

    [SerializeField]
    private UIManager uiMenager;

    [SerializeField]
    private AudioClip playerTouch, playerDeath;

    [SerializeField]
    private GameObject adsButton;
    
    #endregion
    
    [HideInInspector]
    public bool isCollectable = false;

    public float health = 10f;
    
    #endregion

    #region Unity

    private void Awake()
    {
        pm = this;
    }
    
    private void Update()
    {

        #region Checking Scale

        //Check any Cylinder in Contact With my Ring - cyl is a transform value of the object that contact with my ring
        Transform cyl = Physics.OverlapSphere(transform.position, checkerRadius, cylinderLayer)[0].transform;

        //cylRadius is a scale that the cylinder scale multiplied by default scale ratio 
        float cylRadius = cyl.localScale.x * size_scalel;
        
        //Are check points collectable == Are my Ring touch the cylinder
        if (cylRadius + offset > transform.localScale.y)
        {
            isCollectable = true;
        }
        else
        {
            isCollectable = false;
        }

        #endregion

        #region Death Conditions

        //Check Player Health
        if (health < 0)
        {
            Death();
        }
        
        //If my Ring's Scale smaller than cylinder Scale
        if (cylRadius > transform.localScale.y)
        {
            Death();
        }
        
        //If my Ring touch black cylinder
        if (cyl.CompareTag("Enemy"))
        {
            if (cylRadius + offset > transform.localScale.y)
            {
                Death();
            }
            
        }
        #endregion
        
        ChangeRingRadius(cylRadius);
        
        HealthCounter();
        
    }
    #endregion

    #region Functions
    public void Death()
    {
        if (Camera.main != null)
        {
            //Stop the Camera
            Camera.main.GetComponent<CameraController>().enabled = false;
            
            //Start UI Panel
            uiMenager.OpenGameOverUI();
            
            //isPlayerAlive false
            GameManager.gm.isPlayerAlive = false;
            
            //Play Death Sound Effect
            Camera.main.GetComponent<AudioSource>().PlayOneShot(playerDeath);
            
            //Save High Score
            if (GameManager.gm.distance > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", GameManager.gm.distance);
            }
            
            //Set High Score
            UIManager.uiM.SetHighScoreText();

            //Destroy myRing
            Destroy(this.gameObject);
        }

    }

    private void ChangeRingRadius(float cylRadius)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //Play Sound Effect
            if (touch.phase == TouchPhase.Began)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(playerTouch, 0.3f);
            }
            
            //Start When Player Touch the Screen. If it work changed the my Ring's radius.
            if (touch.phase == TouchPhase.Stationary)
            {
                //targetScale is a new scale of my rings when it align with any cylinder. 
                Vector3 targetScale = new Vector3(default_scale.x, cylRadius, cylRadius);
                
                //Transform my Ring's scale to target scale slowly when player touch the screen
                transform.localScale = Vector3.Slerp(transform.localScale, targetScale, 0.125f);

            }
        }
        else
        {
            //If player don't touch screen transform scale to default scale.
            transform.localScale = Vector3.Slerp(transform.localScale, default_scale, 0.250f);
                
        }

    }

    private void HealthCounter()
    {
        health = Mathf.Clamp(health, -1f, 10f);
        
        health -= Time.deltaTime;
        
        UIManager.uiM.SetPlayerHealth(health);
    }

    public void IncreaseHealth(float value)
    {
        health += value;
    }
    
    #endregion

}
