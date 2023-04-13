using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    private RewardedAd rewardedAd;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        string adUnityId;
#if UNITY_ANDROID
        adUnityId = "ca-app-pub-3893463479125610~3309740723";
#else
        adUnityId = "Unknown Platform"
#endif

       //this.rewardedAd = new RewardedAd(adUnityId);
       //
       //this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
       //
       //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
    }

}
