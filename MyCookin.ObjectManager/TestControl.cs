using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace MyCookin.ObjectManager
{
    public class TestControl
    {
        public static Control[] GetControls()
        {
            Control[] _control = new Control[3];

            Label lblTab4 = new Label();
            lblTab4.Text = "Tab4";
            lblTab4.ID = "lblTab4";
            lblTab4.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            _control[0] = lblTab4;

            TextBox txtTab4 = new TextBox();
            txtTab4.Text = "Tab4";
            txtTab4.ID = "txtTab4";
            txtTab4.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            _control[1] = txtTab4;

            Button btnTab4 = new Button();
            btnTab4.Text = "buttonTab4";
            btnTab4.ID = "btnTab4";
            btnTab4.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            //btnTab4.Click += new EventHandler(button_Click);
            _control[2] = btnTab4;

            return _control;
        }

    }
}
