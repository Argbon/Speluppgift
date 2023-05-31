using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tre_i_rad_MKV
{
    internal class Program
    {

        public string[] placements = { " ", " ", " ", " ", " ", " ", " ", " ", " ", };
        public List<string> AIplacements = new List<string>()
        {
             "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };
        public int[] wins = { 0, 0, 0 };


        static void Main(string[] args)
        {
            Program program = new Program();
            program.main_menu();
        }

        public void main_menu()
        {
            Console.WriteLine("1 : Player vs Player");
            Console.WriteLine("2 : player vs AI");
            Console.WriteLine("3 : Reset Wincounter");
            Console.WriteLine("4 : Quit game");
            string inp = Console.ReadLine();
            switch(inp)
            {
                case "1":
                    string player = randomizer("X");
                    if (player == "X")
                    {
                        Console.Clear();
                        place("X", false, "player");
                    }
                    else if (player == "O")
                    {
                        Console.Clear();
                        place("O", false, "player");
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Pick a difficulty!\n 1 : Easy\n 2 : Hard");
                    int inp2 = Convert.ToInt32(Console.ReadLine());
                    if (inp2 == 1)
                    {
                        string player2 = randomizer("AI");
                        if (player2 == "X")
                        {
                            Console.Clear();
                            place("X", true, "easy");
                        }
                        else if (player2 == "AI")
                        {
                            Console.Clear();
                            eAIplace();
                        }
                    }
                    else if (inp2 == 2)
                    {
                        string player3 = randomizer("AI");
                        if (player3 == "X")
                        {
                            Console.Clear();
                            place("X", true, "easy");
                        }
                        else if (player3 == "AI")
                        {
                            Console.Clear();
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                        Console.Clear();
                        main_menu();
                    }
                    break;

                case "3":
                    wins[0] = 0;
                    wins[1] = 0;
                    wins[2] = 0;
                    Console.Clear();
                    main_menu();
                    break;

                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    Console.Clear();
                    main_menu();
                    break;
            }
        }

        public string randomizer(string inp)
        {
            Random random = new Random();
            int r = random.Next(1, 3);
            if (r == 1)
            {
                return "X";
            }
            else if (r ==2)
            {
                if (inp == "AI")
                {
                    return "AI";
                }
                else
                {
                    return "O";
                }
            }
            return "X";
            
        }

        public void place(string player, bool vsAI, string aiDiff)
        {
            if (player == "X" && vsAI == false)
            {
                Console.WriteLine();
                playing_field();
                
                Console.WriteLine("\nWhere would you like to place your X");
                string inp = Console.ReadLine();

                if (inp != "1" && inp != "2" && inp != "3" && inp != "4" && inp != "5" && inp != "6" && inp != "7" && inp != "8" && inp != "9")
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input!");
                    place(player, vsAI, aiDiff);
                }

                bool valid = taken(inp);
                if (valid == true)
                {
                    int nInp = Int32.Parse(inp);
                    placements[nInp - 1] = "X";
                    win("X");
                    Console.Clear();
                    place("O", vsAI, aiDiff);
                }
                else if (valid == false)
                {
                    Console.Clear();
                    Console.WriteLine("This place is already taken!");    
                    place("X", vsAI, aiDiff);
                }
            }

            else if (player == "O" && vsAI == false)
            {
                Console.WriteLine();
                playing_field();
                
                Console.WriteLine("\nWhere would you like to place your O");
                string inp = Console.ReadLine();

                if (inp != "1" && inp != "2" && inp != "3" && inp != "4" && inp != "5" && inp != "6" && inp != "7" && inp != "8" && inp != "9")
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input!");
                    place("O", vsAI, aiDiff);
                }

                bool valid = taken(inp);
                if (valid == true)
                {
                    int nInp = Int32.Parse(inp);
                    placements[nInp - 1] = "O";
                    Console.Clear();
                    win("O");
                    place("X", vsAI, aiDiff);
                }
                else if (valid == false)
                {
                    Console.Clear();
                    Console.WriteLine("This place is already taken!");
                    place("X", vsAI, aiDiff);
                }
            }

            else if (player == "X" && vsAI == true)
            {
                Console.WriteLine();
                playing_field();

                Console.WriteLine("\nWhere would you like to place your X");
                string inp = Console.ReadLine();

                if (inp != "1" && inp != "2" && inp != "3" && inp != "4" && inp != "5" && inp != "6" && inp != "7" && inp != "8" && inp != "9")
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input!");
                    place(player, vsAI, aiDiff);
                }

                bool valid = taken(inp);
                if (valid == true)
                {
                    int nInp = Int32.Parse(inp);
                    placements[nInp - 1] = "X";
                    win("X");
                    Console.Clear();
                    eAIplace();
                }
                else if (valid == false)
                {
                    Console.Clear();
                    Console.WriteLine("This place is already taken!");
                    place("X", vsAI, aiDiff);
                }
            }
        }

        public void eAIplace()
        {
            Random random = new Random();
            int r = random.Next(0, AIplacements.Count);
            string inp = AIplacements[r];
            bool valid = taken(inp);

            if (valid == true)
            {
                placements[Int32.Parse(AIplacements[r]) - 1] = "O";
                AIplacements.Remove(AIplacements[r]);
                win("AI");
                Console.Clear();
                place("X", true, "easy");
            }

            else if (valid == false)
            {
                AIplacements.Remove(AIplacements[r]);
                eAIplace();
            }
        }

        public void playing_field()
        {
            Console.WriteLine();
            Console.WriteLine($"            {placements[0]} | {placements[1]} | {placements[2]}");
            Console.WriteLine("            ---------");
            Console.WriteLine($"            {placements[3]} | {placements[4]} | {placements[5]}");
            Console.WriteLine("            ---------");
            Console.WriteLine($"            {placements[6]} | {placements[7]} | {placements[8]}");
        }
   
        public bool taken(string inp)
        {
            int nInp = Int32.Parse(inp);
            if (placements[nInp - 1] == "X" || placements[nInp - 1] == "O")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void win(string player)
        {
            if (player == "X" || player == "O")
            {
                if (placements[0] == player && placements[1] == player && placements[2] == player ||
                    placements[3] == player && placements[4] == player && placements[5] == player ||
                    placements[6] == player && placements[7] == player && placements[8] == player ||

                    placements[0] == player && placements[3] == player && placements[6] == player ||
                    placements[1] == player && placements[4] == player && placements[7] == player ||
                    placements[2] == player && placements[5] == player && placements[8] == player ||

                    placements[0] == player && placements[4] == player && placements[8] == player ||
                    placements[2] == player && placements[4] == player && placements[6] == player)
                {
                    Console.Clear();
                    playing_field();
                    Console.WriteLine($"\n\n      Player {player} has won the game!\n");

                    if (player == "X")
                    {
                        wins[0]++;
                    }
                    else if (player == "O")
                    {
                        wins[1]++;
                    }

                    print_wins();
                    reset();

                    Console.WriteLine("\n      Press enter to continue!");
                    Console.ReadLine();
                    Console.Clear();
                    main_menu();

                }
            }
            else if (player == "AI")
            {
                if (placements[0] == "O" && placements[1] == "O" && placements[2] == "O" ||
                    placements[3] == "O" && placements[4] == "O" && placements[5] == "O" ||
                    placements[6] == "O" && placements[7] == "O" && placements[8] == "O" ||

                    placements[0] == "O" && placements[3] == "O" && placements[6] == "O" ||
                    placements[1] == "O" && placements[4] == "O" && placements[7] == "O" ||
                    placements[2] == "O" && placements[5] == "O" && placements[8] == "0" ||

                    placements[0] == "O" && placements[4] == "O" && placements[8] == "O" ||
                    placements[2] == "O" && placements[4] == "O" && placements[6] == "O")
                {
                    Console.Clear();
                    playing_field();
                    Console.WriteLine($"\nThe AI has won the game!\n");

                    wins[2]++;

                    print_wins();
                    reset();

                    Console.WriteLine("\nPress enter to continue!");
                    Console.ReadLine();
                    Console.Clear();
                    main_menu();

                }
            }
        }

        public void print_wins()
        {
            Console.WriteLine($"      Player X wins: {wins[0]}\n      Player O wins: {wins[1]}\n      AI wins:       {wins[2]}");
        }

        public void reset()
        {
            placements[0] = " ";
            placements[1] = " ";
            placements[2] = " ";
            placements[3] = " ";
            placements[4] = " ";
            placements[5] = " ";
            placements[6] = " ";
            placements[7] = " ";
            placements[8] = " ";

            AIplacements.Clear();
            for(int i = 1; i < 10; i++)
            {
                string a = Convert.ToString(i);
                AIplacements.Add(a);
            }
        }
    }
}


