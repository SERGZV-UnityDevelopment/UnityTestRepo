using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BannerButton : MonoBehaviour
{
    [SerializeField] private BannerPosition bannerPosition = BannerPosition.TOP_CENTER;
    [SerializeField] private Button showBannerButton;

    private string _androidAdUnitId = "Banner_Android";
    private string _iosAdUnitId = "Banner_iOS";
    private string _adUnitId;

    private void Awake()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosAdUnitId : _androidAdUnitId;
        showBannerButton.interactable = false;
    }

    private void Start()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        LoadAd();
    }

    private void LoadAd()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        
        Advertisement.Banner.Load(_adUnitId, options);
    }
    
    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        showBannerButton.onClick.AddListener(ShowBannerAd);
        showBannerButton.interactable = true;
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}, load another banner");
        LoadAd();
    }
    
    private void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            showCallback = OnBannerShown,
            hideCallback = OnBannerHidden
        };
        
        
        Advertisement.Banner.Show(_adUnitId, options);
        showBannerButton.interactable = false;
    }
    
    void OnBannerClicked() {}
    void OnBannerShown() {}
    void OnBannerHidden() {}
}
