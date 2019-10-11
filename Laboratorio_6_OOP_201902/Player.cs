﻿using Laboratorio_6_OOP_201902.Cards;
using Laboratorio_6_OOP_201902.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_6_OOP_201902
{
    public class Player : IAttackPoints
    {
        //Constantes
        private const int LIFE_POINTS = 2;
        private const int START_ATTACK_POINTS = 0;

        //Static
        private static int idCounter;

        //Atributos
        private int id;
        private int lifePoints;
        private int attackPoints;
        private Deck deck;
        private Hand hand;
        private Board board;
        private SpecialCard captain;

        //Constructor
        public Player()
        {
            LifePoints = LIFE_POINTS;
            AttackPoints = START_ATTACK_POINTS;
            Deck = new Deck();
            Hand = new Hand();
            Id = idCounter++;
        }

        //Propiedades
        public int Id { get => id; set => id = value; }
        public int LifePoints
        {
            get
            {
                return this.lifePoints;
            }
            set
            {
                this.lifePoints = value;
            }
        }
        public int AttackPoints
        {
            get
            {
                return this.attackPoints;
            }
            set
            {
                this.attackPoints = value;
            }
        }
        public Deck Deck
        {
            get
            {
                return this.deck;
            }
            set
            {
                this.deck = value;
            }
        }
        public Hand Hand
        {
            get
            {
                return this.hand;
            }
            set
            {
                this.hand = value;
            }
        }
        public Board Board
        {
            get
            {
                return this.board;
            }
            set
            {
                this.board = value;
            }
        }
        public SpecialCard Captain
        {
            get
            {
                return this.captain;
            }
            set
            {
                this.captain = value;
            }
        }

        //Metodos
        public void DrawCard(int cardId = 0)
        {
            Card tempCard = CreateTempCard(cardId);
            hand.AddCard(tempCard);
            deck.DestroyCard(cardId);
        }
        public void PlayCard(int cardId, EnumType buffRow = EnumType.None)
        {
            
            Card tempCard = CreateTempCard(cardId, false);

            if (tempCard is CombatCard)
            {
                board.AddCard(tempCard, this.Id);
            }
            else
            {
                if (tempCard.Type == EnumType.buff)
                {
                    board.AddCard(tempCard, this.Id, buffRow);
                }
                else
                {
                    board.AddCard(tempCard);
                }
            }
            hand.DestroyCard(cardId);
        }

        public void ChangeCard(int cardId)
        {
            Card tempCard = CreateTempCard(cardId, false);
            hand.DestroyCard(cardId);
            Random random = new Random();
            int deckCardId = random.Next(0, deck.Cards.Count);
            Card tempDeckCard = CreateTempCard(deckCardId);
            hand.AddCard(tempDeckCard);
            deck.DestroyCard(deckCardId);
            deck.AddCard(tempCard);
        }

        public void FirstHand()
        {
            Random random = new Random();
            for (int i = 0; i<10; i++)
            {
                DrawCard(random.Next(0, deck.Cards.Count));
            }
        }

        public void ChooseCaptainCard(SpecialCard captainCard)
        {
            Captain = captainCard;
            board.AddCard(new SpecialCard(Captain.Name, Captain.Type, Captain.Effect), Id);
        }


        public Card CreateTempCard(int cardId, bool useDeck = true)
        {
            Deck cardList = useDeck ? deck : hand;

            if (cardList.Cards[cardId] is CombatCard)
            {
                CombatCard card = cardList.Cards[cardId] as CombatCard;
                return new CombatCard(card.Name, card.Type, card.Effect, card.AttackPoints, card.Hero);
            }
            else
            {
                SpecialCard card = cardList.Cards[cardId] as SpecialCard;
                return new SpecialCard(card.Name, card.Type, card.Effect);
            }
        }

        int[] IAttackPoints.GetAttackPoints(EnumType line)
        {
            CombatCard meleeCard;
            CombatCard rangeCard;
            CombatCard longRangeCard;

            int[] temparrayMelee = new Array[this.board.PlayerCards[Id][EnumType.melee].Count()];
            int[] temparrayRange = new Array[this.board.PlayerCards[Id][EnumType.range].Count()];
            int[] temparrayLongRange = new Array[this.board.PlayerCards[Id][EnumType.longRange].Count()];
            int[] zero = new int[] { 0, 0 };
            if (line = EnumType.melee)
            {
                for (int p = 0; p < this.board.PlayerCards[Id][EnumType.melee].Count; p++)
                {
                    meleeCard = this.board.PlayerCards[Id][EnumType.melee][p] as CombatCard;
                    temparrayMelee[p] = meleeCard.AttackPoints;

                }
                return temparrayMelee;
            }

            if (line = EnumType.melee)
            {
                for (int k = 0; k < this.board.PlayerCards[Id][EnumType.range].Count; k++)
                {
                    rangeCard = this.board.PlayerCards[Id][EnumType.range][k] as CombatCard;
                    temparrayRange[k] = rangeCard.AttackPoints;

                }
                return temparrayRange;
            }
            if (line = EnumType.longRange)
            {
                for (int j = 0; p < this.board.PlayerCards[Id][EnumType.longRange].Count; j++)
                {
                    longRangeCard = this.board.PlayerCards[Id][EnumType.melee][j];
                    temparrayLongRange[j] = longRangeCard.AttackPoints;

                }
                return temparrayLongRange;
            }
            else
            {
                return zero;
            }
            


        } 

    }
}
