using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> audioClips;

    AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound (int _indexSound)
    {
        audioSource.clip = audioClips[_indexSound];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
