using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_SE : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip sound1;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        Invoke("Sedes", 1.0f);
    }

    void Sedes()
    {
        Destroy(gameObject);
    }
}
