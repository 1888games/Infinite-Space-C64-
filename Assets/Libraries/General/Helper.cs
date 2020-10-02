using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Linq;
using System.Globalization;

public static class Helper
{
    private static System.Random _global = new System.Random();

    [ThreadStatic]
    private static System.Random _local;
    
    public enum SizeUnits
    {
        Byte, KB, MB, GB, TB, PB, EB, ZB, YB
    
    }
    public static string ToSize (this Int64 value, SizeUnits unit)
    {
        return (value / (double)Math.Pow(1024, (Int64)unit)).ToString("0.00");
    }

    public static int Random (int min, int max)
    {
        System.Random inst = _local;
        if (inst == null) {
            int seed;
            lock (_global) seed = _global.Next();
            _local = inst = new System.Random(seed);
        }
        return inst.Next(min, max + 1);
    }


   

	public static string GetSeasonName (int seasonYearOne) {

		int seasonYearTwo = seasonYearOne + 1 - 1900;

		if (seasonYearOne < 1900) {
			seasonYearTwo = seasonYearOne + 1 - 1800;
		}

		return seasonYearOne.ToString () + "/" + seasonYearTwo.ToString ();

	}
	

    public static bool IsBetween (this int value, int minimum, int maximum) {

        return value >= minimum && value <= maximum;
    }
    
      public static bool IsBetween (this float value, float minimum, float maximum) {

        return value >= minimum && value <= maximum;
    }


	

    public static int CalculateStars (int value, int minValue, int maxValue) {

        int stars = 0;

        value = Mathf.Clamp(value, minValue, maxValue);

        stars = Mathf.FloorToInt(10f *
        ((float)value - (float)minValue) /
        ((float)maxValue - (float)minValue));



        return stars;


    }


    public static int CalculateStars (float value, float minValue, float maxValue) {

        int stars = 0;

        value = Mathf.Clamp(value, minValue, maxValue);

        stars = Mathf.FloorToInt(10f *
        ((float)value - (float)minValue) /
        ((float)maxValue - (float)minValue));


        return stars;


    }

	public static string BoolToYesNo (bool input) {

		if (input) {
			return "Yes";
		} else {

			return "No";
		}
	}
    
    public static string RemoveAccents(string input)
    {

        return new string(
            input
            .Normalize(System.Text.NormalizationForm.FormD)
            .ToCharArray()
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray());
        // the normalization to FormD splits accented letters in accents+letters
        // the rest removes those accents (and other non-spacing characters)
    }


    public static string IDsToSQLQuery (List<int> ids) {

        string[] stringsArray = ids.Select(i => i.ToString()).ToArray();

        string values = string.Join(",", stringsArray);



        return values;

    }

	


	public static string CommaValue (float amount) {


		return string.Format("{0:n0}", amount);


	}
	
	public static string CommaValue (int amount) {
		
		
		return string.Format("{0:n0}", amount);

	}

    
    public static void SeedRandomGenerator (int seed) {

        System.Random inst = _local;

        _local = inst = new System.Random(seed);

    }

    public static float Random (float min, float max)
    {
        System.Random inst = _local;
        if (inst == null) {
            int seed;
            lock (_global) seed = _global.Next();
            _local = inst = new System.Random(seed);
        }

        double range = (double)max - (double)min;
        double sample = inst.NextDouble();
        double scaled = (sample * range) + min;

        return (float)scaled;


    }
    public static void DebugText (object[] items)
    {

#if UNITY_EDITOR
			
		string text = "";

		foreach (object o in items) {

			if (o != null) {

				text = text + o.ToString ();

			} else {

				text = text + "NULL";
			}

		}

		Debug.Log (text);

#endif


    }



    public static Dictionary<string, int> MoveDictValue (Dictionary<string, int> dict, string field, int change)
    {

        if (dict.ContainsKey(field)) {
            dict[field] = dict[field] + change;
        }

        return dict;

    }




    public static T[] Shuffle<T> (T[] array)
    {
        int n = array.Length;
        while (n > 1) {
            int k = Random(0, (n-- - 1));
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }

        return array;
    }

    public static int GetAllocationSign (int leftToAllocate)
    {

        int sign = 1;

        if (leftToAllocate < 0) {
            sign = -1;
        }

        return sign;


    }

    

    public static string NumberWithSuffix (int num)
    {
        string suffix = "th";

        if (num.ToString().EndsWith("1"))
            suffix = "st";
        if (num.ToString().EndsWith("2"))
            suffix = "nd";
        if (num.ToString().EndsWith("3"))
            suffix = "rd";

        return num.ToString() + suffix;
    }


    public static int ConvertDateTimeToDayID (DateTime dateTime)
    {

        long julianDate = ToJulian(dateTime);

        return (int)(julianDate - (long)2404975);


    }

    public static int ConvertDayIDtoDateTime (DateTime dateTime)
    {

        long julianDate = ToJulian(dateTime);

        return (int)(julianDate - (long)2404975);


    }

    public static long ToJulian (DateTime dateTime)
    {
        int day = dateTime.Day;
        int month = dateTime.Month;
        int year = dateTime.Year;

        if (month < 3) {
            month = month + 12;
            year = year - 1;
        }

        return day + (153 * month - 457) / 5 + 365 * year + (year / 4) - (year / 100) + (year / 400) + 1721119;
    }

    public static string FromJulian (long julianDate, string format)
    {
        long L = julianDate + 68569;
        long N = (long)((4 * L) / 146097);
        L = L - ((long)((146097 * N + 3) / 4));
        long I = (long)((4000 * (L + 1) / 1461001));
        L = L - (long)((1461 * I) / 4) + 31;
        long J = (long)((80 * L) / 2447);
        int Day = (int)(L - (long)((2447 * J) / 80));
        L = (long)(J / 11);
        int Month = (int)(J + 2 - 12 * L);
        int Year = (int)(100 * (N - 49) + I + L);

        // example format "dd/MM/yyyy"
        return new DateTime(Year, Month, Day).ToString(format);
    }


	static int RoundToNearest (int value, int unit) {

        return Mathf.RoundToInt((float)value / (float)unit) * unit;

    }

    public static float RoundToNearest (float value, int unit) {

        return Mathf.RoundToInt((float)value / (float)unit) * unit;

    }



    


}
