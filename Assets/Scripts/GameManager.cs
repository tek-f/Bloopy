﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Bloopy.Spawn;
using Bloopy.Player;

namespace Bloopy.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager singleton;
        [Header("Player")]
        [SerializeField] PlayerHandler player;
        [Header("Spawning")]
        public SpawnGeneral[] layer1SpawnClasses;
        public SpawnGeneral[] layer2SpawnClasses;
        [Header("UI")]
        public GameObject startMenuPanel;
        public GameObject playerHUDPanel;
        public GameObject optionsMenuPanel;
        public GameObject upgradesMenuPanel;
        [Header("Camera")]
        CameraMovement cameraMovement;
        public void StartGame()
        {
            startMenuPanel.SetActive(false);
            player.PrepareToLaunch();
        }
        public void Launch()
        {
            cameraMovement.enabled = true;
        }
        private void Awake()
        {
            cameraMovement = Camera.main.GetComponent<CameraMovement>();

            Time.timeScale = 1;

            #region Singleton Setup
            if(singleton == null)
            {
                singleton = this;
            }
            else if(singleton != this)
            {
                Destroy(gameObject);
            }
            #endregion

            #region Spawners Setup
            SpawnGeneral[] spawners = GetComponents<SpawnGeneral>();
            List<SpawnGeneral> layer1SpawnersTemp = new List<SpawnGeneral>();
            List<SpawnGeneral> layer2SpawnersTemp = new List<SpawnGeneral>();
            foreach (var spawner in spawners)
            {
                spawner.player = player;
                switch (spawner.gameLayer)
                {
                    case 1:
                        layer1SpawnersTemp.Add(spawner);
                        break;
                    case 2:
                        layer2SpawnersTemp.Add(spawner);
                        break;
                }
            }
            layer1SpawnClasses = layer1SpawnersTemp.ToArray();
            layer2SpawnClasses = layer2SpawnersTemp.ToArray();
            #endregion

        }
    }
}