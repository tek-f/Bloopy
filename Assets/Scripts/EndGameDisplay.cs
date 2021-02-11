using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Bloopy.GameManagement;
using Bloopy.Saving;

namespace Bloopy.UI
{
    public class EndGameDisplay : MonoBehaviour
    {
        /// <summary>
        /// Reference to Text Mesh Pro element that displays the player's final height during play on endGamePanel.
        /// </summary>
        public TMP_Text finalHeightDisplay;
        /// <summary>
        /// Reference to Text Mesh Pro element that displays the player's high score (highest height reached) on endGamePanel.
        /// </summary>
        public TMP_Text highScoreHeightDisplay;
        /// <summary>
        /// Used to increase finalHeightDisplay over time.
        /// </summary>
        public float heightDisplayCounter = 0;
        /// <summary>
        /// Used to track what the final height is for increasing finalHeightDisplay over time.
        /// </summary>
        public float finalHeight = 0;
        /// <summary>
        /// The rate that heightDisplayCounter is increased by when it reaches above 75% of finalHeight.
        /// </summary>
        public float heightDisplayCounterIncreaseRate = 0.2f;

        void Start()
        {
            //Set finalHeight to the height the player reached during gameplay session.
            finalHeight = NewGameManager.singleton.height;

            //Set highScoreHeightDisplay to the players high score.
            highScoreHeightDisplay.text = NewGameManager.singleton.highScore.ToString();
        }

        void FixedUpdate()
        {
            //If the heightDisplayCounter counter is less than 75% of finalHeight
            if (heightDisplayCounter < finalHeight * 0.75f)
            {
                //Increase heightDisplayCounter by TBD
                heightDisplayCounter += (finalHeight * 0.01f);
            }
            //If heightDisplayCounter is less than finalHeight, but more than 75% of finalHeight
            else if (heightDisplayCounter < finalHeight)
            {
                //Increase heightDisplayCounter by TBD
                heightDisplayCounter += finalHeight * 0.005f;
            }
            //Set finalHeightDisplay text to equal heightDisplayCounter.
            finalHeightDisplay.text = heightDisplayCounter.ToString();
            //If heightDisplayCounter is equal to or greater than finalHeight
            if (heightDisplayCounter >= finalHeight)
            {
                //Set finalHeightDisplay text to equal final height as an int
                finalHeightDisplay.text = ((int)finalHeight).ToString();
                //Disable this script.
                this.enabled = false;
            }
        }
    }
}