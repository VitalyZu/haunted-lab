using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YandexMobileAds;
using YandexMobileAds.Base;

public class InterstitialAd : MonoBehaviour
{
    [SerializeField] private UnityEvent _callback;

    private Interstitial interstitial;

    public void RequestInterstitial()
    {
        string adUnitId = "R-M-2429645-1";

        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        interstitial = new Interstitial(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();

        interstitial.OnInterstitialLoaded += HandleInterstitialLoaded;
        interstitial.OnInterstitialFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnReturnedToApplication += HandleReturnedToApplication;

        interstitial.OnAdClicked += HandleReturnedToApplication;

        interstitial.OnInterstitialDismissed += HandleReturnedToApplication;
        interstitial.OnImpression += HandleImpression;
        interstitial.OnInterstitialFailedToShow += HandleInterstitialFailedToShow;

        interstitial.LoadAd(request);
    }

    private void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        _callback?.Invoke();
        interstitial.Destroy();
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        _callback?.Invoke();
        interstitial.Destroy();
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        _callback?.Invoke();
        interstitial.Destroy();
    }

    public void HandleInterstitialFailedToShow(object sender, AdFailureEventArgs args)
    {
        _callback?.Invoke();
        interstitial.Destroy();
    }
}
