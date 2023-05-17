﻿using ColonizationMobileGame.GameFinalization;
using UnityEngine;

namespace ColonizationMobileGame.UI.GameOver
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameOverOutcomeText outcomeText;
        [SerializeField] private GameOverTimeSpentText timeSpentText;


        public void Show(GameOutcome outcome, float timeSpent)
        {
            gameObject.SetActive(true);
            
            outcomeText.SetText(outcome);
            
            if (outcome == GameOutcome.Win)
            {
                timeSpentText.SetText(timeSpent);
            }
        }
    }
}