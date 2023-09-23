using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Brewery.BL.Contracts.Requests.Users;
using Brewery.BL.Contracts.Responses.Users;
using Brewery.BO.Entities;
using Brewery.DAL.Repositories.Users;
using Elia.Core.Attributes;
using Elia.Core.Extensions;
using Elia.Core.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AppSettings = Elia.Core.Utils.AppSettings;

namespace Brewery.BL.Business.Users;

    /// <summary>
    ///     <para>
    ///         This class implements the methods concerning the management 
    ///         of Beer.
    ///     </para>
    /// </summary>
    [Injectable()]
    public class UserBl
    {
        #region Properties (Private)

        /// <summary>
        ///     <para>
        ///         This property represents the repository
        ///     </para>
        /// </summary>
        private readonly UserRepository _repository;
        
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
        public UserBl(UserRepository repository, 
            IOptions<AppSettings.Jwt> appSettingsJwtSection )
        {
            _repository = repository;
            _appSettingsJwt = appSettingsJwtSection.Value;
        }

        #endregion
        
        
        #region Public methods

      
        
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<BaseResult<CreateUserResponse>> CreateUserAsync(CreateUserRequest request)
        {

            if ((await _repository.AnyAsync(u => u.Email == request.Email)).IsSuccess)
            {
                return new BaseResult<CreateUserResponse>(BaseResultStatus.Already, new Exception("The email is already used !"));
            }

            var token = GenerateJwt(_appSettingsJwt.Key, _appSettingsJwt.Issuer);
            var user = new UserEntity()
            {
                Email = request.Email,
                Firstname = request.Firstname,
                Password = request.Password.Hash(),
                Token = token
            };
            
            var entityData = await _repository.CreateAsync(user);

            if (entityData.IsSuccess)
            {
                return new BaseResult<CreateUserResponse>(new CreateUserResponse()
                {
                    Email = entityData.Data.Email,
                    Firstname = entityData.Data.Firstname,
                    Id = entityData.Data.Id,
                    Token= entityData.Data.Token
                });
            }
            
            return new BaseResult<CreateUserResponse>(entityData.Status, entityData.Exception);
        }
        
        
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<BaseResult<CreateUserResponse>> LoginAsync(LoginRequest request)
        {

            var password = request.Password.Hash();
            var entityData = await _repository.SearchOneByByAsync(c => c.Email == request.Email 
                                                                       && c.Password == password);
          
            if (entityData.IsSuccess)
            {
                return new BaseResult<CreateUserResponse>(new CreateUserResponse()
                {
                    Email = entityData.Data.Email,
                    Firstname = entityData.Data.Firstname,
                    Id = entityData.Data.Id,
                    Token= entityData.Data.Token
                });
            }
            
            return new BaseResult<CreateUserResponse>(entityData.Status, new Exception("Bad credential"));
        }
        
        #endregion


        #region Static methods

        /// <summary>
        /// Generate the token
        /// </summary>
        /// <returns></returns>
        public static string GenerateJwt(string key, string issuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(key,
                issuer,
                null,
                expires: DateTime.Now.AddDays(365),
                signingCredentials: credentials);

            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }

        #endregion

    }