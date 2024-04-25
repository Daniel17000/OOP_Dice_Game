using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Game
{
    // Abstract method Play, to be implemented by derived classes
    public abstract class Game
    {
        public abstract void Play(bool isAgainstFriend);
    }

    public class SevensOut : Game
    {
        private Die die1;   // Die 1 instance
        private Die die2;   // Die 2 instance
        private int player1Score;   // Player 1 score
        private int player2Score;   // Player 2 score
        public int TotalScore => player1Score;

        // Constructor to initialize SevensOut game
        public SevensOut()
        {
            die1 = new Die();
            die2 = new Die();
            player1Score = 0;
            player2Score = 0;
        }

        // Method to roll die1
        public int RollDie1()
        {
            return die1.Roll();
        }

        // Method to roll die2
        public int RollDie2()
        {
            return die2.Roll();
        }

        // Implementation of abstract method Play from the base class Game
        public override void Play(bool isAgainstFriend)
        {
            int currentPlayer = 1;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"Player {currentPlayer}'s turn");

                // Check if the player wants to roll manually
                if (isAgainstFriend)
                {
                    Console.WriteLine("Type 'roll' to roll the dice.");
                    string? rollCommand = Console.ReadLine(); // Adds a nullable reference type '?'
                    if (rollCommand != null && rollCommand.ToLower() != "roll") // Check for the null before comparison
                    {
                        Console.WriteLine("Invalid command. Please type 'roll' to roll the dice.");
                        continue; // Skip this iteration and prompt again
                    }
                }

                // Roll both dice
                int rollResult1 = die1.Roll();
                int rollResult2 = die2.Roll();
                int rollResult = rollResult1 + rollResult2;

                // Display roll results
                Console.WriteLine();
                Console.WriteLine($"Die 1: {rollResult1}, Die 2: {rollResult2}, Total: {rollResult}");
                Console.WriteLine();

                // Check if the game should end
                if (rollResult == 7)
                {
                    Console.WriteLine($"Player {currentPlayer} rolled a 7! Game over.");
                    break;
                }

                // Check for double and double the score if applicable
                if (rollResult1 == rollResult2)
                {
                    rollResult *= 2;
                    Console.WriteLine($"You rolled a double! Total doubled to: {rollResult}");
                }

                // Add roll result to the respective player's score
                if (currentPlayer == 1)
                    player1Score += rollResult;
                else
                    player2Score += rollResult;

                // Display current scores to the users
                Console.WriteLine($"Player 1 score: {player1Score}, Player 2 score: {player2Score}");

                // Switch to the next player
                if (isAgainstFriend)
                    currentPlayer = (currentPlayer == 1) ? 2 : 1;
                else
                    currentPlayer = 2; // If playing against the computer, always switch to player 2 
            }

            // Determine the loser based on the lowest total
            if (player1Score < player2Score)
            {
                Console.WriteLine($"Player 1 loses with a score of {player1Score}!");
            }
            else if (player2Score < player1Score)
            {
                Console.WriteLine($"Player 2 loses with a score of {player2Score}!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
    }

    public class ThreeOrMore : Game
    {
        private List<Die> dice; // List to hold dice instances
        private int currentPlayer;
        private int totalPlayer1;  // Total score of player 1
        private int totalPlayer2;  // Total score of player 2

        // Constructor to initialize ThreeOrMore game
        public ThreeOrMore()
        {
            dice = new List<Die>();
            for (int i = 0; i < 5; i++)
            {
                dice.Add(new Die());
            }
        }

        // Method to roll a specific die
        public int RollDie(int dieIndex)
        {
            return dice[dieIndex].Roll();
        }

        // Method to get the total score of player 1
        public int GetTotalPlayer1Score()
        {
            return totalPlayer1;
        }

        // Method to get the total score of player 2
        public int GetTotalPlayer2Score()
        {
            return totalPlayer2;
        }

        // Method to get the total score of the current player
        public int GetTotalScore()
        {
            if (currentPlayer == 1)
                return totalPlayer1;
            else
                return totalPlayer2;
        }

        // Implementation of abstract method Play from the base class Game
        public override void Play(bool isAgainstFriend)
        {
            currentPlayer = 1;
            totalPlayer1 = 0; // Initialize total score of Player 1
            totalPlayer2 = 0; // Initialize total score of Player 2

            // Loop until one player reaches a total score of 20 or more
            while (totalPlayer1 < 20 && totalPlayer2 < 20)
            {
                Console.WriteLine();
                Console.WriteLine($"Player {currentPlayer}'s turn");

                int[] rollResults = new int[dice.Count];
                for (int i = 0; i < dice.Count; i++)
                {
                    rollResults[i] = dice[i].Roll();  // Roll each die and store the result
                }

                // Display the roll results
                Console.Write("Rolled: ");
                foreach (int result in rollResults)
                {
                    Console.Write(result + " ");
                }
                Console.WriteLine();

                Array.Sort(rollResults);  // Sorts the roll results

                // Check for combinations and calculate scores accordingly for each player
                if (HasFiveOfAKind(rollResults))
                {
                    int points = 12;
                    Console.WriteLine($"Player {currentPlayer} got 5 of a kind! Total score: {(currentPlayer == 1 ? totalPlayer1 += points : totalPlayer2 += points)}");
                }
                else if (HasFourOfAKind(rollResults))
                {
                    int points = 6;
                    Console.WriteLine($"Player {currentPlayer} got 4 of a kind! Total score: {(currentPlayer == 1 ? totalPlayer1 += points : totalPlayer2 += points)}");
                }
                else if (HasThreeOfAKind(rollResults))
                {
                    int points = 3;
                    Console.WriteLine($"Player {currentPlayer} got 3 of a kind! Total score: {(currentPlayer == 1 ? totalPlayer1 += points : totalPlayer2 += points)}");
                }
                else if (HasTwoOfAKind(rollResults))
                {
                    if (!RerollOption(isAgainstFriend))
                    {
                        Console.WriteLine("Invalid choice. Rerolling all dice.");
                        continue;
                    }

                    // Update rollResults after rerolling
                    for (int i = 0; i < dice.Count; i++)
                    {
                        rollResults[i] = dice[i].FaceValue;
                    }

                    // Check for combinations after rerolling

                    if (HasFiveOfAKind(rollResults))
                    {
                        int points = 12;
                        Console.WriteLine($"Player {currentPlayer} got 5 of a kind after rerolling! Total score: {(currentPlayer == 1 ? totalPlayer1 += points : totalPlayer2 += points)}");
                    }

                    else if (HasFourOfAKind(rollResults))
                    {
                        int points = 6;
                        Console.WriteLine($"Player {currentPlayer} got 4 of a kind after rerolling! Total score: {(currentPlayer == 1 ? totalPlayer1 += points : totalPlayer2 += points)}");
                    }

                    else if (HasThreeOfAKind(rollResults))
                    {
                        int points = 3;
                        Console.WriteLine($"Player {currentPlayer} got 3 of a kind after rerolling! Total score: {(currentPlayer == 1 ? totalPlayer1 += points : totalPlayer2 += points)}");
                    }


                    // Switch to the next player
                    currentPlayer = (currentPlayer == 1) ? 2 : 1;

                    // Continue to the next turn
                    continue;
                }

                // Display total scores for both players
                Console.WriteLine();
                Console.WriteLine($"Total score for player 1: {totalPlayer1}, Total score for player 2: {totalPlayer2}");

                // Switch to the next player
                currentPlayer = (currentPlayer == 1) ? 2 : 1;
            }

            // Determine the winner
            if (totalPlayer1 >= 20 && totalPlayer2 >= 20)
            {
                if (totalPlayer1 > totalPlayer2)
                    Console.WriteLine($"Player 1 wins with a score of {totalPlayer1}!");
                else if (totalPlayer2 > totalPlayer1)
                    Console.WriteLine($"Player 2 wins with a score of {totalPlayer2}!");
                else
                    Console.WriteLine("It's a tie!");
            }
            else if (totalPlayer1 >= 20)
            {
                Console.WriteLine();
                Console.WriteLine($"Player 1 wins with a score of {totalPlayer1}!");
            }
            else if (totalPlayer2 >= 20)
            {
                Console.WriteLine();
                Console.WriteLine($"Player 2 wins with a score of {totalPlayer2}!");
            }
        }


        private bool HasThreeOfAKind(int[] rollResults)
        {
            return rollResults.Any(result => rollResults.Count(x => x == result) >= 3);
        }

        private bool HasFourOfAKind(int[] rollResults)
        {
            return rollResults.Any(result => rollResults.Count(x => x == result) >= 4);
        }

        private bool HasFiveOfAKind(int[] rollResults)
        {
            return rollResults.Any(result => rollResults.Count(x => x == result) == 5);
        }

        private bool HasTwoOfAKind(int[] rollResults)
        {
            return rollResults.GroupBy(x => x).Any(g => g.Count() == 2);
        }

        // Method to handle the reroll option, allowing the player to reroll all dice or just the remaining ones
        private bool RerollOption(bool isAgainstFriend)
        {
            int choice = 0; // Variable to store the player's choice

            // Prompt the player if playing against a friend, otherwise randomly choose between 1 and 2 because it will be the computer playing
            if (isAgainstFriend)
            {
                Console.WriteLine();
                Console.WriteLine("You rolled 2 of a kind. Do you want to reroll all dice (1) or just the remaining ones (2)?");
                while (true)
                {
                    // Read the player's choice, ensuring it's either 1 or 2
                    if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2))
                    {
                        break; // Break the loop if a valid choice is entered
                    }
                    Console.WriteLine("Invalid choice. Please enter 1 to reroll all dice or 2 to reroll just the remaining ones.");
                }
            }
            else // If playing against the computer, randomly choose between 1 and 2
            {
                Random random = new Random();
                choice = random.Next(1, 3); // Randomly choose between 1 and 2
            }

            Console.WriteLine();
            Console.WriteLine($"Choice: {choice}"); // Display the player's choice

            if (choice == 2) // If the player chose to reroll just the remaining ones
            {
                // Identify the value of the two of a kind
                int value = 0;
                foreach (var die in dice)
                {
                    // Check which die has a count of 2, indicating the two of a kind
                    if (dice.Count(d => d.FaceValue == die.FaceValue) == 2)
                    {
                        value = die.FaceValue;
                        break;
                    }
                }

                // If the value is still 0, there was an error, reroll all dice
                if (value == 0)
                {
                    Console.WriteLine("Error: Two of a kind not found. Rerolling all dice.");
                    foreach (Die die in dice)
                    {
                        die.Roll();
                    }
                    return false;
                }

                Console.WriteLine($"Two of a kind value: {value}"); // Display the value of the two of a kind

                // Reroll the remaining dice based on the choice
                foreach (Die die in dice)
                {
                    // Reroll only the dice with a value different from the two of a kind
                    if (die.FaceValue != value)
                    {
                        die.Roll();
                    }
                }
            }
            else // Rerolls all dice
            {
                foreach (Die die in dice)
                {
                    die.Roll();
                }
            }

            // Display the face values of the dice after rerolling to the user
            Console.WriteLine("Dice after reroll:");
            foreach (Die die in dice)
            {
                Console.WriteLine(die.FaceValue);
            }

            return true; // Return true to indicate successful rerolling
        }
    }
}
