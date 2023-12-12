using System;
using System.Drawing;
using System.Windows.Forms;

namespace LR4_Proxy
{
    public partial class Form1 : Form
    {
        LR4_Proxy.Proxy.Simplex proxyClient;

        public Form1()
        {
            InitializeComponent();
            proxyClient = new LR4_Proxy.Proxy.Simplex();
        }

        private void Sum_1_Button_Click(object sender, EventArgs e)
        {
            Sum_1_TextBox3.ForeColor = Color.Black;
            int x, y;
            if (int.TryParse(Sum_1_TextBox1.Text.ToString(), out x) && int.TryParse(Sum_1_TextBox2.Text.ToString(), out y))
            {
                Sum_1_TextBox3.Text = proxyClient.Add(x, y).ToString();
            }
            else
            {
                Sum_1_TextBox3.ForeColor = Color.Red;
                Sum_1_TextBox3.Text = "Error!";
            }
        }

        private void Sum_2_Button_Click(object sender, EventArgs e)
        {
            Sum_2_TextBox3.ForeColor = Color.Black;
            string s = Sum_2_TextBox1.Text.ToString();
            double d;
            if (double.TryParse(Sum_2_TextBox2.Text.ToString(), out d))
            {
                Sum_2_TextBox3.Text = proxyClient.Concat(s, d).ToString();
            }
            else
            {
                Sum_2_TextBox3.ForeColor = Color.Red;
                Sum_2_TextBox3.Text = "Error!";
            }
        }

        private void Sum_3_Button_Click(object sender, EventArgs e)
        {
            Sum_3_TextBox3_S.ForeColor = Color.Black;
            Sum_3_TextBox3_K.ForeColor = Color.Black;
            Sum_3_TextBox3_F.ForeColor = Color.Black;

            try
            {
                var a1 = new LR4_Proxy.Proxy.A
                {
                    s = Sum_3_TextBox1_S.Text,
                    k = int.Parse(Sum_3_TextBox1_K.Text),
                    f = float.Parse(Sum_3_TextBox1_F.Text)
                };

                var a2 = new LR4_Proxy.Proxy.A
                {
                    s = Sum_3_TextBox2_S.Text,
                    k = int.Parse(Sum_3_TextBox2_K.Text),
                    f = float.Parse(Sum_3_TextBox2_F.Text)
                };

                var a = proxyClient.Sum(a1, a2);

                Sum_3_TextBox3_S.Text = a.s;
                Sum_3_TextBox3_K.Text = a.k.ToString();
                Sum_3_TextBox3_F.Text = a.f.ToString();
            }
            catch (Exception ex)
            {
                Sum_3_TextBox3_S.ForeColor = Color.Red;
                Sum_3_TextBox3_K.ForeColor = Color.Red;
                Sum_3_TextBox3_F.ForeColor = Color.Red;
                Sum_3_TextBox3_S.Text = ex.Message.ToString();
                Sum_3_TextBox3_K.Text = "Error!";
                Sum_3_TextBox3_F.Text = "Error!";
            }
        }
    }
}
