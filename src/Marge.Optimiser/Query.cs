using System.Collections.Specialized;

namespace Marge.Optimiser;

public class Query(NameValueCollection query)
{
    public string Format { get => query["format"] ?? query["f"]; }

    public uint Quality
    {
        get
        {
            var strValue = query["quality"] ?? query["q"];

            var isNumber = uint.TryParse(strValue, out uint value);

            return isNumber ? value : 80;
        }
    }

    public uint? Width
    {
        get
        {
            var strValue = query["width"] ?? query["w"];

            var isNumber = uint.TryParse(strValue, out uint value);

            return isNumber ? value : null;
        }
    }

    public uint? Height
    {
        get
        {
            var strValue = query["height"] ?? query["h"];

            var isNumber = uint.TryParse(strValue, out uint value);

            return isNumber ? value : null;
        }
    }
}