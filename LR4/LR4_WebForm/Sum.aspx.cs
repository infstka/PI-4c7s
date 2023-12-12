using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LR4_WebForm
{
    public partial class Sum : Page
    {
        private LR4_WebForm.Proxy.Simplex proxyClient;
        private LR4_WebForm.Proxy.A A;

        protected void Page_Load(object sender, EventArgs e)
        {
            proxyClient = new LR4_WebForm.Proxy.Simplex();
        }

        protected void Sum_Click(object sender, EventArgs e)
        {
            try
            {
                var a1 = new LR4_WebForm.Proxy.A(A1_s.Text, int.Parse(A1_k.Text), float.Parse(A1_f.Text));
                var a2 = new LR4_WebForm.Proxy.A(A2_s.Text, int.Parse(A2_k.Text), float.Parse(A2_f.Text));
                var a = proxyClient.Sum(a1, a2);

                result_s.Text = a.s;
                result_k.Text = a.k.ToString();
                result_f.Text = a.f.ToString();
            }
            catch (Exception)
            {
                result_s.Text = "Error!";
                result_k.Text = "Error!";
                result_f.Text = "Error!";
            }
        }
    }
}