using System;
using System.Collections.Generic;
using System.Text;

namespace MyCookin.Common
{
    class UnitConversion
    {
        #region Temperature
        //T° CELSIUS - FAHRENHEIT
        public static double CelsiusToFahrenheit(string temperatureCelsius)
        {
            // Convert argument to double for calculations.
            double celsius = Double.Parse(temperatureCelsius);

            // Convert Celsius to Fahrenheit.
            double fahrenheit = (celsius * 9 / 5) + 32;

            return fahrenheit;
        }

        //T° FAHRENHEIT - CELSIUS 
        public static double FahrenheitToCelsius(string temperatureFahrenheit)
        {
            // Convert argument to double for calculations.
            double fahrenheit = Double.Parse(temperatureFahrenheit);

            // Convert Fahrenheit to Celsius.
            double celsius = (fahrenheit - 32) * 5 / 9;

            return celsius;
        }

        #endregion  

        #region Lenght
        //METER (m) - FEET (ft)
        public static double MeterToFeet(string lenghtMeter)
        {
            // Convert argument to double for calculations.
            double meter = Double.Parse(lenghtMeter);

            // Convert Meter to Feet.
            double feet = meter / 0.3048;

            return feet;
        }

        //FEET (ft) - METER (m)
        public static double FeetToMeter(string lenghtFeet)
        {
            // Convert argument to double for calculations.
            double feet = Double.Parse(lenghtFeet);

            // Convert Meter to Feet.
            double meter = feet * 0.3048;

            return meter;
        }

        #endregion

        #region Volume
        //CUP (Breakfast - cup) - LITRE (l)

        //LITRE (l) - CUP (Breakfast - cup)



        //OUNCE (fluid US food nutrition labeling - US fl oz) - LITRE (l)

        //LITRE (l) - OUNCE (fluid US food nutrition labeling - US fl oz)



        //PINT (US fluid - pt) - LITRE (l)

        //LITRE (l) - PINT (US fluid - pt)



        //TABLESPOON (US food nutrition labeling - tbsp) - LITRE (l)

        //LITRE (l) - TABLESPOON (US food nutrition labeling - tbsp)


        #endregion

        #region Weight
        //KILOGRAM (Kg) - POUND (lb)

        //POUND (lb) - KILOGRAM (Kg)



        //OUNCE (US food nutrition labeling - oz) - KILOGRAMS (kg)

        //KILOGRAMS (kg) - OUNCE (US food nutrition labeling - oz)

        #endregion


    }
}
