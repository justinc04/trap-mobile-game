using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private AreaObject area;
    [SerializeField] float areaHeight;
    [SerializeField] float areaFloatDistance;
    [SerializeField] float areaFloatTime;
    [SerializeField] Camera cam;

    [SerializeField] GameObject titleMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] TMP_Text starProgressText;

    [SerializeField] TMP_Text gemsText;

    private GameObject menuAreaObject;

    private void Start()
    {
        LoadData();

        menuAreaObject = Instantiate(area.menuObject, new Vector3(0, areaHeight, 0), Quaternion.identity);
        menuAreaObject.transform.DOMoveY(areaHeight + areaFloatDistance, areaFloatTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        starProgressText.text = PlayerPrefs.GetInt($"stars{PlayerPrefs.GetInt("area")}") + "/" + area.starsToComplete;
        cam.backgroundColor = area.backgroundColor;
    }

    void LoadData()
    {
        if (!PlayerPrefs.HasKey("area"))
        {
            PlayerPrefs.SetInt("area", 1);
        }

        if (!PlayerPrefs.HasKey("gems"))
        {
            PlayerPrefs.SetInt("gems", 0);
        }

        area = Resources.Load<AreaObject>($"Areas/{PlayerPrefs.GetInt("area", 1)}");
        gemsText.text = PlayerPrefs.GetInt("gems").ToString();
    }

    public void OnClickPlay()
    {
        if (!SplashScreen.isFinished)
        {
            return;
        }

        Fade.Instance.FadeToScene(1);
    }

    public void OnClickSettings()
    {
        settingsMenu.SetActive(true);
        titleMenu.SetActive(false);
        menuAreaObject.SetActive(false);
    }

    public void OnClickBack()
    {
        titleMenu.SetActive(true);
        settingsMenu.SetActive(false);
        menuAreaObject.SetActive(true);
    }

    public void OnClickReset()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
