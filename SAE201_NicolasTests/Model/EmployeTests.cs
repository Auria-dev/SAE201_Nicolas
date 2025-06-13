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
    public class EmployeTests
    {
        [TestMethod()]
        public void EmployeTest()
        {
            //int numEmploye, int numRole, string nom, string prenom, string login, string mdp
            Employe e = new Employe(1, 1, "Dupont", "Martin", "dupontm", "123456");
            Assert.IsNotNull(e);

            Assert.AreEqual(1, e.NumEmploye);
            Assert.AreEqual(1, (int)Role.ResponsableDeMagasin);
            Assert.AreEqual("Dupont", e.Nom);
            Assert.AreEqual("Martin", e.Prenom);
            Assert.AreEqual("dupontm", e.Login);
            Assert.AreEqual("123456", e.Mdp);
        }

        [TestMethod()]
        public void IntToRoleTest()
        {
            Employe e = new Employe(1, 1, "Dupont", "Martin", "dupontm", "123456");

            Assert.AreEqual(Role.ResponsableDeMagasin, e.IntToRole(1));
            Assert.AreEqual(Role.Vendeur, e.IntToRole(2));
        }
    }
}