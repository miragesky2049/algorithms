using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDemo
{
    class Program
    {
        static void Main1(string[] args)
        {
            Program p = new Program();
            var a = new IComparable[] { 3, 1, 15, 6,45, 13, 51, 9, 45, 0, 85, 67, 39, 68, 42, 26, 457 };
            SortCommon.show(new IComparable[] { 3, 1, 3, 5 });
            //Shell.Sort(new IComparable[] { 3, 1, 15, 6, 13, 51, 9, 45, 0,85 ,67,39,68,42,26,457});
            //Merge.sort(a);
            //MergeBU.sort(a);
            //Quick.sort(a);
            Quick3Way.sort(a);
        }


    }
    //2.1初级排序算法
    //2.1.1游戏规则
    /// <summary>
    /// 排序算法的模板
    /// </summary>
    public class SortCommon
    {
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
    public class Selection : SortCommon
    {
        /// <summary>
        /// 将a[]按升序排列
        /// </summary>
        /// <param name="a"></param>
        public static void sort(IComparable[] a)
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
    public class Insertion : SortCommon
    {
        /// <summary>
        /// 将a[]按升序排列
        /// </summary>
        /// <param name="a"></param>
        public static void sort(IComparable[] a)
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
    /// <summary>
    /// 2.1.6希尔排序
    /// </summary>
    public class Shell : SortCommon
    {
        /// <summary>
        /// 将a[]按升序排列
        /// </summary>
        /// <param name="a"></param>
        public static void sort(IComparable[] a)
        {
            int N = a.Length;
            int h = 1;
            while (h < N / 3) h = 3 * h + 1;//1,,4,13,40,121,364,1093
            while (h >= 1)
            {//将数组变为h有序
                for (int i= h;i< N; i++)
                {//将a[i]插入到a[i-h],a[i-2*h],a[i-3*h]...之中
                    for (int j= i; j >= h && less(a[j], a[j - h]); j -= h)
                        exch(a, j, j - h);
                }
                h = h / 3;
            }
        }
    }

    //2.2 归并排序
    /// <summary>
    /// 2.2.2 自顶向下的归并排序
    /// </summary>
    public class Merge:SortCommon
    {
        private static IComparable[] aux;//归并所需的辅助数组
        public static void sort(IComparable[] a)
        {
            aux = new IComparable[a.Length];//一次性分配空间
            sort(a, 0, a.Length - 1);
        }
        /// <summary>
        /// 将数组a[lo..hi]排序
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        private static void sort(IComparable[] a,int lo,int hi)
        {
            if (hi <= lo) return;
            int mid = lo + (hi - lo) / 2;
            sort(a, lo, mid);//将左半边排序
            sort(a, mid + 1, hi);//将右半边排序
            merge(a, lo, mid, hi);//归并结果
        }

        /// <summary>
        /// 将a[lo..mid]和a[mind+1..hi]归并
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        public static void merge(IComparable[] a,int lo,int mid,int hi)
        {
            int i = lo, j = mid + 1;
            for(int k= lo; k <= hi; k++)//将a[lo..hi]复制到aux[lo..hi]
            {
                aux[k] = a[k];
            }
            for(int k= lo;k<= hi; k++)//归并回到a[lo..hi]
            {
                if (i > mid)
                {
                    a[k] = aux[j++];
                }
                else if (j > hi)
                {
                    a[k] = aux[i++];
                }
                else if (less(aux[j], aux[i]))
                {
                    a[k] = aux[j++];
                }
                else
                {
                    a[k] = aux[i++];
                }
            }

        }
    }
    /// <summary>
    /// 2.2.3 自底向上的归并排序
    /// </summary>
    public class MergeBU:SortCommon
    {
        private static IComparable[] aux;//归并所需的辅助数组

        public static void sort(IComparable[] a)
        {//进行lgN次两两归并
            int N = a.Length;
            aux = new IComparable[N];
            for(int sz = 1;sz < N; sz = sz + sz)
            {
                for(int lo = 0;lo < N-sz; lo += sz + sz)
                {
                    merge(a, lo, lo + sz - 1, Math.Min(lo + sz + sz - 1, N - 1));
                }
            }
        }
        /// <summary>
        /// 将a[lo..mid]和a[mind+1..hi]归并
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        public static void merge(IComparable[] a, int lo, int mid, int hi)
        {
            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)//将a[lo..hi]复制到aux[lo..hi]
            {
                aux[k] = a[k];
            }
            for (int k = lo; k <= hi; k++)//归并回到a[lo..hi]
            {
                if (i > mid)
                {
                    a[k] = aux[j++];
                }
                else if (j > hi)
                {
                    a[k] = aux[i++];
                }
                else if (less(aux[j], aux[i]))
                {
                    a[k] = aux[j++];
                }
                else
                {
                    a[k] = aux[i++];
                }
            }

        }
    }

    //2.3 快速排序
    /// <summary>
    /// 2.3.1 快速排序
    /// </summary>
    public class Quick : SortCommon
    {
        public static void sort(IComparable[] a)
        {
            sort(a, 0, a.Length - 1);
        }
        private static void sort(IComparable[] a,int lo,int hi)
        {
            if (hi <= lo) return;
            int j = partition(a, lo, hi);
            sort(a, lo, j - 1);
            sort(a, j + 1, hi);
        }

        private static int partition(IComparable[] a,int lo,int hi)
        {//将数组切分为a[lo..i-1],a[i],a[i+1..hi]
            int i = lo, j = hi + 1;//左右扫描指针
            IComparable v = a[lo];//切分元素
            while (true)
            {//扫描左右，检查扫描是否结束并交换元素
                while (less(a[++i], v)) if (i == hi) break;
                while (less(v, a[--j])) if (j == lo) break;
                if (i >= j) break;
                exch(a, i, j);
            }
            exch(a, lo, j);//将v=a[j]放入正确的位置
            return j;//a[lo..j-1]<=a[j]<=a[j+1..hi]达成
        }
    }
    /// <summary>
    /// 2.3.3 三向切分的快速排序
    /// </summary>
    public class Quick3Way : SortCommon
    {
        public static void sort(IComparable[] a)
        {
            sort(a, 0, a.Length - 1);
        }
        private static void sort(IComparable[] a,int lo,int hi)
        {
            if (hi <= lo) return;
            int lt = lo, i = lo + 1, gt = hi;
            IComparable v = a[lo];
            while (i <= gt)
            {
                int cmp = a[i].CompareTo(v);
                if (cmp < 0) exch(a, lt++, i++);
                else if (cmp > 0) exch(a, i, gt--);
                else i++;
            }//现在a[lo..lt-1]<v=a[lt..gt]<a[gt+1..hi]成立
            sort(a, lo, lt - 1);
            sort(a, gt + 1, hi);
        }
    }

    //2.4 优先队列
    //public class Key : IComparable
    //{
    //    public int CompareTo(object obj)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    // <summary>
    // 算法2.6 基于堆的优先队列
    // </summary>
    /// <typeparam name="Key"></typeparam>
    public class MaxPQ<Key> where Key:IComparable
    {
        private Key[] pq;//基于堆的完全按二叉树
        private int N = 0;//存储于pq[1..N]中，pq[0]没有使用
        public MaxPQ(int maxN)
        {
            pq = new Key[maxN + 1];
        }

        public bool isEmpty()
        {
            return N == 0;
        }

        public int size()
        {
            return N;
        }

        public void insert(Key v)
        {
            pq[++N] = v;
            swim(N);
        }

        public Key delMax()
        {
            Key max = pq[1];
            exch(1, N--);
            pq[N + 1] = default(Key);
            sink(1);
            return max;
        }
        private bool less(int i,int j)
        {
            return pq[i].CompareTo(pq[j]) < 0;
        }

        private void exch(int i, int j)
        {
            Key t = pq[i]; pq[i] = pq[j];pq[j] = t;
        }

        public void swim(int k)
        {
            while(k>1 && less(k / 2, k))
            {
                exch(k / 2, k);
                k = k / 2;
            }
        }

        public void sink(int k)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && less(j, j + 1)) j++;
                if (!less(k, j)) break;
                exch(k, j);
                k = j;
            }
        }
    }
}
