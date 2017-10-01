using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public float MAX_LENGTH = 10;
    public List<float> SCORE_HACK = new List<float>();
    public Text winnerText;

    List<Player> players = new List<Player>();
    List<Team> teams = new List<Team>();

    // Use this for initialization
    void Start () {
        teams = GetComponent<TeamManager>().teams;
        players = GetComponent<TeamManager>().players;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetWinner_Balance1()
    {
        float best = 0;
        int winner = -1;

        // Check the best score out of all teams
        foreach(Team team in teams)
        {
            float currentWorst = MAX_LENGTH;

            // Get the worst score from all players in the team
            foreach(Player player in team.players)
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

    public void UpdateScores()
    {
        for(int i = 0; i < SCORE_HACK.Count; i++)
        {
            players[i].score = SCORE_HACK[i];
        }
    }

    public void FindWinner()
    {
        winnerText.text = "Winner: " + GetWinner_Balance1();
    }
}
