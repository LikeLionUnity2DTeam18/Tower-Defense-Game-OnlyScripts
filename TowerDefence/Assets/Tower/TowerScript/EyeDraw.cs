using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EyeDraw : MonoBehaviour
{
    Animator anim;
    [SerializeField] private GameObject[] icons;
    private bool isClickable = true;
    // 소환 골드
    [SerializeField] private int priceGold;
    [SerializeField] private GoldController goldController;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        anim.SetBool("Enter",true);
    }
    private void OnMouseDown()
    {
        if (!isClickable) return;


        int currentGold = goldController.Model.CurrentGold;
        if (currentGold < priceGold) return;

        isClickable = false;
        anim.SetBool("Click", true);

        DrawTowerIcon();

        //골드 소모
        EventManager.Trigger<GoldSpended>(new GoldSpended(priceGold));
        SoundManager.Instance.Play(SoundType.Gacha, transform);
    }

    private void OnMouseExit() 
    {
        anim.SetBool("Enter", false);
        anim.SetBool("Click", false);
    }

    private void AnimTrigger()
    {
        anim.SetBool("Click", false);
    }

    private void DrawTowerIcon()
    {
        GameObject t = PoolManager.Instance.Get(icons[DrawRandom()]);
        t.transform.position = transform.position;
        StartCoroutine(summonEffect(t));
    }

    IEnumerator summonEffect(GameObject t)
    {
        float rand = (Random.Range(0, 2) == 0) ? -3f : 3f;
        t.transform.DOJump(new Vector3(t.transform.position.x+rand, t.transform.position.y, t.transform.position.z), 2f, 2, 0.5f);
        yield return null;
        isClickable = true;
    }


    private int DrawRandom()
    {
        int rand = Random.Range(0, 100);
        int index;
        if (rand < 99) 
        {
            index= Random.Range(0, 5);
        }
        else if (rand < 100)
        {
            index= Random.Range(5, 9);
        }
        else
        {
            index= Random.Range(9, 12);
        }
        return index;
    }


    private void OnEnable()
    {
        EventManager.AddListener<StageChangeEvent>(OnStageChange);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<StageChangeEvent>(OnStageChange);
    }

    private void OnStageChange(StageChangeEvent evt)
    {
        ActiveSwitch(evt);
    }

    public void ActiveSwitch(StageChangeEvent evt)
    {
        switch (evt.EventType)
        {
            case StageChangeEventType.Start:
                isClickable = false;
                break;
            case StageChangeEventType.End:
                isClickable = true;
                priceGold += 50;
                break;
        }
    }
}
