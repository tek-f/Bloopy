using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bloopy.Player;

namespace Bloopy.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        PlayerHandler player;
        public Text distance, coin;
        int distanceDisplay, coinDisplay;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
            distanceDisplay = 0;
            coinDisplay = 0;
        }
        private void Update()
        {
            coin.text = "Coins: " + coinDisplay.ToString();
            distance.text = "Distance: " + distanceDisplay.ToString();
            if (distanceDisplay < player.distanceTravelled)
            {
                distanceDisplay++;
            }
            if (coinDisplay < player.bloopCollected)
            {
                coinDisplay++;
            }
        }
    }
}