using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAchievement : MonoBehaviour
{
  public AchievementID TargetAchievement;

  void OnTriggerEnter2D(Collider2D col){
    if (col.tag == "Player"){
      AchievementManager.TryUnlockAchievement(TargetAchievement);
    }
  }
}
