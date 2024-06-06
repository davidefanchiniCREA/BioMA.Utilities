using BioMA.Utilities.NetFramework.Tests;
using JRC.IPSC.MARS.Utilities;

namespace BioMA.Utilities.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void TestClassCopier()
        {
            MockUpDomainClass original = new MockUpDomainClass()
            {
                Intero = 4,
                ArrayDiInteri = new int[] { 1, 2, 3 }
            };

            ClassCopier classCopier = new ClassCopier();

            MockUpDomainClass cloned = new MockUpDomainClass();
            classCopier.CopyFields(original, cloned);

            Assert.AreEqual(original.Intero, cloned.Intero);
            CollectionAssert.AreEquivalent(original.ArrayDiInteri, cloned.ArrayDiInteri);

            cloned.Intero = 5;
            Assert.AreNotEqual(original.Intero, cloned.Intero);
            cloned.ArrayDiInteri[0] = 2;
            CollectionAssert.AreEquivalent(original.ArrayDiInteri, cloned.ArrayDiInteri);
        }
    }
}