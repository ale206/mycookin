using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ErrorAndMessage;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlRecipeComplexity : System.Web.UI.UserControl
    {
        public int ComplexityLevel
        {
            get { return MyConvert.ToInt32(hfComplexityLevel.Value, 1); }
            set { hfComplexityLevel.Value = value.ToString(); }
        }
        //public string ComplexityLevelText
        //{
        //    get { return hfComplexityLevelText.Value; }
        //}
        public int ControlSize
        {
            get { return MyConvert.ToInt32(imgComplexity.Width.ToString(),80); }
            set { imgComplexity.Width = value; }
        }
        public int IDLanguage
        {
            get { return MyConvert.ToInt32(hfIDLanguage.Value, 1); }
            set { hfIDLanguage.Value = value.ToString(); }
        }
        public bool LoadControl
        {
            get { return MyConvert.ToBoolean(hfLoadControl.Value, false); }
            set { hfLoadControl.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (LoadControl)
                {
                    imgComplexity.Height = imgComplexity.Width;

                    switch (ComplexityLevel)
                    {
                        case 1:
                            imgComplexity.ImageUrl = "/Images/iconDifficult_1.png";
                            imgComplexity.ToolTip = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0001");
                            break;
                        case 2:
                            imgComplexity.ImageUrl = "/Images/iconDifficult_2.png";
                            imgComplexity.ToolTip = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0002");
                            break;
                        case 3:
                            imgComplexity.ImageUrl = "/Images/iconDifficult_3.png";
                            imgComplexity.ToolTip = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0003");
                            break;
                        default:
                            imgComplexity.ImageUrl = "/Images/iconDifficult_2.png";
                            imgComplexity.ToolTip = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0002");
                            break;
                    }
                    hfComplexityLevelText.Value = imgComplexity.ToolTip;
                }
                
            }
            catch
            {
            }
        }
    }
}