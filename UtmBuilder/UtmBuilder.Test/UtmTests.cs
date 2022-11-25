using UtmBuilder.Exceptions;

namespace UtmBuilder.Test
{
    [TestClass]
    public class UtmTests
    {
        //Fail

        [TestMethod]
        [ExpectedException(typeof(InvalidUtmException))]
        public void Should_Fail_To_Create_Utm_With_Invalid_Url()
        {
            var utm = new Utm(url:"Invalid URL", source:"source", medium:"medium", name:"name");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUtmException))]
        public void Should_Fail_When_Medium_Is_Not_Valid()
        {
            var utm = new Utm(url: "https://balta.io", source: "source", medium: "", name: "name");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUtmException))]
        public void Should_Fail_When_Name_Is_Not_Valid()
        {
            var utm = new Utm(url: "https://balta.io", source: "source", medium: "medium", name: "");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUtmException))]
        public void Should_Fail_When_Source_Is_Not_Valid()
        {
            var utm = new Utm(url: "https://balta.io", source: "", medium: "medium", name: "name");
            Assert.IsTrue(true);
        }

        //Sucess

        [TestMethod]
        public void Should_Generate_Utm()
        {
            var utm = new Utm(url: "https://balta.io", source: "source", medium: "medium", name: "name");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Should_Generate_Utm_Link()
        {
            var utm = new Utm(
                url: "https://balta.io",
                source: "source",
                medium: "medium",
                name: "name",
                id: "id",
                term: "term",
                content: "content");
            Assert.AreEqual(
                expected: utm.ToString(),
                actual: "https://balta.io?utm_source=source&utm_medium=medium&utm_campaign=name&utm_id=id&utm_term=term&utm_content=content");
        }
    }
}