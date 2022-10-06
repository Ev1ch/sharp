using NUnit.Framework;
using FluentAssertions;

namespace Core.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string username = "dennis";
            username.Should().Be("dennis");
        }
    }
}