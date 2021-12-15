using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> audioClips;

    List<AudioSource> audioSource=new List<AudioSource>(0);

    [SerializeField]
    GameObject Pref;

    void Start()
    {
        for(int i=0; i < audioClips.Count; i++)
        {
            AudioSource audio = Instantiate<GameObject>(Pref).AddComponent<AudioSource>();

            print("Audio " + i + ":" + audio);
            audioSource.Add(audio);
            audioSource[i].clip = audioClips[i];
        }
        

    }

    public void PlaySound (int _indexSound)
    {
        audioSource[index: _indexSound].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
