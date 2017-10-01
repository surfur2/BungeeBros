using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class TeamManager : MonoBehaviour {

    public List<int> playerToTeam = new List<int>();
    public List<Player> players = new List<Player>();
    public List<Team> teams = new List<Team>();

	// Use this for initialization
	void Start ()
    {
        int playerNumber = 1;
        foreach (int teamNumber in playerToTeam)
        {
            players.Add(new Player(playerNumber, teamNumber));
            playerNumber++;
        }

        MakeTeams(players);
	}

    /// <summary>
    /// Makes Teams from the list of players
    /// </summary>
    /// <param name="players">List of Player objects</param>
    public void MakeTeams(List<Player> players)
    {
        foreach(Player player in players)
        {
            // Create team if team doesn't exist
            if (teams.Count < player.teamNumber)
            {
                for (int i = teams.Count; i < player.teamNumber; i++)
                    teams.Add(new Team(i + 1));
            }

            // Add player to team
            teams[player.teamNumber - 1].players.Add(player);
        }
    }
}
