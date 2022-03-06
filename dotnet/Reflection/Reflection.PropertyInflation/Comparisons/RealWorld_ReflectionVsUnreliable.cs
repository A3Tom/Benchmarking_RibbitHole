using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Reflection.ObjectInflation.Models;
using System.Reflection;

namespace Reflection.ObjectInflation.Comparisons;

[RPlotExporter]
public class RealWorld_ReflectionVsUnreliable
{
    private const char DELIMITER = '|';

    private readonly string[] SERIALIZED_DATA = new string[] 
    {
        "NAME|FMLY|GNUS|SPCS|IUCN",
        "Archey's frog|Leiopelmatidae|Leiopelma|Leiopelma archeyi|4",
        "Mexican burrowing toad|Rhinophrynidae|Rhinophrynus|Rhinophrynus dorsalis|6",
        "Chiriqui harlequin frog|Bufonidae|Atelopus|Atelopus chiriquiensis|0"
    };

    private readonly string[][] _splitString;

    public RealWorld_ReflectionVsUnreliable()
    {
        _splitString = SERIALIZED_DATA
            .Select(x => x.Split(DELIMITER))
            .ToArray();
    }

    [Benchmark]
    public Frog[] DeserializeByIndex_Plain()
    {
        var froggyArray = new Frog[_splitString.Length - 1];

        for (int i = 0; i < _splitString.Length; i++)
        {
            if (i == 0)
                continue;

            _ = int.TryParse(_splitString[i][4], out int iucnStatusInt);

            froggyArray[i - 1] = new Frog()
            {
                KnownAs = _splitString[i][0],
                Family = _splitString[i][1],
                Genus = _splitString[i][2],
                Species = _splitString[i][3],
                IUCNStatus = (IUCNStatusEnum)iucnStatusInt
            };
        }

        return froggyArray;
    }

    [Benchmark]
    public Frog[] DeserializeByIndex_Linq() =>
        _splitString
        .Skip(1)
        .Select(lilFrog =>
            new Frog()
            {
                KnownAs = lilFrog[0],
                Family = lilFrog[1],
                Genus = lilFrog[2],
                Species = lilFrog[3],
                IUCNStatus = (IUCNStatusEnum)int.Parse(lilFrog[4])
            })
        .ToArray();

    [Benchmark]
    public Frog[] DeserializeByReflection()
    {
        var result = new Frog[_splitString.Length - 1];

        var props = typeof(Frog)
            .GetProperties();

        var propIndexes = BuildPropPositionDictionary(props);

        for (int i = 1; i < _splitString.Length; i++)
        {
            var newLilFrog = new Frog();

            for (int j = 0; j < props.Length; j++)
            {
                var frogProp = props[j];
                var froggyIdx = propIndexes[frogProp.Name];

                if (frogProp.PropertyType == typeof(IUCNStatusEnum))
                    props[j].SetValue(newLilFrog, (IUCNStatusEnum)int.Parse(_splitString[i][froggyIdx]));
                else
                    props[j].SetValue(newLilFrog, _splitString[i][froggyIdx]);
            }

            result[i - 1] = newLilFrog;
        }

        return result;
    }

    private IDictionary<string, int> BuildPropPositionDictionary(PropertyInfo[] props)
    {
        var result = new Dictionary<string, int>();

        foreach (var prop in props)
        {
            var propAlias = prop.GetCustomAttribute<FroggyApiShiteNameAttribute>()?.Name;
            var propIdx = Array.IndexOf(_splitString[0], propAlias);

            result.Add(prop.Name, propIdx);
        }

        return result;
    }
}

