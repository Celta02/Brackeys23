using System.Collections.Generic;

namespace CeltaGames
{
    public class SaveData
    {
        public float MaxDepth;
        public float RivalMaxDepth;
        public float BestScore;
        public long TurnAroundTime;
        public Queue<long> StrokeTimes;

        public SaveData()
        {
            MaxDepth = 0f;
            TurnAroundTime = 0;
            StrokeTimes = new Queue<long>();
        }
        
        public SaveData(bool isAIDefault)
        {
            MaxDepth = 0f;
            RivalMaxDepth = 0f;
            TurnAroundTime = 260;
            StrokeTimes = new Queue<long>();
            StrokeTimes.Enqueue(40);
            StrokeTimes.Enqueue(70);
            StrokeTimes.Enqueue(150);
            StrokeTimes.Enqueue(200);
            StrokeTimes.Enqueue(245);
            StrokeTimes.Enqueue(318);
            StrokeTimes.Enqueue(370);
            StrokeTimes.Enqueue(420);
            StrokeTimes.Enqueue(460);
            StrokeTimes.Enqueue(510);
        }
    }
}