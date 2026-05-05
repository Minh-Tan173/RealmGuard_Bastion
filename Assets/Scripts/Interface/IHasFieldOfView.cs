using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasFieldOfView
{
    // UnLock
    public event EventHandler<FOVInfoEventArgs> UnlockFOV;

    // On / Off
    public event EventHandler<FOVInfoEventArgs> OnFOV;
    public event EventHandler OffFOV;

    // Update Size
    public event EventHandler<FOVInfoEventArgs> UpdateFOV;

    public class FOVInfoEventArgs : EventArgs {

        public SoldierSO.SoldierDirection soldierDirection;
        public float attackZone;
    }

}
