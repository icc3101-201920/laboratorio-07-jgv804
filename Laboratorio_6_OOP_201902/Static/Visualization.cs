using Laboratorio_6_OOP_201902.Cards;
using Laboratorio_6_OOP_201902.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_6_OOP_201902.Static
{
    public static class Visualization
    {
        public static void ShowHand(Hand hand)
        {
            CombatCard combatCard;
            Console.WriteLine("Hand: ");
            for (int i = 0; i<hand.Cards.Count; i++)
            {
                if (hand.Cards[i] is CombatCard)
                {
                    combatCard = hand.Cards[i] as CombatCard;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"|({i}) {combatCard.Name} ({combatCard.Type}): {combatCard.AttackPoints} |");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"|({i}) {hand.Cards[i].Name} ({hand.Cards[i].Type}) |");
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        public static void ShowDecks(List<Deck> decks)
        {
            Console.WriteLine("Select one Deck:");
            for (int i = 0; i<decks.Count; i++)
            {
                Console.WriteLine($"({i}) Deck {i+1}");
            }
        }
        public static void ShowCaptains(List<SpecialCard> captains)
        {
            Console.WriteLine("Select one captain:");
            for (int i = 0; i < captains.Count; i++)
            {
                Console.WriteLine($"({i}) {captains[i].Name}: {captains[i].Effect}");
            }
        }
        public static int GetUserInput(int maxInput, bool stopper = false)
        {
            bool valid = false;
            int value;
            int minInput = stopper ? -1 : 0;
            while (!valid)
            {

                if (int.TryParse(Console.ReadLine(), out value))
                {
                    if (value >= minInput && value <= maxInput)
                    {
                        return value;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine($"The option ({value}) is not valid, try again");
                        Console.ResetColor();
                    }
                }
                else
                {
                    ConsoleError($"Input must be a number, try again");
                }
            }
            return -1;
        }
        public static void ConsoleError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void ShowProgramMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void ShowListOptions (List<string> options, string message = null)
        {
            if (message != null) Console.WriteLine($"{message}");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"({i}) {options[i]}");
            }
        }
        public static void ClearConsole()
        {
            Console.ResetColor();
            Console.Clear();
        }
        public static void ShowBoard(Board board, int player, int[] lifePoints, int[] attackPoints)
        {
            IAttackPoints Boarde = board;
            Console.WriteLine("Board\n\n");
            CombatCard LongRangeC;
            CombatCard RangeC;
            CombatCard MeleeC;
            if (player == 1)
            {
                Console.WriteLine($"Opponent: - Life Points:   {lifePoints[0]} AttackPoints:  {attackPoints[0]} " );

                Console.Write("(LongRange) ");

                if (board.PlayerCards[0].ContainsKey(EnumType.bufflongRange))
                {
                    Console.Write("(Buffed");
                    
                    
                }
                Console.Write( $"[{Boarde.GetAttackPoints(EnumType.longRange)[0]}]: ");
               
                
                for (int o = 0; o < board.PlayerCards[0][EnumType.longRange].Count; o++)
                {
                    LongRangeC = board.PlayerCards[0][EnumType.longRange][o] as CombatCard;
                    Console.Write($"|{LongRangeC.AttackPoints}|");
                }
                
                Console.Write("\n");
                Console.Write("(Range) ");
                if (board.PlayerCards[0].ContainsKey(EnumType.buffrange))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.range)[0]}]: ");
                for (int f = 0; f < board.PlayerCards[0][EnumType.range].Count; f++)
                {
                    RangeC = board.PlayerCards[0][EnumType.range][f] as CombatCard;
                    Console.Write($"|{RangeC.AttackPoints}|");
                   
                }
                Console.Write("\n");
                Console.Write("(Melee) ");
                if (board.PlayerCards[0].ContainsKey(EnumType.buffmelee))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.melee)[0]}]: ");
                for (int a = 0; a < board.PlayerCards[0][EnumType.melee].Count; a++)
                {
                    MeleeC = board.PlayerCards[0][EnumType.melee][a] as CombatCard;
                    Console.Write($"|{MeleeC.AttackPoints}|");
                    
                }
                Console.Write("\n\n");
                Console.Write("Weather Cards: ");
                for(int h = 0; h < board.WeatherCards.Count; h++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write($"|{board.WeatherCards[h].Name}|");
                }
                Console.ResetColor();
                Console.Write("\n\n");
                Console.WriteLine($"You: - Life Points:   {lifePoints[player]} AttackPoints:  {attackPoints[player]} ");

                Console.Write("(LongRange) ");
                if (board.PlayerCards[player].ContainsKey(EnumType.bufflongRange))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.longRange)[player]}]: ");
                for (int t = 0; t < board.PlayerCards[player][EnumType.longRange].Count; t++)
                {
                    LongRangeC = board.PlayerCards[player][EnumType.longRange][t] as CombatCard;
                    Console.Write($"|{LongRangeC.AttackPoints}|");
                }
                Console.Write("\n");
                Console.Write("(Range) ");
                if (board.PlayerCards[player].ContainsKey(EnumType.buffrange))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.range)[player]}]: ");
                for (int s = 0; s < board.PlayerCards[player][EnumType.range].Count; s++)
                {
                    RangeC = board.PlayerCards[player][EnumType.range][s] as CombatCard;
                    Console.Write($"|{RangeC.AttackPoints}|");
                }
                Console.Write("\n");
                Console.Write("(Melee) ");
                if (board.PlayerCards[player].ContainsKey(EnumType.buffmelee))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.melee)[player]}]: ");
                for (int v = 0; v < board.PlayerCards[player][EnumType.melee].Count; v++)
                {
                    MeleeC = board.PlayerCards[player][EnumType.melee][v] as CombatCard;
                    Console.Write($"|{MeleeC.AttackPoints}|");
                }
                Console.Write("\n\n");
                Console.Write("Weather Cards: ");
                for (int d = 0; d < board.WeatherCards.Count; d++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write($"|{board.WeatherCards[d].Name}|");
                }
                Console.ResetColor();
                Console.Write("\n\n");








            }
            else
            {

                Console.WriteLine($"Opponent: - Life Points:   {lifePoints[1]} AttackPoints:  {attackPoints[1]} ");

                Console.Write("(LongRange) ");
                if (board.PlayerCards[1].ContainsKey(EnumType.bufflongRange))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.longRange)[1]}]: ");
                for (int c = 0; c < board.PlayerCards[1][EnumType.longRange].Count; c++)
                {
                    LongRangeC = board.PlayerCards[1][EnumType.longRange][c] as CombatCard;
                    Console.Write($"|{LongRangeC.AttackPoints}|");
                }
                Console.Write("\n");
                Console.Write("(Range) ");
                if (board.PlayerCards[1].ContainsKey(EnumType.buffrange))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.range)[1]}]: ");
                for (int x = 0; x < board.PlayerCards[1][EnumType.range].Count; x++)
                {
                    RangeC = board.PlayerCards[1][EnumType.range][x] as CombatCard;
                    Console.Write($"|{RangeC.AttackPoints}|");
                }
                Console.Write("\n");
                Console.Write("(Melee) ");
                if (board.PlayerCards[1].ContainsKey(EnumType.buffmelee))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.melee)[1]}]: ");
                for (int q = 0; q < board.PlayerCards[1][EnumType.melee].Count; q++)
                {
                    Console.Write($"|{board.PlayerCards[1][EnumType.melee][q]}|");
                }
                Console.Write("\n\n");
                Console.Write("Weather Cards: ");
                for (int z = 0; z < board.WeatherCards.Count; z++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write($"|{board.WeatherCards[z]}|");
                }
                Console.ResetColor();
                Console.Write("\n\n");
                Console.WriteLine($"You: - Life Points:   {lifePoints[player]} AttackPoints:  {attackPoints[player]} ");

                Console.Write("(LongRange) ");
                if (board.PlayerCards[player].ContainsKey(EnumType.bufflongRange))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.longRange)[player]}]: ");
                for (int b = 0; b < board.PlayerCards[player][EnumType.longRange].Count; b++)
                {
                    Console.Write($"|{board.PlayerCards[player][EnumType.longRange][b]}|");
                }
                Console.Write("\n");
                Console.Write("(Range) ");
                if (board.PlayerCards[player].ContainsKey(EnumType.buffrange))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.range)[player]}]: ");
                for (int r = 0; r < board.PlayerCards[player][EnumType.range].Count; r++)
                {
                    Console.Write($"|{board.PlayerCards[player][EnumType.range][r]}|");
                }
                Console.Write("\n");
                Console.Write("(Melee) ");
                if (board.PlayerCards[player].ContainsKey(EnumType.buffmelee))
                {
                    Console.Write("(Buffed");


                }
                Console.Write($"[{Boarde.GetAttackPoints(EnumType.melee)[player]}]: ");
                for (int vb = 0; vb < board.PlayerCards[player][EnumType.melee].Count; vb++)
                {
                    Console.Write($"|{board.PlayerCards[player][EnumType.melee][vb]}|");
                }
                Console.Write("\n\n");
                Console.Write("Weather Cards: ");
                for (int df = 0; df < board.WeatherCards.Count; df++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write($"|{board.WeatherCards[df]}|");
                }
                Console.ResetColor();
                Console.Write("\n\n");

            }

        }
    }
    
}
