﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pumpkin : Dialog2 {

    public void ChangeToHalloween() {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }
}
