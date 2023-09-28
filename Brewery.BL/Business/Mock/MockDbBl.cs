using Brewery.BL.Business.Users;
using Brewery.Contract.Contracts.Requests.Mock;
using Brewery.Contract.Contracts.Responses.Mock;
using Brewery.DAL.Repositories.Mock;
using Elia.Core.Attributes;
using Elia.Core.Enums;
using Elia.Core.Utils;
using Microsoft.Extensions.Options;
using AppSettings = Elia.Core.Utils.AppSettings;

namespace Brewery.BL.Business.Mock;

    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management mock db 
    ///     </para>
    /// </summary>
    [Injectable()]
    public class MockDbBl
    {
        #region Properties (Private)

        /// <summary>
        ///     <para>
        ///         This property represents the repository
        ///     </para>
        /// </summary>
        private readonly MockDbRepository _repository;
        
        
        /// <summary>
        /// This properties must save jwt param in appsetting
        /// </summary>
        private  readonly AppSettings.Jwt _appSettingsJwt;
        
        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"> beer repository</param>
        /// <param name="appSettingsJwtSection"> This properties must save jwt param in appsetting </param>
        public MockDbBl(MockDbRepository repository, 
            IOptions<AppSettings.Jwt> appSettingsJwtSection)
        {
            _repository = repository;
            
            _appSettingsJwt = appSettingsJwtSection.Value;
        }

        #endregion
        
        
        #region Public methods

        /// <summary>
        /// Clean all tables
        /// </summary>
        /// <param name="request">Information to auth user</param>
        /// <returns></returns>
        public async Task<BaseResult<MockResponse>> CleanTablesAsync(MockRequest request)
        {
            if (Environments.Current != EnvironmentEnum.Test )
            {
                return new BaseResult<MockResponse>(new Exception("You can't clean this db. Contact admin"));
            }
            
            if (request.Email != _appSettingsJwt.Email  || request.Password != _appSettingsJwt.Password )
            {
                return new BaseResult<MockResponse>(new Exception("The user is not authorize"));
            }

            var response = await _repository.CleanTables();

            if (response.IsSuccess)
            {
                return new BaseResult<MockResponse>(new MockResponse() { Status = response.Data });
            }
            
            return new BaseResult<MockResponse>(response.Status, response.Exception);
        }


        /// <summary>
        /// Seed 
        /// </summary>
        /// <param name="request">Information to auth user</param>
        /// <returns></returns>
        public async Task<BaseResult<MockResponse>> RunSeedAsync(MockRequest request)
        {
            if (Environments.Current != EnvironmentEnum.Test )
            {
                return new BaseResult<MockResponse>(new Exception("You can't seed this db. Contact admin"));
            }
            
            if (request.Email != _appSettingsJwt.Email  || request.Password != _appSettingsJwt.Password )
            {
                return new BaseResult<MockResponse>(new Exception("The user is not authorize"));
            }

            var token = UserBl.GenerateJwt(_appSettingsJwt.Key, _appSettingsJwt.Issuer);

            var response = await _repository.RunSeed(token);

            if (response.IsSuccess)
            {
                return new BaseResult<MockResponse>(new MockResponse() { Status = response.Data });
            }
            
            return new BaseResult<MockResponse>(response.Status, response.Exception);
        }

        #endregion

    }