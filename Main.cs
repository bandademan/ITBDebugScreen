using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Object;
using MelonLoader;
using static PlayerStats;
using static Mirror.NetworkBehaviour;
using UnityEngine.InputSystem;

namespace ITBDebugMenu
{
    public class Main : MelonMod
    {
        bool isDebugMenu = false;

        PlayerStats playerStats;
        RoomManager roomManager;
        InGameLobby inGameLobby;
        GameObject playerObject;

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName == "MainLevel")
            {
                LoggerInstance.Msg("MainLevel Scene was loaded!");

                roomManager = FindObjectOfType<RoomManager>();
                inGameLobby = FindObjectOfType<InGameLobby>();
                playerObject = GameObject.FindGameObjectWithTag("Player");

                var foundPlayerStats = FindObjectsOfType<PlayerStats>();

                if (foundPlayerStats.Length >= 2)
                {
                    LoggerInstance.Msg("Multiplayer is ACTIVE");
                }

                playerStats = foundPlayerStats[0];

            }
        }
        public override void OnUpdate()
        {
            if (Keyboard.current.backquoteKey.wasPressedThisFrame)
            {
                if (isDebugMenu)
                {
                    isDebugMenu = false;
                    MelonEvents.OnGUI.Unsubscribe(DrawDebugMenu);
                } 
                else
                {
                    isDebugMenu = true;
                    MelonEvents.OnGUI.Subscribe(DrawDebugMenu, 100);
                }
            }
            
        }
        
        private void DrawDebugMenu()
        {
            //GUI.Label(new Rect(20, 20, 1000, 200), "<b><color=cyan><size=100>Frozen</size></color></b>");

            GUI.Label(new Rect(10, 10, 1000, 1000), "<b><size=25>ITB Debug Info</size></b>"
                + "\n<b><size=25>by @Mr. Monocle#0433</size></b>"
                + "\n<b><size=20>Player Information</size></b>"
                + "\n<size=15>XYZ: " + playerObject.transform.position.x.ToString("0.000") + " / " + playerObject.transform.position.y.ToString("0.000") + " / " + playerObject.transform.position.z.ToString("0.000") + "</size>"
                + "\n<size=15>Health: " + playerStats.Health + "</size>"
                + "\n<size=15>Stamina: " + playerStats.Stamina.ToString("0.000") + "</size>"
                + "\n<size=15>Anxiety: " + playerStats.Anxiety.ToString("0.000") + "</size>"
                + "\n<size=15>Radiation: " + playerStats.m_Radiation + "</size>"
                + "\n<b><size=20>Game Information</size></b>"
                + "\n<size=15>Difficulty: " + roomManager.m_RoomDifficulty + "</size>"
                + "\n<size=15>PlayerCount: " + inGameLobby.PlayersCount + "</size>"
                );

            //GUI.Box(new Rect(0, 0, 300, 500), "Debug Menu"
            //    + "\nHealth: " + playerStats.Health
            //    + "\nStamina: " + playerStats.Stamina
            //    + "\nAnxiety: " + playerStats.Anxiety
            //    + "\nEnergy Boosted: " + playerStats.EnergyBoosted
            //    + "\nHaveinvulnerability: " + playerStats.Haveinvulnerability
            //    + "\nisDead: " + playerStats.isDead
            //    + "\nisParalized: " + playerStats.IsParalized
            //    + "\nisPermanentDead: " + playerStats.isPermanentDead
            //    + "\nisPursuitedByMonster: " + playerStats.IsPursuitedByMonster
            //    + "\nm_CurrentMonsterTargeted: " + playerStats.m_CurrentMonsterTargeted
            //    + "\nm_EnergyBoostTimer: " + playerStats.m_EnergyBoostTimer
            //    + "\nm_HasEnergyBoost: " + playerStats.m_HasEnergyBoost
            //    + "\nm_HasInvulnerability: " + playerStats.m_HasInvulnerability
            //    + "\nm_InsideRadiationZone: " + playerStats.m_InsideRadiationZone
            //    + "\nm_LastElectrifyTime: " + playerStats.m_LastElectrifyTime
            //    + "\nm_LastParalizeTime: " + playerStats.m_LastParalizeTime
            //    + "\nm_LastToxicTime: " + playerStats.m_LastToxicTime
            //    + "\nm_MusicTimer: " + playerStats.m_MusicTimer
            //    + "\nm_NextUnparalizeTime: " + playerStats.m_NextUnparalizeTime
            //    + "\nm_Paralized: " + playerStats.m_Paralized
            //    + "\nm_ParalizeTime: " + playerStats.m_ParalizeTime
            //    + "\nm_PlayersDeathsCount: " + playerStats.m_PlayersDeathsCount
            //    + "\nm_Radiation: " + playerStats.m_Radiation
            //    + "\nm_RadiationKillTime: " + playerStats.m_RadiationKillTime
            //    + "\nm_RadiationTime: " + playerStats.m_RadiationTime
            //    + "\nm_VulnerableTimeout: " + playerStats.m_VulnerableTimeout
            //    );
        }

    }
}
