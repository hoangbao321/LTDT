using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_DFS_BFS
{
    public struct Edge
    {
        public int v;
        public int w;
        public int weight;
        public Edge(int _v, int _w, int _weight)
        {
            v = _v;
            w = _w;
            weight = _weight;
        }
    }
    class prim
    {
        public int n;
        public int[,] a;
        public bool read_ma_tran()
        {
            var duong_dan = ".\\du_lieu\\prim.txt";
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
        public void Prim(int[,] a, int source)
        {
            bool[] marked = new bool[n];

            for (int i = 0; i < n; i++)
            {
                marked[i] = false;
            }

            marked[source] = true;
            List<Edge> tree = new List<Edge>();

            while (tree.Count < n - 1)
            {
                int nMinWeight = 1000;
                int j = 0;
                int t = 0;

                // tim dinh trong marked 
                for (int v = 0; v < marked.Length; v++)
                {
                    // tim ra roi tim tiep thằng nào nối dc với nó
                    if (marked[v] == true)
                    {
                        for (int w = 0; w < a.GetLength(1); w++)
                        {
                            if (marked[w] == false && a[v, w] != 0 && a[v, w] < nMinWeight)
                            {
                                nMinWeight = a[v, w];
                                t = v;
                                j = w;
                            }
                        }
                    }
                }
                marked[j] = true;
                tree.Add(new Edge(t, j, nMinWeight));
            }

            // in ra cây
            int sum = 0;
            foreach (var edge in tree)
            {
                Console.WriteLine(edge.v + "-" + edge.w + "=" + edge.weight);
                sum += edge.weight;
            }
            Console.WriteLine($"tong trong so prim = {sum}");
        }
        // kruskal
        public void kruscal(int[,] a)
        {
            List<Edge> ds = new List<Edge>();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = i; j < a.GetLength(1); j++)
                {
                    if (a[i, j] != 0)
                        ds.Add(new Edge(i, j, a[i, j]));
                }
            }

            // sắp xếp
            //ds.Sort((a, b) => a.weight.CompareTo(b.weight));
            List<Edge> ds_sap_xep = sap_xep(ds);


            int[] label = new int[n];
            for (int i = 0; i < n; i++)
            {
                label[i] = i;
            }

            // cây tree 
            int bi_thay_doi = 0;
            int sau_khi_thay_doi_bang_cai_nay = 0;

            List<Edge> Tree = new List<Edge>();

            while(Tree.Count < n -1 )
            { 
                for (int i = 0; i < ds_sap_xep.Count; i++)
                {
                    if (label[ds[i].v] != label[ds[i].w])
                    {
                        if (label[ds[i].v] > label[ds[i].w])
                        {
                            // xem giá trị nào là giá trị bị thay đổi

                            // vì thằng v lớn hơn
                            bi_thay_doi = label[ds[i].v];

                            // cập nhật thằng lôi ra bằng thằng nhỏ hơn
                            label[ds[i].v] = label[ds[i].w];

                            // sau khi thay đổi thì cái label khác bằng cái này 
                            sau_khi_thay_doi_bang_cai_nay = label[ds[i].w];

                            Tree.Add(new Edge(ds[i].v, ds[i].w, ds[i].weight));
                        }
                        else
                        {
                            if (label[ds[i].v] < label[ds[i].w])
                            {
                                bi_thay_doi = label[ds[i].w];
                                label[ds[i].w] = label[ds[i].v];
                                sau_khi_thay_doi_bang_cai_nay = label[ds[i].v];
                                Tree.Add(new Edge(ds[i].v, ds[i].w, ds[i].weight));
                            }
                        }
                    }
                    // quét thằng nào có giá trị lớn bị thay đổi
                    // sau đó cho nó bằng cái thằng sau khi thay đoi
                    for (int h = 0; h < n; h++)
                    {
                        if (label[h] == bi_thay_doi)
                        {
                            label[h] = sau_khi_thay_doi_bang_cai_nay;
                        }
                    }
                }
            }
            // in ra cây
            int sum = 0;
            foreach (var edge in Tree)
            {
                Console.WriteLine(edge.v + "-" + edge.w + "=" + edge.weight);
                sum += edge.weight;
            }
            Console.WriteLine($"tong trong so = {sum}");
        }
        // sắp xếp
        public List<Edge> sap_xep(List<Edge> ds)
        {
            for (int i = 0; i < ds.Count; i++)
            {
                int min = ds[i].weight;
                // nếu không có index = i sẽ khiến bài toán bị sai VÌ ?
                // nếu tại vị trí i thì i đã có weight nhỏ nhất, nó không nhảy
                // vào cái if dưới
                // thì nó sẽ lấy cái vị trí index trước (ví dụ i=7, index phải = 7 ) vì tại 7 đã nhỏ nhất
                // nhưng nếu ko có index = i thì nó lấy cái index trước của (i =6 index =10 chẳng hạn)
                int index = i;
                for (int j = i; j < ds.Count; j++)
                {
                    if (min > ds[j].weight)
                    {
                        min = ds[j].weight;
                        index = j;
                    }
                }
                Hoanvi(ds, i, index);
            }
            return ds;
        }
        // Hoán vị 2 thằng
        public void Hoanvi(List<Edge> ds, int k, int l)
        {
            Edge temp;
            temp = ds[k];
            ds[k] = ds[l];
            ds[l] = temp;
        }
    }
}

