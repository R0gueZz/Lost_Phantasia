using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class DemoPause : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    Text[] pauseText;

    [SerializeField]
    AudioClip[] sounds;

    [SerializeField]
    GameObject backPanel;

    [SerializeField]
    GameObject[] button;

    private float beforeTrigger;

    public static bool isPause = false;

    public static bool isSelect = false;

    public static bool outGame = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        backPanel.SetActive(false);

        button[0].SetActive(false);
        button[1].SetActive(false);
        button[2].SetActive(false);
        button[3].SetActive(false);
        button[4].SetActive(false);
        button[5].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && isPause)
        {
            audioSource.PlayOneShot(sounds[1]);
            if(!isSelect)
            {
                backPanel.SetActive(false);
                button[0].SetActive(false);
                button[1].SetActive(false);
                button[2].SetActive(false);
                button[3].SetActive(false);
                button[4].SetActive(false);
                button[5].SetActive(false);
                Time.timeScale = 1f;
            }
            if(isSelect)
            {
                backPanel.SetActive(false);
                button[0].SetActive(false);
                button[1].SetActive(false);
                button[2].SetActive(false);
                button[3].SetActive(false);
                button[4].SetActive(false);
                button[5].SetActive(false);
                outGame = true;
                Time.timeScale = 1f;
            }
            isPause = false;
        }
        Pause();
        CursorSystem();
    }

    private void Pause()
    {
        if (Input.GetKeyDown("joystick button 7"))
        {
            pauseText[0].color = Color.black;
            pauseText[1].color = Color.white;
            backPanel.SetActive(true);
            button[0].SetActive(true);
            button[1].SetActive(false);
            button[2].SetActive(false);
            button[3].SetActive(true);
            button[4].SetActive(true);
            button[5].SetActive(true);
            Time.timeScale = 0f;
            isPause = true;
            isSelect = false;
        }
    }
    void CursorSystem()
    {
        float cursor = Input.GetAxis("D_Pad_V");
        if (isPause)
        {
            if (cursor == 1 && beforeTrigger == 0.0f)
            {
                pauseText[0].color = Color.black;
                pauseText[1].color = Color.white;
                audioSource.PlayOneShot(sounds[0]);
                button[0].SetActive(true);
                button[1].SetActive(false);
                button[2].SetActive(false);
                button[3].SetActive(true);
                isSelect = false;
            }
            if (cursor == -1 && beforeTrigger == 0.0f)
            {
                pauseText[0].color = Color.white;
                pauseText[1].color = Color.black;
                audioSource.PlayOneShot(sounds[0]);
                button[0].SetActive(false);
                button[1].SetActive(true);
                button[2].SetActive(true);
                button[3].SetActive(false);
                isSelect = true;
            }
        }
        beforeTrigger = cursor;
    }
}
