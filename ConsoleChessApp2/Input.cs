using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChessApp
{
    class Input
    {

        // Input
        public static bool PlayAgain()
        {
            Console.WriteLine("\nTo play again write \"again\":");
            try
            {
                String again = Console.ReadLine();
                if (again == "again" || again == "Again" || again == "AGAIN")
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("End.");
                return false;
            }
        }


        // Input
        public static String InputGameMode()
        {
            try
            {
                String gameMode = Console.ReadLine();
                if (gameMode == "players" || gameMode == "player" || gameMode == "Player" || gameMode == "Players" || gameMode == "PLAYERS" || gameMode == "PLAYER")
                {
                    gameMode = "Players";
                }
                else
                {
                    gameMode = "Bot";
                }
                return gameMode;
            }
            catch (Exception e)
            {
                return "Players";
            }
        }


        // Input
        public static String[] InputPlayerNames()
        {

            Console.WriteLine("\nEnter the first player name:");
            String playerName1 = Console.ReadLine();

            Console.WriteLine("\nEnter the second player name:");
            String playerName2 = Console.ReadLine();

            String[] result = { playerName1, playerName2 };

            return result;
        }

        // Input
        public static String[] InputPlayerName()
        {
            Console.WriteLine("Enter player name:");
            String playerName1 = Console.ReadLine();

            return new[] { playerName1, "Bot" };
        }

        internal static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue:");
            Console.ReadLine();
        }


        public static bool ChooseColor()
        {
            String color = Console.ReadLine();

            return color.ToUpper() == "BLACK";
        }
    }
}
