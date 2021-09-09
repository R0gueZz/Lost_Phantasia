    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    delegate void Func();
    delegate int Value();

    enum State
    {
        Pause,
        Option,
        Game,
    }

    State state = State.Game;

    List<Func> escape = new List<Func>();


    List<Func> button = new List<Func>();
    [SerializeField]
    List<RectTransform> PausebuttonObj;
    [SerializeField]
    List<RectTransform> OptionbuttonObj;

    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject OptionMenu;

    [SerializeField]
    RectTransform arrowObj;

    int selectCount = 0;

    [SerializeField]
    Slider bgmslider;
    [SerializeField]
    Text bgmVoltext;
    int gbmVolume = 40;

    [SerializeField]
    Slider seslider;
    [SerializeField]
    Text seVoltext;
    int seVolme = 40;

    [SerializeField]
    string TitleScene;

    // Start is called before the first frame update
    void Start()
    {
        button.AddRange(new Func[] { ReturnGame, OptionOpen, ReturnTitle });
        escape.AddRange(new Func[] { ReturnGame, ReturnPause });

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Pause:
                Select();
                ArrowSet(PausebuttonObj);
                break;
            case State.Option:
                Select();
                ArrowSet(OptionbuttonObj);
                break;
            case State.Game:
                if (Input.GetKeyDown("joystick button 7")) 
                {
                    state = State.Pause;
                    pauseMenu.SetActive(true);
                    arrowObj.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
                break;
        }

        
    }

    float oldH;
    float oldV;
    void Select()
    {

        if (Input.GetAxis("D_Pad_V") == 1 && oldV != Input.GetAxis("D_Pad_V"))
        {
            selectCount--;
            selectCount = Mathf.Clamp(selectCount, 0, button.Count - 1);
        }
        if (Input.GetAxis("D_Pad_V") == -1 && oldV != Input.GetAxis("D_Pad_V"))
        {
            selectCount++;
            selectCount = Mathf.Clamp(selectCount, 0, button.Count - 1);
        }
        if (Input.GetKeyDown("joystick button 0"))
        {
            button[selectCount]();
        }
        if ((selectCount == 0 || selectCount == 1) && state == State.Option && Input.GetAxis("D_Pad_H") == 1 && Input.GetAxis("D_Pad_H") != oldH)
        {
            VolSet += 10;
            VolSet = Mathf.Clamp(VolSet, 0, 100);
            
        }
        if ((selectCount == 0 || selectCount == 1) && state == State.Option && Input.GetAxis("D_Pad_H") == -1 && Input.GetAxis("D_Pad_H") != oldH)
        {
            VolSet -= 10;
            VolSet = Mathf.Clamp(VolSet, 0, 100);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escape[(int)state]();
        }
        oldV = Input.GetAxis("D_Pad_V");
        oldH = Input.GetAxis("D_Pad_H");
    }

    void ArrowSet(List<RectTransform> buttonObj)
    {
        arrowObj.position = buttonObj[selectCount].position - new Vector3(150, 0, 0);
    }



    void ReturnGame()
    {
        Debug.Log("ReturnGame");
        state = State.Game;
        pauseMenu.SetActive(false);
        arrowObj.gameObject.SetActive(false);
        Time.timeScale = 1;

    }
    void OptionOpen()
    {
        state = State.Option;
        Debug.Log("Option");

        pauseMenu.SetActive(false);
        OptionMenu.SetActive(true);

        selectCount = 0;

        button = new List<Func>();
        button.AddRange(new Func[] { BGMVol, SEVol, ReturnPause });


    }
    void ReturnTitle()
    {
        Debug.Log("ReturnTitle");

    }

    void BGMVol()
    {

    }
    void SEVol()
    {

    }
    void ReturnPause()
    {
        state = State.Pause;
        pauseMenu.SetActive(true);
        OptionMenu.SetActive(false);
        selectCount = 0;
        button = new List<Func>();
        button.AddRange(new Func[] { ReturnGame, OptionOpen, ReturnTitle });
    }

    int VolSet
    {
        get
        {
            if (selectCount == 0)
            {
                return gbmVolume;
            }

            if (selectCount == 1)
            {
                return seVolme ;
            }
            return 0;
        }
        set {
            if (selectCount == 0)
            {
                gbmVolume = value;
                bgmslider.value = value;
                bgmVoltext.text = value.ToString();

            }
        
            if(selectCount == 1)
            {
                seVolme = value;
                seslider.value = value;
                seVoltext.text = value.ToString();
                
            }
        }
       

    }
}
