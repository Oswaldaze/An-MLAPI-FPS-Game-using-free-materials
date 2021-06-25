using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private AsyncOperation mAsyncOperation = null;
    private float progressValue = 0;
    IEnumerator LoadScene()
    {
        mAsyncOperation = SceneManager.LoadSceneAsync("OpeningMenu");
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
                mAsyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public void OnClick()
    {
        StartCoroutine(LoadScene());
    }

}
