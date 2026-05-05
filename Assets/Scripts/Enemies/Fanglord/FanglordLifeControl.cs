using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanglordLifeControl : MonoBehaviour, IHasProgressBar {

    // Health Bar Event
    public event EventHandler<IHasProgressBar.OnChangeProgressEventArgs> OnChangeProgress;

    public event EventHandler OnSpawn;
    public event EventHandler OnDeathAnim;

    private Fanglord fangLord;

    private BaseEnemy.EnemyLifeState currentFangLordLifeState;
    private float currentHealth;

    private void Awake() {

        fangLord = GetComponent<Fanglord>();

    }

    public void ChangeLifeStateTo(BaseEnemy.EnemyLifeState beeLifeState) {

        this.currentFangLordLifeState = beeLifeState;

        switch (currentFangLordLifeState) {

            case BaseEnemy.EnemyLifeState.Alive:


                break;

            case BaseEnemy.EnemyLifeState.Death:

                // DeathCoroutine Happen
                StartCoroutine(DeathCoroutine());

                break;

            case BaseEnemy.EnemyLifeState.Despawn:

                //fangLord.Hide();

                break;
        }

    }

    public IEnumerator RespawnCoroutine() {

        currentHealth = fangLord.GetFangLordSO().totalHealth;

        // 1. Reset Life State
        ChangeLifeStateTo(BaseEnemy.EnemyLifeState.Alive);

        yield return null;

        // 2.Reset Movement and Animator
        OnSpawn?.Invoke(this, EventArgs.Empty);

        yield return null;

        // 3. Spawn event happen
        OnChangeProgress?.Invoke(this, new IHasProgressBar.OnChangeProgressEventArgs { progressNormalized = currentHealth / fangLord.GetFangLordSO().totalHealth });
    }

    private IEnumerator DeathCoroutine() {

        //fangLord.SetCanMove(false);

        OnChangeProgress?.Invoke(this, new IHasProgressBar.OnChangeProgressEventArgs { progressNormalized = 0f });

        yield return null;

        OnDeathAnim?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(fangLord.GetFangLordSO().deathTimer);

        LevelManager.Instance.ChangedCoinTo(ILevelManager.CoinChangedState.Increase, fangLord.GetFangLordSO().enemyPrice);
        fangLord.Hide();

    }

    public void TakeDamage(float damageGet) {

        if (currentFangLordLifeState == BaseEnemy.EnemyLifeState.Death) {
            return;
        }

        currentHealth -= damageGet;

        if (currentHealth <= 0f) {

            ChangeLifeStateTo(BaseEnemy.EnemyLifeState.Death);
        }

        OnChangeProgress?.Invoke(this, new IHasProgressBar.OnChangeProgressEventArgs { progressNormalized = Mathf.Clamp01(currentHealth / fangLord.GetFangLordSO().totalHealth) });

    }

    public BaseEnemy.EnemyLifeState GetCurrentBeeLifeState() {
        return this.currentFangLordLifeState;
    }
}
