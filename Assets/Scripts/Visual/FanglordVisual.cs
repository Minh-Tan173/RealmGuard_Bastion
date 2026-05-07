using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanglordVisual : MonoBehaviour
{
    [SerializeField] private Fanglord fanglord;

    [Header("Ref Component")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // --- CONST STRING ---
    #region Up
    private const string IS_WALK_UP = "IsWalkUp";
    private const string TRIGGER_DEATH_UP = "TriggerDeathUp";
    #endregion

    #region Down
    private const string IS_WALK_DOWN = "IsWalkDown";
    private const string TRIGGER_DEATH_DOWN = "TriggerDeathDown";
    #endregion

    #region Side
    private const string IS_WALK_SIDE = "IsWalkSide";
    private const string TRIGGER_DEATH_SIDE = "TriggerDeathSide";
    #endregion

    private bool isWalkUp;
    private bool isWalkDown;
    private bool isWalkSide;

    private void Awake() {

    }

    private void Start() {

        fanglord.ChangedLeftDir += Fanglord_ChangedLeftDir;
        fanglord.ChangedRightDir += Fanglord_ChangedRightDir;
    }

    private void OnDestroy() {

        fanglord.ChangedLeftDir -= Fanglord_ChangedLeftDir;
        fanglord.ChangedRightDir -= Fanglord_ChangedRightDir;
    }

    private void Fanglord_ChangedRightDir(object sender, System.EventArgs e) {

        spriteRenderer.flipX = true;
    }

    private void Fanglord_ChangedLeftDir(object sender, System.EventArgs e) {

        spriteRenderer.flipX = false;
    }

    private void Update() {
        
    }

    private void UpdateVisual() {

        
    }
}
