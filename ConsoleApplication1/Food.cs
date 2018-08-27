using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// 食物类
    /// </summary>
    class Food
    {
        //字段
        /// <summary>
        /// 当前食物的x坐标
        /// </summary>
        private int x;
        /// <summary>
        /// 当前食物的y坐标
        /// </summary>
        private int y;

        //属性
        /// <summary>
        /// 当前食物的x坐标
        /// </summary>
        public int X
        {
            get { return x; }
        }
        /// <summary>
        /// 当前食物的y坐标
        /// </summary>
        public int Y
        {
            get { return y; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Food()
        {
            getNewPos();
        }

        //方法
        /// <summary>
        /// 是否显示食物坐标
        /// </summary>
        /// <param name="isDisplay"></param>
        public void display(bool isDisplay)
        {
            try
            {
                Console.SetCursorPosition(x, y);
            }
            catch (ArgumentOutOfRangeException) //倘若坐标超出界限就停止这一次的食物显示或者隐藏
            {
                return;
            }
            if (isDisplay) //显示/隐藏食物
            {
                Console.Write('*');
            }
            else
            {
                Console.Write(' ');
            }
        }

        /// <summary>
        /// 生成一个新的食物坐标
        /// </summary>
        public void getNewPos()
        {
            int newX = new Random().Next(0, Console.WindowWidth - 1); //生成新的x坐标
            while (newX == x) //避免新的x坐标和现在的x坐标重复
            {
                newX = new Random().Next(0, Console.WindowWidth - 1);
            }
            int newY = new Random().Next(0, Console.WindowHeight - 1); //生成新的y坐标
            while (newY == y) //避免新的y坐标和现在的y坐标重复
            {
                newY = new Random().Next(0, Console.WindowHeight - 1);
            }
            //更新坐标
            x = newX;
            y = newY;
        }
    }
}
