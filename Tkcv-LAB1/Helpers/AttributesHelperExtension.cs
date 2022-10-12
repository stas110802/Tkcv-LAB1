using System;
using System.ComponentModel;
using System.Reflection;

namespace Tkcv_LAB1.Helpers;

public static class AttributesHelperExtension
{
    public static string ToDescription(this Enum value)
    {
        var da = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);

        return da.Length > 0 ? da[0].Description : value.ToString();
    }
    
    public static string[] GetStringValues<T>(T obj)
        where T : class
    {
        var type = obj.GetType();
        var props = type.GetProperties();
        var result = new string[props.Length];

        for (var i = 0; i < props.Length; i++)
        {
            var value = props[i].GetValue(obj, null)?.ToString();
            if(value is not null)
                result[i] = value;
        }
       
        return result;
    }
    
    public static T? GetEnumValueFromDescription<T>(string description)
    {
        MemberInfo[] fis = typeof(T).GetFields();

        foreach (var fi in fis)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                return (T)Enum.Parse(typeof(T), fi.Name);
        }
        
        throw new Exception("Not found");
    }
}