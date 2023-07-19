using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using MyCookin.DAL.Ingredient.ds_IngredientTableAdapters;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;

namespace MyCookin.ObjectManager.IngredientManager
{
    public class Ingredient
    {
        #region PrivateFields

        protected Guid _IDIngredient;
        protected Recipe _IngredientPreparationRecipe;
        protected Photo _IngredientImage;
        protected double? _AverageWeightOfOnePiece;
        protected double? _Kcal100gr;
        protected double? _grProteins;
        protected double? _grFats;
        protected double? _grCarbohydrates;
        protected double? _grAlcohol;
        protected double? _mgCalcium;
        protected double? _mgSodium;
        protected double? _mgPhosphorus;
        protected double? _mgPotassium;
        protected double? _mgIron;
        protected double? _mgMagnesium;
        protected double? _mcgVitaminA;
        protected double? _mgVitaminB1;
        protected double? _mgVitaminB2;
        protected double? _mcgVitaminB9;
        protected double? _mcgVitaminB12;
        protected double? _mgVitaminC;
        protected double? _grSaturatedFat;
        protected double? _grMonounsaturredFat;
        protected double? _grPolyunsaturredFat;
        protected double? _mgCholesterol;
        protected double? _mgPhytosterols;
        protected double? _mgOmega3;
        protected bool _IsForBaby;
        protected bool _IsVegetarian;
        protected bool _IsVegan;
        protected bool _IsGlutenFree;
        protected bool _IsHotSpicy;
        protected bool _Checked;
        protected MyUser _IngredientCreatedBy;
        protected DateTime? _IngredientCreationDate;
        protected MyUser _IngredientModifiedByUser;
        protected DateTime? _IngredientLastMod;
        protected bool _IngredientEnabled;
        protected IngredientCategory[] _IngredientCategory;
        protected IngredientQuantityType[] _IngredientQuantityType;
        protected bool _January;
        protected bool _February;
        protected bool _March;
        protected bool _April;
        protected bool _May;
        protected bool _June;
        protected bool _July;
        protected bool _August;
        protected bool _September;
        protected bool _October;
        protected bool _November;
        protected bool _December;
        protected IngredientAlternative[] _AlternativeIngrediets;
        protected double? _grDietaryFiber;
        protected double? _grStarch;
        protected double? _grSugar;

        #endregion

        #region PublicProperties

        public Guid IDIngredient
        {
            get { return _IDIngredient; }
        }
        public Recipe IngredientPreparationRecipe
        {
            get { return _IngredientPreparationRecipe; }
            set { _IngredientPreparationRecipe = value; }
        }
        public Photo IngredientImage
        {
            get { return _IngredientImage; }
            set { _IngredientImage = value; }
        }
        public double? AverageWeightOfOnePiece
        {
            get { return _AverageWeightOfOnePiece; }
            set { _AverageWeightOfOnePiece = value; }
        }
        public double? Kcal100gr
        {
            get { return _Kcal100gr; }
            set { _Kcal100gr = value; }
        }
        public double? grProteins
        {
            get { return _grProteins; }
            set { _grProteins = value; }
        }
        public double? grFats
        {
            get { return _grFats; }
            set { _grFats = value; }
        }
        public double? grCarbohydrates
        {
            get { return _grCarbohydrates; }
            set { _grCarbohydrates = value; }
        }
        public double? grAlcohol
        {
            get { return _grAlcohol; }
            set { _grAlcohol = value; }
        }
        public double? mgCalcium
        {
            get { return _mgCalcium; }
            set { _mgCalcium = value; }
        }
        public double? mgSodium
        {
            get { return _mgSodium; }
            set { _mgSodium = value; }
        }
        public double? mgPhosphorus
        {
            get { return _mgPhosphorus; }
            set { _mgPhosphorus = value; }
        }
        public double? mgPotassium
        {
            get { return _mgPotassium; }
            set { _mgPotassium = value; }
        }
        public double? mgIron
        {
            get { return _mgIron; }
            set { _mgIron = value; }
        }
        public double? mgMagnesium
        {
            get { return _mgMagnesium; }
            set { _mgMagnesium = value; }
        }
        public double? mcgVitaminA
        {
            get { return _mcgVitaminA; }
            set { _mcgVitaminA = value; }
        }
        public double? mgVitaminB1
        {
            get { return _mgVitaminB1; }
            set { _mgVitaminB1 = value; }
        }
        public double? mgVitaminB2
        {
            get { return _mgVitaminB2; }
            set { _mgVitaminB2 = value; }
        }
        public double? mcgVitaminB9
        {
            get { return _mcgVitaminB9; }
            set { _mcgVitaminB9 = value; }
        }
        public double? mcgVitaminB12
        {
            get { return _mcgVitaminB12; }
            set { _mcgVitaminB12 = value; }
        }
        public double? mgVitaminC
        {
            get { return _mgVitaminC; }
            set { _mgVitaminC = value; }
        }
        public double? grSaturatedFat
        {
            get { return _grSaturatedFat; }
            set { _grSaturatedFat = value; }
        }
        public double? grMonounsaturredFat
        {
            get { return _grMonounsaturredFat; }
            set { _grMonounsaturredFat = value; }
        }
        public double? grPolyunsaturredFat
        {
            get { return _grPolyunsaturredFat; }
            set { _grPolyunsaturredFat = value; }
        }
        public double? mgCholesterol
        {
            get { return _mgCholesterol; }
            set { _mgCholesterol = value; }
        }
        public double? mgPhytosterols
        {
            get { return _mgPhytosterols; }
            set { _mgPhytosterols = value; }
        }
        public double? mgOmega3
        {
            get { return _mgOmega3; }
            set { _mgOmega3 = value; }
        }
        public bool IsForBaby
        {
            get { return _IsForBaby; }
            set { _IsForBaby = value; }
        }
        public bool IsVegetarian
        {
            get { return _IsVegetarian; }
            set { _IsVegetarian = value; }
        }
        public bool IsVegan
        {
            get { return _IsVegan; }
            set { _IsVegan = value; }
        }
        public bool IsGlutenFree
        {
            get { return _IsGlutenFree; }
            set { _IsGlutenFree = value; }
        }
        public bool IsHotSpicy
        {
            get { return _IsHotSpicy; }
            set { _IsHotSpicy = value; }
        }
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }
        public MyUser IngredientCreatedBy
        {
            get { return _IngredientCreatedBy; }
            set { _IngredientCreatedBy = value; }
        }
        public DateTime? IngredientCreationDate
        {
            get { return _IngredientCreationDate; }
            set { _IngredientCreationDate = value; }
        }
        public MyUser IngredientModifiedByUser
        {
            get { return _IngredientModifiedByUser; }
            set { _IngredientModifiedByUser = value; }
        }
        public DateTime? IngredientLastMod
        {
            get { return _IngredientLastMod; }
            set { _IngredientLastMod = value; }
        }
        public bool IngredientEnabled
        {
            get { return _IngredientEnabled; }
            set { _IngredientEnabled = value; }
        }
        public IngredientCategory[] IngredientCategories
        {
            get { return _IngredientCategory; }
            set { _IngredientCategory = value; }
        }
        public IngredientQuantityType[] IngredientQuantityTypes
        {
            get { return _IngredientQuantityType; }
            set { _IngredientQuantityType = value; }
        }

