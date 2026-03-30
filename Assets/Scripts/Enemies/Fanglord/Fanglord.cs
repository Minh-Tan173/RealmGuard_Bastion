using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fanglord : BaseEnemy, IHasProgressBar {

    public event EventHandler<IHasProgressBar.OnChangeProgressEventArgs> OnChangeProgress;


    private void Awake() {
        
    }
}
