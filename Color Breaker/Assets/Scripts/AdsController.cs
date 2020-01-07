using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{

    private string playstore_id = "3423699";
    private string banner_ad = "bannerAd";
    private string video_ad = "video";
    bool testMode = true; // For testing ads, turn off for release
    bool videoAdPlaying = false;

    [Tooltip("Distance for the player to travel before triggering a video ad")]
    public static int distanceForAd = 2000;

    // Start is called before the first frame update
    void Start()
    {
        Monetization.Initialize(playstore_id, testMode);
        Advertisement.Initialize(playstore_id, testMode);
        InitializeBannerAd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeBannerAd()
    {
        StartCoroutine("ShowBannerWhenReady");
    }

    private IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(banner_ad))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(banner_ad);
    }

    public void ShowVideoAd()
    {
        StartCoroutine("ShowVideoWhenReady");
    }

    private IEnumerator ShowVideoWhenReady()
    {
        while (!Monetization.IsReady(video_ad))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent (video_ad) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(VideoAdFinished);
            videoAdPlaying = true;
        }
    }

    private void VideoAdFinished(UnityEngine.Monetization.ShowResult result)
    {
        videoAdPlaying = false;
    }

    public bool IsVideoAdPlaying()
    {
        return videoAdPlaying;
    }

    public bool DistanceSinceAdChecker()
    {
        if (PlayerPrefsController.GetDistanceSinceAd() > distanceForAd)
        {
            return true;
        }
        return false;
    }
     
}
