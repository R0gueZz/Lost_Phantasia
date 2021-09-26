using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;
    GameObject fadeCanvas;

    [SerializeField]
    AudioClip[] sounds;
    
    //ƒ^ƒCƒgƒ‹ŠÖŒW
    [SerializeField]
    Image[] cursorObj;

    private float beforeTrigger;

    bool gameSelect = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("findFadeObj", 0.02f);
        audioSource = GetComponent<AudioSource>();  
    }

    void findFadeObj()
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade");
        fadeCanvas.GetComponent<FadeManager>().fadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Title")
        {
            CursorSystem();
            if (Input.GetButtonDown("Jump"))
            {
                audioSource.PlayOneShot(sounds[1]);
                if (gameSelect) sceneChange1();
                if(!gameSelect)
                {
                    #if UNITY_EDITOR
                          UnityEditor.EditorApplication.isPlaying = false;
                    #elif UNITY_STANDALONE
                          UnityEngine.Application.Quit();
                    #endif
                }
            }
        }
        if(SceneManager.GetActiveScene().name == "Player_Scene")
        {
            if(DemoPause.outGame)
            {
                DemoPause.outGame = false;
                DemoPause.isSelect = false;
                sceneChange2();
            }
            if(Input.GetKeyDown(KeyCode.C))
            {
                sceneChange3();
            }
        }
    }

    async void sceneChange1()
    {
        fadeCanvas.GetComponent<FadeManager>().fadeOut();
        await Task.Delay(1000);
        SceneManager.LoadScene("Player_Scene");
    }

    async void sceneChange2()
    {
        fadeCanvas.GetComponent<FadeManager>().fadeOut();
        await Task.Delay(1000);
        SceneManager.LoadScene("Title");
    }

    async void sceneChange3()
    {
        fadeCanvas.GetComponent<FadeManager>().fadeOut();
        await Task.Delay(1000);
        SceneManager.LoadScene("Clear");
    }


    void CursorSystem()
    {
        float cursorButton = Input.GetAxis("D_Pad_V");
        if(Input.GetAxis("D_Pad_V") == 1 && beforeTrigger == 0.0f)
        {
            audioSource.PlayOneShot(sounds[0]);
            cursorObj[0].gameObject.SetActive(true);
            cursorObj[1].gameObject.SetActive(false);
            gameSelect = true;
        }
        if (Input.GetAxis("D_Pad_V") == -1 && beforeTrigger == 0.0f)
        {
            audioSource.PlayOneShot(sounds[0]);
            cursorObj[1].gameObject.SetActive(true);
            cursorObj[0].gameObject.SetActive(false);
            gameSelect = false;
        }
        beforeTrigger = cursorButton;
    }

}
