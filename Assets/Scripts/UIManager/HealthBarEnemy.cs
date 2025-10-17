using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image emptyBar;
    [SerializeField] private TextMeshProUGUI runTimeId;
    [SerializeField] private TextMeshProUGUI belowText;

    private Transform targetTransform;
    private Camera mainCamera;
    private RectTransform hbRectTransform;
    private int realTimeId;
    private Vector3 offset = new Vector3(0, 2.0f, 0);

    public void Init(int enemyRealTimeId)
    {
        realTimeId = enemyRealTimeId;

        UnitBase unitbase = EnemyManager.Instance.EnemyDict[realTimeId];
        targetTransform = unitbase.transform;
        mainCamera = FollowingCamera.Instance.GetComponent<Camera>();
        hbRectTransform = GetComponent<RectTransform>();
        UpdateIdText();
        UpdateHp();
    }

    private void LateUpdate()
    {
        if (targetTransform == null || mainCamera == null || hbRectTransform == null) return;

        Vector3 worldPos = targetTransform.position + offset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
        hbRectTransform.position = screenPos;
    }


    public void UpdateHp()
    {
        int hp = EnemyManager.Instance.GetEnemyData(realTimeId).Hp;
        int maxHp = EnemyManager.Instance.GetEnemyData(realTimeId).MaxHp;
        belowText.text = hp.ToString() + "/" + maxHp.ToString();
        if (hp <= 0)
        {
            emptyBar.fillAmount = 1;
        }
        else
        {
            emptyBar.fillAmount = 1 - (float)hp / maxHp;
        }
    }

    private void UpdateIdText()
    {
        runTimeId.text = realTimeId.ToString();
    }


}
