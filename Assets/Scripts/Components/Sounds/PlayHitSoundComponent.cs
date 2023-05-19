using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayHitSoundComponent : MonoBehaviour
{
    [SerializeField] private AudioClip[] _hitClips;
    [SerializeField] private bool _playOnAwake;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (_playOnAwake) PlayHitSound();
    }

    public void PlayHitSound()
    {
        var clipNum = Random.Range(0, _hitClips.Length);
        _audio.PlayOneShot(_hitClips[clipNum]);
    }
}
