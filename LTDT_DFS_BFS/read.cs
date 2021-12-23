using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace LTDT_DFS_BFS
{
    class read
    {
        public int n;
        public int[,] a;
        public bool read_ma_tran()
        {
            //var duong_dan = ".\\du_lieu\\dfs.txt";
            var link_file = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var duong_dan = link_file+ "\\du_lieu\\dfs.txt";
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
                //string[] token = f[i].Trim().Split(" ");
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
        public void DFS(int start, int[,] a, int end)
        {
            var stack = new Stack<int>();
            bool[] visited = new bool[n];
            int[] parent = new int[n];

            for (int i = 0; i < n; i++)
            {
                visited[i] = false;
                parent[i] = -1;
            }

            int dinh = start;
            stack.Push(dinh);

            while (dinh != end)
            {
                dinh = stack.Pop();
                if (visited[dinh] == false)
                {
                    for (int j = a.GetLength(1) - 1; j >= 0; j--)
                    {
                        if (a[dinh, j] != 0 && visited[j] == false)
                        {
                            stack.Push(j);
                            parent[j] = dinh;
                        }
                    }
                    Console.Write(dinh + "->");
                    visited[dinh] = true;
                }
            }

            Console.WriteLine("\nduong di ");
            int cur = end;
            while (cur != start)
            {
                Console.Write(cur + " ");
                cur = parent[cur];
            }
            Console.WriteLine(cur);
        }
        public void BFS(int start, int[,] a, int end)
        {
            bool[] visited = new bool[n];
            var que = new Queue<int>();
            int[] parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                visited[i] = false;
                parent[i] = -1;
            }

            int dinh = start;
            que.Enqueue(dinh);

            while (dinh != end)
            {
                dinh = que.Dequeue();
                if (visited[dinh] == false)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (a[dinh, j] != 0 && visited[j] == false)
                        {
                            que.Enqueue(j);
                            parent[j] = dinh;
                        }
                    }
                    Console.Write(dinh + "->");
                    visited[dinh] = true;
                }
            }


            Console.WriteLine("duong di");
            int cur = end;
            while (cur != start)
            {
                Console.Write(cur + " ");
                cur = parent[cur];
            }
            Console.WriteLine(cur);
        }
        public void DFS_duyet_het(int start)
        {
            Stack<int> stack = new Stack<int>();
            bool[] visited = new bool[n];
            for (int i = 0; i < n; i++)
            {
                visited[i] = false;
            }

            int v = start;
            stack.Push(v);

            while (stack.Count != 0)
            {
                v = stack.Pop();
                if (visited[v] == false)
                {
                    for (int j = a.GetLength(1) - 1; j >= 0; j--)
                    {
                        if (a[v, j] != 0 && visited[j] == false)
                        {
                            stack.Push(j);
                        }
                    }
                    Console.Write(v + "->");
                    visited[v] = true;
                }
            }
        }
        // tìm tp liên thông nhưng ko dùng đệ quy
        public void check()
        {
            int[] label = new int[n];
            int count = 0;
            for (int i = 0; i < label.Length; i++)
            {
                label[i] = -1;
            }

            for (int i = 0; i < n; i++)
            {
                if (label[i] == -1)
                {
                    count++;
                    label = thanh_phan_lien_thong(count, i, label);
                }
            }

            // in ra tp lien thông
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"thanh phan lien thong thu:{i}");
                for (int j = 0; j < label.Length; j++)
                {
                    if (label[j] == i)
                    {
                        Console.Write(j + " ");
                    }
                }
                Console.WriteLine();
            }
        }
        // dùng DFS, thì nhớ đỉnh nhỏ lấy ra nên phải đi ngược ở vòng for
        public int[] thanh_phan_lien_thong(int count, int start, int[] label)
        {
            var stack = new Stack<int>();
            bool[] visited = new bool[n];

            int dinh = start;
            stack.Push(dinh);

            while (stack.Count != 0)
            {
                dinh = stack.Pop();
                if (visited[dinh] == false)
                {
                    for (int j = a.GetLength(1) - 1; j >= 0; j--)
                    {
                        if (a[dinh, j] != 0 && visited[j] == false)
                        {
                            stack.Push(j);
                        }
                    }
                    visited[dinh] = true;
                    label[dinh] = count;
                }
            }
            return label;
        }

        // tìm thành phần liên thông dùng đệ quy
        public void BFS_recursive()
        {
            bool[] visited = new bool[n];
            var que = new Queue<int>();
            int[] label = new int[n];

            for (int i = 0; i < n; i++)
            {
                label[i] = -1;
            }

            int count = 0;
            for (int i = 0; i < label.Length; i++)
            {
                if (label[i] == -1)
                {
                    que.Enqueue(i);
                    count++;
                    BFSutil(visited, que, ref label, count);
                }
            }

            // in thành phần liên thông
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"thanh phan lien thong thu:{i}");
                for (int j = 0; j < label.Length; j++)
                {
                    if (label[j] == i)
                    {
                        Console.Write(j + " ");
                    }
                }
                Console.WriteLine();
            }
        }
        public void BFSutil(bool[] visited, Queue<int> que, ref int[] label, int count)
        {
            while ( que.Count != 0 )
            {
                int v = que.Dequeue();
                if (visited[v] == false)
                {
                    visited[v] = true;
                    label[v] = count;

                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (a[v, j] != 0 && visited[j] == false)
                        {
                            que.Enqueue(j);
                        }
                    }
                    // key nằm ở đây
                    BFSutil( visited, que, ref label, count);
                }
            }
        }
        
    }
}
