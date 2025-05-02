using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class BaseTowerFX : MonoBehaviour
{
    [SerializeField] private Transform shakeTarget; // TilemapContainer 할당
    private void Start()
    {
        shakeTarget = GetComponent<Tilemap>().transform;
    }
    public void Shake()
    {
        shakeTarget.DOShakePosition(
            duration: 0.15f,
            strength: new Vector3(0.1f, 0, 0),
            vibrato: 10,
            randomness: 90,
            fadeOut: true
        );
    }

}