        public bool January
        {
            get { return _January; }
            set { _January = value; }
        }
        public bool February
        {
            get { return _February; }
            set { _February = value; }
        }
        public bool March
        {
            get { return _March; }
            set { _March = value; }
        }
        public bool April
        {
            get { return _April; }
            set { _April = value; }
        }
        public bool May
        {
            get { return _May; }
            set { _May = value; }
        }
        public bool June
        {
            get { return _June; }
            set { _June = value; }
        }
        public bool July
        {
            get { return _July; }
            set { _July = value; }
        }
        public bool August
        {
            get { return _August; }
            set { _August = value; }
        }
        public bool September
        {
            get { return _September; }
            set { _September = value; }
        }
        public bool October
        {
            get { return _October; }
            set { _October = value; }
        }
        public bool November
        {
            get { return _November; }
            set { _November = value; }
        }
        public bool December
        {
            get { return _December; }
            set { _December = value; }
        }

        public IngredientAlternative[] AlternativeIngrediets
        {
            get { return _AlternativeIngrediets; }
        }

        public double? grDietaryFiber
        {
            get { return _grDietaryFiber; }
            set { _grDietaryFiber = value; }
        }

        public double? grStarch
        {
            get { return _grStarch; }
            set { _grStarch = value; }
        }

        public double? grSugar
        {
            get { return _grSugar; }
            set { _grSugar = value; }
        }

        #endregion

        #region Constructors

        protected Ingredient()
        {

        }
        /// <summary>
        /// DO NOT USE IF POSSIBLE, use costructor with Guid parameter as IDIngredient
        /// </summary>
        /// <param name="IDIngredient"></param>
        public Ingredient(string IDIngredient)
        {
            //Convert IDIngredient into GUID
            Guid IDIngredientGuid = new Guid(IDIngredient);
            _IDIngredient =  IDIngredientGuid;
            //QueryIngredientInfo();
        }

