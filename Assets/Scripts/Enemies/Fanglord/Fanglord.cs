using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fanglord : BaseEnemy {

    public enum FanglordBehavior {
        Walk,
        Attack
    }

    public event EventHandler ChangedLeftDir;
    public event EventHandler ChangedRightDir;

    [Header("FangLord Data")]
    [SerializeField] private EnemySO fangLordSO;

    private FanglordLifeControl fanglordLifeControl;
    private EnemySpawner parentSpawner;

    private List<Transform> waypointList;

    private BaseEnemy.EnemyDirection currentFangLordDirection;

    private int targetIndex;
    private Vector3 targetPos;
    private float randomTargetTimer;

    private bool canMove = false;


    private void Awake() {

        waypointList = new List<Transform>();

        fanglordLifeControl = GetComponent<FanglordLifeControl>();
        parentSpawner = GetComponentInParent<EnemySpawner>();

    }

    private void Start() {

        parentSpawner.ActiveEnemy += ParentSpawner_ActiveEnemy;
        fanglordLifeControl.OnSpawn += FanglordLifeControl_OnSpawn;
    }

    private void FanglordLifeControl_OnSpawn(object sender, EventArgs e) {

        OnInit();
    }

    private void ParentSpawner_ActiveEnemy(object sender, EnemySpawner.OnActiveEnemyEventArgs e) {

        if (this == e.baseEnemy) {

            OnActive();
        }
    }

    private void Update() {

        HandleMovement();
    }   

    private void HandleMovement() {

        if (!canMove) { return; }

        Vector3 moveDir = (targetPos - this.transform.position).normalized;
        float sqrDistance = (this.transform.position - targetPos).sqrMagnitude;

        transform.position += moveDir * fangLordSO.moveSpeed * Time.deltaTime;

        if (sqrDistance <= 0.1f * 0.1f) {
            // If reachingTargetPoint

            if (targetIndex == waypointList.Count - 1) {
                // If reach last index

                fanglordLifeControl.ChangeLifeStateTo(EnemyLifeState.Despawn);

                return;

            }
            else {
                // If not reach last index 
                targetIndex += 1;

                targetPos = RandomWaypointPos(waypointList[targetIndex]);

                ChangeDirection();

            }
        }
        else {
            // If not reaching targetPoint

            randomTargetTimer -= Time.deltaTime;

            if (randomTargetTimer <= 0f) {
                targetPos = RandomWaypointPos(waypointList[targetIndex]);

            }
        }
    }

    private void ChangeDirection() {

        Vector3 moveDir = waypointList[targetIndex].position - this.transform.position;

        if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y)) {
            // Đang đi ngang

            if (moveDir.x < 0) {
                // Turn Left

                this.currentFangLordDirection = BaseEnemy.EnemyDirection.Left;

                ChangedLeftDir?.Invoke(this, EventArgs.Empty);
            }
            else if (moveDir.x > 0) {
                // Turn Right

                this.currentFangLordDirection = BaseEnemy.EnemyDirection.Right;

                ChangedRightDir?.Invoke(this, EventArgs.Empty);
            }


        }

        if (Mathf.Abs(moveDir.y) > Mathf.Abs(moveDir.x)) {
            // Đang đi dọc

            if (moveDir.y < 0) {
                // Turn Down

                this.currentFangLordDirection = BaseEnemy.EnemyDirection.Down;
            }
            else if (moveDir.y > 0) {
                // Turn Up

                this.currentFangLordDirection = BaseEnemy.EnemyDirection.Up;
            }

        }

    }


    private Vector3 RandomWaypointPos(Transform targetWaypoint) {

        // Mặc định đặt lại randomTargetTimer mỗi lần randomWaypoint
        randomTargetTimer = fangLordSO.randomTargetTimer;

        // 1 node có kích thước 2 * 2 với center là waypoint ---> waypointRandom nằm trong khoảng (2,2)
        float offsetX = UnityEngine.Random.Range(-fangLordSO.radiusOffset, fangLordSO.radiusOffset);
        float offsetY = UnityEngine.Random.Range(-fangLordSO.radiusOffset, fangLordSO.radiusOffset);

        Vector3 randomPos = new Vector3(targetWaypoint.position.x + offsetX, targetWaypoint.position.y + offsetY, 0f);

        return randomPos;
    }


    private void Show() {
        this.gameObject.SetActive(true);
    }

    public override void OnInit() {

        // 1. Reset Position and Next Target
        this.transform.position = waypointList[0].position;
        targetIndex = 1;

        // 2. Reset movement and direction
        ChangeDirection();
        canMove = true;
        targetPos = RandomWaypointPos(waypointList[targetIndex]);
    }

    public override void OnActive() {

        waypointList = PathGenerator.Instance.GetWaypointList();

        this.transform.position = waypointList[0].position;

        Show();

        this.ActiveEvent();

        StartCoroutine(fanglordLifeControl.RespawnCoroutine());
    }


    public override void OnDespawn() {

        this.gameObject.SetActive(false);
    }

    public EnemySO GetFangLordSO() {
        return this.fangLordSO;
    }
}
