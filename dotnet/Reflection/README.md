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

TBD