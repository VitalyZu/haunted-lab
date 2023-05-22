using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomStartSoundComponent : MonoBehaviour
{
    private AudioSource _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        var time = _audio.clip.length;
        var randomTime = Random.Range(0, time);
        _audio.time = randomTime;
        _audio.Play();
    }
}
