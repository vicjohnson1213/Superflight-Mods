﻿using SFMF;
using System.Collections.Generic;
using UnityEngine;

namespace Trails
{
    public class Trails : IMod
    {
        private List<GameObject> trails;
        private GameObject currentTrailGO;

        private TrailRenderer trail;

        private int? currentSeed;
        private float trailStartTime;

        void Start()
        {
            trailStartTime = 0;
            trails = new List<GameObject>();
        }

        public void Update()
        {
            var camera = LocalGameManager.Singleton.playerCamManager.mainCamera.mainCameraReference.gameObject;

            // A run is over if the player is dead or the world changes (through a portal or the bottom of a world).
            var IsPayerDead = LocalGameManager.Singleton.playerState == LocalGameManager.PlayerState.Dead;
            var isNextWorld = currentSeed != null && (currentSeed.Value != WorldManager.currentWorld.seed);
            var isPlayerReset = LocalGameManager.Singleton.playerState == LocalGameManager.PlayerState.Flying && Input.GetButtonDown("ResetPlayer");

            // If the run is over, save the trail from the last run if it set a new high score.
            if (currentSeed != null && (IsPayerDead || isPlayerReset))
            {
                currentSeed = null;

                trails.Add(currentTrailGO);

                return;
            }

            // Once the player is flying again, create a new game object for the next trail.
            if (currentSeed == null && LocalGameManager.Singleton.playerState == LocalGameManager.PlayerState.Flying)
            {
                currentSeed = WorldManager.currentWorld.seed;

                currentTrailGO = new GameObject();
                trail = currentTrailGO.AddComponent<TrailRenderer>();
                trail.time = Mathf.Infinity;
                var mat = new Material(Shader.Find("Diffuse"));
                trail.material = mat;
                trail.material.color = new Color(.09f, .745f, .733f);
                trailStartTime = Time.time + .1f;
            }

            // Once enough time has elapsed from the start of this run to prevent a line from the camera reset, start the trail again.
            if (currentSeed != null && Time.time > trailStartTime && LocalGameManager.Singleton.playerState == LocalGameManager.PlayerState.Flying)
            {
                currentTrailGO.transform.position = LocalGameManager.Singleton.playerCamManager.mainCamera.mainCameraReference.gameObject.transform.position;
            }

            // If the player advances to the next world or presses the "clear" button (c on keyboard and b on a controller), clear all trails.
            var clearPressed = Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton1);

            if (isNextWorld)
            {
                Destroy(currentTrailGO);
                currentSeed = null;
            }

            if (clearPressed || isNextWorld)
            {
                foreach (var trail in trails)
                    Destroy(trail);

                trails.Clear();
            }
        }
    }
}
