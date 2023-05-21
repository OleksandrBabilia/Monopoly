using System;

namespace Monopoly
{
    public class GameBoard
    {
        private static GameBoard _instance = null;
        public Square[] board = new Square[16];

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
            SquareFactory ComunityChestFactory = new SquareFactory(TypeCard.CommunityChest);
            SquareFactory ChanceFactory = new SquareFactory(TypeCard.Chance);

            board[0] = new Square(); 
            board[1] = new Property("Stepana Banderu ST.", TypeProperty.Street, 60, 0, PropertySituation.Free, null, 1);
            board[2] = (Card)ComunityChestFactory.CreateSquare(2); 
            board[3] = new Property("Lviv Polytechnic", TypeProperty.Building, 60, 0, PropertySituation.Free, null, 3);
            board[4] = new Square(); 
            board[5] = new Property("Karpinskoho ST.", TypeProperty.Street, 200, 0, PropertySituation.Free, null, 5);
            board[6] = (Card)ChanceFactory.CreateSquare(6);
            board[7] = new Property("Railway Station", TypeProperty.TrainStation, 100, 0, PropertySituation.Free, null, 7);
            board[8] = new Card(); 
            board[9] = new Property("LNU Franka", TypeProperty.Building, 140, 0, PropertySituation.Free, null, 9);
            board[10] = (Card)ComunityChestFactory.CreateSquare(10);
            board[11] = new Property("Universytetska ST.", TypeProperty.Street, 180, 0, PropertySituation.Free, null, 11);
            board[12] = new Card(); 
            board[13] = new Property("LNMU Halytsky", TypeProperty.Building, 220, 0, PropertySituation.Free, null, 13);
            board[14] = (Card)ChanceFactory.CreateSquare(14);
            board[15] = new Property("Pekarska ST.", TypeProperty.Street, 220, 0, PropertySituation.Free, null, 15);
        }
    }

}

