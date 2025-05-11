using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f; // 3D 사운드, 필요시 0으로 변경
    }

    public void Play(AudioClip clip, Vector3 pos, float volume = 1f)
    {
        transform.position = pos;
        source.clip = clip;
        source.volume = volume;
        source.Play();

        StartCoroutine(ReturnAfter(clip.length));
    }

    private IEnumerator ReturnAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        PoolManager.Instance.Return(gameObject);
    }
}
