using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlRating : System.Web.UI.UserControl
    {
        #region PublicProperties

        public string IDVote
        {
            get { return hfIDVote.Value; }
            set { hfIDVote.Value = value; }
        }
        public string StartScore
        {
            get { return hfStartScore.Value; }
            set { hfStartScore.Value = value.Replace(",","."); }
        }
        public string NoRateMessage
        {
            get { return hfNoRate.Value; }
            set { hfNoRate.Value = value; }
        }
        public string ImageOffPath
        {
            get { return hfImageOffPath.Value; }
            set { hfImageOffPath.Value = value; }
        }
        public string ImageOnPath
        {
            get { return hfImageOnPath.Value; }
            set { hfImageOnPath.Value = value; }
        }
        public string ImageHalfPath
        {
            get { return hfImageHalfPath.Value; }
            set { hfImageHalfPath.Value = value; }
        }
        public string StarNumber
        {
            get { return hfStarNumber.Value; }
            set { hfStarNumber.Value = value; }
        }
        public string ReadOnly
        {
            get { return hfReadOnly.Value; }
            set { hfReadOnly.Value = value; }
        }
        public string EnableCancel
        {
            get { return hfEnableCancel.Value; }
            set { hfEnableCancel.Value = value; }
        }
        public string CancelToolTip
        {
            get { return hfCancelToolTip.Value; }
            set { hfCancelToolTip.Value = value; }
        }
        public string CancelImageOffPath
        {
            get { return hfCancelImageOffPath.Value; }
            set { hfCancelImageOffPath.Value = value; }
        }
        public string CancelImageOnPath
        {
            get { return hfCancelImageOnPath.Value; }
            set { hfCancelImageOnPath.Value = value; }
        }
        public string Width
        {
            get { return hfWidth.Value; }
            set { hfWidth.Value = value; }
        }
        public string RatedValue
        {
            get { return hfRatedValue.Value; }
        }
        public string RatingWebMethodPath
        {
            get { return hfRatingWebMethodPath.Value; }
            set { hfRatingWebMethodPath.Value = value; }
        }
        public string IDObjectToRate
        {
            get { return hfIDObjectToRate.Value; }
            set { hfIDObjectToRate.Value = value; }
        }
        public string IDUserVoter
        {
            get { return hfIDUserVoter.Value; }
            set { hfIDUserVoter.Value = value; }
        }
        public string RateResult
        {
            get { return hfRateResult.Value; }
        }
        public string MessageOnError
        {
            get { return hfMessageOnError.Value; }
            set { hfMessageOnError.Value = value; }
        }
        //public string ReadOnlyMsg
        //{
        //    get { return hfReadOnlyMsg.Value; }
        //    set { hfReadOnlyMsg.Value = value; }
        //}


        #endregion

        bool _start = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool testBool = Convert.ToBoolean(hfReadOnly.Value);
                testBool = Convert.ToBoolean(hfEnableCancel.Value);
                int testInt = Convert.ToInt32(hfWidth.Value);
                testInt = Convert.ToInt32(hfStarNumber.Value);
                double testDouble = MyConvert.ToDouble(hfStartScore.Value,0);

                if (testDouble > testInt)
                {
                    hfStarNumber.Value = hfStartScore.Value;
                }

                if (String.IsNullOrEmpty(hfRatingWebMethodPath.Value))
                {
                    hfReadOnly.Value = "true";
                }
                if (Convert.ToInt32(hfStarNumber.Value) < 3)
                {
                    hfStarNumber.Value = "3";
                }

                if (Convert.ToInt32(hfWidth.Value) < 150)
                {
                    hfWidth.Value = "150";
                }

                if (String.IsNullOrEmpty(hfIDObjectToRate.Value) || String.IsNullOrEmpty(hfIDUserVoter.Value))
                {
                    hfReadOnly.Value = "true";
                }
                else
                {
                    try
                    {
                        Guid testGuid = new Guid(hfIDObjectToRate.Value);
                        testGuid = new Guid(hfIDUserVoter.Value);
                    }
                    catch
                    {
                        hfReadOnly.Value = "true";
                    }
                }
                if (!String.IsNullOrEmpty(hfIDVote.Value))
                {
                    try
                    {
                        Guid testGuid = new Guid(hfIDVote.Value);
                        hfReadOnly.Value = "true";
                    }
                    catch
                    {
                        //hfReadOnly.Value = "true";
                    }
                }
            }
            catch
            {
                //throw new System.ArgumentException("You missed some configuration parameters.");
                _start = false;
            }
        }

        internal void StartRaty()
        {
            if (_start)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                             "StartRaty();", true);
            }
        }
    }
}