using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LR4_WebForm
{
    public partial class Concat : Page
    {
        private LR4_WebForm.Proxy.Simplex proxyClient;

        protected void Page_Load(object sender, EventArgs e)
        {
            proxyClient = new LR4_WebForm.Proxy.Simplex();
        }

        protected void Concat_Click(object sender, EventArgs e)
        {
            string s = first.Text.ToString();
            double d;
            if (Double.TryParse(second.Text.ToString(), out d))
            {
                result.Text = proxyClient.Concat(s, d).ToString();
            }
            else
            {
                result.Text = "Error!";
            }
        }
    }
}