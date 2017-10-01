using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float MAX_LENGTH = 10;
    //public List<float> SCORE_HACK = new List<float>();
    //public Text winnerText;

    TeamManager teamMan;

    // Use this for initialization
    void Start()
    {
        teamMan = GetComponent<TeamManager>();
    }

    /// <summary>
    /// Calculates winner considering the worst score from each team for Balance option 1
    /// </summary>
    /// <returns>The teamNumber that won</returns>
    public int GetWinner_Balance1()
    {
        float best = 0;
        int winner = -1;

        // Check the best score out of all teams
        foreach (Team team in teamMan.Teams)
        {
            float currentWorst = MAX_LENGTH;

            // Get the worst score from all players in the team
            foreach (PlayerController player in team.players)
            {
                if (player.GetPlayerScore() >= MAX_LENGTH)
                {
                    currentWorst = -1;
                    break;
                }

                if (player.GetPlayerScore() < currentWorst)
                    currentWorst = player.GetPlayerScore();
            }

            if (currentWorst > best)
            {
                best = currentWorst;
                winner = team.teamNumber;
            }
        }

        return winner;
    }

    /// <summary>
    /// Calculates winner considering the addition of all cord lengths from each team for Balance option 2
    /// </summary>
    /// <returns>The teamNumber that won</returns>
    public int GetWinner_Balance2()
    {
        float best = 0;
        int winner = -1;

        // Check the best score out of all teams
        foreach (Team team in teamMan.Teams)
        {
            float currentSum = 0;

            // Get the sum of scores of all players in the team
            foreach (PlayerController player in team.players)
            {
                currentSum += player.GetPlayerScore();
                if (currentSum >= MAX_LENGTH)
                {
                    currentSum = -1;
                    break;
                }
            }

            if (currentSum > best)
            {
                best = currentSum;
                winner = team.teamNumber;
            }
        }

        return winner;
    }

    /// <summary>
    /// Calculates winner considering the average cord length from each team for Balance option 2
    /// </summary>
    /// <returns>The teamNumber that won</returns>
    public int GetWinner_Balance3()
    {
        float best = 0;
        int winner = -1;

        // Check the best score out of all teams
        foreach (Team team in teamMan.Teams)
        {
            float currentSum = 0;

            // Get the worst score from all players in the team
            foreach (PlayerController player in team.players)
            {
                currentSum += player.GetPlayerScore();
                if (currentSum >= MAX_LENGTH)
                {
                    currentSum = -1;
                    break;
                }
            }

            currentSum /= team.players.Count;

            if (currentSum > best)
            {
                best = currentSum;
                winner = team.teamNumber;
            }
        }

        return winner;
    }

    ///// <summary>
    ///// DEBUG ONLY CODE!!!! 
    ///// Updates the scores of the players using the SCORE_HACK list of floats
    ///// </summary>
    //public void UpdateScores()
    //{
    //    for (int i = 0; i < SCORE_HACK.Count; i++)
    //    {
    //        MiniGameManager.Instance.Players[i].score = SCORE_HACK[i];
    //    }
    //}

    ///// <summary>
    ///// DEBUG ONLY CODE!!!!
    ///// Updates text on screen with the winner
    ///// </summary>
    //public void FindWinner_Balance1()
    //{
    //    winnerText.text = "Winner: " + GetWinner_Balance1();
    //}

    ///// <summary>
    ///// DEBUG ONLY CODE!!!!
    ///// Updates text on screen with the winner
    ///// </summary>
    //public void FindWinner_Balance2()
    //{
    //    winnerText.text = "Winner: " + GetWinner_Balance2();
    //}

    ///// <summary>
    ///// DEBUG ONLY CODE!!!!
    ///// Updates text on screen with the winner
    ///// </summary>
    //public void FindWinner_Balance3()
    //{
    //    winnerText.text = "Winner: " + GetWinner_Balance3();
    //}
}
