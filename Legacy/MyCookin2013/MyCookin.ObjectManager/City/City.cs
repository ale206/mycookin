using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyCookin.DAL.City.ds_CityTableAdapters;


namespace MyCookin.ObjectManager.CityManager
{
    public class City
    {
        #region PrivateFileds

        int _geonameid;
        string _City;

        #endregion

        #region PublicProperties

        public int IDCity
        {
            get { return _geonameid; }
            set { _geonameid = value; }
        }

        public string CityName
        {
            get { return _City; }
            set { _City = value; }
        }

        #endregion

        #region Costructors

        public City(int IDCity)
        {
            _geonameid=IDCity;

            GetCitiesDAL dalCities = new GetCitiesDAL();
            DataTable dtCities = dalCities.GetCities("", 0, 15, _geonameid);

            if (dtCities.Rows.Count > 0)
            {
                _City = dtCities.Rows[0].Field<string>("CityName");
            }
        }

        public City()
        { 
        }
        #endregion

        #region Methods

        public static List<City> CitiesList(string SearchWord,string LangCode)
        {
            GetCitiesDAL dalCities = new GetCitiesDAL();
            DataTable dtCities = dalCities.GetCities(SearchWord, MyCookin.Common.MyConvert.ToInt32(LangCode,1), 15, null);

            List<City> CitiesList = new List<City>();

            if (dtCities.Rows.Count > 0)
            {
                for (int i = 0; i < dtCities.Rows.Count; i++)
                {
                    CitiesList.Add(new City()
                    {
                        IDCity = dtCities.Rows[i].Field<int>("GeoNameID"),
                        CityName = dtCities.Rows[i].Field<string>("CityName"),
                    });
                }
            }
            return CitiesList;
        }

        #endregion
    }
}
