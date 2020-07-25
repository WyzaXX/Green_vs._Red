using System;
using System.Linq;
using System.Collections.Generic;

using MentorMateIntership.Models;

namespace MentorMateIntership.Controller
{
    public class Engine
    {
        public Engine()
        {
        }

        public void Run()
        {
            //This method is used to parse inputs and avoid potential user mistakes or bad inputs.
            var size = ParseInput();

            var width = size[0];
            var height = size[1];

            //create the grid for the game and validate the inputs
            var grid = new Playground(width, height);

            //This method is used to fill the grid with data provided.
            grid.GenerationZero();

            var coordinates = ParseInput();

            var x1 = coordinates[0];
            var y1 = coordinates[1];
            var n = coordinates[2];

            //check coordinate inputs
            if (x1 < 0 || y1 < 0 || n < 0)
            {
                throw new ArgumentException("Coordinates cannot be negative numbers!");
            }

            //business logic
            var result = grid.CountGenerationsInWhichCellIsGreen(x1, y1, n);

            //output result
            Console.WriteLine($"Answer: {result}");
        }

        private List<int> ParseInput()
        {
            return Console.ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }
}
