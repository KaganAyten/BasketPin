using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADManager : MonoBehaviour
{
    public BallControl deathManager;
    private InterstitialAd interstitial;
    private bool loaded;
    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-9688588223927395/9005590468";
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
        Debug.Log("Ad loaded");
    }
    void Start()
    {
        loaded = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (deathManager.GetDeathCount() == 1 && loaded==false)
        {
            RequestInterstitial();
            loaded = true;
        }
        if (deathManager.GetDeathCount() >= 3)
        {
            loaded = false;
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
                Debug.Log("AD Showed");
                deathManager.SetDeathCount(0);
                
            }
        }
    }
}
