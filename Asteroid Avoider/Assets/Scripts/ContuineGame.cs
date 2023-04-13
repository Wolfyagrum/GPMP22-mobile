using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContuineGame : MonoBehaviour
{
    

    public void ContuineGameAd()
    {
        print(GoogleAdMobController.Instance);
        GoogleAdMobController.Instance.ShowRewardedAd();
    }

    public void AfterAdRewardContuineGame()
    {

    }

}
