using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Threading;

namespace game
{
    class Block
    {
        public int shaftType;
        public int subType;
        public Random rand = new Random();
        public int spinCount=1;

        public int x = 6, y = 0;

        public Block()
        {

            shaftType = rand.Next(1, 5);
            subType = rand.Next(1, 5);
            
        }
        public void swap()//임시임
        {
            int temp = this.subType;
            subType = shaftType;
            shaftType = temp;
        }
        public void spin()//스핀 만들예정
        {
            if(spinCount % 2 == 0)
            {
                if(Program.map[this.y, this.x - 1].type != " " && Program.map[this.y, this.x - 1].type != " ")
                {
                    return;
                }

            }
            else
            {
                if(Program.map[this.y-1,this.x].type != " ")
                {
                    return;
                }
            }

            if (spinCount > 3)
            {
                spinCount = 1;
            }
            else
            {
                spinCount++;
            }
            if (spinCount % 2 == 1) swap();
            Program.DeleteThisBlock(this);
            Program.InputArrayBlock(this);
        }
        public void move(ConsoleKey key)//이동을 제어하는 메소드
        {
            switch (key)
            {
                case ConsoleKey.RightArrow:
                    if(spinCount % 2 == 1)
                    {
                        if (x < 15 && Program.map[y, x + 2].type == " ")
                        {
                            this.x++;
                            Program.InputArrayBlock(this);
                            Program.DeleteArrayBlock(this, key);
                            //Program.PrintMap();
                        }
                    }
                    else
                    {
                        if (x < 15 && Program.map[y, x + 1].type == " ")
                        {
                            this.x++;
                            Program.InputArrayBlock(this);
                            Program.DeleteArrayBlock(this, key);
                            //Program.PrintMap();
                        }
                    }
                    
                    break;
                case ConsoleKey.LeftArrow:
                    if (x > 1 && Program.map[y, x - 1].type == " ")
                    {
                        this.x--;
                        Program.InputArrayBlock(this);
                        Program.DeleteArrayBlock(this,key);
                        //Program.PrintMap();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    downOnePoint();
                    break;
                case ConsoleKey.Spacebar:
                    downBottom();
                    break;
                case ConsoleKey.G:
                    spin();
                    break;
                default:
                    break;
            }
        }
        public void down()
        {
            while (true)
            {
                if (spinCount % 2 == 1)
                {
                    if (this.y > 12 || Program.map[this.y + 1, this.x].type != " " || Program.map[this.y + 1, this.x + 1].type != " ")
                    {
                        break;
                    }
                }
                else
                {
                    if (this.y > 12 || Program.map[this.y + 1, this.x].type != " ")
                    {
                        break;
                    }
                }
                y++;
                Program.DeleteArrayBlock(this);
                Program.InputArrayBlock(this);
                //Program.PrintMap();
                Thread.Sleep(Program.SleepPoint);

            }
            
        }//자동으로 내려감
        public void downOnePoint()
        {
            if (spinCount % 2 == 1)
            {
                if (this.y > 12 || Program.map[this.y + 1, this.x].type != " " || Program.map[this.y + 1, this.x + 1].type != " ")
                {
                    y++;
                    Program.DeleteArrayBlock(this);
                    Program.InputArrayBlock(this);
                }
            }
            else
            {
                if (this.y > 12 || Program.map[this.y + 1, this.x].type != " ")
                {
                    y++;
                    Program.DeleteArrayBlock(this);
                    Program.InputArrayBlock(this);
                }
            }
        }
        public void downBottom()
        {
            while (true)
            {

                if (spinCount % 2 == 1)
                {
                    if (this.y > 12 || Program.map[this.y + 1, this.x].type != " " || Program.map[this.y + 1, this.x + 1].type != " ")
                    {
                        break;
                    }
                }
                else
                {
                    if (this.y > 12 || Program.map[this.y + 1, this.x].type != " ")
                    {
                        break;
                    }
                }
                y++;
                Program.DeleteArrayBlock(this);
                Program.InputArrayBlock(this);
                //Program.PrintMap();
            }
        }//바닥으로 drop해줌
        
    }
    class Program
    {
        public struct Map
        {
            public string type;
            public bool check;
            public bool Delete;
        }
        public const int BlockMax = 500;
        public static int i = 0;
        public const int width = 17;
        public const int height = 15;
        public static Map[,] map = new Map[height, width];
        public static Block[] blocks = new Block[BlockMax];
        public static int score = 140; 
        public static int found = 0;
        public static int SleepPoint = 500; static int pointer = 1;
        public static bool delete = false;
        static void PrintMenu()
        {
            SetCursorPosition(0, 0);
            WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
            SetCursorPosition(0, 1);
            WriteLine("□□■■□□■□□■□■□□□■□□■■□□□□■■□□■□□■□■□□□■□□■■□□"); 
            SetCursorPosition(0, 2);
            WriteLine("□□■□■□■□□■□□■□■□□■□□■□□□■□■□■□□■□□■□■□□■□□■□"); 
            SetCursorPosition(0, 3);
            WriteLine("□□■■□□■□□■□□□■□□□■□□■□□□■■□□■□□■□□□■□□□■□□■□"); 
            SetCursorPosition(0, 4);
            WriteLine("□□■□□□■□□■□□□■□□□■□□■□□□■□□□■□□■□□□■□□□■□□■□"); 
            SetCursorPosition(0, 5);
            WriteLine("□□■□□□□■■□□□□■□□□□■■□□□□■□□□□■■□□□□■□□□□■■□□"); 
            SetCursorPosition(0, 6);
            WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
            SetCursorPosition(0, 8);
            WriteLine("원하는 모드를 선택한 뒤 Enter키 를 눌러주세요");
            SetCursorPosition(10, 10);
            WriteLine("Easy Mode");
            SetCursorPosition(10, 12);
            WriteLine("Normal Mode");
            SetCursorPosition(10, 14);
            WriteLine("Hard Mode");
        }
        static void PrintPointer()
        {
            SetCursorPosition(7, 7+pointer * 2 + 1);
            Write("▷");
        }
        static void PrintPointer(int move)
        {
            SetCursorPosition(7, 7+ pointer * 2 + 1);
            Write("▷");
            SetCursorPosition(7, 7+ pointer * 2 + 1 - move);
            Write("  ");
        }
        static bool InputKey()
        {
            ConsoleKeyInfo c = Console.ReadKey();
            switch (c.Key)
            {
                case ConsoleKey.DownArrow:
                    if (pointer < 3)
                    {
                        pointer++;
                        PrintPointer(2);
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (pointer > 1)
                    {
                        pointer--;
                        PrintPointer(-2);
                    }
                    break;
                case ConsoleKey.Enter:
                    SetCursorPosition(0, 0);
                    return false;
                default:
                    break;
            }
            return true;
        }
        static void MakeMap()
        {
            for (int i = 0; i < height; i++)//세로맵
            {
                for (int j = 0; j < width; j++)//가로맵
                {
                    if (j == 0)
                    {
                        if (i == 0)
                        {
                            map[i, j].type = "┌";
                            map[i, j].check = true;
                            map[i, j].Delete = true;
                        }
                        else if (i == (height - 1))
                        {
                            map[i, j].type = "└"; 
                            map[i, j].check = true;
                            map[i, j].Delete = true;
                        }
                        else
                        {
                            map[i, j].type = "│";
                            map[i, j].check = true;
                            map[i, j].Delete = true;
                        }
                    }
                    else if (j == width-1)
                    {
                            if (i == 0)
                            {
                                map[i, j].type = "┐";
                                map[i, j].check = true;
                                map[i, j].Delete = true;
                        }
                            else if (i == (height-1))
                            {
                                map[i, j].type = "┘";
                                map[i, j].check = true;
                                map[i, j].Delete = true;
                        }
                            else
                            {
                                map[i, j].type = "│";
                                map[i, j].check = true;
                                map[i, j].Delete = true;
                        }
                    }
                    else
                    {
                        if (i == 0 || i == height-1)
                        {
                            map[i, j].type = "─"; 
                            map[i, j].check = true;
                            map[i, j].Delete = true;
                        }
                        else
                        {
                            map[i, j].type = " "; 
                            map[i, j].check = false;
                            map[i, j].Delete = false;
                        }
                    }
                }
            }
        }
        public static void BreakBlock()
        {
            delete = false;
            for (int i = 1; i < width - 3; i++)//세로
            {
                for (int j = 1; j < height - 2; j++)//가로
                {
                    if (map[i, j].type != " ")
                    {
                        BreakBlockPlus(i, j);
                        found = 0;

                    }
                }

            }

            DoubleCheck();
            DeleteCheckBlock();
            TrimArray();

            if (delete)
            {
                BreakBlock();
            }
        }
        public static void BreakBlockPlus(int i, int j)
        {
            if (map[i, j].check)
            {
                return;
            }

            map[i, j].check = true;
            if (map[i, j].type != " " && map[i, j].type == map[i + 1, j].type)
            {
                found++;
                BreakBlockPlus(i + 1, j);
            }
            if (map[i, j].type != " " && map[i, j].type == map[i, j + 1].type)
            {
                found++;
                BreakBlockPlus(i, j + 1);
            }
            if (map[i, j].type != " " && map[i, j].type == map[i - 1, j].type)
            {
                found++;
                BreakBlockPlus(i - 1, j);
            }
            if (map[i, j].type != " " && map[i, j].type == map[i, j - 1].type)
            {
                found++;
                BreakBlockPlus(i, j - 1);
            }
            DeleteBlock(i, j);

        }
        public static void DeleteCheckBlock()
        {
            for (int i = 1; i < width - 2; i++)//세로
            {
                for (int j = 1; j < height - 2; j++)//가로
                {
                    if (map[i, j].Delete)
                    {
                        score++;
                        map[i, j].Delete = false;
                        map[i, j].type = " ";
                        delete = true;
                    }
                }
            }

            TrimArray();
        }
        public static void DoubleCheck()
        {
            for (int i = 1; i < width - 3; i++)//세로
            {
                for (int j = 1; j < height - 2; j++)//가로
                {

                    if (map[i + 1, j].Delete && map[i, j].type == map[i + 1, j].type)
                    {
                        map[i, j].Delete = true;
                    }
                    if (map[i - 1, j].Delete && map[i, j].type == map[i - 1, j].type)
                    {
                        map[i, j].Delete = true;
                    }
                    if (map[i, j + 1].Delete && map[i, j].type == map[i, j + 1].type)
                    {
                        map[i, j].Delete = true;
                    }
                    if (map[i, j - 1].Delete && map[i, j].type == map[i, j - 1].type)
                    {
                        map[i, j].Delete = true;
                    }
                }

            }
        }
        public static void DeleteBlock(int i, int j)
        {
            if (found > 4)
            {
                map[i, j].Delete = true;
            }
        }
        public static void DeleteArrayBlock(Block blocks, ConsoleKey key)//이동후 원래있던 칸을 없애줌
        {

            switch (key) 
            {
                case ConsoleKey.RightArrow:
                    if (blocks.spinCount % 2 == 1)
                    {
                        map[blocks.y, blocks.x - 1].type = " ";
                    }
                    else
                    {
                        map[blocks.y, blocks.x - 1].type = " ";
                        map[blocks.y-1, blocks.x - 1].type = " ";
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (blocks.spinCount % 2 == 1)
                    {
                        map[blocks.y, blocks.x + 2].type = " ";
                    }
                    else
                    {
                        map[blocks.y, blocks.x + 1].type = " ";
                        map[blocks.y - 1, blocks.x + 1].type = " ";
                    }
                    break;
                default:
                    break;
            }


           
        }
        public static void TrimArray()//정리
        {
            
            for (int j = 1; j < width; j++)//세로맵
            {
                for (int i = 2; i < height; i++)//가로맵
                {
                    map[i, j].check = false;
                    map[i, j].Delete = false;
                    while (map[i, j].type == " " && map[i - 1, j].type != " ")
                    {
                            map[i, j].type = map[i - 1, j].type;
                            map[i - 1, j].type = " ";


                    }
                }
                map[1, j].check = false;
                map[1, j].Delete = false;
            }
        }
        public static void DeleteThisBlock(Block blocks)//spin때 현재있던 블록을 없애줌
        {
                if (blocks.spinCount % 2 == 0)
                {
                    map[blocks.y, blocks.x].type = " ";
                    map[blocks.y, blocks.x + 1].type = " ";

                }
                else
                {
                    if (map[blocks.y, blocks.x + 1].type != " ")
                    {
                        blocks.x--;
                        map[blocks.y - 1, blocks.x + 1].type = " ";
                    }

                    map[blocks.y, blocks.x].type = " ";
                    map[blocks.y - 1, blocks.x].type = " ";
                }
            
        }
        public static void DeleteArrayBlock(Block blocks)//down함수에서 위에있던 칸을 지워줌
        {
            if (blocks.y != 1)
            {
                if (blocks.spinCount % 2 == 1)
                {
                    map[blocks.y - 1, blocks.x].type = " ";
                    map[blocks.y - 1, blocks.x + 1].type = " ";
                }
                else
                {
                    map[blocks.y - 2, blocks.x].type = " ";
                }
            }
        }
        public static void InputArrayBlock(Block blocks)//down 함수에서 다음 칸을 채워줌
        {
            if (blocks.spinCount % 2 == 1)
            {
                map[blocks.y, blocks.x].type = blocks.shaftType.ToString();
                map[blocks.y, blocks.x + 1].type = blocks.subType.ToString();
            }
            else
            {
                map[blocks.y, blocks.x].type = blocks.shaftType.ToString();
                map[blocks.y-1, blocks.x].type = blocks.subType.ToString();
            }
        }
        public static void PrintSideMenu(Block block)//다음에 나올 블럭을 print 해줌
        {

            SetCursorPosition(19, 2);
            Write("Next");
            SetCursorPosition(20, 3); 
            switch (block.shaftType.ToString())
            {
                case "1":
                    ForegroundColor = ConsoleColor.Red;
                    Write("*");
                    ForegroundColor = ConsoleColor.White;
                    break;

                case "2":
                    ForegroundColor = ConsoleColor.Blue;
                    Write("*");
                    ForegroundColor = ConsoleColor.White;
                    break;
                case "3":
                    ForegroundColor = ConsoleColor.Yellow;
                    Write("*");
                    ForegroundColor = ConsoleColor.White;
                    break;
                case "4":
                    ForegroundColor = ConsoleColor.Green;
                    Write("*");
                    ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
            switch (block.subType.ToString())
            {
                case "1":
                    ForegroundColor = ConsoleColor.Red;
                    Write("*");
                    ForegroundColor = ConsoleColor.White;
                    break;

                case "2":
                    ForegroundColor = ConsoleColor.Blue;
                    Write("*");
                    ForegroundColor = ConsoleColor.White;
                    break;
                case "3":
                    ForegroundColor = ConsoleColor.Yellow;
                    Write("*");
                    ForegroundColor = ConsoleColor.White;
                    break;
                case "4":
                    ForegroundColor = ConsoleColor.Green;
                    Write("*");
                    ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    break;
            }
            SetCursorPosition(20, 5);
            Write($"score : {score}");
            SetCursorPosition(18, 7);
            Write("이동 : ←,→");
            SetCursorPosition(18, 8);
            Write("드랍 : space"); 
            SetCursorPosition(18, 9);
            Write("스핀 : G");

        }
        public static void PrintMap()
        {
            while (true)
            {
                //Clear();
                SetCursorPosition(0, 0);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        //Write(map[i, j].type);
                        switch (map[i, j].type) 
                        {
                            case "1":
                                ForegroundColor = ConsoleColor.Red;
                                Write("*");
                                ForegroundColor = ConsoleColor.White;
                                break;

                            case "2":
                                ForegroundColor = ConsoleColor.Blue;
                                Write("*");
                                ForegroundColor = ConsoleColor.White;
                                break;
                            case "3":
                                ForegroundColor = ConsoleColor.Yellow;
                                Write("*");
                                ForegroundColor = ConsoleColor.White;
                                break;
                            case "4":
                                ForegroundColor = ConsoleColor.Green;
                                Write("*");
                                ForegroundColor = ConsoleColor.White;
                                break;
                            default:
                                Write(map[i, j].type);
                                break;
                        }

                    }
                    WriteLine();
                }
                PrintSideMenu(blocks[i + 1]);
                Thread.Sleep(20);
            }
            
        }
        public static void PlayGame()
        {
            MakeMap();
            Thread t2 = new Thread(new ThreadStart(PrintMap));
            t2.Start();
            blocks[0] = new Block();
            while (true)
            {
                    blocks[i + 1] = new Block();
                Thread t1 = new Thread(new ThreadStart(blocks[i].down));
                t1.Start();
                while (true)
                {
                    blocks[i].move(ReadKey(true).Key);
                    if (blocks[i].spinCount % 2 == 1)
                    {
                        if (blocks[i].y > 12 ||
                        map[blocks[i].y + 1, blocks[i].x].type != " " ||
                        map[blocks[i].y + 1, blocks[i].x + 1].type != " ")
                        {
                            t1.Abort();
                            break;
                        }
                    }
                    else
                    {
                        if (blocks[i].y > 12 ||
                        map[blocks[i].y + 1, blocks[i].x].type != " " 
                        )
                        {
                            t1.Abort();
                            break;
                        }
                    }
                    
                }

                
                if (blocks[i].y == 1)
                {
                    t1.Abort();
                    t2.Abort();
                    return;
                }

                if (score > 149)
                {
                    t1.Abort();
                    t2.Abort();
                    return;
                }

                TrimArray();
                Thread.Sleep(100);
                BreakBlock();
                i++;
            }
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(100,30);
            CursorVisible = false;
            do
            {
                PrintMenu();
                PrintPointer();
            } while (InputKey());

            SleepPoint = 1000 - (pointer * 200);

            Clear();
            PlayGame();

            Clear();
            SetCursorPosition(5, 2);
            if(score > 1000)
            {
                Write("Clear!");
            }
            else
            {
                Write("Game Over");
            }
            SetCursorPosition(5, 4);
            WriteLine($"획득 점수 : {score}");

        }
    }
}
