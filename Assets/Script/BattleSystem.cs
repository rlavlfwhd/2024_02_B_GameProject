using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    //싱글톤 패턴
    public static BattleSystem Instance { get; private set; }

    //캐릭터 배열
    public Character[] players = new Character[3];
    public Character[] enemies = new Character[3];

    //UI 요소들
    public Button attackBtn;                //공격 버튼
    public TextMeshProUGUI turnText;            //현재 턴 표시 텍스트
    public GameObject damageTextPrefab;     //데미지 표시용 프리팹
    public Canvas uiCanvas;                 //UI 캔버스

    //전투 관리 변수
    Queue<Character> turnQueue = new Queue<Character>();            //턴 순서 큐
    Character currentChar;                                          //현재 턴 캐릭터
    bool selectingTarget;                                           //타겟 선택 중인지 여부

    void Awake() => Instance = this;

    public Character GetCurrentChar() => currentChar;
    void OnAttackClick() => selectingTarget = true;

    void Start()
    {
        var orderedChars = players.Concat(enemies).OrderByDescending(c => c.speed);

        foreach(var c in orderedChars)
        {
            turnQueue.Enqueue(c);
        }
        attackBtn.onClick.AddListener(OnAttackClick);
        NextTurn();
    }

    void Update()
    {
        if(selectingTarget && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Character target = hit.collider.GetComponent<Character>();
                if(target != null)
                {
                    currentChar.Attack(target);
                    ShowDamageText(target.transform.position, "20");
                    selectingTarget = false;
                    NextTurn();
                }
            }
        }
    }
    
    void NextTurn()
    {
        currentChar = turnQueue.Dequeue();
        turnQueue.Enqueue(currentChar);
        turnText.text = turnText.text = $"{currentChar.name} 의 턴 (Speed:{currentChar.speed})";

        if(currentChar.isPlayer)
        {
            attackBtn.gameObject.SetActive(true);
        }
        else
        {
            attackBtn.gameObject.SetActive(false);
            Invoke("EnemyAttack", 1f);
        }
    }
    void ShowDamageText(Vector3 position, string damage)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        GameObject damageObj = Instantiate(damageTextPrefab, screenPos, Quaternion.identity, uiCanvas.transform);
        damageObj.GetComponent<TextMeshProUGUI>().text = damage;
        Destroy(damageObj, 1f);
    }

    void EnemyAttack()
    {
        var aliveTargets = players.Where(p => p.gameObject.activeSelf).ToArray();

        if (aliveTargets.Length == 0) return;

        var target = aliveTargets[Random.Range(0, aliveTargets.Length)];
        currentChar.Attack(target);
        ShowDamageText(target.transform.position, "20");
        NextTurn();
    }
}
