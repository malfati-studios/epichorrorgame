using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class TextWriter : MonoBehaviour
    {
        public Action<bool> onCompleteText;
        private TextMeshProUGUI uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;

        public void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter,
            bool invisibleCharacters)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            characterIndex = 0;
            this.invisibleCharacters = invisibleCharacters;
            AudioController.instance.StartDialogSound();
        }

        private void Update()
        {
            if (uiText != null)
            {
                timer -= Time.deltaTime;
                while (timer <= 0f)
                {
                    timer += timePerCharacter;
                    characterIndex++;
                    string text = textToWrite.Substring(0, characterIndex);
                    if (invisibleCharacters)
                    {
                        text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                    }

                    uiText.text = text;
                    if (characterIndex >= textToWrite.Length)
                    {
                        AudioController.instance.StopDialogSound();
                        uiText = null;
                        onCompleteText.Invoke(true);
                    }
                }
            }
        }
    }
}