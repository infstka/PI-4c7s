using System;
using System.Drawing;
using System.Windows.Forms;

namespace LR4_check_sum_method
{
    public partial class Form1 : Form
    {
        SimplexService.SimplexSoapClient simplexSoapClient;

        public Form1()
        {
            InitializeComponent();
            simplexSoapClient = new SimplexService.SimplexSoapClient();
            Sum_3_Button.Click += Sum_3_Button_Click;
        }

        private void Sum_3_Button_Click(object sender, EventArgs e)
        {
            Sum_3_TextBox3_S.ForeColor = Color.Black;
            Sum_3_TextBox3_K.ForeColor = Color.Black;
            Sum_3_TextBox3_F.ForeColor = Color.Black;

            try
            {
                var a1 = new SimplexService.A
                {
                    s = Sum_3_TextBox1_S.Text,
                    k = int.Parse(Sum_3_TextBox1_K.Text),
                    f = float.Parse(Sum_3_TextBox1_F.Text)
                };

                var a2 = new SimplexService.A
                {
                    s = Sum_3_TextBox2_S.Text,
                    k = int.Parse(Sum_3_TextBox2_K.Text),
                    f = float.Parse(Sum_3_TextBox2_F.Text)
                };


                var a = simplexSoapClient.Sum(a1, a2);

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
