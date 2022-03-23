using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static RewardAd singletone;
    
    private string _androidAdUnitId = "Rewarded_Android";
    private string _iosAdUnitId = "Rewarded_iOS";
    private string _adUnitId;

    private int _currentReward;

    private void Awake()
    {
        singletone = this;
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosAdUnitId : _androidAdUnitId;
        LoadAd();
    }
    
    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd(int energyReward)
    {
        _currentReward = energyReward;
        Advertisement.Show(_adUnitId, this);
    }
    
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Reward Ad Ready");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Reward ad failed to load");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Failed to show ads");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId) {}

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("The player watched the ad to the end, we give a reward");
            GiveReward(_currentReward);
        }
        else
        {
            Debug.Log("The player skipped the ad and will not receive a reward");
        }
        
        LoadAd();
    }

    private void GiveReward(int energyReward)
    {
        // We turn to the service of awards and credit the award there
    }
}
