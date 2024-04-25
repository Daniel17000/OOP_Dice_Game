using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Game
{
    public class Statistics
    {
        private Dictionary<string, GameStatistics> gameStats;

        // Constructor used in order to initialize the dictionary
        public Statistics()
        {
            gameStats = new Dictionary<string, GameStatistics>();
        }

        // Method to record the game results
        public void RecordGameResult(string gameType, int score)
        {
            // Checks if the game type exists in the dictionary we've got 
            if (!gameStats.ContainsKey(gameType))
            {
                // If not, it will create a new entry for the game type
                gameStats[gameType] = new GameStatistics();
            }

            // Increment the total plays for the game type
            gameStats[gameType].TotalPlays++;

            // Check if the current score is higher than the existing high score
            if (score > gameStats[gameType].HighScore)
            {
                // If so, we will update the high score for the game type
                gameStats[gameType].HighScore = score;
            }
        }

        // Method used to display the statistics to the user
        public void DisplayStatistics()
        {
            Console.WriteLine();
            Console.WriteLine("Game Statistics:");

            // Iterate through each game type in the dictionary
            foreach (var kvp in gameStats)
            {
                // Display the game type, total plays, and high score to the user
                Console.WriteLine($"Game: {kvp.Key}, Total Plays: {kvp.Value.TotalPlays}, High Score: {kvp.Value.HighScore}");
            }
        }
    }

    // Class to hold statistics for each game type
    public class GameStatistics
    {
        // Property to store the total number of plays for the game type using a get and setter
        public int TotalPlays { get; set; }

        // Property to store the highest score achieved for the game type using a get and setter
        public int HighScore { get; set; }
    }
}
