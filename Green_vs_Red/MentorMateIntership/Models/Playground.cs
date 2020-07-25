using System;

namespace MentorMateIntership.Models
{
    public class Playground
    {
        private static int[,] matrix;
        private static int width;
        private static int height;

        public Playground(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            matrix = new int[this.Height, this.Width];
        }

        //set value and check for bad input
        public int Width
        {
            get => width;
            set
            {
                width = value;

                if (width < 0)
                {
                    throw new ArgumentException("Width cannot be a negative number!!!");
                }

                if (width == 0)
                {
                    throw new ArgumentException("Width cannot be zero!!!");
                }
            }
        }

        //set value and check for bad input
        public int Height
        {
            get => height;
            set
            {
                height = value;

                if (height < 0)
                {
                    throw new ArgumentException("Height cannot be a negative number!!!");
                }
                if (height == 0)
                {
                    throw new ArgumentException("Height cannot be zero!!!");
                }
            }
        }

        //Fill the matrix
        public void GenerationZero()
        {
            for (int row = 0; row < this.Height; row++)
            {
                var input = Console.ReadLine();
                for (int col = 0; col < this.Width; col++)
                {
                    var current = input[col] - '0';
                    matrix[row, col] = current;
                }
            }
        }

        //business logic
        public int CountGenerationsInWhichCellIsGreen(int column, int Row, int n)
        {
            var answer = 0;

            //1st cycle is for the generations
            for (int gen = 0; gen <= n; gen++)
            {
                // If initial cell is green
                bool isInitialCellGreen = matrix[Row, column] == 1;

                if (isInitialCellGreen)
                {
                    answer++;
                }

                //2nd and 3rd cycles are for the logic
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        var greenCount = 0;

                        //positions
                        var upperRow = row - 1;
                        var bottomRow = row + 1;
                        var leftCol = col - 1;
                        var rightCol = col + 1;

                        //conditions
                        bool isUpRowAvailable = upperRow >= 0;
                        bool isDownRowAvailable = bottomRow < height;
                        bool isLeftColAvailable = leftCol >= 0;
                        bool isRightColAvailable = rightCol < width;
                        bool isCellred = matrix[row, col] == 0;
                        bool isCellGreen = matrix[row, col] == 1;

                        //check if there is an upper row of elements
                        if (isUpRowAvailable)
                        {
                            //above the current cell
                            if (IsCellGreen(upperRow, col))
                            {
                                greenCount++;
                            }

                            //check upper left
                            if (isLeftColAvailable && IsCellGreen(upperRow, leftCol))
                            {
                                greenCount++;
                            }

                            //check upper right
                            if (isRightColAvailable && IsCellGreen(upperRow, rightCol))
                            {
                                greenCount++;
                            }
                        }

                        //check current row left and right  
                        if (isLeftColAvailable && IsCellGreen(row, leftCol))
                        {
                            greenCount++;
                        }
                        if (isRightColAvailable && IsCellGreen(row, rightCol))
                        {
                            greenCount++;
                        }

                        //check if there is bottom row of elements
                        if (isDownRowAvailable)
                        {
                            //below the current cell
                            if (IsCellGreen(bottomRow, col))
                            {
                                greenCount++;
                            }

                            //check bottom left
                            if (isLeftColAvailable && IsCellGreen(bottomRow, leftCol))
                            {
                                greenCount++;
                            }

                            //check bottom right
                            if (isRightColAvailable && IsCellGreen(bottomRow, rightCol))
                            {
                                greenCount++;
                            }
                        }


                        //this is a trick that i use in order to iterate and apply the conditions at the same time for all of the
                        //elements in the matrix... see below

                        //if current cell is red and conditions are met:
                        //set value to 11 and with that the cell is still considered red but will change to 1 (green) ->
                        // -> in next generation provided with the method PrepareForNextGeneration();
                        if (isCellred)
                        {
                            switch (greenCount)
                            {
                                case 3:
                                case 6:
                                    matrix[row, col] = 11;
                                    break;
                            }
                        }

                        //if current cell is green and conditions are met do the ->
                        // -> same as the one up top but the value is 10 and it will be changed to 0 (red) with PrepareForNextGeneration();
                        else if (isCellGreen)
                        {
                            switch (greenCount)
                            {
                                case 0:
                                case 1:
                                case 4:
                                case 5:
                                case 7:
                                case 8:
                                    matrix[row, col] = 10;
                                    break;
                            }
                        }
                    }
                }

                //see description below
                PrepareForNextGeneration();
            }

            return answer;
        }

        //This method is used to set values to normal for next generation
        private void PrepareForNextGeneration()
        {
            for (int row1 = 0; row1 < height; row1++)
            {
                for (int col1 = 0; col1 < width; col1++)
                {
                    var current = matrix[row1, col1];

                    //if cell is green
                    if (current == 11)
                    {
                        matrix[row1, col1] = 1;
                    }

                    //if cell is red
                    if (current == 10)
                    {
                        matrix[row1, col1] = 0;
                    }
                }
            }
        }

        //This method is used to validate if the cell is green
        private bool IsCellGreen(int row, int col)
        {
            return matrix[row, col] == 1 || matrix[row, col] == 10;
        }
    }
}
