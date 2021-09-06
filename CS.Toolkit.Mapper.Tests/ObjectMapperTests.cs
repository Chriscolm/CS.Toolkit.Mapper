using CS.Mapper.Example.Datamodels.ACMEJsonModels;
using CS.Mapper.Example.Datamodels.Common;
using CS.Mapper.Example.Datamodels.MegaCrapDataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CS.Toolkit.Mapper.Tests
{
    [TestClass()]
    public class ObjectMapperTests
    {
        private AcmeProduct _acme;
        private MegaCrapProduct _crap;

        [TestInitialize]
        public void Up()
        {
            _acme = new AcmeProduct()
            {
                ProductKey = new AcmeIdentifier()
                {
                    Gtin13 = "1234567891234",
                    ItemNumber = "A.C.M.E.-1"
                },
                Logistics = new List<AcmeLogistic>()
                {
                    new AcmeLogistic()
                    {
                        Width = 42d,
                        Depth = 7d,
                        Length = 6d,
                        PackingUnit = "U2"
                    }
                },
                Features = new List<AcmeFeature>()
                {
                    new AcmeFeature()
                    {
                        Name = "wtf-Factor",
                        Value = 125
                    }
                },
                Description = new AcmeDescription()
                {
                    Description = "Flibbertigibbet"
                },
                Manufacturer = "ACME ltd.",
                Status = "In Stock"
            };

            _crap = new MegaCrapProduct()
            {
                Ean = "987654321987",
                EnglishDescription = "Flibbertigibbet",
                KlingonDescription = "Verengan",
                ManufacturerProductId = "MC-5",
                Features = new List<MegaCrapFeature>()
                {
                    new MegaCrapFeature()
                    {
                        Name = "wtf-Factor",
                        Value = "125"
                    }
                },
                Logistic1 = new MegaCrapLogistic()
                {
                    Height = "1 cm",
                    Width = "2 cm",
                    Length = "3 cm",
                    PackingUnit = "U1"
                },
                Logistic2 = new MegaCrapLogistic()
                {
                    Height = "42 cm",
                    Width = "7 cm",
                    Length = "6 cm",
                    PackingUnit = "U2"
                },
                Manufacturer = "M.C. inc.",
                State = "In Stock"
            };
        }

        [TestMethod()]
        public void ItMapsASingleAcmeItem()
        {            
            var sut = new ObjectMapper();            
            var res = sut.Map<AcmeProduct, CommonDataModel>(_acme);
            Assert.IsInstanceOfType(res, typeof(CommonDataModel), $"Source Type: {_acme.GetType()} -> TargetType: {res?.GetType()}");
            Assert.AreEqual(_acme.Manufacturer ?? "exspected", res.Manufacturer ?? "actual", $"{nameof(_acme.Manufacturer)}");
            Assert.AreEqual(_acme.Status ?? "exspected", res.ProductState ?? "actual", $"{nameof(_acme.Status)}");
        }

        [TestMethod()]
        public void ItMapsASingleMegaCrapItem()
        {
            var sut = new ObjectMapper();
            var res = sut.Map<MegaCrapProduct, CommonDataModel>(_crap);
            Assert.IsInstanceOfType(res, typeof(CommonDataModel), $"Source Type: {_crap.GetType()} -> TargetType: {res?.GetType()}");
            Assert.AreEqual(_crap.Manufacturer ?? "exspected", res.Manufacturer ?? "actual", $"{nameof(_crap.Manufacturer)}");
            Assert.AreEqual(_crap.State ?? "exspected", res.ProductState ?? "actual", $"{nameof(_crap.State)}");
        }

        [TestMethod]
        public void ItMapsAcmeProductIdentifiers()
        {
            var sut = new ObjectMapper();
            var res = sut.Map<AcmeProduct, CommonDataModel>(_acme);
            Assert.IsNotNull(res.Id);
            Assert.AreEqual(_acme.ProductKey.Gtin13, res.Id.Gtin13);
            Assert.AreEqual(_acme.ProductKey.ItemNumber, res.Id.ItemNumber);
        }

        [TestMethod]
        public void ItMapsMegaCrapProductIdentifiers()
        {
            var sut = new ObjectMapper();            
            var res = sut.Map<MegaCrapProduct, CommonDataModel>(_crap);
            Assert.IsNotNull(res.Id);
            Assert.AreEqual(_crap.Ean, res.Id.Gtin13);
            Assert.AreEqual(_crap.ManufacturerProductId, res.Id.ItemNumber);
        }

        [TestMethod]
        public void ItMapsAcmeProductDescription()
        {
            var sut = new ObjectMapper();
            var res = sut.Map<AcmeProduct, CommonDataModel>(_acme);
            Assert.IsNotNull(res.Id);
            Assert.AreEqual(_acme.Description.Description, res.Description.Description);
            Assert.AreEqual(default, res.Description.LanguageCode);
        }

        [TestMethod]
        public void ItMapsMegaCrapProductDescription()
        {
            var sut = new ObjectMapper();
            var res = sut.Map<MegaCrapProduct, CommonDataModel>(_crap);
            Assert.IsNotNull(res.Id);
            Assert.AreEqual(_crap.EnglishDescription, res.Description.Description);
            Assert.AreEqual(default, res.Description.LanguageCode);
        }

        [TestMethod]
        public void ItMapsMegaCrapLogistics()
        {
            var sut = new ObjectMapper();
            var res = sut.Map<MegaCrapProduct, CommonDataModel>(_crap);
            Assert.IsNotNull(res.Logistics);
            Assert.AreEqual(2, res.Logistics.Count); // es gibt im Beispielobjekt nur Logistic1 und Logistic2, der Rest ist null und wird nicht gemappt
            Assert.AreEqual(_crap.Logistic1.Height, $"{res.Logistics[0].Height} cm");
            Assert.AreEqual(_crap.Logistic2.PackingUnit, res.Logistics[1].PackingUnit);
        }
    }
}