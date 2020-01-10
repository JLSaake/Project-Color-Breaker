using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
    /*
        A manager class for initializing and displaying Unity advertisements.
    */

    #region id Variables

    private string playstore_id = "3423699"; // Game id for Google Play Store
    private string banner_ad = "bannerAd"; // Placement id for banner ad
    private string video_ad = "video"; // Placement id for video ad

    #endregion

    #region Helper Variables

    bool testMode = false; // For testing ads, turn off (false) for release
    bool videoAdPlaying = false; // Is a video ad currently being played

    [Tooltip("Distance for the player to travel before triggering a video ad")]
    public static int distanceForAd = 1500; // Saved distance (meters), not Unity units

    #endregion

    // Initialize advertisements
    void Start()
    {
        Monetization.Initialize(playstore_id, testMode);
        Advertisement.Initialize(playstore_id, testMode);
        InitializeBannerAd();
    }

    #region Ad initialization and endings

    // Coroutine wrapper function for creating a banner ad
    public void InitializeBannerAd()
    {
        StartCoroutine("ShowBannerWhenReady");
    }

    // Place a banner ad at the bottom of the screen, if one is available
    private IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(banner_ad))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(banner_ad);
    }

    // Coroutine wrapper funtion for creating a video ad
    public void ShowVideoAd()
    {
        StartCoroutine("ShowVideoWhenReady");
    }

    // Show video ad, if one is available
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

    // When the video ad is finished, update variables
    private void VideoAdFinished(UnityEngine.Monetization.ShowResult result)
    {
        videoAdPlaying = false;
    }

    #endregion

    #region Public Helper Functions

    // Getter function for if a video ad is playing
    public bool IsVideoAdPlaying()
    {
        return videoAdPlaying;
    }

    // Have the distance requirements been met to play a video ad
    public bool DistanceSinceAdChecker()
    {
        if (PlayerPrefsController.GetDistanceSinceAd() > distanceForAd)
        {
            return true;
        }
        return false;
    }

    #endregion
     
}
