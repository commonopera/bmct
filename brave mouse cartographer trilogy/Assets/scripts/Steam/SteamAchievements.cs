using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SteamAchievements : MonoBehaviour
{
    private CGameID m_GameID;

    private bool m_bRequestedStats;
    private bool m_bStatsValid;

    private bool m_bStoreStats;

    protected Callback<UserStatsReceived_t> m_UserStatsReceived;
    protected Callback<UserStatsStored_t> m_UserStatsStored;
    protected Callback<UserAchievementStored_t> m_UserAchievementStored;

    void Start()
    {
        if (!SteamManager.Initialized)
            return;

        m_GameID = new CGameID(SteamUtils.GetAppID());
        m_UserStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
        m_UserStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
        m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

        m_bRequestedStats = false;
        m_bStatsValid = false;
    }

    private void Update()
    {
        if (!SteamManager.Initialized)
            return;

        if (!m_bRequestedStats)
        {
            if (!SteamManager.Initialized)
            {
                m_bRequestedStats = true;
                return;
            }

            bool bSuccess = SteamUserStats.RequestCurrentStats();
            m_bRequestedStats = bSuccess;
        }

        if (!m_bStatsValid)
            return;

        //Store stats in the Steam database if necessary
        if (m_bStoreStats)
        {
            bool bSuccess = SteamUserStats.StoreStats();
            m_bStoreStats = !bSuccess;
        }
    }

    public bool StoreStats()
    {
        return SteamUserStats.StoreStats();
    }

    public void SetStat(AchievementStatID statID, int statValue)
    {
        SteamUserStats.SetStat(statID.ToString(), statValue);
    }

    public void SetStat(AchievementStatID statID, float statValue)
    {
        SteamUserStats.SetStat(statID.ToString(), statValue);
    }

    void LoadStat(AchievementStat achievementStat)
    {
        switch(achievementStat.StatType)
        {
            case AchievementStatType.INT:
                int intValue;
                if (SteamUserStats.GetStat(achievementStat.StatID.ToString(), out intValue))
                    achievementStat.SetStat(intValue);
                break;

            case AchievementStatType.FLOAT:
                float floatValue;
                if (SteamUserStats.GetStat(achievementStat.StatID.ToString(), out floatValue))
                    achievementStat.SetStat(floatValue);
                break;
        }
    }

    public bool GetAchievementProgressLimits(string achievementName, out int minValue, out int maxValue)
    {
        minValue = 0;
        maxValue = 0;
        return SteamUserStats.GetAchievementProgressLimits(achievementName, out minValue, out maxValue);
    }

    public bool GetAchievementProgressLimits(string achievementName, out float minValue, out float maxValue)
    {
        minValue = 0f;
        maxValue = 0f;
        return SteamUserStats.GetAchievementProgressLimits(achievementName, out minValue, out maxValue);
    }

    public void UpdateAvgRateState(string statName, float countThisSession, double sessionLength)
    {
        SteamUserStats.UpdateAvgRateStat(statName, countThisSession, sessionLength);
    }

    public void UnlockAchievement(Achievement achievement)
    {
        // the icon may change once it's unlocked
        //achievement.m_iIconImage = 0;

        // mark it down
        SteamUserStats.SetAchievement(achievement.AchievementID.ToString());

        // Store stats end of frame
        m_bStoreStats = true;
    }

    private void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        if (!SteamManager.Initialized)
            return;

        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("Received stats and achievements from Steam\n");

                m_bStatsValid = true;

                // load achievements
                foreach (Achievement ach in AchievementsAndStats.Achievements)
                {
                    bool ret = SteamUserStats.GetAchievement(ach.AchievementID.ToString(), out ach.Achieved);
                    if (ret)
                    {
                        ach.Name = SteamUserStats.GetAchievementDisplayAttribute(ach.AchievementID.ToString(), "name");
                        ach.Description = SteamUserStats.GetAchievementDisplayAttribute(ach.AchievementID.ToString(), "desc");
                    }
                    else
                    {
                        Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + ach.AchievementID + "\nIs it registered in the Steam Partner site?");
                    }
                }

                // load stats
                foreach(AchievementStat achievementStat in AchievementsAndStats.AchievementStats)
                    LoadStat(achievementStat);
            }
            else
            {
                Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // Purpose: Our stats data was stored!
    //-----------------------------------------------------------------------------
    private void OnUserStatsStored(UserStatsStored_t pCallback)
    {
        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("StoreStats - success");
            }
            else if (EResult.k_EResultInvalidParam == pCallback.m_eResult)
            {
                // One or more stats we set broke a constraint. They've been reverted,
                // and we should re-iterate the values now to keep in sync.
                Debug.Log("StoreStats - some failed to validate");
                // Fake up a callback here so that we re-load the values.
                UserStatsReceived_t callback = new UserStatsReceived_t();
                callback.m_eResult = EResult.k_EResultOK;
                callback.m_nGameID = (ulong)m_GameID;
                OnUserStatsReceived(callback);
            }
            else
            {
                Debug.Log("StoreStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // Purpose: An achievement was stored
    //-----------------------------------------------------------------------------
    private void OnAchievementStored(UserAchievementStored_t pCallback)
    {
        // We may get callbacks for other games' stats arriving, ignore them
        if ((ulong)m_GameID == pCallback.m_nGameID)
        {
            if (0 == pCallback.m_nMaxProgress)
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
            }
            else
            {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress + "," + pCallback.m_nMaxProgress + ")");
            }
        }
    }
}
