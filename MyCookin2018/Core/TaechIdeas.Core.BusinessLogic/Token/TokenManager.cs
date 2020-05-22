using System;
using AutoMapper;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.BusinessLogic.Token
{
    public class TokenManager : ITokenManager
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenConfig _tokenConfig;
        private readonly IMapper _mapper;

        public TokenManager(ITokenRepository tokenRepository, ITokenConfig tokenConfig, IMapper mapper)
        {
            _tokenRepository = tokenRepository;
            _tokenConfig = tokenConfig;
            _mapper = mapper;
        }

        #region NewToken

        public NewTokenOutput NewToken(NewTokenInput newTokenInput)
        {
            return _mapper.Map<NewTokenOutput>(_tokenRepository.NewToken(_mapper.Map<NewTokenIn>(newTokenInput)));
        }

        #endregion

        #region CheckToken

        public CheckTokenOutput CheckToken(CheckTokenInput checkTokenInput)
        {
            checkTokenInput.TokenRenewMinutes = _tokenConfig.TokenRenewMinutes;
            var checkTokenOutput = _mapper.Map<CheckTokenOutput>(_tokenRepository.CheckToken(_mapper.Map<CheckTokenIn>(checkTokenInput)));

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return checkTokenOutput;
        }

        #endregion

        #region ExpireToken

        public ExpireTokenOutput ExpireToken(ExpireTokenInput expireTokenInput)
        {
            //return _tokenRepository.ExpireToken(expireTokenInput);
            return _mapper.Map<ExpireTokenOutput>(_tokenRepository.ExpireToken(_mapper.Map<ExpireTokenDataInput>(expireTokenInput)));
        }

        #endregion
    }
}