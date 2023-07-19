using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.User
{
    public partial class TestProperty001 :  MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<MyUserPropertyCategory> catList= MyUserPropertyCategory.GetAllMyUserPropertyCategory(2);
            int totPropertyActive = 0;

            foreach (MyUserPropertyCategory cat in catList)
            {
                Label1.Text += "<br/> " + cat.PropertyCategoryName;
                List<MyUserProperty> propList = MyUserProperty.GetAllMyUserPropertyByCategory(cat.IDPropertyCategory, 2);
                totPropertyActive += propList.Count;
                foreach (MyUserProperty property in propList)
                {
                    Label1.Text += "<br/> ----> " + property.PropertyName;
                    MyUserPropertyCompiled propComp = new MyUserPropertyCompiled(new Guid("E78330F7-DB37-4A22-8ED1-DBB80AB9E944"), property.IDProperty, 2);
                    Label1.Text += ": " + propComp.PropertyValue;
                }
            }

            Label1.Text += "<br/><br/>";
            Label1.Text += "Propietà attive: " + totPropertyActive.ToString();
            Label1.Text += "<br/>";
            Label1.Text += "Propietà CompilateUtente: " + MyUserPropertyCompiled.GetCountPropertyCompiledByUser(new Guid("E78330F7-DB37-4A22-8ED1-DBB80AB9E944")).Rows.Count.ToString();


            MyUserPropertyCompiled propComp2 = new MyUserPropertyCompiled(new Guid("e78330f7-db37-4a22-8ed1-dbb80ab9e944"), 5, 2);
            Label2.Text = propComp2.PropertyValue;
        }

    }
}