using Common;

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day13 : DayBase, IDay
    {
        private const int day = 13;
        private string[] data;
        public List<Cart> Carts { get; set; }
        private Map2D<MapTile> NewMap { get; set; }


        public Day13(string? testdata = null) : base(Global.Year, day, testdata != null)
        {
            if (testdata != null)
            {
                data = testdata.SplitOnNewlineArray();
                return;
            }
            data = input.GetDataCached().SplitOnNewlineArray();
        }



        public void Run()
        {
            Vector2D result1 = Problem1();
            Console.WriteLine($"P1: Crash at {result1.ToString()}");

            Vector2D result2 = Problem2();
            Console.WriteLine($"P2: Last cart {result2.ToString()}");
        }

        public bool MoveCart(Cart cart)
        {
            if (cart.CartID == 0)
            {
                cart.CartID = 0;
            }

            if (NewMap[cart.Position].Directions.Where(d => d == true).Count() == 4)
            {
                if (cart.AI == 0)
                {
                    cart.Direction--;
                    if (cart.Direction < CartDirections.Up)
                    {
                        cart.Direction = CartDirections.Left;
                    }
                }
                if (cart.AI == 2)
                {
                    cart.Direction++;
                    if (cart.Direction > CartDirections.Left)
                    {
                        cart.Direction = CartDirections.Up;
                    }
                }
                cart.AI = (cart.AI + 1) % 3;
            }
            else
            {
                CartDirections camefrom = (CartDirections)(((int)cart.Direction + 2) % 4);

                for (int i = 0; i < 4; i++)
                {

                    if (i != (int)camefrom && NewMap[cart.Position].Directions[i])
                    {
                        cart.Direction = (CartDirections)i;
                    }
                }
            }

            switch (cart.Direction)
            {
                case CartDirections.Up:
                    cart.Position += Directions.GetDirection(Directions.Up);
                    break;
                case CartDirections.Down:
                    cart.Position += Directions.GetDirection(Directions.Down);
                    break;
                case CartDirections.Right:
                    cart.Position += Directions.GetDirection(Directions.Right);
                    break;
                case CartDirections.Left:
                    cart.Position += Directions.GetDirection(Directions.Left);
                    break;
            }

            if (Carts.Where(c => c.Position.Equals(cart.Position) && !c.Crashed).Count() > 1)
            {
                return true;
            }

            return false;
        }

        public Vector2D Problem1()
        {
            InitMap();
            return RunUntilCrash();
        }
        public Vector2D Problem2()
        {
            InitMap();
            return RunUntilLastCrash();

        }

        public Vector2D RunUntilCrash()
        {
            bool running = true;
            Vector2D crashPosition = new Vector2D();

            while (running)
            {
                List<int> MovedCarts = new List<int>();

                foreach (Cart currentCart in Carts.OrderBy(c => c.Position.Y).ThenBy(c => c.Position.X))
                {
                    if (MoveCart(currentCart))
                    {
                        running = false;
                        crashPosition = new Vector2D { X = currentCart.Position.X, Y = currentCart.Position.Y };
                    }
                    MovedCarts.Add(currentCart.CartID);
                }
            }
            return crashPosition;
        }

        public Vector2D RunUntilLastCrash()
        {
            while (Carts.Where(c => !c.Crashed).Count() > 1)
            {
                foreach (Cart currentCart in (Carts.Where(c => !c.Crashed).OrderBy(c => c.Position.Y).ThenBy(c => c.Position.X)))
                {
                    if (currentCart != null)
                    {
                        if (MoveCart(currentCart))
                        {
                            for (int i = Carts.Count - 1; i >= 0; i--)
                            {
                                if (Carts[i].Position.Equals(currentCart.Position) && !Carts[i].Crashed)
                                {

                                    Carts[i].Crashed = true;
                                }

                            }
                            // Console.WriteLine($"Crash at {currentCart.Position.ToString()}, {Carts.Where(c => !c.Crashed).Count()}");
                        }
                    }
                }

            }
            Cart remainingCart = Carts.Where(c => !c.Crashed).Single();

            return remainingCart.Position;
        }

        public void InitMap()
        {
            Carts = new List<Cart>();
            NewMap = new Map2D<MapTile>();
            NewMap.Init(data[0].Length, data.Count());

            int cartCounter = 0;
            for (int y = 0; y < NewMap.SizeY; y++)
            {
                for (int x = 0; x < NewMap.SizeX; x++)
                {
                    MapTile newTile = null;
                    switch (data[y][x])
                    {

                        case '|':
                            newTile = new MapTile();
                            newTile.Directions[0] = true;
                            newTile.Directions[2] = true;
                            break;
                        case '-':
                            newTile = new MapTile();
                            newTile.Directions[1] = true;
                            newTile.Directions[3] = true;
                            break;
                        case '/':
                            newTile = new MapTile();
                            if (y > 0 && NewMap[x, y - 1] != null && NewMap[x, y - 1].Directions[2] == true)
                            {
                                newTile.Directions[0] = true;
                                newTile.Directions[3] = true;
                            }
                            else
                            {
                                newTile.Directions[1] = true;
                                newTile.Directions[2] = true;
                            }
                            break;
                        case '\\':
                            newTile = new MapTile();
                            if (y > 0 && NewMap[x, y - 1] != null && NewMap[x, y - 1].Directions[2] == true)
                            {
                                newTile.Directions[0] = true;
                                newTile.Directions[1] = true;
                            }
                            else
                            {
                                newTile.Directions[2] = true;
                                newTile.Directions[3] = true;
                            }

                            break;
                        case '+':

                            newTile = new MapTile();
                            newTile.Directions[0] = true;
                            newTile.Directions[1] = true;
                            newTile.Directions[2] = true;
                            newTile.Directions[3] = true;
                            break;
                        case '^':
                            newTile = new MapTile();
                            newTile.Directions[0] = true;
                            newTile.Directions[2] = true;
                            Carts.Add(new Cart { Direction = CartDirections.Up, Position = new Vector2D { X = x, Y = y }, CartID = cartCounter++ });
                            break;
                        case 'v':
                            newTile = new MapTile();
                            newTile.Directions[0] = true;
                            newTile.Directions[2] = true;
                            Carts.Add(new Cart { Direction = CartDirections.Down, Position = new Vector2D { X = x, Y = y }, CartID = cartCounter++ });
                            break;
                        case '>':
                            newTile = new MapTile();
                            newTile.Directions[1] = true;
                            newTile.Directions[3] = true;
                            Carts.Add(new Cart { Direction = CartDirections.Right, Position = new Vector2D { X = x, Y = y }, CartID = cartCounter++ });
                            break;
                        case '<':
                            newTile = new MapTile();
                            newTile.Directions[1] = true;
                            newTile.Directions[3] = true;
                            Carts.Add(new Cart { Direction = CartDirections.Left, Position = new Vector2D { X = x, Y = y }, CartID = cartCounter++ });
                            break;
                    }
                    NewMap[x, y] = newTile;
                }
            }
        }

    }

    public class Cart
    {
        public int CartID { get; set; }
        public CartDirections Direction { get; set; }
        public Vector2D Position { get; set; }
        public int AI { get; set; }
        public bool Crashed { get; set; }
    }

    public class MapTile
    {
        public bool[] Directions { get; set; }
        public MapTile()
        {
            Directions = new bool[4];
        }
    }

    public enum CartDirections
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}
