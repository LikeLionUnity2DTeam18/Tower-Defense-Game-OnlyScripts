using UnityEngine;
using TMPro;
using DG.Tweening; // 반드시 추가!

public class GoldPopUp : MonoBehaviour
{
    [SerializeField] private GameObject moneyPopupPrefab; // "+50" 프리팹
    [SerializeField] private RectTransform popupsRoot;    // Canvas 안 PopupsRoot



    private void CreateMoneyPopup(Vector3 worldPos, int goldAmount)
    {
        // 1. 몬스터 월드 좌표를 화면 좌표로 변환
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 2. 팝업 프리팹 인스턴스 생성
        GameObject popup = Instantiate(moneyPopupPrefab, popupsRoot);
        popup.transform.position = screenPos;

        // 3. 텍스트 내용 설정
        TextMeshProUGUI text = popup.GetComponent<TextMeshProUGUI>();
        text.text = $"+{goldAmount}G";

        // 4. DOTween 애니메이션 설정
        RectTransform rect = popup.GetComponent<RectTransform>();

        Sequence seq = DOTween.Sequence();
        seq.Append(rect.DOMoveY(rect.position.y + 50f, 0.5f))  // 1초 동안 위로 100 이동
           .Join(text.DOFade(0f, 0.5f))                         // 동시에 1초 동안 페이드아웃
           .OnComplete(() => Destroy(popup));                // 끝나면 삭제
    }
    
    private void OnMonsterDied(MonsterDied evt)
    {
        CreateMoneyPopup(evt.Position, evt.RewardGold);
    }

    private void OnEnable()
    {
        EventManager.AddListener<MonsterDied>(OnMonsterDied);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<MonsterDied>(OnMonsterDied);
    }
}
