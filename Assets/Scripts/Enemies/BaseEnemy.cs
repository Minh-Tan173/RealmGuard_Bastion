using System;
using TMPro;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public enum DamageResistance {

        None,
        PhysicResistance,
        MagicResistance

    }

    public enum EnemyLifeState {

        Alive,
        Death,
        Despawn
    }

    public enum EnemyDirection {
        Default,
        Up,
        Down,
        Left,
        Right

    }

    // Active State Event
    public static event EventHandler OnActiveEnemy;
    public static event EventHandler UnActiveEnemy;

    // Animator Event
    public event EventHandler ChangedLeftDir;
    public event EventHandler ChangedRightDir;


    public void OnDisable() {
        // When enemy inherit was Hide or Destroy

        UnActiveEnemy?.Invoke(this, EventArgs.Empty);
    }

    public void ActiveEvent() {

        OnActiveEnemy?.Invoke(this, EventArgs.Empty);
    }

    public EnemyDirection ChangeDirection(Vector3 targetPos) {

        Vector3 moveDir = targetPos - this.transform.position;

        if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y)) {
            // Đang đi ngang

            if (moveDir.x < 0) {
                // Turn Left

                ChangedLeftDir?.Invoke(this, EventArgs.Empty);
                return BaseEnemy.EnemyDirection.Left;

            }
            else if (moveDir.x > 0) {
                // Turn Right

                ChangedRightDir?.Invoke(this, EventArgs.Empty);
                return BaseEnemy.EnemyDirection.Right;
            }


        }

        if (Mathf.Abs(moveDir.y) > Mathf.Abs(moveDir.x)) {
            // Đang đi dọc

            if (moveDir.y < 0) {
                // Turn Down

                return BaseEnemy.EnemyDirection.Down;
            }
            else if (moveDir.y > 0) {
                // Turn Up

                return BaseEnemy.EnemyDirection.Up;
            }

        }

        Debug.LogError("Something wrong with direction calculate");
        return EnemyDirection.Default;
    }

    public virtual void OnInit() {
        Debug.LogError("Trigger baseEnemy");
    }

    public virtual void OnActive() {
        Debug.LogError("Trigger baseEnemy");
    }

    public virtual void OnDespawn() {
        Debug.LogError("Trigger baseEnemy");
    }

    public virtual void HitDamage(float damageGet) {
        Debug.LogError("Trigger baseEnemy");
    }

    public virtual bool IsCantAttack() {
        Debug.LogError("Trigger baseEnemy");
        return true;
    }

    public virtual bool IsResistPhysic() {
        Debug.LogError("Trigger baseEnemy");
        return true;
    }

    public virtual bool IsResistMagic() {
        Debug.LogError("Trigger baseEnemy");
        return true;
    }

    public virtual Vector3 GetEnemyVelocity() {
        Debug.LogError("Trigger baseEnemy");    
        return Vector3.zero;
    }

    public virtual float GetEnemyProgress() {
        Debug.LogError("Trigger baseEnemy");
        return 0f;
    }

    public virtual float GetEnemyHealth() {
        Debug.LogError("Trigger baseEnemy");
        return 0f;
    }

    public virtual EnemyDirection GetEnemyCurrentDirection() {
        Debug.LogError("Trigger baseEnemy");
        return EnemyDirection.Down;
    }
    
    public static BaseEnemy SpawnEnemy(EnemySO enemySO, Transform parent) {

        Transform enemyTransform = Instantiate(enemySO.prefab, parent);

        enemyTransform.localPosition = Vector3.zero;

        BaseEnemy baseEnemy = enemyTransform.GetComponent<BaseEnemy>();

        return baseEnemy;

    }

}
