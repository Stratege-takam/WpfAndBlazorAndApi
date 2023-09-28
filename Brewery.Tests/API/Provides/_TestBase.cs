using Brewery.Tests.API.FakeData;

namespace Brewery.Tests.API.Provides;


    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {

        /// <summary>
        /// 
        /// </summary>
        protected FakeModel _fakeModel;

        /// <summary>
        /// 
        /// </summary>
        protected TestBase()
        {
            _fakeModel = new FakeModel();
        }


        /// <summary>
        /// 
        /// </summary>
        [TestInitialize]
        public virtual async Task Setup()
        {
            // Arrange
            FakeRepository.FakeBreweryContext = await BreweryFakeContext.GetDatabaseContextAsync();
            _fakeModel =  FakeModel.GetData();
            
            FakeSeedManager.Run();
        }
    }