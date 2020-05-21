using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyCookin.DAL.Ingredient.ds_IngredientTableAdapters;


namespace MyCookin.ObjectManager.IngredientManager
{
   public class QuantityNotStdType
    {
        #region PrivateFields

        int _IDQuantityNotStdLanguage;
        int _IDQuantityNotStd;
        int _IDLanguage;
        string _QuantityNotStdSingular;
        string _QuantityNotStdPlural;
        double _PercentageFactor;
        bool _EnabledToUser;
        int? _IDQuantityNotStdNullable;

        #endregion

        #region PublicProperties

        public int IDQuantityNotStdLanguage
        {
            get { return _IDQuantityNotStdLanguage; }
        }
        public int IDQuantityNotStd
        {
            get { return _IDQuantityNotStd; }
        }
        public int? IDQuantityNotStdNullable
        {
            get { return _IDQuantityNotStdNullable; }
        }
        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }
        public string QuantityNotStdSingular
        {
            get { return _QuantityNotStdSingular; }
            set { _QuantityNotStdSingular = value; }
        }
        public string QuantityNotStdPlural
        {
            get { return _QuantityNotStdPlural; }
            set { _QuantityNotStdPlural = value; }
        }
        public double PercentageFactor
        {
            get { return _PercentageFactor; }
            set { _PercentageFactor = value; }
        }
        public bool EnabledToUser
        {
            get { return _EnabledToUser; }
            set { _EnabledToUser = value; }
        }

        #endregion

        #region Costructors

        public QuantityNotStdType()
        {
        }

        /// <summary>
        /// Create Istance on QuantityNotStd with Language Info
        /// Remember to use QueryBaseDbInfo and QueryLanguageDbInfo method to retrieve info from database
        /// </summary>
        /// <param name="IDQuantityNotStandard">ID to find in database</param>
        public QuantityNotStdType(int IDQuantityNotStandard)
        {
            _IDQuantityNotStd = IDQuantityNotStandard;
            if (_IDQuantityNotStd == 0)
            {
                _IDQuantityNotStdNullable = null;
            }
            else
            {
                _IDQuantityNotStdNullable = _IDQuantityNotStd;
            }
        }

        /// <summary>
        /// Create Istance on QuantityNotStd without Language Info
        /// Remember to use QueryBaseDbInfo method to retrieve info from database
        /// </summary>
        /// <param name="IDQuantityNotStandard">ID to find in database</param>
        /// <param name="IDLanguage">ID of Language in use</param>
        public QuantityNotStdType(int IDQuantityNotStandard, int IDLanguage)
        {
            _IDQuantityNotStd = IDQuantityNotStandard;
            _IDLanguage = IDLanguage;

            if (_IDQuantityNotStd == 0)
            {
                _IDQuantityNotStdNullable = null;
            }
            else
            {
                _IDQuantityNotStdNullable = _IDQuantityNotStd;
            }
        }

        #endregion

        #region Methods

        public void QueryBaseDbInfo()
        {
            GetQuantityNotStdDAL dalQuantityNotStd = new GetQuantityNotStdDAL();
            DataTable dtQuantityNotStd = dalQuantityNotStd.GetQuantityNotStdByID(_IDQuantityNotStd);

            if (dtQuantityNotStd.Rows.Count > 0)
            {
                _IDQuantityNotStd = dtQuantityNotStd.Rows[0].Field<int>("IDQuantityNotStd");
                _PercentageFactor = dtQuantityNotStd.Rows[0].Field<double>("PercentageFactor");
                _EnabledToUser = dtQuantityNotStd.Rows[0].Field<bool>("EnabledToUser");

            }
        }

        public void QueryLanguageDbInfo()
        {
            GetQuantityNotStdLanguageDAL dalQuantityNotStdLang = new GetQuantityNotStdLanguageDAL();
            DataTable dtQuantityNotStdLang = dalQuantityNotStdLang.GetQuantityNotStdByIDQtaNotStdIDLanguage(_IDLanguage, _IDQuantityNotStd);

            if (dtQuantityNotStdLang.Rows.Count > 0)
            {
                _IDQuantityNotStdLanguage = dtQuantityNotStdLang.Rows[0].Field<int>("IDQuantityNotStdLanguage");
                _IDLanguage = dtQuantityNotStdLang.Rows[0].Field<int>("IDLanguage");
                _QuantityNotStdSingular = dtQuantityNotStdLang.Rows[0].Field<string>("QuantityNotStdSingular");
                _QuantityNotStdPlural = dtQuantityNotStdLang.Rows[0].Field<string>("QuantityNotStdPlural");
            }
        }

        public static DataTable GetQuantityNotStdList(int IDLanguage)
        {
            GetQuantityNotStdLanguageDAL dalQuantityNotStdLang = new GetQuantityNotStdLanguageDAL();
            return dalQuantityNotStdLang.GetQuantityNotStdByIDLang(IDLanguage);
        }

        public static List<QuantityNotStdType> GetAllowedQtaNotStdLangList(int IDIngredientQuantityType, int IDLanguage)
        {
            GetQuantityNotStdLanguageDAL dalAllowedQtaNotStdLangList = new GetQuantityNotStdLanguageDAL();

            DataTable dtAllowedQtaNotStdLangList = dalAllowedQtaNotStdLangList.GetQtaNotStdByIDQtaTypeIDLang(IDIngredientQuantityType, IDLanguage);

            List<QuantityNotStdType> IngredientAllowedQtaNotStdLangList = new List<QuantityNotStdType>();

            if (dtAllowedQtaNotStdLangList.Rows.Count > 0)
            {
                for (int i = 0; i < dtAllowedQtaNotStdLangList.Rows.Count; i++)
                {
                    IngredientAllowedQtaNotStdLangList.Add(new QuantityNotStdType()
                    {

                        _IDQuantityNotStd = dtAllowedQtaNotStdLangList.Rows[i].Field<int>("IDQuantityNotStd"),
                        _QuantityNotStdSingular = dtAllowedQtaNotStdLangList.Rows[i].Field<string>("QuantityNotStdSingular"),
                    });
                }
            }

            return IngredientAllowedQtaNotStdLangList;
        }

        #endregion

        #region Operators

        public static implicit operator QuantityNotStdType(int value)
        {
            QuantityNotStdType qtaNotStd = new QuantityNotStdType(value);
            return qtaNotStd;
        }

        public static implicit operator int(QuantityNotStdType qtaNotStd)
        {
            if (qtaNotStd == null)
            {
                return 0;
            }
            else
            {
                return qtaNotStd.IDQuantityNotStd;
            }
        }
        #endregion
    }
}
