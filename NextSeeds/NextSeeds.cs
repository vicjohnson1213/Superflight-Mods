using SFMF;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NextSeeds
{
    public class NextSeeds : IMod
    {

        Text text;
        private int currentSeed;
        private string currentAlphaSeed;
        private int nextSeed;
        private string nextAlphaSeed;
        private List<string> seeds = new List<string>();

        private void Start()
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var settingsPath = $"{documents}/SFMF/NextSeeds.txt";

            try
            {
                var seedsToAdd = File.ReadAllLines(settingsPath);
                seeds.AddRange(seedsToAdd);

            }
            catch(Exception e)
            {
                Log($"ERROR {e.Message}");
            }

            Log("Queuing seeds:");
            foreach (var seed in seeds)
                Log(seed);

            LocalGameManager.Singleton.fixedWorlds = GlobalGameManager.currentSaveGame.stats.worldsGenerated + seeds.Count;

            if (seeds.Count > 0)
            {
                currentAlphaSeed = seeds.FirstOrDefault();
                currentSeed = StringToSeed.returnSeed(currentAlphaSeed);

                SetNextWorld();
                LevelGenerator.Singleton.GenerateLevel(WorldManager.NewWorld());
                SetNextWorld();
            }

            var canvasGO = new GameObject("myCanvas");
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            var labelGO = new GameObject("myText");
            text = labelGO.AddComponent<Text>();

            text.alignment = TextAnchor.MiddleCenter;

            text.font = UIManager.Singleton.scoreThisComboUI.font;
            text.fontSize = 40;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;

            text.transform.SetParent(canvasGO.transform, false);
        }

        private void Update()
        {
            text.text = currentAlphaSeed ?? WorldManager.currentWorld.seed.ToString();
            SetTextPosition(text.text);

            if (currentAlphaSeed == null)
                return;

            var isNextWorld = WorldManager.currentWorld.seed != currentSeed;

            if (isNextWorld)
            {
                currentAlphaSeed = nextAlphaSeed;
                currentSeed = nextSeed;
                SetNextWorld();
            }
        }

        private void SetNextWorld()
        {
            var next = seeds.FirstOrDefault();
            nextAlphaSeed = next;

            if (nextAlphaSeed == null)
                return;
            
            nextSeed = StringToSeed.returnSeed(next);
            GlobalGameManager.currentSaveGame.fixedWorldSeeds = new List<int> { nextSeed };

            seeds.RemoveAt(0);
        }

        private void SetTextPosition(string s)
        {
            var textGen = new TextGenerator();
            var settings = text.GetGenerationSettings(text.rectTransform.rect.size);
            var width = textGen.GetPreferredWidth(s, settings);
            var height = textGen.GetPreferredHeight(s, settings);

            text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            text.transform.position = new Vector3(text.rectTransform.rect.width / 2 + 25, text.rectTransform.rect.height / 2 + 20, 0);
        }

        private void Log(string message)
        {
            Console.WriteLine($"NextSeeds - {message}");
        }
    }
}
