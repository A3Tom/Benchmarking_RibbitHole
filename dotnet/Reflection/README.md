# Reflection in C# .Net

## Object inflation

### Raw C# | LINQ | Reflection

#### Background


This comparison was done on a dataset that is a near replica structure of one which I've recently had the pleasure of working with in work. This dataset is returned in the format of a string array, with each string being a character separated value row.
To further add a lil' bitta complexity; the query which defines the dataset is a custom SQL which is not directly maintained by the same team maintaining the consuming API  

I view this level of guarantee as rather uncool

#### Comparison
Given an array of strings - where row [0] is a row of headers and rows [1..] are value rows; what is the time difference between two methods of object inflation? 

The two methods being:
- pre-determined map of array column positions using hard coded indexes to inflate the datamodel
  - Raw C#, no Linq
  - Linqified
- runtime determination of array column positions using reflection to inflate the datamodel

#### Expected

I think that reflection will definitely be worse off here; but I hope that improvements in .Net - especially .Net Core - over the years will have meant that there are a few 'under the hood' tricks that have drastically increased the speed of object inflation.

I reckon the reflection method will surprise me with how fast it is now and so I'm gonna say raw C# will outperform reflection by around 8x

#### Actual

```
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1526 (21H1/May2021Update)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT


|                   Method |        Mean |      Error |      StdDev |      Median |
|------------------------- |------------:|-----------:|------------:|------------:|
| DeserializeByIndex_Plain |    81.28 ns |   4.211 ns |    12.41 ns |    74.92 ns |
|  DeserializeByIndex_Linq |   172.52 ns |   9.471 ns |    27.92 ns |   159.18 ns |
|  DeserializeByReflection | 8,106.40 ns | 384.511 ns | 1,133.74 ns | 7,713.99 ns |
```

I thought reflection would perform far better than this ... turns out it's pretty shanner as a go-to tool. 
I'm surprised at how 'slow' LINQ performed given how gid it is to use - but in the order of nanoseconds for most use cases probably isn't at all a concern for 99.999% of C# use cases


So the moral of the story is that using reflection still should be considered as a last resort in terms of performance. With this being said there is a large argument to be made for it's uses in scenarios where you can absorb the cost of performance in order to allow your code to be more reliable and robust.
 
It is fun to write though ... so I still love it, even if it is a tortoise