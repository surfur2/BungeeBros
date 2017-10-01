using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameUtilities
{
    public class Player
    {
        public int playerNumber;
        public float score;
        public int teamNumber;
        public Sprite playerCharacterSprite;

        public Player()
        {
            playerNumber = 1;
            teamNumber = 1;
        }

        public Player(int playerNum, int teamNum, Sprite playerSprite)
        {
            playerNumber = playerNum;
            teamNumber = teamNum;
            playerCharacterSprite = playerSprite;
        }
    }

    public class Team
    {
        public List<Player> players;
        public int teamNumber;

        public Team()
        {
            players = new List<Player>();
            teamNumber = 0;
        }

        public Team(int team)
        {
            players = new List<Player>();
            teamNumber = team;
        }
    }
}
