IpToCountry
===========

IpToCountry is a .NET library to provide an easy IP to country mapping. Everything is done in memory, 
no annoying bulk inserts into sql databases is needed. Just add the package, load the cache and you are ready to go.
It uses IP2Location Lite, SpartanExtensions and CsvHelper nuget packages.

## Usage

Whenever you desire call this method

```c#

IpToCountryCache.Load();

```

You can use it, for example, in Application_Start method of your Global.asax.cs file.

This method loads the cache of ip ranges and country codes using IP2Location Lite Nuget package.
As a result it saves a "ipCountryRanges.csv" file in the bin folder of the hosting application.
If the "ipCountryRanges.csv" file already exists in the bin folder of the hosting application, 
IpToCountryCache will be loaded from the csv file instead of making a roundtrip to get IP2Location csv database. 
You need to load the cache only once during your application life time.

You can then get the country code that falls within an ip range simply by calling "GetIpAddressLocation" method 
of "IpToCountryCache" class like this:

```c#

var ipAddressLocation = IpToCountryCache.GetIpAddressLocation(IPAddress.Parse("23.17.255.255"));
//ipAddressLocation.CountryCode contains the Country code represented by the ip address

```

## Updating ip range database

Simply delete "ipCountryRanges.csv" file from the bin folder of the hosting application. 
The new version of the IP2Location csv database will be stored as soon as you load the "IpToCountryCache".

Enjoy!
