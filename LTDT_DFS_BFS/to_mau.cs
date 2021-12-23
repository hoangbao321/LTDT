using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_DFS_BFS
{
    class to_mau
    {
        public int n;
        public int[,] a;
        public bool read_ma_tran()
        {
            var duong_dan = ".\\du_lieu\\to_mau.txt";
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
        public void show()
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
        public void tomau()
        {
            int[] dinh = new int[n];
            for (int i = 0; i < n; i++)
            {
                dinh[i] = -1;
            }

            int label = 1;
            for (int i = 0; i < n; i++)
            {
                if (dinh[i] == -1)
                {
                    dinh[i] = label;
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j && a[i, j] == 0 && a[j, i] == 0 && dinh[j] == -1)
                        {
                            dinh[j] = label;
                            if (check_dinh_sap_danh(dinh, j, label) == false)
                            {
                                dinh[j] = -1;
                            }
                        }
                    }
                    label++;
                }
            }

            int count = 1;
            Console.WriteLine("dinh(mau)");
            while (count <= label)
            {
                for (int i = 0; i < n; i++)
                {
                    if (dinh[i] == count)
                    {
                        Console.Write($"{i}({count})  ");
                    }
                }
                count++;
            }
        }
        public bool check_dinh_sap_danh(int[] dinh, int j, int label)
        {
            for (int col = 0; col < n; col++)
            {
                if (a[j, col] == 1 && dinh[col] == label)
                {
                    return false;
                }
            }
            return true;
        }
        class node
        {
            public int color;
            public List<int> Edge= new();
        }
        public void to_mau_2()
        {
            var nodes = new node[n];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new node();
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] != 0)
                    {
                        nodes[i].color = 1;
                        nodes[i].Edge.Add(j);
                    }
                }
            }

            bool[] visited = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (visited[i] == false)
                {
                    visited[i] = true;

                    //foreach (var j in nodes[i].Edge)
                    //{
                    //    // top - adjancy
                    //    if (nodes[i].color == nodes[j].color && visited[j] == false)
                    //    {
                    //        nodes[j].color++;
                    //    }
                    //    else if (nodes[i].color == nodes[j].color && visited[j] == true)
                    //    {
                    //        nodes[i].color++;
                    //    }
                    //}
                    for (int j = 0; j < nodes[i].Edge.Count; j++)
                    {
                        if (nodes[i].color == nodes[nodes[i].Edge[j] ].color && visited[nodes[i].Edge[j]] == false)
                        {
                            nodes[nodes[i].Edge[j]].color++;
                        }
                        else if (nodes[i].color == nodes[nodes[i].Edge[j]].color && visited[nodes[i].Edge[j]] == true)
                        {
                            nodes[i].color++;
                        }
                    }
                }
            }

            bool[] xet_dinh = new bool[n];
            Console.WriteLine("dinh(mau)");
            for (int i = 0; i < n; i++)
            {
                int same_color = nodes[i].color;
                int j = 0;
                while (j < n)
                {
                    if (same_color == nodes[j].color && xet_dinh[j] == false)
                    {
                        Console.Write($"{j + 1}({nodes[j].color})  ");
                        xet_dinh[j] = true;
                    }
                    j++;
                }
            }
        }
    }
}
