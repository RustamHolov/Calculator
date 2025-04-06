public class View
{

    public void DisplayStringMatrix(string[,] buttons, (int x, int y)? coords = null)
    {
        int rows = buttons.GetLength(0);
        int cols = buttons.GetLength(1);

        // Calculate the maximum cell width
        int maxCellWidth = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                maxCellWidth = Math.Max(maxCellWidth, buttons[i, j].Length);
            }
        }

        // Add padding to the cell width
        maxCellWidth += 2; // Add 2 for spaces on either side of the text

        // Build the top border
        string topBorder = "┌";
        for (int j = 0; j < cols; j++)
        {
            topBorder += new string('─', maxCellWidth);
            if (j < cols - 1)
            {
                topBorder += "┬";
            }
            else
            {
                topBorder += "┐";
            }
        }
        Console.WriteLine(topBorder);

        // Build the rows
        for (int i = 0; i < rows; i++)
        {
            Console.Write("│"); // Start with the left border
            for (int j = 0; j < cols; j++)
            {
                string cellContent = buttons[i, j];
                int padding = maxCellWidth - cellContent.Length;
                int leftPadding = padding / 2;
                int rightPadding = padding - leftPadding;
                if(coords != null && (i,j)==coords){
                    leftPadding--;
                    rightPadding--;
                    cellContent = "[" + cellContent + "]";
                }

                Console.Write(new string(' ', leftPadding) + cellContent + new string(' ', rightPadding) + "│");
            }
            Console.WriteLine();

            // Add the separator between rows
            if (i < rows - 1)
            {
                Console.Write("├");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(new string('─', maxCellWidth));
                    if (j < cols - 1)
                    {
                        Console.Write("┼");
                    }
                    else
                    {
                        Console.Write("┤");
                    }
                }
                Console.WriteLine();
            }
        }

        // Build the bottom border
        string bottomBorder = "└";
        for (int j = 0; j < cols; j++)
        {
            bottomBorder += new string('─', maxCellWidth);
            if (j < cols - 1)
            {
                bottomBorder += "┴";
            }
            else
            {
                bottomBorder += "┘";
            }
        }
        Console.WriteLine(bottomBorder);
    }
}