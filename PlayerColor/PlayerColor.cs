using SFMF;
using System;
using System.IO;
using UnityEngine;

namespace PlayerColor
{
    public class PlayerColor : IMod
    {
        private const string SettingsPath = @".\SFMF\ModSettings\Player Color.csv";

        private string Color { get; set; }

        private void Start()
        {
            var settings = File.ReadAllLines(SettingsPath);

            foreach (var line in settings)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                var parts = line.Split(',');

                if (parts[0] == "Setting" && parts[1] == "Color")
                    Color = parts[2];
            }
        }

        public void Update()
        {
            Color color;
            var success = ColorUtility.TryParseHtmlString(Color, out color);

            if (!success)
                return;

            int childCount = PlayerMaterials.Singleton.playerBodyParts.transform.childCount;
            for (int index = 0; index < childCount; ++index)
            {

                Material material = PlayerMaterials.Singleton.playerBodyParts.transform.GetChild(index).GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
                Utility.SetTriplanarColors(material, color, color, color, color, color);
                Utility.SetCustomFog(material, WorldManager.currentWorld.FogSettings);
            }
        }
    }
}
