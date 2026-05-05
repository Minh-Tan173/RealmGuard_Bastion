using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewVisual : MonoBehaviour
{
    [Header("Parent")]
    [SerializeField] private Transform parent;

    [Header("Field Of View")]
    [SerializeField] private List<TypeFOV> typeFOVList;

    private Dictionary<SoldierSO.SoldierDirection, TypeFOV> fovDict;

    private IHasFieldOfView hasFieldOfView;

    private void Awake() {

        // Init dict
        fovDict = new Dictionary<SoldierSO.SoldierDirection, TypeFOV>();

        foreach(TypeFOV currentFOV in typeFOVList) {

            if (!fovDict.ContainsKey(currentFOV.soldierDirection)) {

                fovDict.Add(currentFOV.soldierDirection, currentFOV);
            }
        }

        // Parent event subcribe
        hasFieldOfView = parent.GetComponent<IHasFieldOfView>();

        if (hasFieldOfView == null) {
            Debug.LogError("Parent dont inherit hasFieldOfView interface");
        }

        hasFieldOfView.UnlockFOV += HasFieldOfView_UnlockFOV; ;
        hasFieldOfView.OnFOV += HasFieldOfView_OnFOV;
        hasFieldOfView.OffFOV += HasFieldOfView_OffFOV;
        hasFieldOfView.UpdateFOV += HasFieldOfView_UpdateFOV;
    }

    private void Start() {
        
        // After Spawn
        foreach (TypeFOV typeFOV in typeFOVList) {

            HideFOV(typeFOV.fov);
        }
    }

    private void OnDestroy() {

        hasFieldOfView.UnlockFOV -= HasFieldOfView_UnlockFOV;
        hasFieldOfView.OnFOV -= HasFieldOfView_OnFOV;
        hasFieldOfView.OffFOV -= HasFieldOfView_OffFOV;
    }


    private void HasFieldOfView_UpdateFOV(object sender, IHasFieldOfView.FOVInfoEventArgs e) {
        
        foreach (TypeFOV typeFOV in typeFOVList) {

            if (typeFOV.IsUnlocked() && typeFOV.fov.gameObject.activeSelf) {
                // If current FOV is unlocked and is active

                typeFOV.fov.localScale = new Vector3(e.attackZone * 2f, e.attackZone * 2f, e.attackZone * 2f);
            }
        }
    }

    private void HasFieldOfView_OffFOV(object sender, EventArgs e) {

        float duration = 0.2f;

        foreach (TypeFOV typeFOV in typeFOVList) {

            if (typeFOV.IsUnlocked() && typeFOV.fov.gameObject.activeSelf) {
                // If current FOV is unlocked and is active

                SpriteRenderer spriteRenderer = typeFOV.fov.GetComponent<SpriteRenderer>();

                spriteRenderer.DOKill();
                spriteRenderer.DOFade(0f, duration).SetEase(Ease.Linear).OnComplete(() => {
                    HideFOV(typeFOV.fov);
                });
            }
        }
    }

    private void HasFieldOfView_OnFOV(object sender, IHasFieldOfView.FOVInfoEventArgs e) {

        float duration = 0.2f;

        foreach (TypeFOV typeFOV in typeFOVList) {

            if (typeFOV.IsUnlocked() && !typeFOV.fov.gameObject.activeSelf) {
                // If current FOV is unlocked and is not active

                SpriteRenderer spriteRenderer = typeFOV.fov.GetComponent<SpriteRenderer>();

                Color tempColor = spriteRenderer.color;
                tempColor.a = 0f;
                spriteRenderer.color = tempColor;

                ShowFOV(typeFOV.fov);

                spriteRenderer.DOKill();
                spriteRenderer.DOFade(0.4f, duration).SetEase(Ease.Linear);
            }
        }
    }

    private void HasFieldOfView_UnlockFOV(object sender, IHasFieldOfView.FOVInfoEventArgs e) {
        
        if (fovDict[e.soldierDirection].IsUnlocked()) {
            // If this fov is unlocked
            return;
        }

        UnlockFOV(e.soldierDirection, e.attackZone);
    }

    private void UnlockFOV(SoldierSO.SoldierDirection soldierDirection, float attackZone) {


        Vector3 attackZoneSize = new Vector3(attackZone * 2f, attackZone * 2f, attackZone * 2f);

        // 1.
        Transform fov = fovDict[soldierDirection].fov;
        fov.localScale = Vector3.zero;
        fovDict[soldierDirection].SetIsUnlocked(true);
        ShowFOV(fov);

        // 2.
        Sequence unlockSequence = DOTween.Sequence();
        float duration = 0.5f;

        //
        unlockSequence.Append(fov.DOScale(attackZoneSize, duration).SetEase(Ease.Linear));
        
    }

    private void ShowFOV(Transform fov) {
        fov.gameObject.SetActive(true);
    }

    private void HideFOV(Transform fov) {
        fov.gameObject.SetActive(false);
    }

}

[System.Serializable]
public class TypeFOV {

    public SoldierSO.SoldierDirection soldierDirection;
    public Transform fov;

    private bool isUnlockedFOV;

    private TypeFOV() {

        SetIsUnlocked(false);   
    }

    public bool IsUnlocked() {
        return this.isUnlockedFOV;
    }

    public void SetIsUnlocked(bool isUnlocked) {
        this.isUnlockedFOV = isUnlocked;
    }
}

