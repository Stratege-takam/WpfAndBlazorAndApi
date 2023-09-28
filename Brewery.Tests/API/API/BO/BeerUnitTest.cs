using Brewery.BO.Entities;
using Brewery.Tests.API.Provides;
using Elia.Core.Extensions;

namespace Brewery.Tests.API.API.BO;

/// <summary>
/// 
/// </summary>
[TestClass]
public class BeerUnitTest: TestBase
{
        
    /// <summary>
    /// 
    /// </summary>
    [TestCleanup]
    public void TestClean()
    {

    }
        
    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public  void valid_model()
    {
        //Arrange
        BeerEntity model = _fakeModel.Beer;

        //Act
        var isValid =  model.IsValid();

        //Asserts
        Assert.IsTrue(isValid);
    }

    /// <summary>
    /// 
    /// </summary>
    [TestMethod]
    public void invalid_model()
    {
        //Arrange
        BeerEntity model = new BeerEntity();

        //Act
        var errors = model.GetMembers();
        var isValid = model.IsValid();

        //Asserts
        Assert.IsFalse(isValid);
        Assert.AreEqual(errors.Count, 1);
        Assert.IsTrue(errors.Contains("Name"));
    }
}
