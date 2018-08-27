using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args) //主方法
        {
            //初始化
            Console.CursorVisible = false; //隐藏光标
            Snake snake = new Snake();
            Food food = new Food();

            while (true)
            {
                snake.display(true); //蛇显示
                food.display(true); //食物显示
                Thread.Sleep(200); //暂停一会儿，用作视觉停留（暂时没想到更好的办法）
                snake.display(false); //蛇身消失
                food.display(false); //食物消失
                snake.changeDir(); //判定方向改变
                snake.move(); //移动
                //判定是否吃到食物
                if (snake.isGettedFood(food.X, food.Y))
                {
                    snake.grow();
                    food.getNewPos();
                }
                //判定蛇是否死亡
                if (snake.isDead())
                {
                    Console.Clear();
                    Console.WriteLine("你挂了~");
                    break;
                }
            }
            Console.ReadKey();
        }
    }
}
