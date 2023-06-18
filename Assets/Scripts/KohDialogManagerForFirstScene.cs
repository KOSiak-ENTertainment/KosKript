using System;
using System.Collections;
using System.Collections.Generic;
using GameManagementScripts;
using UnityEngine;
using UnityEngine.UI;

public class KohDialogManagerForFirstScene : MonoBehaviour
{
    public GameManagerScript gameManager;
    public Text kohTextUi;
    public AudioClip audioToPlay;
    public AudioSource currentAudioSource;
    public GameManagerScript.GameStates savedGameState;
    public GameManagerScript.OrderLoading savedOrderState;
    public bool isAudioPlayed;
    
}
