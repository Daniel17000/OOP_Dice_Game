using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Diagnostics;

namespace OOP_Dice_Game
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to the Dice Games");

                Game game;
                Statistics statistics = new Statistics(); // Create an instance of the Statistics class

                // Main game loop here
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Choose a game:");
                    Console.WriteLine();
                    Console.WriteLine("1. Sevens Out");
                    Console.WriteLine("2. Three or More");
                    Console.WriteLine("3. View Statistics");
                    Console.WriteLine("4. Run Tests");
                    Console.WriteLine("5. Exit");

                    int choice;

                    // Loop to make sure that there is valid input for the choice of game
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out choice))
                        {
                            if (choice >= 1 && choice <= 5)
                            {
                                break;
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    }

                    switch (choice)
                    {
                        case 1:
                            game = new SevensOut();                 // Instantiate the SevensOut game
                            game.Play(ChooseOpponent() == 1);       // Start the game with the chosen opponent 
                            if (game is SevensOut sevensOutGame)
                            {
                                // Record the result in statistics
                                statistics.RecordGameResult("SevensOut", sevensOutGame.TotalScore);
                            }
                            break;
                        case 2:
                            game = new ThreeOrMore();               // Instantiate the ThreeOrMore game
                            game.Play(ChooseOpponent() == 1);       // Start the game with the chosen opponent 
                            if (game is ThreeOrMore threeOrMoreGame)
                            {
                                // Record the result in statistics
                                statistics.RecordGameResult("ThreeOrMore", threeOrMoreGame.GetTotalScore());
                            }
                            break;
                        case 3:
                            statistics.DisplayStatistics(); // Display statistics when requested
                            break;
                        case 4:
                            Testing.RunTests(); // Run the testing methods
                            break;
                        case 5:
                            Console.WriteLine();
                            Console.WriteLine("Thank you for playing the dice games, goodbye");
                            return;     // Exits the program
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");      // This will find and display any exceptions use the 'catch' keyword
            }
        }

        static int ChooseOpponent()
        {
            int opponentChoice;
            // Loop to ensure valid input for opponent choice
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Would you like to play against another player (1) or against the computer (2)?");
                if (int.TryParse(Console.ReadLine(), out opponentChoice))
                {
                    if (opponentChoice == 1 || opponentChoice == 2)
                    {
                        break;
                    }
                }
                Console.WriteLine("Invalid choice. Please enter 1 to play against another player or 2 to play against the computer.");
            }
            return opponentChoice;
        }
    }
}
