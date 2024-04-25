using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Game
{
    public static class Testing
    {

        // Method to run tests that are needed
        public static void RunTests()
        {
            // Runs the tests for SevensOut and ThreeOrMore games
            TestSevensOut();
            TestThreeOrMore();

            // Display message to the user when tests are completed
            Console.WriteLine("Tests completed. Press any key to exit.");
            Console.ReadKey();
        }

        // Method to test the SevensOut game
        private static void TestSevensOut()
        {
            Console.WriteLine("Running Sevens Out Testing...");

            // Create a SevensOut game object
            SevensOut sevensOut = new SevensOut();

            // Test case 1: Check if the sum of dice rolls stops when total equals 7
            int sum = 0;
            while (true)
            {
                // Roll two dice and calculate sum
                int rollResult1 = sevensOut.RollDie1();
                int rollResult2 = sevensOut.RollDie2();
                sum = rollResult1 + rollResult2;

                // Assert that the sum is not 7 (the game should stop when it's 7)
                Debug.Assert(sum != 7, "Sum equals 7, game should stop.");

                break; // Ensure the loop terminates after the first iteration
            }

            Console.WriteLine("Sevens Out Testing Completed.");
        }

        // Method to test the ThreeOrMore game
        private static void TestThreeOrMore()
        {
            Console.WriteLine("Running Three Or More Testing...");

            // Create a ThreeOrMore game object
            ThreeOrMore threeOrMore = new ThreeOrMore();

            // Test case 1: Check if scores are set and added correctly
            int expectedTotal = 0;
            for (int i = 0; i < 5; i++)
            {
                int rollResult = threeOrMore.RollDie(i);
                expectedTotal += rollResult;
            }

            Debug.Assert(threeOrMore.GetTotalPlayer1Score() == expectedTotal, "Scores not set or added correctly.");

            // Test case 2: Check if the game recognizes when total score >= 20
            while (true)
            {
                // Roll a die and add the result of the dice.
                int rollResult = threeOrMore.RollDie(0); // Rolling only the first die for simplicity
                expectedTotal += rollResult;

                // Break the loop if the total score is greater than or equal to 20 because thats when the game should end
                if (expectedTotal >= 20)
                    break;
            }

            // Assert that the total score is greater than or equal to 20 so the user knows what has happened
            Debug.Assert(threeOrMore.GetTotalPlayer1Score() >= 20, "Total score should be greater than or equal to 20.");

            Console.WriteLine("Three Or More Testing Completed.");
        }
    }
}
