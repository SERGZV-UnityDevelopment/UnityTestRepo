using UnityEngine;

public class AdvertisingButton : MonoBehaviour
{
    [SerializeField] private AdvertisingService service;
    [SerializeField] private int energyReward;

    public void ShowInterstitialAd()
    {
        InterstitialAd.singletone.ShowAd();
    }

    public void ShowRewardAd()
    {
        RewardAd.singletone.ShowAd(energyReward);
    }
}
