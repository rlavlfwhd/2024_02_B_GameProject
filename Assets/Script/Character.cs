using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    //캐릭터 스탯
    public bool isPlayer;               //플레이어 여부
    public int hp = 100;                //체력
    public int speed;                   //속도 (턴 순서 결정)

    //UI 요소
    TextMeshProUGUI nameText;           //이름 표시
    TextMeshProUGUI hpText;             //HP 표시
    Vector3 startPos;                   //시작 위치 (공격 애니메이션 용)

    void Start()
    {
        SetupNameText();
        startPos = transform.position;
    }
    //UI 텍스트 초기화
    void SetupNameText()
    {
        //이름 텍스트 설정
        GameObject textObj = new GameObject("NameText");
        textObj.transform.SetParent(BattleSystem.Instance.uiCanvas.transform);
        nameText = textObj.AddComponent<TextMeshProUGUI>();
        nameText.text = isPlayer ? "P" : "E";
        nameText.fontSize = 36;
        nameText.alignment = TextAlignmentOptions.Center;

        //HP 텍스터 설정
        GameObject hpObj = new GameObject("HPText");
        hpObj.transform.SetParent(BattleSystem.Instance.uiCanvas.transform);
        hpText = hpObj.AddComponent<TextMeshProUGUI>();
        hpText.fontSize = 30;
        hpText.alignment = TextAlignmentOptions.Center;
    }

    public void Attack(Character target)
    {
        if (!target.gameObject.activeSelf) return;
        StartCoroutine(AttackRoutine(target));

    }

    private void Update()
    {
        if(!gameObject.activeSelf)
        {
            if (nameText != null) Destroy(nameText.gameObject);
            if (hpText != null) Destroy(hpText.gameObject);
            return;
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        nameText.transform.position = screenPos;
        hpText.transform.position = screenPos + Vector3.down * 30;

        nameText.color = (BattleSystem.Instance.GetCurrentChar() == this) ? Color.green : Color.white;
        hpText.text = hp.ToString();
    }

    IEnumerator AttackRoutine(Character target)
    {
        Vector3 attackPos = target.transform.position + (target.transform.position - transform.position).normalized * 1.5f;
        float moveTime = 0.3f;
        float elapsed = 0;

        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            transform.position = Vector3.Lerp(startPos, attackPos, t);
            yield return null;
        }
        target.hp -= 20;
        if(target.hp <= 0) target.gameObject. SetActive(false);
        elapsed = 0;
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            transform.position = Vector3.Lerp(attackPos, startPos, t);
            yield return null;
        }
        transform.position = startPos;
    }
}