        public Ingredient(Guid IDIngredient)
        {
            _IDIngredient = IDIngredient;
            //QueryIngredientInfo();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Ingredient value from DB and compile privite fields
        /// </summary>
        public void QueryIngredientInfo()
        {
            GetIngredientsDAL IngredientDAL = new GetIngredientsDAL();
            DataTable IngredientInfo = new DataTable();

            IngredientInfo = IngredientDAL.GetIngredientByIDIngredient(_IDIngredient);

            if (IngredientInfo.Rows.Count > 0)
            {
                _IngredientPreparationRecipe = IngredientInfo.Rows[0].Field<Guid?>("IDIngredientPreparationRecipe");
                _IngredientImage = IngredientInfo.Rows[0].Field<Guid?>("IDIngredientImage");
                _AverageWeightOfOnePiece = IngredientInfo.Rows[0].Field<double?>("AverageWeightOfOnePiece");
                _Kcal100gr = IngredientInfo.Rows[0].Field<double?>("Kcal100gr");
                _grProteins = IngredientInfo.Rows[0].Field<double?>("grProteins");
                _grFats = IngredientInfo.Rows[0].Field<double?>("grFats");
                _grCarbohydrates = IngredientInfo.Rows[0].Field<double?>("grCarbohydrates");
                _grAlcohol = IngredientInfo.Rows[0].Field<double?>("grAlcohol");
                _mgCalcium = IngredientInfo.Rows[0].Field<double?>("mgCalcium");
                _mgSodium = IngredientInfo.Rows[0].Field<double?>("mgSodium");
                _mgPhosphorus = IngredientInfo.Rows[0].Field<double?>("mgPhosphorus");
                _mgPotassium = IngredientInfo.Rows[0].Field<double?>("mgPotassium");
                _mgIron = IngredientInfo.Rows[0].Field<double?>("mgIron");
                _mgMagnesium = IngredientInfo.Rows[0].Field<double?>("mgMagnesium");
                _mcgVitaminA = IngredientInfo.Rows[0].Field<double?>("mcgVitaminA");
                _mgVitaminB1 = IngredientInfo.Rows[0].Field<double?>("mgVitaminB1");
                _mgVitaminB2 = IngredientInfo.Rows[0].Field<double?>("mgVitaminB2");
                _mcgVitaminB9 = IngredientInfo.Rows[0].Field<double?>("mcgVitaminB9");
                _mcgVitaminB12 = IngredientInfo.Rows[0].Field<double?>("mcgVitaminB12");
                _mgVitaminC = IngredientInfo.Rows[0].Field<double?>("mgVitaminC");
                _grSaturatedFat = IngredientInfo.Rows[0].Field<double?>("grSaturatedFat");
                _grMonounsaturredFat = IngredientInfo.Rows[0].Field<double?>("grMonounsaturredFat");
                _grPolyunsaturredFat = IngredientInfo.Rows[0].Field<double?>("grPolyunsaturredFat");
                _mgCholesterol = IngredientInfo.Rows[0].Field<double?>("mgCholesterol");
                _mgPhytosterols = IngredientInfo.Rows[0].Field<double?>("mgPhytosterols");
                _mgOmega3 = IngredientInfo.Rows[0].Field<double?>("mgOmega3");
                _IsForBaby = IngredientInfo.Rows[0].Field<bool>("IsForBaby");
                _IsVegetarian = IngredientInfo.Rows[0].Field<bool>("IsVegetarian");
                _IsVegan = IngredientInfo.Rows[0].Field<bool>("IsVegan");
                _IsGlutenFree = IngredientInfo.Rows[0].Field<bool>("IsGlutenFree");
                _IsHotSpicy = IngredientInfo.Rows[0].Field<bool>("IsHotSpicy");
                _Checked = IngredientInfo.Rows[0].Field<bool>("Checked");
                _IngredientCreatedBy = IngredientInfo.Rows[0].Field<Guid?>("IngredientCreatedBy");
                _IngredientCreationDate = IngredientInfo.Rows[0].Field<DateTime?>("IngredientCreationDate");
                _IngredientModifiedByUser = IngredientInfo.Rows[0].Field<Guid?>("IngredientModifiedByUser");
                _IngredientLastMod = IngredientInfo.Rows[0].Field<DateTime?>("IngredientLastMod");
                _IngredientEnabled = IngredientInfo.Rows[0].Field<bool>("IngredientEnabled");
                _January = IngredientInfo.Rows[0].Field<bool>("January");
                _February = IngredientInfo.Rows[0].Field<bool>("February");
                _March = IngredientInfo.Rows[0].Field<bool>("March");
                _April = IngredientInfo.Rows[0].Field<bool>("April");
                _May = IngredientInfo.Rows[0].Field<bool>("May");
                _June = IngredientInfo.Rows[0].Field<bool>("June");
                _July = IngredientInfo.Rows[0].Field<bool>("July");
                _August = IngredientInfo.Rows[0].Field<bool>("August");
                _September = IngredientInfo.Rows[0].Field<bool>("September");
                _October = IngredientInfo.Rows[0].Field<bool>("October");
                _November = IngredientInfo.Rows[0].Field<bool>("November");
                _December = IngredientInfo.Rows[0].Field<bool>("December");
                _grDietaryFiber = IngredientInfo.Rows[0].Field<double?>("grDietaryFiber");
                _grStarch = IngredientInfo.Rows[0].Field<double?>("grStarch");
                _grSugar = IngredientInfo.Rows[0].Field<double?>("grSugar");

            }
            else
            {
                _IDIngredient = new Guid();
            }

        }

        /// <summary>
        /// Get the defaul Ingredient Image ONLY IF the Ingredient Image is null
        /// </summary>
        public void GetDefaultIngredientImage()
        {
            if (_IngredientImage == null)
            {
                _IngredientImage = new DefaultMedia(ObjectType.Ingredient);
            }
        }

        /// <summary>
        /// Get from database all Categories of the Ingredient
        /// </summary>
        public void GetIngredientCategories()
        {
            GetIngredientsIngredientsCategoriesDAL IngredientCategoryInfo = new GetIngredientsIngredientsCategoriesDAL();
            DataTable dtIngredientCategoryInfo = IngredientCategoryInfo.GetAllIngredientCategoriesByIDIngredient(_IDIngredient);
            int x = 0;

            if (dtIngredientCategoryInfo.Rows.Count > 0)
            {
                _IngredientCategory = new IngredientCategory[dtIngredientCategoryInfo.Rows.Count];

                foreach (DataRow IngCategory in dtIngredientCategoryInfo.Rows)
                {
                    IngredientCategory IngCat = new IngredientCategory(IngCategory.Field<int>("IDIngredientCategory"));
                    _IngredientCategory[x] = IngCat;
                    x++;
                }
            }
        }
       
        /// <summary>
        /// Get from database all Quantity Types of the Ingredient
        /// </summary>
        public void GetIngredientQuantityTypes()
        {
            GetIngredientsAllowedQuantityTypesDAL IngredientAllowedQuantityTypeInfo = new GetIngredientsAllowedQuantityTypesDAL();

            DataTable dtIngredientQuantityTypeInfo = IngredientAllowedQuantityTypeInfo.GetIngredientsQuantityTypesByIDIngredient(_IDIngredient);
            int x = 0;

            if (dtIngredientQuantityTypeInfo.Rows.Count > 0)
            {
                _IngredientQuantityType = new IngredientQuantityType[dtIngredientQuantityTypeInfo.Rows.Count];

                foreach (DataRow IngQuantity in dtIngredientQuantityTypeInfo.Rows)
                {
                    IngredientQuantityType IngQta = new IngredientQuantityType(IngQuantity.Field<int>("IDIngredientQuantityType"));
                    _IngredientQuantityType[x] = IngQta;
                    x++;
                }
            }
        }

        /// <summary>
        /// Get the ID of a random Ingredient not yet checked
        /// </summary>
        /// <returns>Returns the string of the random GUID</returns>
        public static string GetRandomIngredientNotChecked()
        {
            GetIngredientsDAL RandomIngredient = new GetIngredientsDAL();
            Guid? IngredientGuid;

            RandomIngredient.USP_GetRandomIngredientNotChecked(out IngredientGuid);
            return IngredientGuid.Value.ToString();
        }

        /// <summary>
        /// Get the ID of a random Ingredient
        /// </summary>
        /// <returns>Returns the string of the random GUID</returns>
        public static string GetRandomIngredient()
        {
            GetIngredientsDAL RandomIngredient = new GetIngredientsDAL();
            Guid? IngredientGuid;
 
            RandomIngredient.USP_GetRandomIngredient(out IngredientGuid);
            return IngredientGuid.Value.ToString();
        }

        public string GetIngredientName(int IDLanguage, bool Plural)
        {
            string IngredientQuantityTypeName = "";
            string DbFieldName = "IngredientSingular";
            if (Plural)
            {
                DbFieldName = "IngredientPlural";
            }

            GetIngredientsLanguagesDAL IngredientLanguageDAL = new GetIngredientsLanguagesDAL();
            DataTable IngredientLanguageInfo = new DataTable();

            IngredientLanguageInfo = IngredientLanguageDAL.USP_GetIngredientLanguageByIDIngredientIDLanguage(_IDIngredient, IDLanguage);

            if (IngredientLanguageInfo.Rows.Count > 0)
            {
                IngredientQuantityTypeName = IngredientLanguageInfo.Rows[0].Field<string>(DbFieldName);
            }
            return IngredientQuantityTypeName;
        }

        public string ToString(int IDLanguage, bool Plural)
        {
            return GetIngredientName(IDLanguage, Plural);
        }

        public ManageUSPReturnValue SaveIngredient()
        {
            ManageIngredientDAL manageIngr = new ManageIngredientDAL();
            DataTable dtmanageIngr;

           dtmanageIngr = manageIngr.USP_InsertOrUpdateIngredient(_IngredientPreparationRecipe,_IngredientImage,
                                     _AverageWeightOfOnePiece, _Kcal100gr, _grProteins, _grFats, _grCarbohydrates,
                                     _grAlcohol, _mgCalcium, _mgSodium, _mgPhosphorus, _mgPotassium, _mgIron,
                                     _mgMagnesium, _mcgVitaminA, _mgVitaminB1, _mgVitaminB2, _mcgVitaminB9,
                                     _mcgVitaminB12, _mgVitaminC, _grSaturatedFat, _grMonounsaturredFat, _grPolyunsaturredFat,
                                     _mgCholesterol, _mgPhytosterols, _mgOmega3, _IsForBaby, _IsVegetarian, 
                                     _IsGlutenFree,_IsVegan, _IsHotSpicy, _Checked,_IngredientCreatedBy,_IngredientCreationDate,
                                     _IngredientModifiedByUser,_IngredientLastMod,_IngredientEnabled,_IDIngredient,_January,
                                    _February,_March,_April,_May,_June,_July,_August,_September,_October,_November,_December,_grDietaryFiber,_grStarch,_grSugar);

           if (_IngredientCategory != null)
           {
               manageIngr.USP_InsertOrUpdateIngredientIngredientCategory(_IDIngredient, _IngredientCategory[0].IDIngredientCategory, true);
           }

           if (_AlternativeIngrediets != null)
           {
               for (int i = 0; i < _AlternativeIngrediets.Length; i++)
               {
                   _AlternativeIngrediets[i].Save();
               }
           }

            ManageUSPReturnValue manageIngrReturn = new ManageUSPReturnValue(dtmanageIngr);

            return manageIngrReturn;
        }

        public void IngredientLogUSP(LogLevel LogDbLevel, LogLevel LogFsLevel, bool SendEmail, ManageUSPReturnValue USPReturn)
        {
            int IDLanguageForLog;

            try
            {
                IDLanguageForLog = Convert.ToInt32(AppConfig.GetValue("IDLanguageForLog", AppDomain.CurrentDomain));
            }
            catch
            {
                IDLanguageForLog = 1;
            }
            try
            {
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogDbLevel.ToString(), "", Network.GetCurrentPageName(), USPReturn.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguageForLog, USPReturn.ResultExecutionCode), USPReturn.USPReturnValue, false, true);
                LogManager.WriteFileLog(LogFsLevel, SendEmail, NewRow);
                LogManager.WriteDBLog(LogDbLevel, NewRow);
            }
            catch
            {
            }
        }

        public void AddOrDeleteAllowedQuantityType(int idIngredientQuantityType, bool isDelete )
        {
            ManageIngredientDAL AddOrDeleteAllowedQuantityTypeDAL = new ManageIngredientDAL();
            AddOrDeleteAllowedQuantityTypeDAL.USP_InsertOrDeleteIngredientAllowedQuantityType(_IDIngredient, idIngredientQuantityType, isDelete);
        }

        public static int GetNumIngredientNotChecked()
        {
            int? NumIngr = 0;
            GetIngredientsDAL ingrDAL = new GetIngredientsDAL();
            NumIngr=ingrDAL.GetNumberOfIngredientNotChecked();
            return (int)NumIngr;
        }

        public static int GetNumIngredientCheckedByUser(Guid? IDUser)
        {
            int NumIngr = 0;
            GetIngredientsDAL ingrDAL = new GetIngredientsDAL();
            NumIngr = (int)ingrDAL.GetNumberOfIngredientCheckedByUser(IDUser);
            return (int)NumIngr;
        }

        public void GetAlternativeIngredients(bool GetOnlyChecked)
        {
            GetIngredientsAlternativesDAL AlternativeIngredientsDAL = new GetIngredientsAlternativesDAL();
            DataTable dtAlternativeIngredients = new DataTable();

            dtAlternativeIngredients = AlternativeIngredientsDAL.GetAlternativeIngredientByIDMainIngr(_IDIngredient, GetOnlyChecked);

            if (dtAlternativeIngredients.Rows.Count > 0)
            {
                _AlternativeIngrediets = new IngredientAlternative[dtAlternativeIngredients.Rows.Count];

                for (int i = 0; i < dtAlternativeIngredients.Rows.Count; i++)
                {
                    _AlternativeIngrediets[i] = new IngredientAlternative(dtAlternativeIngredients.Rows[i].Field<Guid>("IDIngredientAlternative"));
                }
            }
        }

        public void AddAlternativeIngredient(Guid IngredientSlave, Guid AddedByUser)
        {
            IngredientAlternative newAlternativeIngredient = new IngredientAlternative(_IDIngredient, IngredientSlave, AddedByUser, DateTime.UtcNow);
            newAlternativeIngredient.Save();
            GetAlternativeIngredients(true);
        }

        #endregion

        #region Operators

        public static implicit operator Ingredient(Guid guid)
        {
            Ingredient ingr = new Ingredient(guid);
            return ingr;
        }

        public static implicit operator Guid(Ingredient ingr)
        {
            Guid guid = new Guid();
            if (ingr == null)
            {
                return guid;
            }
            else
            {
                return ingr.IDIngredient;
            }
        }

        public static bool operator ==(Ingredient ingr1, Ingredient ingr2)
        {
            if ((Object)ingr1 == null)
            {
                ingr1 = new Ingredient(new Guid());
            }
            if ((Object)ingr2 == null)
            {
                ingr2 = new Ingredient(new Guid());
            }
            if ((Object)ingr1 == null || (Object)ingr2 == null)
            {
                return (Object)ingr1 == (Object)ingr2;
            }
            return ingr1.IDIngredient == ingr2.IDIngredient;
        }

        public static bool operator !=(Ingredient ingr1, Ingredient ingr2)
        {
            return !(ingr1 == ingr2);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Ingredient return false.
            Ingredient ing = obj as Ingredient;
            if ((System.Object)ing == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDIngredient == ing.IDIngredient);
        }

        public bool Equals(Ingredient ing)
        {
            // If parameter is null return false:
            if ((object)ing == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDIngredient == ing.IDIngredient);
        }

        public override int GetHashCode()
        {
            return IDIngredient.GetHashCode();
        } 

        #endregion
    }

   public class IngredientLanguage : Ingredient
    {
        #region PrivateFields

        private Guid _IDIngredientLanguage;
        private int _IDLanguage;
        private string _IngredientSingular;
        private string _IngredientPlural;
        private string _IngredientDescription;
        private bool _isAutoTranslate;
        private int? _GeoIDRegion;
        

        #endregion

        #region PublicProperties

        public Guid IDIngredientLanguage
        {
            get { return _IDIngredientLanguage; }
        }

        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }
        public string IngredientSingular
        {
            get { return _IngredientSingular; }
            set { _IngredientSingular = value; }
        }
        public string IngredientPlural
        {
            get { return _IngredientPlural; }
            set { _IngredientPlural = value; }
        }
        public string IngredientDescription
        {
            get { return _IngredientDescription; }
            set { _IngredientDescription = value; }
        }
        public bool isAutoTranslate
        {
            get { return _isAutoTranslate; }
            set { _isAutoTranslate = value; }
        }
        public int? GeoIDRegion
        {
            get { return _GeoIDRegion; }
            set { _GeoIDRegion = value; }
        }

        #endregion

        #region Constructors

        public IngredientLanguage()
        {
        }

       public IngredientLanguage(string IDIngredient, int IDLanguage)
        {           
            _IDLanguage = IDLanguage;

            Guid IngredientGuid = new Guid(IDIngredient);

            _IDIngredient = IngredientGuid;

           // QueryIngredientLanguageInfo();
            
        }

       public IngredientLanguage(string IngredientName, int IDLanguage, bool GetData)
       {
          _IngredientSingular=IngredientName;
          _IDLanguage = IDLanguage;
          if (GetData)
          {
              GetIDIngredientByIngredientName();
          }
       }

       public IngredientLanguage(string IngredientName)
       {
           _IngredientSingular = IngredientName;
           GetIDIngredientByIngredientName();
       }

       public IngredientLanguage(Guid IDIngredient, int IDLanguage)
       {
           _IDLanguage = IDLanguage;

           _IDIngredient = IDIngredient;

          // QueryIngredientLanguageInfo();
       }

       public IngredientLanguage(Ingredient baseIngredient, int IDLanguage)
       {
           if (baseIngredient != null)
           {
               _IDIngredient = baseIngredient.IDIngredient;
               _IngredientPreparationRecipe = baseIngredient.IngredientPreparationRecipe;
               _IngredientImage = baseIngredient.IngredientImage;
               _IngredientPreparationRecipe = baseIngredient.IngredientPreparationRecipe;
               _IngredientImage = baseIngredient.IngredientImage;
               _AverageWeightOfOnePiece = baseIngredient.AverageWeightOfOnePiece;
               _Kcal100gr = baseIngredient.Kcal100gr;
               _grProteins = baseIngredient.grProteins;
               _grFats = baseIngredient.grFats;
               _grCarbohydrates = baseIngredient.grCarbohydrates;
               _grAlcohol = baseIngredient.grAlcohol;
               _mgCalcium = baseIngredient.mgCalcium;
               _mgSodium = baseIngredient.mgSodium;
               _mgPhosphorus = baseIngredient.mgPhosphorus;
               _mgPotassium = baseIngredient.mgPotassium;
               _mgIron = baseIngredient.mgIron;
               _mgMagnesium = baseIngredient.mgMagnesium;
               _mcgVitaminA = baseIngredient.mcgVitaminA;
               _mgVitaminB1 = baseIngredient.mgVitaminB1;
               _mgVitaminB2 = baseIngredient.mgVitaminB2;
               _mcgVitaminB9 = baseIngredient.mcgVitaminB9;
               _mcgVitaminB12 = baseIngredient.mcgVitaminB12;
               _mgVitaminC = baseIngredient.mgVitaminC;
               _grSaturatedFat = baseIngredient.grSaturatedFat;
               _grMonounsaturredFat = baseIngredient.grMonounsaturredFat;
               _grPolyunsaturredFat = baseIngredient.grPolyunsaturredFat;
               _mgCholesterol = baseIngredient.mgCholesterol;
               _mgPhytosterols = baseIngredient.mgPhytosterols;
               _mgOmega3 = baseIngredient.mgOmega3;
               _IsForBaby = baseIngredient.IsForBaby;
               _IsVegetarian = baseIngredient.IsVegetarian;
               _IsVegan = baseIngredient.IsVegan;
               _IsGlutenFree = baseIngredient.IsGlutenFree;
               _IsHotSpicy = baseIngredient.IsHotSpicy;
               _Checked = baseIngredient.Checked;
               _IngredientModifiedByUser = baseIngredient.IngredientModifiedByUser;
               _IngredientLastMod = baseIngredient.IngredientLastMod;
               _IsVegetarian = baseIngredient.IsVegetarian;
               _IsVegan = baseIngredient.IsVegan;
               _IsHotSpicy = baseIngredient.IsHotSpicy;
               _Checked = baseIngredient.Checked;
               _IngredientModifiedByUser = baseIngredient.IngredientModifiedByUser;
               _IngredientLastMod = baseIngredient.IngredientLastMod;
               _IngredientCategory = baseIngredient.IngredientCategories;
               _IngredientQuantityType = baseIngredient.IngredientQuantityTypes;

               _IDLanguage = IDLanguage;
           }
          // QueryIngredientLanguageInfo();
       }

        #endregion

        #region Methods

        /// <summary>
        /// Get IngredientLanguage value from DB and compile privite fields
        /// </summary>
        public void QueryIngredientLanguageInfo()
        {
            GetIngredientsLanguagesDAL IngredientLanguageDAL = new GetIngredientsLanguagesDAL();
            DataTable IngredientLanguageInfo = new DataTable();

            IngredientLanguageInfo = IngredientLanguageDAL.USP_GetIngredientLanguageByIDIngredientIDLanguage(_IDIngredient, _IDLanguage);

            if (IngredientLanguageInfo.Rows.Count > 0)
            {
                _IDIngredientLanguage = IngredientLanguageInfo.Rows[0].Field<Guid>("IDIngredientLanguage");
                _IngredientSingular = IngredientLanguageInfo.Rows[0].Field<string>("IngredientSingular");
                _IngredientPlural = IngredientLanguageInfo.Rows[0].Field<string>("IngredientPlural");
                _IngredientDescription = IngredientLanguageInfo.Rows[0].Field<string>("IngredientDescription");
                _isAutoTranslate = IngredientLanguageInfo.Rows[0].Field<bool>("isAutoTranslate");
                _GeoIDRegion = IngredientLanguageInfo.Rows[0].Field<int?>("GeoIDRegion");
            }

        }

        public static List<IngredientLanguage> GetIngredientAlternativesLang(Guid IDIngredientMain, int IDLanguage)
        {
            GetIngredientsLanguagesDAL dalIngredientLang = new GetIngredientsLanguagesDAL();
            DataTable dtIngredientLang = dalIngredientLang.GetIngredientAlternativesLangByID(IDIngredientMain, IDLanguage);
            
            List<IngredientLanguage> IngredientAlternativesList = new List<IngredientLanguage>();

            if (dtIngredientLang.Rows.Count > 0)
            {
                foreach(DataRow drIngr in dtIngredientLang.Rows)
                {
                    IngredientAlternativesList.Add(new IngredientLanguage()
                    {
                        _IDIngredient = drIngr.Field<Guid>("IDIngredient"),
                        _IngredientSingular = drIngr.Field<string>("IngredientSingular"),
                    });
                }
            }

            return IngredientAlternativesList;
        }

        public static DataTable GetIngredientSiteMap(int idLanguage)
        {
            GETSiteMapIngredientDAL dalIngredientSiteMap = new GETSiteMapIngredientDAL();

            return dalIngredientSiteMap.USP_SiteMapIngredient(idLanguage);
        }

        public static List<IngredientLanguage> IngredientList(string Ingredient, int IDLanguage)
        {
            GetIngredientsLanguagesDAL dalIngredientLang = new GetIngredientsLanguagesDAL();
            DataTable dtIngredientLang = dalIngredientLang.GetIngredientByName(Ingredient, IDLanguage);

            List<IngredientLanguage> IngredientList = new List<IngredientLanguage>();

            if (dtIngredientLang.Rows.Count > 0)
            {
                for (int i = 0; i < dtIngredientLang.Rows.Count; i++)
                {
                    IngredientList.Add(new IngredientLanguage()
                    {
                        _IDIngredient = dtIngredientLang.Rows[i].Field<Guid>("IDIngredient"),
                        _IngredientSingular = dtIngredientLang.Rows[i].Field<string>("IngredientSingular"),
                    });
                }
            }

            return IngredientList;
        }

        public static List<IngredientLanguage> IngredientList(string StartWith, bool Vegan, bool Vegetarian, 
                                                                    bool GlutenFree, bool HotSpicy, int IDLanguage, int OffSetRow, int FetchRows)
        {
            GetIngredientsLanguagesDAL dalIngredientLang = new GetIngredientsLanguagesDAL();
            DataTable dtIngredientLang = dalIngredientLang.USP_GetIngredientList(StartWith, Vegan, Vegetarian, GlutenFree, HotSpicy, IDLanguage, OffSetRow, FetchRows);

            List<IngredientLanguage> IngredientList = new List<IngredientLanguage>();

            if (dtIngredientLang.Rows.Count > 0)
            {
                for (int i = 0; i < dtIngredientLang.Rows.Count; i++)
                {
                    IngredientList.Add(new IngredientLanguage()
                    {
                        _IDIngredientLanguage = dtIngredientLang.Rows[i].Field<Guid>("IDIngredientLanguage"),
                        _IDIngredient = dtIngredientLang.Rows[i].Field<Guid>("IDIngredient"),
                        _IngredientPlural = dtIngredientLang.Rows[i].Field<string>("IngredientPlural"),
                        _IngredientSingular = dtIngredientLang.Rows[i].Field<string>("IngredientSingular"),
                        _IngredientImage = dtIngredientLang.Rows[i].Field<Guid>("IDIngredientImage"),
                    });
                }
            }

            return IngredientList;
        }

        public DataTable ListIngredientCategory()
        {
            DataTable dtListIngredientCategory = new DataTable();
            DataColumn IngrCatLang = new DataColumn("IngredientCategoryLanguage");
            dtListIngredientCategory.Columns.Add(IngrCatLang);

            GetIngredientsCategoriesDAL dtListIngredientCategoryLang = new GetIngredientsCategoriesDAL();

           
            if (_IngredientCategory != null)
            {
                foreach (IngredientCategory IngCat in _IngredientCategory)
                {
                    DataRow NewRow = dtListIngredientCategory.NewRow();
                    NewRow["IngredientCategoryLanguage"] = dtListIngredientCategoryLang.USP_GetAllCategoryByIDLanguageIDFatherCategory(_IDLanguage, IngCat.IDIngredientCategory, 0).Rows[0].Field<string>("IngredientCategoryLanguage");
                    dtListIngredientCategory.Rows.Add(NewRow);
                }
            }
            return dtListIngredientCategory;
        }

        public DataTable ListIngredientAllowedQuantityTypes()
        {
            DataTable dtIngredientAllowedQuantityTypes = new DataTable();
            DataColumn IDIngrQuantityTypes = new DataColumn("IDIngredientQuantityType");
            DataColumn IngrQuantityTypesLang = new DataColumn("IngredientQuantityType");
            dtIngredientAllowedQuantityTypes.Columns.Add(IDIngrQuantityTypes);
            dtIngredientAllowedQuantityTypes.Columns.Add(IngrQuantityTypesLang);

            GetIngredientsQuantityTypesLanguagesDAL dtListAllowedQuantityTypesLang = new GetIngredientsQuantityTypesLanguagesDAL();


            if (_IngredientQuantityType != null)
            {
                foreach (IngredientQuantityType Ingqta in _IngredientQuantityType)
                {
                    DataRow NewRow = dtIngredientAllowedQuantityTypes.NewRow();
                    NewRow["IDIngredientQuantityType"] = dtListAllowedQuantityTypesLang.USP_GetIngredientsQuantityTypesLangByID(Ingqta.IDIngredientQuantityType, _IDLanguage).Rows[0].Field<int>("IDIngredientQuantityType");
                    NewRow["IngredientQuantityType"] = dtListAllowedQuantityTypesLang.USP_GetIngredientsQuantityTypesLangByID(Ingqta.IDIngredientQuantityType, _IDLanguage).Rows[0].Field<string>("IngredientQuantityTypeSingular");
                    dtIngredientAllowedQuantityTypes.Rows.Add(NewRow);
                }
            }
            return dtIngredientAllowedQuantityTypes;
        }

        public ManageUSPReturnValue SaveIngredientLanguage()
        {
            ManageIngredientDAL manageIngrLang = new ManageIngredientDAL();
            DataTable dtmanageIngrLang;

            dtmanageIngrLang = manageIngrLang.USP_InsertOrUpdateIngredientLanguage(IngredientSingular, IngredientPlural,
                                                                IngredientDescription, isAutoTranslate, IDIngredient,
                                                                IDIngredientLanguage, IDLanguage,_GeoIDRegion);

            ManageUSPReturnValue manageIngrLangReturn = new ManageUSPReturnValue(dtmanageIngrLang);

            return manageIngrLangReturn;
        }

        public static DataTable GetIngredientToTranslate(int idLanguageFrom, int idLanguageTo, int IngrToTranslate)
        {
            GetIngredientsLanguagesDAL ingrLang = new GetIngredientsLanguagesDAL();
            return ingrLang.USP_GetIngredientToTranslate(idLanguageFrom, idLanguageTo, IngrToTranslate);
        }

        public static DataTable GetAllIngredientLangNotChecked(int idLanguage)
        {
            GetIngredientsLanguagesDAL ingrLang = new GetIngredientsLanguagesDAL();
            return ingrLang.GetAllIngredientLangNotCheckedByIDLang(idLanguage);
        }

        public static DataTable GetAllIngredientChecked(int idLanguage)
        {
            GetIngredientsLanguagesDAL ingrLang = new GetIngredientsLanguagesDAL();
            return ingrLang.GetAllIngredientChecked(idLanguage);
        }

        public static DataTable GetAllIngredientLangNotCheckedByIDLangIDUser(int idLanguage, Guid idUser)
        {
            GetIngredientsLanguagesDAL ingrLang = new GetIngredientsLanguagesDAL();
            return ingrLang.GetAllIngredientLangNotCheckedByIDLangIDUser(idLanguage,idUser);
        }

        public void GetIDIngredientByIngredientName()
        {
            GetIngredientsLanguagesDAL ingrLang = new GetIngredientsLanguagesDAL();
            DataTable dtingrLang;

            if (_IDLanguage != null && _IDLanguage > 0)
            {
                dtingrLang = ingrLang.GetIngrLangByIngredientName(_IngredientSingular, _IDLanguage);
            }
            else
            {
                dtingrLang = ingrLang.GetIngredientByNameNoLangSpecified(_IngredientSingular);
            }

            if (dtingrLang.Rows.Count > 0)
            {
                _IDIngredient = dtingrLang.Rows[0].Field<Guid>("IDIngredient");
            }
        }

        #endregion

    }

   public class IngredientAlternative
   {

       #region PrivateFileds

       private Guid _IDIngredientAlternative;
       private Ingredient _IngredientMain;
       private Ingredient _IngredientSlave;
       private MyUser _AddedByUser;
       private DateTime _AddedOn;
       private MyUser _CheckedBy;
       private DateTime? _CheckedOn;
       private bool _Checked;

       #endregion

       #region PublicProperties

       public Guid IDIngredientAlternative
       {
           get { return _IDIngredientAlternative; }
       }
       public Ingredient IngredientMain
       {
           get { return _IngredientMain; }
           set { _IngredientMain = value; }
       }
       public Ingredient IngredientSlave
       {
           get { return _IngredientSlave; }
           set { _IngredientSlave = value; }
       }
       public MyUser AddedByUser
       {
           get { return _AddedByUser; }
           set { _AddedByUser = value; }
       }
       public DateTime AddedOn
       {
           get { return _AddedOn; }
           set { _AddedOn = value; }
       }
       public MyUser CheckedBy
       {
           get { return _CheckedBy; }
           set { _CheckedBy = value; }
       }
       public DateTime? CheckedOn
       {
           get { return _CheckedOn; }
           set { _CheckedOn = value; }
       }

       public bool Checked
       {
           get { return _Checked; }
           set { _Checked = value; }
       }

       #endregion

       #region Costructors

       /// <summary>
       /// Get info of an alternative ingredient
       /// </summary>
       /// <param name="IDIngredientAlternative">ID of alternative ingredient</param>
       public IngredientAlternative(Guid IDIngredientAlternative)
       {
           _IDIngredientAlternative = IDIngredientAlternative;
           QueryAlternativeIngredientInfo();
       }

       public IngredientAlternative()
       {
        
       }

       /// <summary>
       /// Create a new Ingredient alternative
       /// </summary>
       /// <param name="IngredientMain"></param>
       /// <param name="IngredientSlave"></param>
       /// <param name="AddedByUser"></param>
       /// <param name="AddedOn"></param>
       public IngredientAlternative(Guid IngredientMain, Guid IngredientSlave, Guid AddedByUser, DateTime AddedOn)
       {
           _IDIngredientAlternative = Guid.NewGuid();
           _IngredientMain = IngredientMain;
           _IngredientSlave = IngredientSlave;
           _AddedByUser = AddedByUser;
           _AddedOn = AddedOn;
           _CheckedOn = null;
           _CheckedBy = null;
           _Checked = false;
       }

       #endregion

       #region Methods

       private void QueryAlternativeIngredientInfo()
       {
           GetIngredientsAlternativesDAL AlternativeIngredientDAL = new GetIngredientsAlternativesDAL();
           DataTable dtAlternativeIngredient = new DataTable();

           dtAlternativeIngredient = AlternativeIngredientDAL.GetAlternativeIngredientByIDIngredientAlternative(_IDIngredientAlternative);

           if (dtAlternativeIngredient.Rows.Count > 0)
           {
               _IngredientMain = dtAlternativeIngredient.Rows[0].Field<Guid>("IDIngredientMain");
               _IngredientSlave = dtAlternativeIngredient.Rows[0].Field<Guid>("IDIngredientSlave");
               _AddedByUser = dtAlternativeIngredient.Rows[0].Field<Guid>("AddedByUser");
               _AddedOn = dtAlternativeIngredient.Rows[0].Field<DateTime>("AddedOn");
               _CheckedBy = dtAlternativeIngredient.Rows[0].Field<Guid>("CheckedBy");
               _CheckedOn = dtAlternativeIngredient.Rows[0].Field<DateTime?>("CheckedOn");
               _Checked = dtAlternativeIngredient.Rows[0].Field<bool>("Checked");
           }
       }

       public ManageUSPReturnValue Delete()
       {
           ManageIngredientDAL manageIngredientAlternativeDAL = new ManageIngredientDAL();
           ManageUSPReturnValue delIngredientAlternativeResult =
           new ManageUSPReturnValue(manageIngredientAlternativeDAL.USP_ManageIngredientAlternative(_IDIngredientAlternative,
                                                null, null, null, null, null, null,false, true));
           return delIngredientAlternativeResult;
       }

       public ManageUSPReturnValue Save()
       {
           ManageIngredientDAL manageIngredientAlternativeDAL = new ManageIngredientDAL();
           ManageUSPReturnValue saveIngredientAlternativeResult =
           new ManageUSPReturnValue(manageIngredientAlternativeDAL.USP_ManageIngredientAlternative(_IDIngredientAlternative,
                                                _IngredientMain,_IngredientSlave,_AddedByUser,_AddedOn,_CheckedBy,_CheckedOn,_Checked, false));
           return saveIngredientAlternativeResult;
       }

       #endregion

       #region Operators

       public static bool operator ==(IngredientAlternative ingrAlt1, IngredientAlternative ingrAlt2)
       {
           if ((Object)ingrAlt1 == null)
           {
               ingrAlt1 = new IngredientAlternative(new Guid());
           }
           if ((Object)ingrAlt2 == null)
           {
               ingrAlt2 = new IngredientAlternative(new Guid());
           }
           if ((Object)ingrAlt1 == null || (Object)ingrAlt2 == null)
           {
               return (Object)ingrAlt1 == (Object)ingrAlt2;
           }
           return ingrAlt1.IDIngredientAlternative == ingrAlt2.IDIngredientAlternative;
       }

       public static bool operator !=(IngredientAlternative ingrAlt1, IngredientAlternative ingrAlt2)
       {
           return !(ingrAlt1 == ingrAlt2);
       }

       public override bool Equals(System.Object obj)
       {
           // If parameter is null return false.
           if (obj == null)
           {
               return false;
           }

           // If parameter cannot be cast to Ingredient return false.
           IngredientAlternative ingAlt = obj as IngredientAlternative;
           if ((System.Object)ingAlt == null)
           {
               return false;
           }

           // Return true if the fields match:
           return (IDIngredientAlternative == ingAlt.IDIngredientAlternative);
       }

       public bool Equals(IngredientAlternative ingAlt)
       {
           // If parameter is null return false:
           if ((object)ingAlt == null)
           {
               return false;
           }

           // Return true if the fields match:
           return (IDIngredientAlternative == ingAlt.IDIngredientAlternative);
       }

       public override int GetHashCode()
       {
           return IDIngredientAlternative.GetHashCode();
       } 

       #endregion

   }
}
