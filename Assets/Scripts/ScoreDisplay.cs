﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        coin.text = coinDisplay.ToString();
        distance.text = distanceDisplay.ToString();
        if(distanceDisplay < player.distanceTravelled)
        {
            distanceDisplay++;
        }
        if(coinDisplay < player.coin)
        {
            coinDisplay++;
        }
    }
}
