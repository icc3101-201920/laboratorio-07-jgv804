﻿using Laboratorio_6_OOP_201902.Cards;
using Laboratorio_6_OOP_201902.Enums;
using Laboratorio_6_OOP_201902.Static;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Laboratorio_6_OOP_201902
{
    public class Game
    {
        //Constantes
        private const int DEFAULT_CHANGE_CARDS_NUMBER = 3;

        //Atributos
        private Player[] players;
        private Player activePlayer;
        private List<Deck> decks;
        private List<SpecialCard> captains;
        private Board boardGame;
        public int[] LifeP;
        public int[] AttackP;
        internal int turn;

        //Constructor
        public Game()
        {
            Random random = new Random();
            decks = new List<Deck>();
            captains = new List<SpecialCard>();
            AddDecks();
            AddCaptains();
            players = new Player[2] { new Player(), new Player() };
            ActivePlayer = Players[random.Next(2)];
            boardGame = new Board();
            //Add board to players
            players[0].Board = boardGame;
            players[1].Board = boardGame;
            turn = 0;
            LifeP = new int[2];
            AttackP = new int[2];
            
            
        }
        //Propiedades
        public Player[] Players
        {
            get
            {
                return this.players;
            }
        }
        public Player ActivePlayer
        {
            get
            {
                return this.activePlayer;
            }
            set
            {
                activePlayer = value;
            }
        }
        public List<Deck> Decks
        {
            get
            {
                return this.decks;
            }
        }
        public List<SpecialCard> Captains
        {
            get
            {
                return this.captains;
            }
        }
        public Board BoardGame
        {
            get
            {
                return this.boardGame;
            }
        }
  

        //Metodos
        public bool CheckIfEndGame()
        {
            if (players[0].LifePoints == 0 || players[1].LifePoints == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetWinner()
        {
            if (players[0].LifePoints == 0 && players[1].LifePoints > 0)
            {
                return 1;
            }
            else if (players[1].LifePoints == 0 && players[0].LifePoints > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        
        public void Play()
        {
            int userInput = 0;
            int useInputB = 0;
            int firstOrSecondUser = ActivePlayer.Id == 0 ? 0 : 1;
            bool GameSwitch = false;
            //int count1=  0;//
            //int count2 = 0;//
            /*IAttackPoints playa1 = players[0];
            IAttackPoints playa2 = players[1];
            LifeP[0] = players[0].LifePoints;
            LifeP[1] = players[1].LifePoints;
            for(int m = 0; m < playa1.GetAttackPoints(EnumType.None).Length; m++)
            {
                count1 += playa1.GetAttackPoints(EnumType.None)[m];
            }
            for (int n = 0; n < playa2.GetAttackPoints(EnumType.None).Length; n++)
            {
                count2 += playa2.GetAttackPoints(EnumType.None)[n];
            }
            AttackP[0] = playa1.GetAttackPoints(EnumType.None)[0];
            AttackP[1] = playa2.GetAttackPoints(EnumType.None)[0];*/





            //turno 0 o configuracion
            if (turn == 0)
            {
                for (int _ = 0; _<Players.Length; _++)
                {
                    ActivePlayer = Players[firstOrSecondUser];
                    Visualization.ClearConsole();
                    //Mostrar mensaje de inicio
                    Visualization.ShowProgramMessage($"Player {ActivePlayer.Id+1} select Deck and Captain:");
                    //Preguntar por deck
                    Visualization.ShowDecks(this.Decks);
                    userInput = Visualization.GetUserInput(this.Decks.Count - 1);
                    Deck deck = new Deck();
                    deck.Cards = new List<Card>(Decks[userInput].Cards);
                    ActivePlayer.Deck = deck;
                    //Preguntar por capitan
                    Visualization.ShowCaptains(Captains);
                    userInput = Visualization.GetUserInput(this.Captains.Count - 1);
                    ActivePlayer.ChooseCaptainCard(new SpecialCard(Captains[userInput].Name, Captains[userInput].Type, Captains[userInput].Effect));
                    //Asignar mano
                    ActivePlayer.FirstHand();
                    //Mostrar mano
                    Visualization.ShowHand(ActivePlayer.Hand);
                    //Mostar opciones, cambiar carta o pasar
                    Visualization.ShowListOptions(new List<string>() { "Change Card", "Pass" }, "Change 3 cards or ready to play:");
                    userInput = Visualization.GetUserInput(1);
                    if (userInput == 0)
                    {
                        Visualization.ClearConsole();
                        Visualization.ShowProgramMessage($"Player {ActivePlayer.Id+1} change cards:");
                        Visualization.ShowHand(ActivePlayer.Hand);
                        for (int i = 0; i < DEFAULT_CHANGE_CARDS_NUMBER; i++)
                        {
                            Visualization.ShowProgramMessage($"Input the numbers of the cards to change (max {DEFAULT_CHANGE_CARDS_NUMBER}). To stop enter -1");
                            userInput = Visualization.GetUserInput(ActivePlayer.Hand.Cards.Count, true);
                            if (userInput == -1) break;
                            ActivePlayer.ChangeCard(userInput);
                            Visualization.ShowHand(ActivePlayer.Hand);
                        }
                    }
                    firstOrSecondUser = ActivePlayer.Id == 0 ? 1 : 0;
                }
                turn += 1;
               
            }

            IAttackPoints playa1 = players[0];
            IAttackPoints playa2 = players[1];
            LifeP[0] = players[0].LifePoints;
            LifeP[1] = players[1].LifePoints;

            /*for(int y = 0; y < players[0].Hand.Cards.Count; y++)
            {
                if (players[0].Hand.Cards[y] is CombatCard)
                {
                    BoardGame.AddCard(players[0].Hand.Cards[y], 0, players[0].Hand.Cards[y].Type);
                }
            }

            for (int w = 0; w < players[1].Hand.Cards.Count; w++)
            {
                if (players[1].Hand.Cards[w] is CombatCard)
                {
                    BoardGame.AddCard(players[1].Hand.Cards[w], 1, players[1].Hand.Cards[w].Type);
                }
                
             }*/
            

           

            while (GameSwitch != true)
            {
                
                if (ActivePlayer.Id == 0)
                {
                    Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                    Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} will draw a card");
                    ActivePlayer.DrawCard();
                    Visualization.ShowHand(ActivePlayer.Hand);
                    Visualization.ShowListOptions(new List<string>() { "Play Card", "Pass" });
                    userInput=Visualization.GetUserInput(1);
                    if (userInput == 0)
                    {                       
                            Visualization.ClearConsole();
                            Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                            Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} play cards:");
                            Visualization.ShowHand(ActivePlayer.Hand);
                            Visualization.ShowProgramMessage($"Input the number of the cards you'd like to  play (max {ActivePlayer.Hand.Cards.Count}). To stop enter -1");
                            userInput = Visualization.GetUserInput(ActivePlayer.Hand.Cards.Count);
                            
                            Visualization.ClearConsole();
                            if (ActivePlayer.Hand.Cards[userInput].Type == EnumType.buff)
                            {
                            Visualization.ShowProgramMessage("Please Select which type you'd like to buff:");
                            Visualization.ShowProgramMessage("(0): LongRange (1): Range (2): Melee");
                            useInputB = Visualization.GetUserInput(2);

                            switch (useInputB)
                            {
                                case 0:
                                    ActivePlayer.PlayCard(userInput, EnumType.bufflongRange);
                                    break;
                                case 1:
                                    ActivePlayer.PlayCard(userInput, EnumType.buffrange);
                                    break;
                                case 2:
                                    ActivePlayer.PlayCard(userInput, EnumType.buffmelee);
                                    break;


                            }
                            }
                            else
                            {
                            ActivePlayer.PlayCard(userInput, ActivePlayer.Hand.Cards[userInput].Type);

                            }
                            
                            AttackP[ActivePlayer.Id] = playa1.GetAttackPoints(EnumType.None)[0];
                            //Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} has played{ActivePlayer.Hand.Cards[userInput].Name} ");//
                            Visualization.ShowHand(ActivePlayer.Hand);
                            Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                            
                        
                    }
                    Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} Turn has ended please press anykey to continue to the next player turn");
                    Console.ReadKey();
                    Visualization.ClearConsole();
                    ActivePlayer.Id += 1;
                    Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                    Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} will draw a card");
                    ActivePlayer.DrawCard();
                    Visualization.ShowHand(ActivePlayer.Hand);
                    Visualization.ShowListOptions(new List<string>() { "Play Card", "Pass" });
                    userInput = Visualization.GetUserInput(1);
                    if (userInput == 0)
                    {
                        Visualization.ClearConsole();
                        Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                        Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} play cards:");
                        Visualization.ShowHand(ActivePlayer.Hand);
                        Visualization.ShowProgramMessage($"Input the number of the cards you'd like to  play (max {ActivePlayer.Hand.Cards.Count}). To stop enter -1");
                        userInput = Visualization.GetUserInput(ActivePlayer.Hand.Cards.Count);

                        Visualization.ClearConsole();
                        if (ActivePlayer.Hand.Cards[userInput].Type == EnumType.buff)
                        {
                            Visualization.ShowProgramMessage("Please Select which type you'd like to buff:");
                            Visualization.ShowProgramMessage("(0): LongRange (1): Range (2): Melee");
                            useInputB = Visualization.GetUserInput(2);

                            switch (useInputB)
                            {
                                case 0:
                                    ActivePlayer.PlayCard(userInput, EnumType.bufflongRange);
                                    break;
                                case 1:
                                    ActivePlayer.PlayCard(userInput, EnumType.buffrange);
                                    break;
                                case 2:
                                    ActivePlayer.PlayCard(userInput, EnumType.buffmelee);
                                    break;


                            }
                        }
                        else
                        {
                            ActivePlayer.PlayCard(userInput, ActivePlayer.Hand.Cards[userInput].Type);

                        }
                        AttackP[ActivePlayer.Id] = playa2.GetAttackPoints(EnumType.None)[0];
                        //Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} has played{ActivePlayer.Hand.Cards[userInput].Name} ");//
                        Visualization.ShowHand(ActivePlayer.Hand);
                        Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);


                    }
                    Visualization.ClearConsole();
                    Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                    if (AttackP[0] < AttackP[1])
                    {
                        Visualization.ShowProgramMessage("Player 2 has won this round, Player 1 has lost one lifepoint  ");
                        LifeP[0] -= 1;
                        players[0].LifePoints -= 1;


                    }
                    if (AttackP[1] < AttackP[0])
                    {
                        Visualization.ShowProgramMessage("Player 1 has won this round, Player 2 has lost one lifepoint  ");
                        LifeP[1] -= 1;
                        players[1].LifePoints -= 1;


                    }
                    if (AttackP[0] == AttackP[1])
                    {
                        Visualization.ShowProgramMessage("Player 2 has tied this round with Player 1,  both Players  have lost one lifepoint  ");
                        LifeP[0] -= 1;
                        LifeP[1] -= 1;
                        players[0].LifePoints -= 1;
                        players[1].LifePoints -= 1;


                    }
                    GameSwitch=this.CheckIfEndGame();






                }
                else
                {
                    Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                    Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} will draw a card");
                    ActivePlayer.DrawCard();
                    Visualization.ShowHand(ActivePlayer.Hand);
                    Visualization.ShowListOptions(new List<string>() { "Play Card", "Pass" });
                    userInput = Visualization.GetUserInput(1);
                    if (userInput == 0)
                    {
                        Visualization.ClearConsole();
                        Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                        Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} play cards:");
                        Visualization.ShowHand(ActivePlayer.Hand);
                        Visualization.ShowProgramMessage($"Input the number of the cards you'd like to  play (max {ActivePlayer.Hand.Cards.Count}). To stop enter -1");
                        userInput = Visualization.GetUserInput(ActivePlayer.Hand.Cards.Count);

                        Visualization.ClearConsole();
                        if (ActivePlayer.Hand.Cards[userInput].Type == EnumType.buff)
                        {
                            Visualization.ShowProgramMessage("Please Select which type you'd like to buff:");
                            Visualization.ShowProgramMessage("(0): LongRange (1): Range (2): Melee");
                            useInputB = Visualization.GetUserInput(2);

                            switch (useInputB)
                            {
                                case 0:
                                    ActivePlayer.PlayCard(userInput, EnumType.bufflongRange);
                                    break;
                                case 1:
                                    ActivePlayer.PlayCard(userInput, EnumType.buffrange);
                                    break;
                                case 2:
                                    ActivePlayer.PlayCard(userInput, EnumType.buffmelee);
                                    break;


                            }
                        }
                        else
                        {
                            ActivePlayer.PlayCard(userInput, ActivePlayer.Hand.Cards[userInput].Type);

                        }
                        AttackP[ActivePlayer.Id] = playa2.GetAttackPoints(EnumType.None)[0];
                        //Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} has played{ActivePlayer.Hand.Cards[userInput].Name} ");//
                        Visualization.ShowHand(ActivePlayer.Hand);
                        Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);


                    }
                    Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} Turn has ended please press anykey to continue to the next player turn");
                    Console.ReadKey();
                    Visualization.ClearConsole();
                    ActivePlayer.Id -= 1;
                    Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                    Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} will draw a card");
                    ActivePlayer.DrawCard();
                    Visualization.ShowHand(ActivePlayer.Hand);
                    Visualization.ShowListOptions(new List<string>() { "Play Card", "Pass" });
                    userInput = Visualization.GetUserInput(1);
                    if (userInput == 0)
                    {
                        Visualization.ClearConsole();
                        Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                        Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} play cards:");
                        Visualization.ShowHand(ActivePlayer.Hand);
                        Visualization.ShowProgramMessage($"Input the number of the cards you'd like to  play (max {ActivePlayer.Hand.Cards.Count}). To stop enter -1");
                        userInput = Visualization.GetUserInput(ActivePlayer.Hand.Cards.Count);

                        Visualization.ClearConsole();

                        if (ActivePlayer.Hand.Cards[userInput].Type == EnumType.buff)
                        {
                            Visualization.ShowProgramMessage("Please Select which type you'd like to buff:");
                            Visualization.ShowProgramMessage("(0): LongRange (1): Range (2): Melee");
                            useInputB = Visualization.GetUserInput(2);

                            switch (useInputB)
                            {
                                case 0:
                                    ActivePlayer.PlayCard(userInput, EnumType.bufflongRange);
                                    break;
                                case 1:
                                    ActivePlayer.PlayCard(userInput, EnumType.buffrange);
                                    break;
                                case 2:
                                    ActivePlayer.PlayCard(userInput, EnumType.buffmelee);
                                    break;


                            }
                        }
                        else
                        {
                            ActivePlayer.PlayCard(userInput, ActivePlayer.Hand.Cards[userInput].Type);

                        }
                        AttackP[ActivePlayer.Id] = playa1.GetAttackPoints(EnumType.None)[0];
                        //Visualization.ShowProgramMessage($"Player {ActivePlayer.Id + 1} has played{ActivePlayer.Hand.Cards[userInput].Name} ");//
                        
                        Visualization.ShowHand(ActivePlayer.Hand);
                        Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);


                    }
                    Visualization.ClearConsole();
                    Visualization.ShowBoard(BoardGame, ActivePlayer.Id, LifeP, AttackP);
                    if (AttackP[0] < AttackP[1])
                    {
                        Visualization.ShowProgramMessage("Player 2 has won this round, Player 1 has lost one lifepoint  ");
                        LifeP[0] -= 1;
                        players[0].LifePoints -= 1;


                    }
                    if (AttackP[1] < AttackP[0])
                    {
                        Visualization.ShowProgramMessage("Player 1 has won this round, Player 2 has lost one lifepoint  ");
                        LifeP[1] -= 1;
                        players[1].LifePoints -= 1;


                    }
                    if (AttackP[0] == AttackP[1])
                    {
                        Visualization.ShowProgramMessage("Player 2 has tied this round with Player 1,  both Players  have lost one lifepoint  ");
                        LifeP[0] -= 1;
                        LifeP[1] -= 1;
                        players[0].LifePoints -= 1;
                        players[1].LifePoints -= 1;


                    }
                    GameSwitch=this.CheckIfEndGame();



                }



            }







            int gk = this.GetWinner();
            switch (gk)
            {
                case 0:
                    Visualization.ShowProgramMessage("Player 1 has won this game");
                    break;
                case 1:
                    Visualization.ShowProgramMessage("Player 2 has won this game");
                    break;
                case -1:
                    Visualization.ShowProgramMessage("the game was tied");
                    break;


            }
            Console.ReadKey();
        }
        public void AddDecks()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Files\Decks.txt";
            StreamReader reader = new StreamReader(path);
            int deckCounter = 0;
            List<Card> cards = new List<Card>();


            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string [] cardDetails = line.Split(",");

                if (cardDetails[0] == "END")
                {
                    decks[deckCounter].Cards = new List<Card>(cards);
                    deckCounter += 1;
                }
                else
                {
                    if (cardDetails[0] != "START")
                    {
                        if (cardDetails[0] == nameof(CombatCard))
                        {
                            cards.Add(new CombatCard(cardDetails[1], (EnumType) Enum.Parse(typeof(EnumType),cardDetails[2]), cardDetails[3], Int32.Parse(cardDetails[4]), bool.Parse(cardDetails[5])));
                        }
                        else
                        {
                            cards.Add(new SpecialCard(cardDetails[1], (EnumType)Enum.Parse(typeof(EnumType), cardDetails[2]), cardDetails[3]));
                        }
                    }
                    else
                    {
                        decks.Add(new Deck());
                        cards = new List<Card>();
                    }
                }

            }
            
        }
        public void AddCaptains()
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + @"\Files\Captains.txt";
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] cardDetails = line.Split(",");
                captains.Add(new SpecialCard(cardDetails[1], (EnumType)Enum.Parse(typeof(EnumType), cardDetails[2]), cardDetails[3]));
            }
        }
        public int GetRoundWinner()
        {
            if (this.CheckIfEndGame())
            {
                return this.GetWinner();
            }
            else
            {
                //-2 en caso de que el juego siga en curso//
                return -2;
            }
        }
        public void play2()
        {

        }
    }
}
