using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static Program program = new Program();

        //Constants
        const sbyte MAX_FAILED_ENTRIES = 5;

        const sbyte MIN_BOARD_SQ_SIZE = 3;
        const sbyte MAX_BOARD_SQ_SIZE = 10;

        const char FIRST_SYM = 'X';
        const char FIRST_PLAYER = '1';
        const char SECOND_SYM = 'O';
        const char SECOND_PLAYER = '2';

        const char YES_ANSWER = 'Y';
        const char NO_ANSWER = 'N';

        int boardSize;

        static void Main(string[] args)
        {
            program.Print("**********************\n");
            program.Print("\tTIC. TAC. TOE!\n");
            program.Print("**********************\n\n");

            //Declaration
            int entries = 0;
            char continueGame = YES_ANSWER;

            while (continueGame != NO_ANSWER)
            {
                program.boardSize = 0;
                do
                {
                    program.Print("What would you like to be the size of your square board (" + MIN_BOARD_SQ_SIZE + " - " + MAX_BOARD_SQ_SIZE + ")?: ");
                    program.boardSize = program.ReadInt();
                }
                while ((program.boardSize < MIN_BOARD_SQ_SIZE || program.boardSize > MAX_BOARD_SQ_SIZE) && (program.Print("Invalid entry. Try again. \n\n")));

                char playerNum = FIRST_PLAYER, playerTurn = '0';
                char[,] board = new char[program.boardSize, program.boardSize];

                //Allow user to pick a player
                if (!program.PlayerPick(ref playerNum, ref playerTurn, ref entries))
                {
                    return;
                }

                 //Prompt the user 9 times (the maximum of spaces on a tic tac toe board)
                entries = 0;
                int maxEntries = program.boardSize * program.boardSize;
                program.PrintBoard(board);
                while (entries < maxEntries)
                {
                    int tempRow, tempCol;
                    program.Print("TURN: Player " + playerNum + " (" + playerTurn + ")\n");

                    //Row Entries & Validation
                    char validSpot = 'N';
                    while (validSpot == 'N')
                    {
                        program.Print("Enter the row you want to be placed in (1 - " + program.boardSize + "): ");
                        tempRow = program.ReadInt(true);

                        while (tempRow <= 0 || tempRow > program.boardSize)
                        {
                            program.Print("Invalid entry. Enter the row you want to be placed in (ROWS: 1 - " + program.boardSize + "): ");
                            tempRow = program.ReadInt(true);
                        }

                        program.Print("Enter the column you want to be placed in (1 - " + program.boardSize + "): ");
                        tempCol = program.ReadInt(true);
                        while (tempCol <= 0 || tempCol > program.boardSize)
                        {
                            program.Print("Invalid entry. Enter the column you want to be placed in (COLUMNS: 1 - " + program.boardSize + "): ");
                            tempCol = program.ReadInt(true);
                        }

                        if (board[tempRow - 1, tempCol - 1] == default(char))
                        {
                            board[tempRow - 1, tempCol - 1] = playerTurn; //Add to board array
                            validSpot = 'Y';
                        }
                        else
                        {
                            program.Print("That spot is filled already. Try again.\n\n");
                        }
                    }

                    program.PrintBoard(board); //Print the board on the screen

                    if (program.CheckBoard(board, ref playerTurn)) //Check if there is a winner.
                    {
                        program.Print("Player " + playerNum + " (" + playerTurn + ") wins!\n----\n"); //" +  + "
                        break;
                    }

                    program.PlayerSwitch(ref playerNum, ref playerTurn); //Switch players

                    entries++;
                }

                // If all squares are filled
                if (entries >= maxEntries)
                {
                    if (program.CheckBoard(board, ref playerTurn))
                    {
                        program.Print("Player " + playerNum + " (" + playerTurn + ") wins!\n----\n");
                    }
                    else
                    {
                        program.Print("It's a draw! Put away your weapons and shake on it.\n----\n");
                    }
                }

                do
                {
                    program.Print("Would you like to continue? (Y for Yes, N for No): ");
                    continueGame = program.ReadChar();
                    continueGame = Char.ToUpper(continueGame);
                }

                while ((continueGame != NO_ANSWER && continueGame != YES_ANSWER) && (program.Print("Invalid entry. Try again.\n\n")));
            }
        }

        bool CheckBoard(char[,] board, ref char playerTurn)
        {
            int checks = 0;
            for(int n = 0; n < boardSize; n++)
            {
                // Vertical checks
                checks = 0;
                for(int i = 0; i < boardSize; i++)
                {
                    if(board[n,i] == playerTurn)
                    {
                        checks++;
                    }
                    else
                    {
                        break;
                    }
                }
                if(checks == boardSize)
                {
                    return true;
                }

                // Horizontal checks
                checks = 0;
                for(int i = 0; i < boardSize; i++)
                {
                    if(board[i,n] == playerTurn)
                    {
                        checks++;
                    }
                    else
                    {
                        break;
                    }
                }
                if(checks == boardSize)
                {
                    return true;
                }
            }

            //Diagonal checks
            checks = 0;
            for(int i = 0; i < boardSize; i++)
            {
                if(board[i,i] == playerTurn)
                {
                    checks++;
                }
                else
                {
                    break;
                }
            }
            if(checks == boardSize)
            {
                return true;
            }

            checks = 0;
            for(int i = 0; i < boardSize; i++)
            {
                if(board[(boardSize - 1) - i,i] == playerTurn)
                {
                    checks++;
                }
                else
                {
                    break;
                }
            }
            if(checks == boardSize)
            {
                return true;
            }

            return false;
        }

        void PrintBoard(char[,] board)
        {
            for(int rows = 0; rows < boardSize; rows++)
            {
                for(int columns = 0; columns < boardSize; columns++)
                {
                    if(!(board[rows,columns] == default(char)))
                    {
                        Print("|" + board[rows,columns]);
                    }
                    else
                    {
                        Print("| ");
                    }
                }
                Print("| ROW " + (rows + 1) + "\n");
            }
            Print("COLUMNS\n\n");
        }

        void PlayerSwitch(ref char playerNum, ref char playerTurn)
        {
            if(playerNum == FIRST_PLAYER)
            {
                playerNum = SECOND_PLAYER;
            }
            else
            {
                playerNum = FIRST_PLAYER;
            }

            if(playerTurn == FIRST_SYM)
            {
                playerTurn = SECOND_SYM;
            }
            else
            {
                playerTurn = FIRST_SYM;
            }
        }

        bool PlayerPick(ref char playerNum, ref char playerTurn, ref int entries)
        {
            Print("Player " + playerNum + ", would you like to be X or O?: ");

            do
            {
                playerTurn = ReadChar(true);
                playerTurn = Char.ToUpper(playerTurn);
                entries += 1;

                if(entries >= MAX_FAILED_ENTRIES && playerTurn != SECOND_SYM && playerTurn != FIRST_SYM)
                {
                    Print("Too many failed entries. Ending the program.\n");
                    return false;
                }
            }
            while((playerTurn != SECOND_SYM && playerTurn != FIRST_SYM) && (Print("Incorrect entry. Player " + playerNum + ", would you like to be X or O? (" + (MAX_FAILED_ENTRIES - entries) +" entries remaining):")));

            return true;
        }

        /// <summary>
        /// A shorthand method for writing to the console.
        /// </summary>
        /// <param name="txt">The text to display to the console.</param>
        /// <returns>Always true.</returns>
        public bool Print(string txt)
        {
            if(txt.Length > 0)
            {
                System.Console.Write(txt);
            }

            return true;
        }

        /// <summary>
        /// A shorthand method for reading integer values from the console.
        /// </summary>
        /// <param name="verify">Whether or not to handle forcefully verifying valid entries.</param>
        /// <returns>The successfully entered integer or 0 if verifying is false and an invalid entry was given.</returns>
        public int ReadInt(bool verify = false)
        {
            while (true)
            {
                string value = System.Console.ReadLine();
                int returnValue;
                if (Int32.TryParse(value, out returnValue))
                {
                    return returnValue;
                }
                else
                {
                    if (!verify)
                    {
                        return 0;
                    }

                    Print("Invalid entry. Enter an integer value.\n");
                }
            }
        }

        /// <summary>
        /// A shorthand method for reading character values from the console.
        /// </summary>
        /// <param name="verify">Whether or not to handle forcefully verifying valid entries.</param>
        /// <returns>The successfully entered char or '0' if verifying is false and an invalid entry was given.</returns>
        public char ReadChar(bool verify = false)
        {
            while (true)
            {
                string value = System.Console.ReadLine();
                char returnValue;
                if (Char.TryParse(value, out returnValue))
                {
                    return returnValue;
                }
                else
                {
                    if (!verify)
                    {
                        return '0';
                    }

                    Print("Invalid entry. Enter a single character value.\n");
                }
            }
        }
    }
}
