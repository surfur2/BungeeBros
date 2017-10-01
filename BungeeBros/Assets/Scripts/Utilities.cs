using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameUtilities
{
    struct Globals
    {
        public const float UNITY_UNIT_TO_METERS = 10.8f;
    }

    public class Team
    {
        public List<PlayerController> players;
        public int teamNumber;

        public Team()
        {
            players = new List<PlayerController>();
            teamNumber = 0;
        }

        public Team(int team)
        {
            players = new List<PlayerController>();
            teamNumber = team;
        }
    }
}
