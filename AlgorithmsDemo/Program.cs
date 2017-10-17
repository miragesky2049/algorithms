using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Example.show(new IComparable[] { 3, 1, 3, 5 });
        }


    }
    //2.1初级排序算法
    //2.1.1游戏规则
    /// <summary>
    /// 排序算法的模板
    /// </summary>
    public class Example
    {
        public static void sort(IComparable[] a)
        {

        }

        public static bool less(IComparable v,IComparable w)
        {
            return v.CompareTo(w) < 0;
        }
        public static void exch(IComparable[] a ,int i,int j)
        {
            IComparable t = a[i];a[i] = a[j];a[j] = t;
        }
        public static void show(IComparable[] a)
        {
            for(int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + " ");
            }
            Console.WriteLine();
        }
        public static bool isSorted(IComparable[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (less(a[i], a[i + 1]))
                {
                    return false;
                }
            }
            return true;
        }
    }
    /// <summary>
    /// 选择排序
    /// </summary>
    public class Selection : Example
    {
        /// <summary>
        /// 将a[]按升序排列
        /// </summary>
        /// <param name="a"></param>
        public static void Sort(IComparable[] a)
        {
            int N = a.Length;//数组长度
            for(int i = 0;i< N; i++)//将a[i]和a[i+1..N]中最小的元素交换
            {
                int min = i;//最小元素的索引
                for(int j=i+1;j< N; j++)
                {
                    if (less(a[j], a[min]))
                        min = j;
                    exch(a, i, min);
                }
            }
        }
    }
    /// <summary>
    /// 2.1.3插入排序
    /// </summary>
    public class Insertion : Example
    {
        /// <summary>
        /// 将a[]按升序排列
        /// </summary>
        /// <param name="a"></param>
        public static void Sort(IComparable[] a)
        {
            int N = a.Length;
            for(int i=1;i< N; i++)
            {
                //将a[i]插入到a[i-1]、a[i-2]、a[i-3]...之中
                for(int j = i; j > 0 && less(a[j], a[j - 1]); j--)
                {
                    exch(a, j, j - 1);
                }
            }
        }
    }

    public class BinarySearch
    {

    }

}
