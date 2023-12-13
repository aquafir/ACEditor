//using ACE.Entity.Enum.Properties;
//using ACE.Server.WorldObjects;
using System;
using UtilityBelt.Scripting.Interop;
//using PropertyBool = ACEditor.Props.PropertyBool;

namespace ACEditor.Props;

public enum PropertyType
{
    Unknown,
    //PropertyAttribute,
    //PropertyAttribute2nd,
    //PropertyBook,
    PropertyBool,
    PropertyDataId,
    PropertyDouble,
    PropertyInstanceId,
    PropertyInt,
    PropertyInt64,
    PropertyString,

    //PropertyPosition
}

public static class PropertyTypeExtensions
{
    public static string[] GetProps(this PropertyType propertyType, WorldObject target = null)
    {
        string[] props;
        //if(target is null)
        //{
        props = propertyType switch
        {
            PropertyType.Unknown => new string[0],
            PropertyType.PropertyBool => Enum.GetNames(typeof(PropertyBool)),
            PropertyType.PropertyDataId => Enum.GetNames(typeof(PropertyDataId)),
            PropertyType.PropertyDouble => Enum.GetNames(typeof(PropertyFloat)),
            PropertyType.PropertyInstanceId => Enum.GetNames(typeof(PropertyInstanceId)),
            PropertyType.PropertyInt => Enum.GetNames(typeof(PropertyInt)),
            PropertyType.PropertyInt64 => Enum.GetNames(typeof(PropertyInt64)),
            PropertyType.PropertyString => Enum.GetNames(typeof(PropertyString)),
            _ => new string[0], //Throw?
        };
        //}

        return props;
    }

}