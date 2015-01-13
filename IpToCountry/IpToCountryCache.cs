using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using SpartanExtensions;
using CsvHelper;
using IP2Location;

namespace IpToCountry
{
    public static class IpToCountryCache
    {
        public static List<IPRangeCountry> Cache;

        public static void Load()
        {
            var csvDatabasePath = string.Format("{0}ipCountryRanges.csv",
                typeof(IpToCountryCache).GetAssemblyOutputPath());

            var csvDatabase = new FileInfo(csvDatabasePath);
            if (!csvDatabase.Exists)
            {
                Cache = IP2LocationHandler.Download();
                SaveCountryRanges(csvDatabasePath, Cache);
            }
            else
            {
                LoadCountryRanges(csvDatabasePath);
            }
        }

        public static IpAddressLocation GetIpAddressLocation(IPAddress ipAddress)
        {
            var ipAddressIntegerRepresentation = ipAddress.ToInteger();

            InitCache();

            var correspondingRange = Cache.FirstOrDefault(r => r.StartIP <= ipAddressIntegerRepresentation
                                                               && r.EndIP >= ipAddressIntegerRepresentation);
            return correspondingRange != null ?
                new IpAddressLocation(correspondingRange.ISO_Code_2)
                : new IpAddressLocation();
        }

        private static void InitCache()
        {
            if (Cache == null)
                Load();
        }

        private static void LoadCountryRanges(string csvDatabasePath)
        {
            var csv = new CsvReader(File.OpenText(csvDatabasePath));
            Cache = csv.GetRecords<IPRangeCountry>().ToList();
        }

        private static void SaveCountryRanges(string csvDatabasePath, IEnumerable<IPRangeCountry> ranges)
        {
            var csv = new CsvWriter(File.CreateText(csvDatabasePath));
            csv.WriteRecords(ranges);
        }
    }
}
