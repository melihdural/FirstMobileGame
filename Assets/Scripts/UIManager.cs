using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Veriables

    public static UIManager uiM;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private TMP_Text distanceValue;

    [SerializeField]

    private RectTransform healthBar;

    [SerializeField]
    private TMP_Text highScoreValue;
    
    #endregion

    #region Unity
    
    private void Awake()
    {
        uiM = this;
    }
    
    #endregion

    #region Functions

    public void SetPlayerHealth(float health)
    {
        healthBar.localScale = new Vector3(health / 10, 1, 1);
    }
    
    public void OpenGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void SetDistanceValue(float distance)
    {
        distanceValue.text = distance.ToString("F1");
    }

    public void SetHighScoreText()
    {
        highScoreValue.text = PlayerPrefs.GetFloat("HighScore").ToString("F1");
    }
    
    #endregion

}
