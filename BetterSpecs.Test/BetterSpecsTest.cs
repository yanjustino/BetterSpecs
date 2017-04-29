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

        [Fact(DisplayName="Describe Should check if LET returns an instance of User")]
        public void Describe_Let_Should_Returns_User()
        {
            _betterSpecsFixture.Describe["Teste do Teste"] = () =>
            {
                _betterSpecsFixture.Let.Add("New User with Id equals to 1", () => new User(1, "José Roberto", "jose.roberto"));

                _betterSpecsFixture.Let.Get<User>("New User with Id equals to 1")
                .Should()
                .BeOfType<User>();
            };
        }

        [Fact(DisplayName = "Describe Should have a User as Subject")]
        public void Describe_Should_Have_A_Subject()
        {
            _betterSpecsFixture.Describe["Teste do Teste"] = () =>
            {
                _betterSpecsFixture.Subject.Assign(new User(1, "José Roberto", "jose.roberto"));
                
                _betterSpecsFixture.Subject.Get<User>()
                    .Should()
                    .BeOfType<User>();
            };
        }

        [Fact(DisplayName = "Check if Expected Values are Equals")]
        public void Check_If_Expected_Values_Are_Equals()
        {
            _betterSpecsFixture.Describe["Teste do Teste"] = () =>
            {
                _betterSpecsFixture.Subject.Assign(new User(1, "José Roberto", "jose.roberto"));

                _betterSpecsFixture.Subject.Get<User>()
                    .Should()
                    .BeOfType<User>();

                _betterSpecsFixture.It["The login's property value shall be equals to 'jose.roberto'"] = () =>
                {
                    var currentSubject = _betterSpecsFixture.Subject.Get<User>();

                    _betterSpecsFixture.It
                        .ExpectThatSubject()
                        .BeEquals(currentSubject.Login, "jose.roberto");
                };
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