using Brewery.BO.Entities;
using Brewery.Tests.API.Provides;
using Elia.Core.Extensions;

namespace Brewery.Tests.API.API.BO;

/// <summary>
/// 
/// </summary>
[TestClass]
public class BreweryUnitTest: TestBase
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
        BreweryEntity model = _fakeModel.Brewery;

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
        BreweryEntity model = new BreweryEntity();

        //Act
        var errors = model.GetMembers();
        var isValid = model.IsValid();

        //Asserts
        Assert.IsFalse(isValid);
        Assert.AreEqual(errors.Count, 1);
        Assert.IsTrue(errors.Contains("Name"));
    }
}
