using FluentAssertions;
using Xunit;

namespace BetterSpecs.Test
{
    public class BetterSpecsTest
    {
        private BetterSpecsFixture _betterSpecsFixture = null;

        public BetterSpecsTest()
        {
            _betterSpecsFixture = new BetterSpecsFixture();
        }

        [Fact]
        public void Describe_Should()
        {
            _betterSpecsFixture.Describe["Teste do Teste"] = () =>
            {
                _betterSpecsFixture.Let.Add("New User with Id equals to 1", () => new User(1, "José Roberto", "jose.roberto"));

                _betterSpecsFixture.Let.Get<User>("New User with Id equals to 1")
                .Should()
                .BeOfType<User>();
            };
        }
    }

    class BetterSpecsFixture : Spec
    {
    }

    class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Login { get; private set; }

        public User(int id, string name, string login)
        {
            Id = id;
            Name = name;
            Login = login;
        }
    }
}