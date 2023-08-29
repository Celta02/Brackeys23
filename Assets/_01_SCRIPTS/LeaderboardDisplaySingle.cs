
namespace CeltaGames
{
    public class LeaderboardDisplaySingle
    {
        public string Rank { get;}
        public string Name { get;}
        public string Score { get;}

        public LeaderboardDisplaySingle(string rank, string name, string score)
        {
            Rank = rank;
            Name = name;
            Score = score;
        }
    }
}