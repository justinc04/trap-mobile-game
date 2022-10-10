using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string adUnitID = "Rewarded_Android";

    private void Start()
    {
        Advertisement.Initialize("4963581", true, this);
        Advertisement.Load(adUnitID, this);
    }

    public void PlayRewardedAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Show(adUnitID, this);
        }
    }

    public void OnInitializationComplete() { }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }

    public void OnUnityAdsAdLoaded(string placementId) { }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(adUnitID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            GameManager.Instance.UpdateData();
            GameManager.Instance.starsEarned *= 2;
            GameManager.Instance.gemsEarned *= 2;
            UIManager.Instance.DoubleReward();
        }
    }
}
