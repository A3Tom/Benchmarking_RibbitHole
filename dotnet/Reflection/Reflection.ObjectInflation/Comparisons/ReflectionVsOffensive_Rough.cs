using BenchmarkDotNet.Attributes;
using Reflection.ObjectInflation.Models;

namespace Reflection.ObjectInflation.Comparisons;
public class ReflectionVsOffensive_Rough
{
    private const string SERIALIZED_DATA = "123456789|Awadwdddadwwdwd|kkkwdkowdok9902|---==adw2444,,";
    private const char DELIMITER = '|';

    private readonly string[] _splitString;

    public ReflectionVsOffensive_Rough()
    {
        _splitString = SERIALIZED_DATA.Split(DELIMITER);
    }

    [Benchmark]
    public ExampleType DeserializeByIndex()
    {
        return new ExampleType()
        {
            PureInt = int.Parse(_splitString[0]),
            PureChars = _splitString[1],
            Alphanumeric = _splitString[2],
            NaeClue = _splitString[3]
        };
    }

    [Benchmark]
    public ExampleType DeserializeByReflection()
    {
        var result = new ExampleType();
        var props = typeof(ExampleType)
            .GetProperties();

        for (int i = 0; i < props.Length; i++)
        {
            var prop = props[i];
            if (prop.PropertyType == typeof(int))
                props[i].SetValue(result, int.Parse(_splitString[i]));
            else
                props[i].SetValue(result, _splitString[i]);

        }

        return result;
    }
}
