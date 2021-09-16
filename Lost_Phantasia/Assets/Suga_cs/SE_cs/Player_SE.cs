using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SE : MonoBehaviour
{
    AudioSource source; 

    [SerializeField]
    AudioClip seClip;

    //足音のランダムなピッチ
    float pitchRange = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FootStep_R()
    {
        source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);

        source.PlayOneShot(seClip);
    }

    void FootStep_L()
    {
        source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);

        source.PlayOneShot(seClip);
    }
}
