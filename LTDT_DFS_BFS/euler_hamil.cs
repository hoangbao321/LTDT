using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_DFS_BFS
{
    class euler_hamil
    {
        public int n;
        public int[,] a;
        public bool read_ma_tran()
        {
            var duong_dan = ".\\du_lieu\\euler.txt";
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
        public void euler()
        {
            int dinh_bac_le = 0;
            int dinh_bac_chan = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    sum += a[i, j];
                }
                if (sum % 2 != 0)
                {
                    dinh_bac_le++;
                }
                else // sum % 2==0 
                {
                    dinh_bac_chan++;
                }
            }
            if (dinh_bac_chan == a.GetLength(1))
            {
                Console.WriteLine("do thi euler");
            }
            else if (dinh_bac_le == 0 || dinh_bac_le == 2)
            {
                Console.WriteLine("do thi nua euler");
            }
            else
            {
                Console.WriteLine("do thi khong euler ");
            }

        }
        public void Hamilton()
        {
            // xét điều kiện 1 
            bool co_dinh_treo = false;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    sum += a[i, j];
                }
                if (sum == 1)
                {
                    //dieu kien 1: có đỉnh treo thì ko phải hamilton
                    co_dinh_treo = true;
                    break;
                }
            }

            // xét điều kiện 2
            bool co_chu_trinh_nho = false;
            //for (int i = 0; i < a.GetLength(0); i++)
            //{
            //    if (circle(i) == true)
            //    {
            //        co_chu_trinh_nho = true;
            //        break;
            //    }
            //}
            if (circle(1) == true)
            {
                co_chu_trinh_nho = true;
            }
            // xét điều kiện 3 
            bool dk3 = dirac();

            // xét điều kiện 4 
            bool dk4 = ore();

            if (co_dinh_treo == true /*dk1*/ || co_chu_trinh_nho == true)
            {
                Console.WriteLine("do thi khong la hamilton");
            }
            else if (dk3 == true|| dk4==true)
            {
                Console.WriteLine("do thi hamilton");
            }

            else
            {
                Console.WriteLine("khong the xac dinh hamilton");
            }

        }
        // đưa đại 1 đỉnh vào check xem có chu trình nhỏ ko
        // dùng dfs
        public bool circle (int start)
        {
            // tìm dfs 
            List<int> duong_di = new List<int>();
            Stack<int> stack = new Stack<int>();
            bool[] visited = new bool[n];
            stack.Push(start);
            while (stack.Count != 0)
            {
                int k = stack.Pop();
                if (visited[k] == false)
                {
                    visited[k] = true;
                    duong_di.Add(k);
                    for (int j = a.GetLength(1) - 1; j >= 0; j--)
                    {
                        if (a[k, j] != 0 && visited[j] == false)
                        {
                            stack.Push(j);
                        }
                    }
                }
            }
            return check(duong_di, start);
        }
        // đỉnh xét từ vị trí [i,i+2]-> i = n-1 có phải bậc 2 ko 
        // nếu là bậc 2 mà có nối với thằng ở trước nó là sai ngay
        public bool check (List<int> duong_di, int start )
        {
            for (int i = 0; i < duong_di.Count-3   ; i++)
            {
                // là đỉnh bậc 2
                if ( check_bac_2(duong_di[i])==true && a[duong_di[i], duong_di[i]+2] !=0 )
                {
                    return true;
                }
            }
            return false;
        }
        public bool check_bac_2(int index)
        {
            int sum = 0;
            for (int j = 0; j < a.GetLength(1); j++)
            {
                sum += a[index, j];
            }
            if (sum != 2)
            {
                return false;
            }
            return true;
        }
        //dk 3
        public bool dirac ()
        {
            if (n >= 3)
            {
                for (int i = 0; i < n; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        sum += a[i, j];
                    }
                    if (sum < n / 2)
                        return false;
                }
            }
            else if (n < 3)
                return false;
            return true;
        }
        // dk 4
        public bool ore()
        {
            int[,] dinh = new int[n, 1];
            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += a[i, j];
                }
                dinh[i, 0] = sum;
            }
            for (int i = 0; i < n ; i++)
            {
                int deg = 0;
                for (int j = 0; j < n; j++)
                {
                    if(a[i,j] == 0 && i != j )
                    {
                        deg = dinh[i,0] + dinh[j, 0];
                    }
                }
                // vì đỉnh có deg =n -1 thì nối với toàn bộ các đỉnh
                if (deg < n && dinh[i,0] != n-1)
                    return false;
            }
            return true;
        }
    }
}
