using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// 蛇类
    /// </summary>
    class Snake
    {
        /// <summary>
        /// 蛇头方向枚举类型
        /// </summary>
        public enum Direction
        {
            up,
            down,
            left,
            right,
        }

        //字段
        /// <summary>
        /// 蛇身坐标数组
        /// </summary>
        private List<Tuple<int, int>> pos;
        /// <summary>
        /// 方向
        /// </summary>
        private Direction dir;

        //属性
        /// <summary>
        /// 蛇身坐标数组
        /// </summary>
        public List<Tuple<int, int>> Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public Direction Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Snake()
        {
            pos = new List<Tuple<int, int>>(); //初始化蛇身坐标列表
            dir = Direction.right; //初始化蛇头方向

            //初始化蛇身节数和每节的坐标
            pos.Add(new Tuple<int, int>(10, 10));
            pos.Add(new Tuple<int, int>(9, 10));
            pos.Add(new Tuple<int, int>(8, 10));
        }

        //方法
        /// <summary>
        /// 蛇移动
        /// </summary>
        public void move()
        {
            //这里是把末尾的一节去掉之后把头按照移动方向加一个单位做成新头从头部压入蛇身列表
            pos.RemoveAt(pos.Count - 1);
            Tuple<int, int> newHead = new Tuple<int, int>(pos.First().Item1, pos.First().Item2);
            switch (dir)
            {
                case Direction.up:
                    {
                        newHead = new Tuple<int, int>(newHead.Item1, newHead.Item2 - 1);
                        break;
                    }
                case Direction.down:
                    {
                        newHead = new Tuple<int, int>(newHead.Item1, newHead.Item2 + 1);
                        break;
                    }
                case Direction.left:
                    {
                        newHead = new Tuple<int, int>(newHead.Item1 - 1, newHead.Item2);
                        break;
                    }
                case Direction.right:
                    {
                        newHead = new Tuple<int, int>(newHead.Item1 + 1, newHead.Item2);
                        break;
                    }
            }
            pos.Insert(0, newHead);
        }

        /// <summary>
        /// 改变蛇的移动方向
        /// </summary>
        public void changeDir()
        {
            if (Console.KeyAvailable) //如果键盘缓冲区里有输入的话
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.W && dir != Direction.down)
                {
                    dir = Direction.up;
                }
                else if (key == ConsoleKey.S && dir != Direction.up)
                {
                    dir = Direction.down;
                }
                else if (key == ConsoleKey.A && dir != Direction.right)
                {
                    dir = Direction.left;
                }
                else if (key == ConsoleKey.D && dir != Direction.left)
                {
                    dir = Direction.right;
                }
            }
        }

        /// <summary>
        /// 显示蛇身或者隐藏蛇身
        /// </summary>
        /// <param name="isDisplay"></param>
        public void display(bool isDisplay)
        {
            foreach (Tuple<int, int> i in pos)
            {
                try
                {
                    Console.SetCursorPosition(i.Item1, i.Item2);
                }
                catch (ArgumentOutOfRangeException) //有参数超出界限异常的时候就直接跳过本次循环
                {
                    continue;
                }
                if (isDisplay)
                {
                    Console.Write('#'); //否则画蛇身
                }
                else
                {
                    Console.Write(' '); //隐藏蛇身
                }
            }
        }

        /// <summary>
        /// 判定是否死亡
        /// </summary>
        /// <returns></returns>
        public bool isDead()
        {
            Tuple<int, int> head = pos[0];
            if (head.Item1 < 0 || head.Item1 >= Console.WindowWidth || head.Item2 < 0 || head.Item2 >= Console.WindowHeight) //判断是否撞墙
            {
                return true;
            }
            for (int i = 1; i < pos.Count; i++) //判断是否自我相撞
            {
                if (head.Equals(pos[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 蛇吃了食物之后的蛇身增长
        /// </summary>
        public void grow()
        {
            //在蛇身末尾相对于移动方向相反的方向一格添加一个尾巴
            if (pos.Count == 1) //如果蛇身只有一节
            {
                switch (dir)
                {
                    case Direction.up:
                        {
                            pos.Add(new Tuple<int, int>(pos.First().Item1, pos.First().Item2 + 1));
                            break;
                        }
                    case Direction.down:
                        {
                            pos.Add(new Tuple<int, int>(pos.First().Item1, pos.First().Item2 - 1));
                            break;
                        }
                    case Direction.left:
                        {
                            pos.Add(new Tuple<int, int>(pos.First().Item1 + 1, pos.First().Item2));
                            break;
                        }
                    case Direction.right:
                        {
                            pos.Add(new Tuple<int, int>(pos.First().Item1 - 1, pos.First().Item2));
                            break;
                        }
                }
            }
            else if (pos.Count > 1) //否则如果有很多节
            {
                int x = pos.Last().Item1 + pos.Last().Item1 - pos[pos.Count - 2].Item1;
                int y = pos.Last().Item2 + pos.Last().Item2 - pos[pos.Count - 2].Item2;
                pos.Add(new Tuple<int, int>(x, y));
            }
        }

        /// <summary>
        /// 蛇是否吃到食物
        /// </summary>
        /// <param name="foodX"></param>
        /// <param name="foodY"></param>
        /// <returns></returns>
        public bool isGettedFood(int foodX, int foodY)
        {
            if (pos.First().Equals(new Tuple<int, int>(foodX, foodY))) //蛇头和食物坐标重合了之后就算是吃到食物了
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
