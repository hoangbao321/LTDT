using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_DFS_BFS
{
    class bellman
    {
        public int n;

        //aaa
        public int[,] a;
        public bool read_ma_tran()
        {
            //var duong_dan = ".\\du_lieu\\bell.txt";
            var filefolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var duong_dan = filefolder+"\\du_lieu\\bell.txt";
            if (!File.Exists(duong_dan))
            {
                Console.WriteLine("file ko ton tai ");
                return false;
            }
            string[] f = File.ReadAllLines(duong_dan);
            n = int.Parse(f[0]);
            a = new int[n, n];
            int k = 0;
            for (int i = 1; i < f.Length; i++)
            {
                string[] token = f[i].Replace(", ", " ").Trim().Split(" ");
                for (int j = 0; j < token.Length; j++)
                {
                    a[k, j] = int.Parse(token[j]);
                }
                k++;
            }
            return true;
        }
        public void show(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write(a[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void Dijkstra(int[,] a, int start)
        {
            int[] L = new int[n];
            int[] Prev = new int[n];
            bool[] visited = new bool[n];
            int vocuc = 100;

            // khởi tạo cho L, Prev, visited= true là bỏ
            for (int i = 0; i < n; i++)
            {
                L[i] = vocuc;
                Prev[i] = i;
                visited[i] = false;
            }

            // khởi tạo cho thằng start
            // bước 0
            Prev[start] = 0;
            L[start] = 0;
            visited[start] = true;

            // khởi tạo từ dòng 0 
            // lúc này L của tất cả = vocuc
            // nên khi + thì + a[i,j]
            // nT=0;
            for (int j = 0; j < n; j++)
            {
                if (a[start, j] != 0)
                {
                    L[j] = a[start, j];
                    Prev[j] = start;
                }
            }

            int nT = 1;
            int index = 0;
            // dòng này là dòng 1
            while (nT <= n - 1)
            {
                // tìm thằng nhỏ nhất trong hàng
                for (int i = 0; i < n; i++)
                {
                    if (visited[i] == false)
                    {
                        int min = L[i];
                        // chú ý cái này nếu ko có index = i
                        // nếu tại đó vị trí nhỏ nhất 
                        // nó ko nhảy vào bên dưới 
                        // sử dụng thằng index cũ gây sai ngay
                        index = i;
                        // bắt đầu từ dòng 1
                        for (int j = 0; j < n; j++)
                        {
                            if (visited[j] == false
                                && min > L[j])
                            {
                                min = L[j];
                                index = j;
                            }
                        }
                        visited[index] = true;
                        break;
                    }
                }
                for (int j = 0; j < n; j++)
                {
                    // nhớ là false nhưng phải có # 0
                    // nếu ko thì bị sai ngay
                    if (visited[j] == false
                        && a[index, j] != 0)
                    {
                        int value = L[index] + a[index, j];
                        if (value < L[j])
                        {
                            L[j] = value;
                            Prev[j] = index;
                        }
                    }
                }
                nT++;
            }
            //in ra đường đi
            for (int i = n - 1; i >= 0; i--)
            {
                int cur = i;
                if (L[i] != vocuc)
                {
                    Console.Write($"duong di tu {start} toi {cur}" +
                        $" trong so = {L[i]} " + "\tduong di:\t");
                    while (cur != start)
                    {
                        Console.Write(cur + "<-");
                        cur = Prev[cur];
                    }
                    Console.Write(start + "\n");
                }
            }
        }

        public void bell_man(int[,] a, int start)
        {
            // L[k,dinh] 
            int[,] L = new int[n + 1, n];
            int[] prev = new int[n];
            int vocuc = 100;

            // khởi tạo ban đầu
            for (int j = 0; j < L.GetLength(1); j++)
            {
                if (j != start)
                {
                    L[0, j] = vocuc;
                    prev[j] = j;
                }
                else // j ==start
                {
                    L[0, start] = 0;
                    prev[start] = start;
                }
            }

            int k = 1;
            while (k <= n)
            {
                for (int j = 0; j < n; j++)
                {
                    int min = L[k - 1, j];
                    // cái này giống mấy cái trước lỡ tại đó nó min
                    // ko nhảy vào cái if 
                    L[k, j] = L[k - 1, j];
                    for (int i = 0; i < n; i++)
                    {
                        if (a[i, j] != 0 && L[k - 1, i] != vocuc)
                        {
                            int value = a[i, j] + L[k - 1, i];
                            if (min > value)
                            {
                                min = value;
                                L[k, j] = value;
                                // đỉnh cha bị thay đổi
                                prev[j] = i;
                            }
                        }
                    }
                }
                // true : có sự khác nhau
                // làm tiếp
                if (changed(k, L) == true)
                {
                    k++;
                }
                // == false
                else
                {
                    break;
                }
            }

            // có mạch âm
            if (k == n + 1)
            {
                Console.WriteLine("do thi co mach am ");
            }
            // có đường đi
            else if (k < n)
            {
                for (int i = n - 1; i >= 0; i--)
                {
                    int cur = i;
                    if (L[k, i] != vocuc)
                    {
                        Console.Write($"duong di tu {start} toi {i} co " +
                            $"trong so: {L[k, i]}\tduong di: \t");
                        while (cur != start)
                        {
                            Console.Write(cur + "<-");
                            cur = prev[cur];
                        }
                        Console.Write(start + "\n");
                    }
                }
            }
        }
        public bool changed(int k, int[,] L)
        {
            for (int j = 0; j < n; j++)
            {
                if (L[k - 1, j] != L[k, j])
                {
                    // làm tiếp
                    return true;
                }
            }
            return false;
        }
        // nháp để suy ra cái min  
        public void tim_min(int[,] L, int[,] a, int k, int[] Prev)
        {
            int vocuc = 1000;
            for (int j = 0; j < n; j++)
            {
                int min = L[k - 1, j];
                for (int i = 0; i < n; i++)
                {
                    if (a[i, j] != 0 && L[k - 1, i] != vocuc)
                    {
                        int value = a[i, j] + L[k - 1, i];
                        if (min > value)
                        {
                            min = value;
                            L[k, j] = value;
                            Prev[j] = i;
                        }
                        else // min <= value
                        {
                            L[k, j] = L[k - 1, j];
                        }
                    }
                }
            }
        }
    }
}

