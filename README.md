# BetterSpecs ![alt tag](https://travis-ci.org/ycodeteam/betterspecs.svg?branch=master)
>xSpec "is a great tool in the behavior-driven development (BDD) process of writing human readable specifications that direct and validate the development of your application" ([betterspecs.org](http://betterspecs.org/)). 

BetterSpecs is the simple way to bring the power of spec to your tests. Based in xSpec (Like RSpec, and Jasmine) tools, BetterSpecs, improves tests experience on xUnit tools. 

### :package: install package
To install Betterspec in your MSTest project use this command:

```csharp
PM> Install-Package BetterSpecs
```

### :package: using betterspecs
```csharp
using BetterSpecs;

[TestClass]
public class SpecContextTests : SpecContext
{
  ...
}
````

### describe
```csharp
[TestMethod]
public void describe_speccontext_while_using_it()
{
    describe["when use SpecContext class"] = () =>
    {
        it["works very well"] = () => Assert.IsTrue(true);
    };
}
````

### mixin contexts
```csharp
[TestMethod]
public void describe_speccontext_while_using_it()
{
    context["when use SpecContext class"] = () =>
    {
        context["with two context"] = () =>
        {
            it["works very well"] = () => Assert.IsTrue(true);
        };
    };
}
````

### using let
defer the instance creation

```csharp
[Test]
public void describe_register_user()
{
    context["when create a user"] = () =>
    {
        User user = null;
        let.Add("user", () => 
        { 
          user = new User("teste@teste.com");
          return user;
        });
        
        it["is null"] = () => Assert.Null(user);
        it["is not null"] = () => Assert.AreEqual(user, let["user"]);
    };
}
````

### output spec
When finished run tests, the output will be like this:

```
when use SpecContext class
with other internal context
    It works very well
```
