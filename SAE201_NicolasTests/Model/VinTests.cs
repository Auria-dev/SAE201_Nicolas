using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE201_Nicolas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Model.Tests
{
    [TestClass()]
    public class VinTests
    {
        private Vin v;

        [TestInitialize()]
        public void Setup()
        {
            v = new Vin(1, "Chablis", 15.99, "Description", 2015, 1, 2, 4);
        }

        [TestMethod()]
        public void VinTest()
        {
            Assert.IsNotNull(v);

            Assert.AreEqual(1, v.NumVin);
            Assert.AreEqual("Chablis", v.NomVin);
            Assert.AreEqual(15.99, v.PrixVin);
            Assert.AreEqual("Description", v.Descriptif);
            Assert.AreEqual(2015, v.Annee);
            Assert.AreEqual(1, v.NumTypeVin);
            Assert.AreEqual(2, v.NumAppelation);
            Assert.AreEqual(4, v.NumFournisseur);
        }

        [TestMethod()]
        public void EnumToIntTest()
        {
            Assert.AreEqual(1, v.EnumToInt(TypeVin.Rouge));
            Assert.AreEqual(2, v.EnumToInt(TypeVin.Blanc));
            Assert.AreEqual(3, v.EnumToInt(TypeVin.Rosé));
            Assert.AreEqual(4, v.EnumToInt(TypeVin.Champagne));
            Assert.AreEqual(5, v.EnumToInt(TypeVin.Mousseux));
            Assert.AreEqual(6, v.EnumToInt(TypeVin.Doux));
            Assert.AreEqual(7, v.EnumToInt(TypeVin.Liquoreux));
        }

        [TestMethod()]
        public void IntToEnumTest()
        {
            Assert.AreEqual(v.IntToEnum(1), TypeVin.Rouge);
            Assert.AreEqual(v.IntToEnum(2), TypeVin.Blanc);
            Assert.AreEqual(v.IntToEnum(3), TypeVin.Rosé);
            Assert.AreEqual(v.IntToEnum(4), TypeVin.Champagne);
            Assert.AreEqual(v.IntToEnum(5), TypeVin.Mousseux);
            Assert.AreEqual(v.IntToEnum(6), TypeVin.Doux);
            Assert.AreEqual(v.IntToEnum(7), TypeVin.Liquoreux);
        }
    }
}