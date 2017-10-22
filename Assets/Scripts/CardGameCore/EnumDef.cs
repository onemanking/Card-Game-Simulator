﻿using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Linq;

[JsonObject(MemberSerialization.OptIn)]
public class EnumDef
{
    [JsonProperty]
    public string Property { get; private set; }

    [JsonProperty]
    public Dictionary<string, string> Values { get; private set; }

    public static bool TryParseInt(string number, out int intValue)
    {
        bool isHex = number.StartsWith("0x");
        return int.TryParse(isHex ? number.Substring(2) : number, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue);
    }

    public string GetStringFromIntFlags(int flags)
    {
        string result = string.Empty;

        int intValue;
        foreach (KeyValuePair<string, string> enumValue in Values) {
            bool isHex = enumValue.Key.StartsWith("0x");
            if (int.TryParse(isHex ? enumValue.Key.Substring(2) : enumValue.Key, isHex ? NumberStyles.AllowHexSpecifier : NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue) && (intValue & flags) != 0) {
                if (!string.IsNullOrEmpty(result))
                    result += "|";
                result += enumValue.Value;
            }
        }

        return result;
    }

    public static bool IsEnumProperty(string propertyName)
    {
        return CardGameManager.Current.Enums.Where((def) => def.Property.Equals(propertyName)).ToList().Count > 0;
    }
}