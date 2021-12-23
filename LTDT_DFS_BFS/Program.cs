using System;
using System.Collections.Generic;
using System.Linq;

namespace LTDT_DFS_BFS
{
    class Program
    {
        static void Main(string[] args)
        {
            #region dfs bfs
            //var g = new read();
            //g.read_ma_tran();
            //int[,] a = g.a;
            //g.show(a);
            //Console.WriteLine("nhap dinh bat dau");
            //int start = int.Parse(Console.ReadLine());
            //Console.WriteLine("nhap dinh ket thuc");
            //int end = int.Parse(Console.ReadLine());
            //Console.WriteLine("DFS");
            //// thuật toán DFS
            //g.DFS(start, a, end);
            //g.DFS_duyet_het(2);
            //Console.WriteLine("======================");
            //// thuật toán BFS
            //Console.WriteLine("BFS");
            //g.BFS(start, a, end);
            //Console.WriteLine("=============== lien thong");
            // cách bình thường dùng DFS tìm liên thông
            //g.check();
            //// dùng BFS+ đệ quy tìm thành phần liên thông
            //g.BFS_recursive();
            #endregion

            #region prim
            var g = new prim();
            g.read_ma_tran();
            int[,] a = g.a;
            g.show(a);
            g.Prim(g.a, 0);
            g.kruscal(a);
            #endregion

            #region bell 
            //var g = new bellman();
            //g.read_ma_tran();
            //int[,] a = g.a;
            //g.show(a);
            //////Console.WriteLine("dikjstra ko co canh am");
            //////g.Dijkstra(g.a, 0);
            //Console.WriteLine("bellman do thi co canh am nhung chua xet");
            //g.bell_man(g.a, 2);
            #endregion

            #region euler-hamilton
            //var g = new euler_hamil();
            //g.read_ma_tran();
            //int[,] a = g.a;
            //g.show();
            //g.euler();
            //g.Hamilton();
            #endregion

            #region do_an02
            //var g = new euler_path();
            //g.read_matrix();
            //g.show();
            //int so_dinh_bac_le = g.kiem_tra_duong_di_hay_chu_trinh();
            //if (so_dinh_bac_le > 2)
            //{
            //    Console.WriteLine(" ko ton tai ");
            //}
            //else if (so_dinh_bac_le == 2)
            //{
            //    Console.WriteLine("duong di euler");
            //    g.euler(g.a);
            //}
            //else if (so_dinh_bac_le == 0)
            //{
            //    Console.WriteLine("chu trinh euler");
            //    g.euler(g.a);
            //}
            #endregion


            #region to_mau
            //var g = new to_mau();
            //g.read_ma_tran();
            //g.show();
            ////g.tomau();
            //g.to_mau_2();
            #endregion 
        }
    }
}
