using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_DFS_BFS
{
    class euler_path
    {
        public int n;
        // dùng để tìm thằng đỉnh lẻ đầu tiên
        public int[][] b;
        // xài chính
        public int[][] a;
        public int start;
        public bool read_matrix()
        {
            //var duong_dan = ".\\du_lieu_1\\project02.txt";
            var x = Directory.GetCurrentDirectory();
            var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName; ;
            var duong_dan = Path.Combine(projectFolder, @"du_lieu\project02.txt");
            if (!File.Exists(duong_dan))
            {
                Console.WriteLine("file ko ton tai");
                return false;
            }
            string[] line = File.ReadAllLines(duong_dan);
            n = int.Parse(line[0]);
            start = int.Parse(line[1]);
            b = new int[n][];
            int k = 0;
            for (int i = 2; i < line.Length; i++)
            {
                string[] token = line[i].Replace(", ", " ").Trim().Split(" ");
                b[k] = new int[token.Length];
                for (int j = 0; j < b[k].Length; j++)
                {
                    b[k][j] = int.Parse(token[j]);
                }
                k++;
            }
            return true;
        }
        public void show()
        {
            a = new int[n][];
            for (int i = 0; i < n; i++)
            {
                a[i] = new int[b[i].Length - 1];
                int k = 0;
                for (int j = 1; j < b[i].Length; j++)
                {
                    a[i][k] = b[i][j];
                    Console.Write(a[i][k] + " ");
                    k++;
                }
                Console.WriteLine();
            }
        }
        public int kiem_tra_duong_di_hay_chu_trinh()
        {
            int so_dinh_bac_le = 0;
            for (int i = 0; i < n; i++)
            {
                if (b[i][0] % 2 != 0)
                {
                    so_dinh_bac_le++;
                }
            }
            return so_dinh_bac_le;
        }
        public void euler(int[][]a )
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(start);
            // danh sách đường đi hoặc chu trình
            List<int> ds = new List<int>();

            while (stack.Count != 0 )
            {
                int p = stack.Peek();
                // tất cả = -1 
                bool all = true;
                for (int j = 0; j < a[p].Length; j++)
                {
                    // cạnh này thật ra là đỉnh
                    int canh = a[p][j];
                    if ( canh != -1 )
                    {
                        stack.Push(canh);
                        //cho a[p][j]=-1
                        a[p][j] = -1;
                        // hàm xóa cạnh
                        xoa_canh(canh, p);
                        all = false;
                        break;
                    }
                }
                if (all == true)
                {
                    int pop = stack.Pop();
                    ds.Add(pop);
                }
            }
            Console.WriteLine("start-> end ");
            for(int i = ds.Count-1; i >= 0 ; i--)
            {
                Console.Write(ds[i] +"->");
            }
        }
        // xóa cạnh là cho nó về -1 luôn
        public void xoa_canh(int canh, int peek )
        {
            for (int j = 0; j< a[canh].Length; j++)
            {
                if(a[canh][j]==peek)
                {
                    a[canh][j] = -1;
                    break;
                }
            }
        }
    }
}
