using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.BusinessLogic.User
{
    public class FollowingManager : IFollowingManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FollowingManager(
            IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #region FollowUser

        public FollowUserOutput FollowUser(FollowUserInput followUserInput)
        {
            //TODO: Write Statistic for FollowUser

            return _mapper.Map<FollowUserOutput>(_userRepository.FollowUser(_mapper.Map<FollowUserIn>(followUserInput)));
        }

        #endregion

        #region DefollowUser

        public DefollowUserOutput DefollowUser(DefollowUserInput defollowUserInput)
        {
            //TODO: Write Statistic for DeFollowUser

            return _mapper.Map<DefollowUserOutput>(_userRepository.DefollowUser(_mapper.Map<DefollowUserIn>(defollowUserInput)));
        }

        #endregion

        #region Following

        public IEnumerable<FollowingOutput> Following(FollowingInput followingInput)
        {
            return _mapper.Map<IEnumerable<FollowingOutput>>(_userRepository.Following(_mapper.Map<FollowingIn>(followingInput)));
        }

        #endregion

        #region Followers

        public IEnumerable<FollowersOutput> Followers(FollowersInput followersInput)
        {
            return _mapper.Map<IEnumerable<FollowersOutput>>(_userRepository.Followers(_mapper.Map<FollowersIn>(followersInput)));
        }

        #endregion

        #region CheckFollowing

        public CheckFollowingOutput CheckFollowing(CheckFollowingInput checkFollowingInput)
        {
            return _mapper.Map<CheckFollowingOutput>(_userRepository.CheckFollowing(_mapper.Map<CheckFollowingIn>(checkFollowingInput)));
        }

        #endregion

        #region NumberOfFollowers

        public NumberOfFollowersOutput NumberOfFollowers(NumberOfFollowersInput numberOfFollowersInput)
        {
            return _mapper.Map<NumberOfFollowersOutput>(_userRepository.NumberOfFollowers(_mapper.Map<NumberOfFollowersIn>(numberOfFollowersInput)));
        }

        #endregion

        #region NumberOfFollowing

        public NumberOfFollowingOutput NumberOfFollowing(NumberOfFollowingInput numberOfFollowingInput)
        {
            return _mapper.Map<NumberOfFollowingOutput>(_userRepository.NumberOfFollowing(_mapper.Map<NumberOfFollowingIn>(numberOfFollowingInput)));
        }

        #endregion
    }
}