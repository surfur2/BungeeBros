using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float MAX_LENGTH = 10;
    public List<float> SCORE_HACK = new List<float>();
    public Text winnerText;

    TeamManager teamMan;

    // Use this for initialization
    void Start()
    {
        teamMan = GetComponent<TeamManager>();
    }

    /// <summary>
    /// Calculates winner considering the worst score from each team
    /// </summary>
    /// <returns>The teamNumber that won</returns>
    public int GetWinner_Balance1()
    {
        List<Player> players = new List<Player>();
        List<Team> teams = new List<Team>();

        players = teamMan.players;
        teams = teamMan.teams;

        float best = 0;
        int winner = -1;

        // Check the best score out of all teams
        foreach (Team team in teams)
        {
            float currentWorst = MAX_LENGTH;

            // Get the worst score from all players in the team
            foreach (Player player in team.players)
            {
                if (player.score >= MAX_LENGTH)
                {
                    currentWorst = -1;
                    break;
                }

                if (player.score < currentWorst)
                    currentWorst = player.score;
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
    /// DEBUG ONLY CODE!!!! 
    /// Updates the scores of the players using the SCORE_HACK list of floats
    /// </summary>
    public void UpdateScores()
    {
        for (int i = 0; i < SCORE_HACK.Count; i++)
        {
            teamMan.players[i].score = SCORE_HACK[i];
        }
    }

    /// <summary>
    /// DEBUG ONLY CODE!!!!
    /// Updates text on screen with the winner
    /// </summary>
    public void FindWinner()
    {
        winnerText.text = "Winner: " + GetWinner_Balance1();
    }
}
