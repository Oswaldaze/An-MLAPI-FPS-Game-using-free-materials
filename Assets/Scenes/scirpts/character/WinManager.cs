using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    private AsyncOperation mAsyncOperation = null;
    private float progressValue = 0;
    public int TotalTime = 8;
    public int lives = 5;
    public static int maxlives;
    public Image lifebar;
    private string sceneload = "lose";
    private bool isloading = false;
    void Start()
    {
        StartCoroutine(Time());
    }
    void Update()
    {
        if (TotalTime <= 0 && isloading==false)
        {
            isloading = true;
            sceneload = "win";
            StartCoroutine(LoadScene());
        }
        if (lives == 0 && isloading == false)
        {
            isloading = true;
            sceneload = "lose";
            StartCoroutine(LoadScene());
        }
        if(lives!=0)
            lifebar.transform.localScale = new Vector3(Convert.ToSingle(Convert.ToDouble(lives) / Convert.ToDouble(maxlives)), 1, 1);
    }

    IEnumerator Time()
    {
        while (TotalTime >= 0)
        {
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
    }
    IEnumerator LoadScene()
    {
        mAsyncOperation = SceneManager.LoadSceneAsync(sceneload);
        mAsyncOperation.allowSceneActivation = false;
        while (!mAsyncOperation.isDone)
        {
            if (mAsyncOperation.progress < 0.9f)
                progressValue = mAsyncOperation.progress;
            else
                progressValue = 1.0f;

            //slider.value = progressValue;

            if (progressValue >= 0.9)
            {
                Destroy(GameObject.Find("Modeloading"));
                Destroy(GameObject.Find("NetworkManager"));
                mAsyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
