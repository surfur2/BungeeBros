using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameUtilities
{
    public class Player
    {
        public int playerNumber;
        public float score;
        public int teamNumber;

        public Player()
        {
            playerNumber = 1;
            teamNumber = 1;
        }

        public Player(int playerNum, int teamNum)
        {
            playerNumber = playerNum;
            teamNumber = teamNum;
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
