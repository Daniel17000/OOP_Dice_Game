using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dice_Game
{
    public class Die
    {
        private Random random;  // Random number generator instance for rolling the die
        public int FaceValue { get; private set; }  // Property to get the value of the die

        // Constructor to initialize the die
        public Die()
        {
            random = new Random();  // Initialize the random number generator
            Roll(); // Roll the die to get its initial value of the die
        }

        // Method to roll the die
        public int Roll()
        {
            FaceValue = random.Next(1, 7);  // Generate a random number between 1 and 6 (inclusive)
            return FaceValue;    // Return the face value of the die
        }
    }
}
