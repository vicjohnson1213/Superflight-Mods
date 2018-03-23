using SFMF;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Speedometer
{
    public class Speedometer : IMod
    {
        Text text;

        public void Start()
        {
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

        public void Update()
        {
            text.text = Math.Round(PlayerMovement.Singleton.currentSpeed).ToString();
            SetTextPosition(text.text);
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
    }
}