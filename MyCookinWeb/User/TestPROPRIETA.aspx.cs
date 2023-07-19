using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyUser user = new MyUser(new Guid("9b844ef1-9690-4753-8929-5ec722aaa636"));
            user.GetUserBasicInfoByID();
            user.GetPropertyCompiledValues();
            Label1.Text="";

            foreach(MyUserPropertyCompiled PropertyCompiled in user.PropertyCompiled)
            {
                Label1.Text += "("+PropertyCompiled.Property.PropertyCategory.PropertyCategoryName+") ";
                Label1.Text += PropertyCompiled.Property.PropertyName + ": ";
                Label1.Text += PropertyCompiled.PropertyValue;
                Label1.Text += "<br/>";
            }
        }
    }
}