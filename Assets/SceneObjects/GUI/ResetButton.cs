﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void StartMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
