using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TMP_Text stonesText;
    [SerializeField] TMP_Text outOfStonesText;
    [SerializeField] CanvasGroup gameOverMenu;
    [SerializeField] Image[] starImages;
    [SerializeField] Color starColor;

    [SerializeField] Button retryButton;
    [SerializeField] Sprite nextImage;

    [SerializeField] TMP_Text starsEarnedText;
    [SerializeField] CanvasGroup starsEarnedPanel;

    [SerializeField] TMP_Text gemsText;
    [SerializeField] CanvasGroup gemsEarnedPanel;
    [SerializeField] TMP_Text gemsEarnedText;

    [SerializeField] CanvasGroup doubleRewardsButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverMenu.gameObject.SetActive(false);
        gameOverMenu.alpha = 0;
        SetGemsText(PlayerPrefs.GetInt("gems"));
    }

    public void SetStonesText(int value)
    {
        stonesText.text = value.ToString();
    }

    void SetGemsText(int value)
    {
        gemsText.text = value.ToString();   
    }

    public async void Won()
    {
        await Task.Delay(1300);

        OpenGameOverMenu();
        retryButton.image.sprite = nextImage;

        await Task.Delay(400);

        for (int i = 0; i < GameManager.Instance.starsEarned; i++)
        {
            await Task.Delay(400);
            starImages[i].DOColor(starColor, .4f).SetEase(Ease.Linear);
        }

        await Task.Delay(700);
        starsEarnedText.text = "+" + GameManager.Instance.starsEarned;
        starsEarnedPanel.transform.DOLocalMoveY(starsEarnedPanel.transform.localPosition.y - 30, .5f).SetEase(Ease.OutSine);
        starsEarnedPanel.DOFade(1, .4f).SetEase(Ease.Linear);

        await Task.Delay(700);
        gemsEarnedText.text = "+" + GameManager.Instance.gemsEarned;
        gemsEarnedPanel.transform.DOLocalMoveY(gemsEarnedPanel.transform.localPosition.y - 30, .5f).SetEase(Ease.OutSine);
        gemsEarnedPanel.DOFade(1, .4f).SetEase(Ease.Linear);

        await Task.Delay(500);
        SetGemsText(PlayerPrefs.GetInt("gems"));
        gemsText.rectTransform.DOPunchAnchorPos(15 * Vector2.up, .3f, 1, 0);

        await Task.Delay(700);
        doubleRewardsButton.gameObject.SetActive(true);
        doubleRewardsButton.DOFade(1, .4f).SetEase(Ease.Linear);

        await Task.Delay(1500);
    }

    public async void Lost()
    {
        await Task.Delay(700);

        outOfStonesText.rectTransform.DOLocalMoveY(outOfStonesText.rectTransform.localPosition.y - 30, .5f).SetEase(Ease.OutSine);
        outOfStonesText.DOFade(1, .5f).SetEase(Ease.Linear);

        await Task.Delay(1500);

        outOfStonesText.rectTransform.DOLocalMoveY(outOfStonesText.rectTransform.localPosition.y - 30, .5f).SetEase(Ease.InSine);
        outOfStonesText.DOFade(0, .5f).SetEase(Ease.Linear);

        await Task.Delay(1000);

        OpenGameOverMenu();
    }

    public async void DoubleReward()
    {
        doubleRewardsButton.gameObject.SetActive(false);

        await Task.Delay(700);
        starsEarnedText.text = "+" + GameManager.Instance.starsEarned;
        gemsEarnedText.text = "+" + GameManager.Instance.gemsEarned;
        starsEarnedText.rectTransform.DOPunchAnchorPos(15 * Vector2.up, .3f, 1, 0);
        gemsEarnedText.rectTransform.DOPunchAnchorPos(15 * Vector2.up, .3f, 1, 0);

        await Task.Delay(500);
        SetGemsText(PlayerPrefs.GetInt("gems"));
        gemsText.rectTransform.DOPunchAnchorPos(15 * Vector2.up, .3f, 1, 0);
    }

    void OpenGameOverMenu()
    {
        gameOverMenu.gameObject.SetActive(true);
        gameOverMenu.DOFade(1, .3f).SetEase(Ease.Linear);
        stonesText.DOFade(0, .3f).SetEase(Ease.Linear);
    }

    public void OnClickRetry()
    {
        Fade.Instance.FadeToScene(1);
    }

    public void OnClickHome()
    {
        Fade.Instance.FadeToScene(0);
    }
}
