using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOption : MonoBehaviour
{
    public Slider slider;
    public Sprite Dessert;
    public Sprite Winter;
    public Sprite Pixel;
    private bool ARGame = false;
    private bool OnlineGame = false;
    private bool Host = false;
    private bool isclicked = false;
    private Image image;
    private AsyncOperation mAsyncOperation = null;
    private float progressValue = 0;
    void Start()
    {
        GameObject.Find("ToggleAR").GetComponent<Toggle>().onValueChanged.AddListener(isOn => ARGame=isOn ? true : false);
        GameObject.Find("ToggleOnline").GetComponent<Toggle>().onValueChanged.AddListener(isOn => OnlineGame=isOn ? true : false);
        GameObject.Find("ToggleOnline").GetComponent<Toggle>().onValueChanged.AddListener(isOn => Host = isOn ? true : false);
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(OnClick);
        GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult);
        image = GameObject.Find("Image").GetComponent<Image>() ;
    }

    void Update()
    {
        if (OnlineGame == true)
        {
            GameObject.Find("IPText").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("InputField").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("ToggleHost").GetComponent<CanvasGroup>().alpha = 1;
        }
        else if(OnlineGame == false)
        {
            GameObject.Find("IPText").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("InputField").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("ToggleHost").GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void ConsoleResult(int value)
    {
        switch (value)
        {
            case 0:
                image.sprite = Dessert;
                break;
            case 1:
                image.sprite = Winter;
                break;
            case 2:
                image.sprite = Pixel;
                break;
        }
    }
    IEnumerator LoadScene()
    {
        mAsyncOperation = SceneManager.LoadSceneAsync("MainBattle"+image.sprite.name);
        mAsyncOperation.allowSceneActivation = false;
        while (!mAsyncOperation.isDone)
        {
            if (mAsyncOperation.progress < 0.9f)
                progressValue = mAsyncOperation.progress;
            else
                progressValue = 1.0f;

            slider.value = progressValue;

            if (progressValue >= 0.9)
            {
                    mAsyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

    }

    public void OnClick()
    {
        if (!isclicked) 
        {
            GameObject.Find("Dropdown").GetComponent<Dropdown>().interactable = false;
            GameObject.Find("ToggleOnline").GetComponent<Toggle>().interactable = false;
            GameObject.Find("ToggleAR").GetComponent<Toggle>().interactable = false;
            GameObject.Find("ToggleHost").GetComponent<Toggle>().interactable = false;
            GameObject.Find("InputField").GetComponent<InputField>().interactable = false;
            GameObject.Find("Modeloading").GetComponent<ModeStorage>().ARMode=ARGame;
            GameObject.Find("Modeloading").GetComponent<ModeStorage>().OnlineMode = OnlineGame;
            GameObject.Find("Modeloading").GetComponent<ModeStorage>().Host = Host;
            if (GameObject.Find("InputText").GetComponent<Text>().text!="")
                GameObject.Find("Modeloading").GetComponent<ModeStorage>().HostIpAddress = GameObject.Find("InputText").GetComponent<Text>().text;
            slider.GetComponent<CanvasGroup>().alpha = 1;
            StartCoroutine("LoadScene");

        }
    }
}
