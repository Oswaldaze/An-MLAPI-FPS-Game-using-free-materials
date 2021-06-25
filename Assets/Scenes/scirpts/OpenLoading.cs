using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OpenLoading : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text text;
    public Slider slider;
    private AsyncOperation mAsyncOperation = null;
    private float progressValue = 0;

    public GameObject targetobj;
    public GameObject Looktargetobj;
    public GameObject NewLooktargetobj;
    public float smoothTime = 0.5F;
    private Vector3 velocity = Vector3.zero;

    // Here jumps to start scene
    void Start()
    {
        slider.GetComponent<CanvasGroup>().alpha = 0;
        slider.GetComponent<CanvasGroup>().interactable = false;
        slider.GetComponent<CanvasGroup>().blocksRaycasts = false;
        NewLooktargetobj.SetActive(false);
    }
    IEnumerator LoadScene()
    {
        mAsyncOperation = SceneManager.LoadSceneAsync("GameOption");
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
                FocusTV();
                /*
                if (Input.anyKeyDown)
                {
                    mAsyncOperation.allowSceneActivation = true;
                }
                */
                if (Input.anyKeyDown && Camera.main.transform.position== targetobj.transform.position)
                {
                    mAsyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }

    }
    private void FocusTV()
    {
        StartCoroutine("LookTV");
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetobj.transform.position, ref velocity, smoothTime);
    }
    private void LookTV()
    {
        Looktargetobj.SetActive(false);
        NewLooktargetobj.SetActive(true);
        Camera.main.transform.LookAt(Looktargetobj.transform.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        text.SetText(" ");
        slider.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine("LoadScene");
        print("OK");

    }
}
