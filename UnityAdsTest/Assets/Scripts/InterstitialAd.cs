using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static InterstitialAd singletone;
    
    private string _androidAdUnitId = "Interstitial_Android";
    private string _iosAdUnitId = "Interstitial_iOS";
    private string _adUnitId;
    
    private void Awake()
    {
        singletone = this;
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosAdUnitId : _androidAdUnitId;
        LoadAd();
    }

    private void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }
    
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial Ad Ready");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Interstitial ad failed to load");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Failed to show ads");
    }

    public void OnUnityAdsShowStart(string placementId) {}

    public void OnUnityAdsShowClick(string placementId) {}

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
    }
}
