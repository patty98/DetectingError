using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Diagnostics;

namespace DecodingError
{
    public partial class Form1 : Form
    {
        int r = 3;
        int d = 3;
        double p_e = 0;
        int n = 0;
        int g = 13;
        double xi = 0.01;
        double p = 0;
        
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private int GetMessage()
        {      int m = 0;
                m = Convert.ToInt16(textBox1.Text);
            
            return m;
        }

        private void Detecting_Error()
        {
                StreamWriter sw = new StreamWriter(@"D:\\err_decode(7,4)2.txt");
                int m = GetMessage();
                
                string message = Convert.ToString(m, 2);
                int k = message.Length;
                int N_e = 0;
                int N =Convert.ToInt16( 4 / (xi * xi * 9));
                int a = ControlAddition(k, m);
                string g_bin = Convert.ToString(g, 2);
                int len = k + r;
           
            for (double j = 0; j < 1; j+=0.1)
            {
                p= j;
                for (int i = 0; i < N; i++)
                {
               
                 
                    int e_vector = Error(len);
                    int b = a ^ e_vector;
                    string a_bin = Convert.ToString(a, 2);
                    string b_bin = Convert.ToString(b, 2);
                  
                    string result_error = Division(b_bin, g_bin);
                    int result = Convert.ToInt16(result_error, 2);

                    if (result == 0 && e_vector != 0)
                    {
                        N_e++;
                    }

                }
                p_e = (double)N_e / (double)N;
                N_e = 0;
                label1.Text = "Founded probability";
                sw.WriteLine(p_e + "\t" + p);
               
            }
            sw.Close();
           
        }
        



        private int ControlAddition(int k,int m)
        {
            n = m;
           for(int i=0;i<r;i++)
            {
                n = n * 2;

            }
            string n_bin = Convert.ToString(n, 2);
            string g_bin = Convert.ToString(g, 2);
            int len = k + r;
            string control_add = Division(n_bin,g_bin);
            int a = n |Convert.ToUInt16(control_add);

            return a;
        }


        private void Theoretical_prob(int len)
        {
            double combin = 0;
            double result = 0;
            for (int i = d; i < len; i++)
            {
                
                combin = (double)(factorial(n) / (factorial(n - i) * factorial(i)));
                result = combin * Math.Pow(p, i) * Math.Pow((1 - p), len - i);
            }
        }


        static BigInteger factorial(BigInteger x) { return x <= 1 ? 1 : x * factorial(x - 1); }


        private int Error(int len)
        {
            StringBuilder e = new StringBuilder(len);  
            double x = 0;
            for (int i=0;i<len;i++)
            {
                x = rnd.NextDouble();
                if(x<p)
                {
                    e.Append('1');

                }
                else
                {
                    e.Append('0');
                }
                
                
            }
            string e1 =e.ToString();
            int e_vector = Convert.ToInt16(e1, 2);

            return e_vector;
        }




      

        private string Division(string a, string b)
        {
            int y = a.Length;
            if(y < b.Length)
            {
                return a;
            }
            int ind = a.Length - b.Length + 1;
            char[] k=new char[ind];
            char[] x = new char[y];

            int x_res;
            for (int i=0;i < x.Length;i++)
            {
                x[i] = '0';
            }
            for (int i = 0; i < k.Length; i++)
            {
                k[i] = '0';
            }
            int p;
            string first = a;

            for (int i=y;i!=-1;i--)
            {
         
                p = first.Length - r-1;
               
                if(p < 0)
                {
                    break;
                }

                    k[ind - p - 1] = '1';
               
                for (int j=r;j!=-1;j--)
                {
                    int r1;
                    r1 = p + j;

                    if (b[r-j]=='1')
                    {
                       
                        x[x.Length-r1-1] = '1';
                    }
                    else
                    {
                        x[x.Length-r1-1] = '0';
                    }

                }
                
                string x_str = new string(x);
                x_res=Convert.ToInt32(first, 2) ^ Convert.ToInt32(x_str, 2);
                x = Convert.ToString(x_res, 2).ToCharArray();
                first =Convert.ToString(x_res, 2);
                for (int f = 0; f < x.Length; f++)
                {
                    x[f] = '0';
                }

            }

            return first;
        }

       
            

        private void button1_Click(object sender, EventArgs e)
        {
            Detecting_Error();
           
        }


      
    }
}
