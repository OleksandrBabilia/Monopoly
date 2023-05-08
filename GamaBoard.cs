using System;

namespace Monopoly
{
    public class GameBoard
    {
        private static GameBoard _instance = null;
        public Card[] board = new Card[16];

        public GameBoard()
        {
            CreateGameBoard();
        }

        public static GameBoard GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameBoard();
            }
            return _instance;
        }
        public void CreateGameBoard()
        {
            CardFactory ComunityChestFactory = new CardFactory(TypeCard.CommunityChest);
            CardFactory ChanceFactory = new CardFactory(TypeCard.Chance);

            board[0] = new Card(); // start: collect $200 as you pass
            board[1] = new Card("Stepana Banderu ST.", TypeCard.Street, 60, 0, PropertySituation.Free, null, 1, true);
            board[2] = (Card)ComunityChestFactory.CreateCard(2, false); 
            board[3] = new Card("Lviv Polytechnic", TypeCard.Building, 60, 0, PropertySituation.Free, null, 3, true);
            board[4] = new Card(); // income tax (pay $200)
            board[5] = new Card("Karpinskoho ST.", TypeCard.Street, 200, 0, PropertySituation.Free, null, 5, false);
            board[6] = (Card)ChanceFactory.CreateCard(6, false);
            board[7] = new Card("Railway Station", TypeCard.TrainStation, 100, 0, PropertySituation.Free, null, 7, false);
            board[8] = new Card(); //jail
            board[9] = new Card("LNU Franka", TypeCard.Building, 140, 0, PropertySituation.Free, null, 9, false);
            board[10] = (Card)ComunityChestFactory.CreateCard(10, false);
            board[11] = new Card("Universytetska ST.", TypeCard.Street, 180, 0, PropertySituation.Free, null, 11, false);
            board[12] = new Card(); // free parking
            board[13] = new Card("LNMU Halytsky", TypeCard.Building, 220, 0, PropertySituation.Free, null, 13, false);
            board[14] = (Card)ChanceFactory.CreateCard(14, false);
            board[25] = new Card("Pekarska ST.", TypeCard.Street, 220, 0, PropertySituation.Free, null, 15, false);
        }
    }

}

