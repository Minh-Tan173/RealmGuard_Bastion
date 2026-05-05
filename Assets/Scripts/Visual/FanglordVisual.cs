using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanglordVisual : MonoBehaviour
{
    [SerializeField] private Fanglord fanglord;

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

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isWalkUp;
    private bool isWalkDown;
    private bool isWalkSide;

    private void Awake() {

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {

        fanglord.ChangedLeftDir += Fanglord_ChangedLeftDir;
        fanglord.ChangedRightDir += Fanglord_ChangedRightDir;
    }

    private void Fanglord_ChangedRightDir(object sender, System.EventArgs e) {

        spriteRenderer.flipX = true;
    }

    private void Fanglord_ChangedLeftDir(object sender, System.EventArgs e) {

        spriteRenderer.flipX = false;
    }

    private void UpdateAnimator() {


    }
}
