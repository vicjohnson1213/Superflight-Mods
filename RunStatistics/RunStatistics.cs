using SFMF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace RunStatistics
{
    public class RunStatistics : IMod
    {
        private static string SaveLocation;

        private RunData CurrentRun { get; set; }
        private RunData LastRun { get; set; }

        private float LastSecond;

        private int LastCombo;
        private int ComboIncrease;

        public void Start()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            SaveLocation = $"{documents}/SFMF/rundata.csv";
            var saveFile = new FileInfo(SaveLocation);

            saveFile.Directory.Create();
            if (!File.Exists(SaveLocation))
                using (var writer = File.AppendText(SaveLocation))
                    writer.WriteLine("Seed,Alphanumeric Seed,SecuredScore,Ending,ScorePerSecond");

            LastSecond = Time.time;

            LastCombo = 0;
            ComboIncrease = 0;
        }

        public void Update()
        {
            // A run is over if the player is dead or the world changes (through a portal or the bottom of a world).
            var IsPayerDead = LocalGameManager.Singleton.playerState == LocalGameManager.PlayerState.Dead;
            var isNextWorld = CurrentRun != null && (CurrentRun.Seed != WorldManager.currentWorld.seed);
            var isPlayerReset = Input.GetButtonDown("ResetPlayer");

            if (CurrentRun != null && (IsPayerDead || isNextWorld || isPlayerReset))
            {
                CurrentRun.TotalScore = LocalGameManager.Singleton.ScoreThisRun;

                if (IsPayerDead)
                    CurrentRun.Ending = RunEnding.Death;
                else if (isNextWorld)
                    CurrentRun.Ending = RunEnding.NextWorld;
                else
                    CurrentRun.Ending = RunEnding.Reset;

                // If the last run ended by traversing worlds, the player likely has points already, so we need to subtract those.
                if (LastRun != null && LastRun.Ending == RunEnding.NextWorld)
                    CurrentRun.SecuredScore = LocalGameManager.Singleton.ScoreThisRun - LastRun.TotalScore;
                else
                    CurrentRun.SecuredScore = LocalGameManager.Singleton.ScoreThisRun;

                if (CurrentRun.ScorePerSecond.Any(s => s > 0)) ;
                using (var writer = File.AppendText(SaveLocation))
                    writer.WriteLine(CurrentRun.ToCsvRow());

                LastRun = CurrentRun;
                CurrentRun = null;
            }

            // Calculate the increase in combo each frame, so no points are lost.
            ComboIncrease += Math.Max(0, LocalGameManager.Singleton.ScoreThisCombo - LastCombo);
            LastCombo = LocalGameManager.Singleton.ScoreThisCombo;

            if (Time.time - LastSecond > 1)
            {
                if (LocalGameManager.Singleton.playerState == LocalGameManager.PlayerState.Flying)
                {
                    if (CurrentRun == null)
                    {
                        CurrentRun = new RunData
                        {
                            Seed = WorldManager.currentWorld.seed,
                            AlphanumericString = WorldManager.currentWorld.alphanumericSeed,
                            SecuredScore = 0,
                            TotalScore = 0,
                            ScorePerSecond = new List<int>()
                        };

                        ComboIncrease = 0;
                    }

                    CurrentRun.ScorePerSecond.Add(ComboIncrease);
                    
                    ComboIncrease = 0;
                }

                LastSecond = Time.time;
            }
        }

        private class RunData
        {
            public int Seed { get; set; }
            public string AlphanumericString { get; set; }
            public int SecuredScore { get; set; }
            public int TotalScore { get; set; }
            public List<int> ScorePerSecond { get; set; }
            public RunEnding Ending { get; set; }

            public string ToCsvRow()
            {
                var scores = "";

                foreach (int s in ScorePerSecond)
                    scores += $"{s}|";

                scores = scores.TrimEnd('|');

                return $"{Seed},{AlphanumericString},{SecuredScore},{Ending},{scores}";
            }
        }

        public enum RunEnding
        {
            Death,
            NextWorld,
            Reset
        }
    }
}