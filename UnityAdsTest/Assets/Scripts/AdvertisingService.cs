using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisingService : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private bool testMode;
    
    private string _androidGameId = "4673085";
    private string _iosGameId = "4673084";
    private string _gameId;

    private void Awake()
    {
        InitializeAds();
    }

    private void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosGameId : _androidGameId;
        Advertisement.Initialize(_gameId, testMode);
    }
    
    public void OnInitializationComplete()
    {
        Debug.Log("Ad initialization successful");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Advertising initialization failure");
    }
}
