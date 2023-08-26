using System.Collections.Generic;

namespace CeltaGames
{
    public class SaveData
    {
        public string PlayerName;
        public float MaxDepth;
        public float RivalMaxDepth;
        public float BestScore;
        public List<long> TurnTimes;
        public List<long> StrokeTimes;

        public SaveData()
        {
            PlayerName = "";
            MaxDepth = 0f;
            TurnTimes = new List<long> { 180 };
            StrokeTimes = new List<long>
            {
                40,
                70,
                150,
                200,
                245,
                318,
                370,
                420
            };
        }

    }
}