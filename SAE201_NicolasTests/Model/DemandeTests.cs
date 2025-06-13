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
    public class DemandeTests
    {
        private Demande d;

        [TestInitialize]
        public void Setup()
        {
            d = new Demande(1, 1, 1, 1, DateTime.Now, 100, EnumEtatCommande.EnAttante, 1);
        }

        [TestMethod()]
        public void DemandeTest()
        {
            Assert.IsNotNull(d);

            Assert.AreEqual(1, d.NumDemande);
            Assert.AreEqual(1, d.NumVin);
            Assert.AreEqual(1, d.NumEmploye);
            Assert.AreEqual(1, d.NumCommande);
            Assert.AreEqual(100, d.QuantiteDemande);
            Assert.AreEqual(EnumEtatCommande.EnAttante, d.EtatDemande);
            Assert.AreEqual(1, d.NumClient);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException), "demande avec Quantité > 100. donc invalide")]
        public void DemandeQuantiteTest()
        {
            Demande d2 = new Demande(1, 1, 1, 1, DateTime.Now, 101, EnumEtatCommande.EnAttante, 1);
        }

        [TestMethod()]
        public void StringToEtatCommandeTest()
        {
            Assert.AreEqual(EnumEtatCommande.Validée, d.StringToEtatCommande("Validée"));
            Assert.AreEqual(EnumEtatCommande.EnAttante, d.StringToEtatCommande("EnAttante"));
            Assert.AreEqual(EnumEtatCommande.Supprimée, d.StringToEtatCommande("Supprimée"));

            // EnAttante est le resultat par defaut
            Assert.AreEqual(EnumEtatCommande.EnAttante, d.StringToEtatCommande("string non correct"));
            Assert.AreEqual(EnumEtatCommande.EnAttante, d.StringToEtatCommande(null));
            Assert.AreEqual(EnumEtatCommande.EnAttante, d.StringToEtatCommande(""));
        }

        [TestMethod()]
        public void EtatCommandeToStringTest()
        {
            Assert.AreEqual("En attante", d.EtatCommandeToString(EnumEtatCommande.EnAttante));

            Assert.AreEqual("Validée", d.EtatCommandeToString(EnumEtatCommande.Validée));
            Assert.AreEqual("Supprimée", d.EtatCommandeToString(EnumEtatCommande.Supprimée));
        }

        [TestMethod()]
        public void EtatDemandeToIntTest()
        {
            Assert.AreEqual(1, d.EtatDemandeToInt(EnumEtatCommande.Validée));
            Assert.AreEqual(2, d.EtatDemandeToInt(EnumEtatCommande.EnAttante));
            Assert.AreEqual(3, d.EtatDemandeToInt(EnumEtatCommande.Supprimée));

            Assert.AreEqual(0, d.EtatDemandeToInt((EnumEtatCommande)999));
        }
    }
}