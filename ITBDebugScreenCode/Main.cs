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

namespace ITBDebugScreen
{
    public class Main : MelonMod
    {
        bool isMainLevel = false;
        bool isDebugMenu = false;

        PlayerStats playerStats;
        RoomManager roomManager;
        GameObject playerObject;

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            isMainLevel = false;
            if (sceneName == "MainLevel")
            {
                isMainLevel = true;
                LoggerInstance.Msg("MainLevel Scene was loaded!");

                roomManager = FindObjectOfType<RoomManager>();
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
            if (Keyboard.current.backquoteKey.wasPressedThisFrame && isMainLevel)
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

            GUI.Label(new Rect(10, 10, 1000, 1000), "<b><size=25>ITB Debug Info</size></b>"
                + "\n<b><size=25>by @Mr. Monocle#0433</size></b>"
                + "\n<b><size=20>Player Information</size></b>"
                + "\n<size=15>XYZ: " + playerObject.transform.position.x.ToString("0.000") + " / " + playerObject.transform.position.y.ToString("0.000") + " / " + playerObject.transform.position.z.ToString("0.000") + "</size>"
                + "\n<size=15>Health: " + playerStats.Health + "</size>"
                + "\n<size=15>Stamina: " + playerStats.Stamina.ToString("0.000") + "</size>"
                + "\n<size=15>Anxiety: " + playerStats.Anxiety.ToString("0.000") + "</size>"
                + "\n<size=15>Radiation: " + playerStats.m_Radiation + "</size>"
                + "\n<size=15>Energy Boosted: " + playerStats.EnergyBoosted + "</size>"
                + "\n<size=15>Invulnerability: " + playerStats.Haveinvulnerability + "</size>"
                + "\n<size=15>Dead: " + playerStats.isDead + "</size>"
                + "\n<size=15>Pursued By Monster: " + playerStats.IsPursuitedByMonster + "</size>"
                + "\n<size=15>Inside Rediation Zone: " + playerStats.m_InsideRadiationZone + "</size>"
                + "\n<b><size=20>Game Information</size></b>"
                + "\n<size=15>Difficulty: " + roomManager.m_RoomDifficulty + "</size>"
                );

        }

    }
}
