using System.Collections;
using UnityEngine;

public class Eksyll_Splash : TowerSplash
{
    [SerializeField] private float delayBetweenChildren = 0.1f;
    [SerializeField] private float activeDuration = 0.5f;

    private void OnEnable()
    {
        StartCoroutine(ActivateChildrenSequentially());
    }

    private IEnumerator ActivateChildrenSequentially()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
            child.GetComponent<Eksyll_Sp>().stats = stats;

            yield return new WaitForSeconds(activeDuration);

            child.gameObject.SetActive(false);

            yield return new WaitForSeconds(delayBetweenChildren);
        }
    }
}
