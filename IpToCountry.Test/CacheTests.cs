using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IpToCountry.Test
{
    [TestClass]
    public class CacheTests
    {
        public CacheTests()
        {
            IpToCountryCache.Load();
        }

        [TestMethod]
        public void CacheShouldBeLoading()
        {
            Assert.IsTrue(IpToCountryCache.Cache.Any(), "Cache should contain at least one item");
        }

        [TestMethod]
        public void IpAddressShouldBeFromCanada()
        {
            var ipAddressLocation = IpToCountryCache.GetIpAddressLocation(IPAddress.Parse("23.17.255.255"));
            Assert.IsTrue(ipAddressLocation.CountryCode == "CA");
        }

        [TestMethod]
        public void IpAddressShouldBeFromLatvia()
        {
            var ipAddressLocation = IpToCountryCache.GetIpAddressLocation(IPAddress.Parse("31.42.95.255"));
            Assert.IsTrue(ipAddressLocation.CountryCode == "LV");
        }

        [TestMethod]
        public void IpAddressShouldBeFromLatviaAccordingToIncident4466()
        {
            var ipAddressLocation = IpToCountryCache.GetIpAddressLocation(IPAddress.Parse("195.244.150.52"));
            Assert.IsTrue(ipAddressLocation.CountryCode == "LV");
        }

        [TestMethod]
        public void IpAddressShouldBeFromPrivateNetwork()
        {
            var ipAddressLocation = IpToCountryCache.GetIpAddressLocation(IPAddress.Parse("10.110.15.92"));
            Assert.IsTrue(ipAddressLocation.PrivateNetwork);
        }

        [TestMethod]
        public void IpAddressShouldBeFromUnrecognizedNetwork()
        {
            var ipAddressLocation = IpToCountryCache.GetIpAddressLocation(IPAddress.Parse("0.0.0.0"));
            Assert.IsTrue(ipAddressLocation.UnrecognizedNetwork);
        }
    }
}
