using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
    public AchievementID AchievementID;
    public string Name;
    public string Description;
    public bool Achieved;

    /// <summary>
    /// Creates an Achievement. You must also mirror the data provided here in https://partner.steamgames.com/apps/achievements/yourappid
    /// </summary>
    /// <param name="achievement">The "API Name Progress Stat" used to uniquely identify the achievement.</param>
    /// <param name="name">The "Display Name" that will be shown to players in game and on the Steam Community.</param>
    /// <param name="desc">The "Description" that will be shown to players in game and on the Steam Community.</param>
    public Achievement(AchievementID achievementID, string name, string desc)
    {
        AchievementID = achievementID;
        Name = name;
        Description = desc;
        Achieved = false;
    }
}
