using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtilities;

public class TeamManager : MonoBehaviour {

    public List<Team> Teams { get { return teams; } }
    List<Team> teams = new List<Team>();

    /// <summary>
    /// Makes Teams from the list of players
    /// </summary>
    /// <param name="players">List of Player objects</param>
    public void MakeTeams(List<PlayerController> players)
    {
        foreach(PlayerController player in players)
        {
            // Create team if team doesn't exist
            if (teams.Count < player.GetPlayerNumber())
            {
                for (int i = teams.Count; i < player.GetPlayerTeam(); i++)
                    teams.Add(new Team(i + 1));
            }

            // Add player to team
            teams[player.GetPlayerTeam() - 1].players.Add(player);

            // Add the appropriate colored harness to the player
            player.harnessSpriteRenderer.sprite = MiniGameManager.Instance.harnessTints[player.GetPlayerTeam() - 1];
        }
    }
}
