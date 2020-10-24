using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Advertisements;
using Debug = UnityEngine.Debug;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

  string gameId = "3869783";
  string myPlacementId = "rewardedVideo";
  bool testMode = true;

// Initialize the Ads listener and service:
  void Start()
  {
    Advertisement.AddListener(this);
    Advertisement.Initialize(gameId, testMode);
  }

  public void ShowRewardedVideo()
  {
// Check if UnityAds ready before calling Show method:
    if (Advertisement.IsReady(myPlacementId))
    {
      Advertisement.Show(myPlacementId);
    }
    else
    {
      Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
    }
  }

  
// Implement IUnityAdsListener interface methods:
  public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
  {
    switch (showResult)
    {
      case ShowResult.Finished:
        GameManager.Instance.Player.AddGams(100);
        UIManager.Instance.OpenShop(GameManager.Instance.Player.diamonds);
        Debug.Log("You finish the Add! Here`s 100 Gems!");
        break;
      case ShowResult.Skipped:
        UIManager.Instance.UpdateGemCount(GameManager.Instance.Player.diamonds);
        Debug.LogWarning("You scipped the Add! No money for you!");
        break;
      case ShowResult.Failed:
        Debug.LogWarning("The ad did not finish due to an error.");
        break;
    }
  }

  public void OnUnityAdsReady(string placementId)
  {
// If the ready Placement is rewarded, show the ad:
    if (placementId == myPlacementId)
    {
      // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
    }
  }

  public void OnUnityAdsDidError(string message)
  {
// Log the error.
  }

  public void OnUnityAdsDidStart(string placementId)
  {
// Optional actions to take when the end-users triggers an ad.
  }

// When the object that subscribes to ad events is destroyed, remove the listener:
  public void OnDestroy()
  {
    Advertisement.RemoveListener(this);
  }
}