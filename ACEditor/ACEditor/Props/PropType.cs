//using ACE.Entity.Enum.Properties;
//using ACE.Server.WorldObjects;
using ACEditor.Table;
using System;
using System.Linq;
using UtilityBelt.Scripting.Interop;
//using PropertyBool = ACEditor.Props.PropertyBool;

namespace ACEditor.Props;

public enum PropType
{
    Unknown,
    //PropertyAttribute,
    //PropertyAttribute2nd,
    //PropertyBook,
    PropertyBool,
    PropertyDataId,
    PropertyFloat,
    PropertyInstanceId,
    PropertyInt,
    PropertyInt64,
    PropertyString,

    //PropertyPosition
}

public static class PropertyTypeExtensions
{
    public static string[] GetProps(this PropType propertyType, PropertyData target = null)
    {
        string[] props;
        if (target is null)
        {
            props = propertyType switch
            {
                PropType.Unknown => new string[0],
                PropType.PropertyBool => Enum.GetNames(typeof(PropertyBool)),
                PropType.PropertyDataId => Enum.GetNames(typeof(PropertyDataId)),
                PropType.PropertyFloat => Enum.GetNames(typeof(PropertyFloat)),
                PropType.PropertyInstanceId => Enum.GetNames(typeof(PropertyInstanceId)),
                PropType.PropertyInt => Enum.GetNames(typeof(PropertyInt)),
                PropType.PropertyInt64 => Enum.GetNames(typeof(PropertyInt64)),
                PropType.PropertyString => Enum.GetNames(typeof(PropertyString)),
                _ => new string[0], //Throw?
            };
        }
        else
        {
            props = propertyType switch
            {
                PropType.Unknown => new string[0],
                PropType.PropertyBool => target.BoolValues.Keys.Select(x => x.ToString()).ToArray(),
                PropType.PropertyDataId => target.DataValues.Keys.Select(x => x.ToString()).ToArray(),
                PropType.PropertyFloat => target.FloatValues.Keys.Select(x => x.ToString()).ToArray(),
                PropType.PropertyInstanceId => target.InstanceValues.Keys.Select(x => x.ToString()).ToArray(),
                PropType.PropertyInt => target.IntValues.Keys.Select(x => x.ToString()).ToArray(),
                PropType.PropertyInt64 => target.Int64Values.Keys.Select(x => x.ToString()).ToArray(),
                PropType.PropertyString => target.StringValues.Keys.Select(x => x.ToString()).ToArray(),
                _ => new string[0], //Throw?
            };
        }

        return props;
    }

}