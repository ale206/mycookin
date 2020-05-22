using System;
using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.BusinessLogic.User
{
    public class FriendshipManager : IFriendshipManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FriendshipManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #region UsersYouMayKnowByFollower

        public IEnumerable<UsersYouMayKnowByFollowerOutput> UsersYouMayKnowByFollower(UsersYouMayKnowByFollowerInput usersYouMayKnowByFollowerInput)
        {
            return _mapper.Map<IEnumerable<UsersYouMayKnowByFollowerOutput>>(_userRepository.UsersYouMayKnowByFollower(_mapper.Map<UsersYouMayKnowByFollowerIn>(usersYouMayKnowByFollowerInput)));
        }

        #endregion

        #region FriendshipRequests

        public IEnumerable<FriendshipRequestsOutput> FriendshipRequests(FriendshipRequestsInput friendshipRequestsInput)
        {
            return _mapper.Map<IEnumerable<FriendshipRequestsOutput>>(_userRepository.FriendshipRequests(_mapper.Map<FriendshipRequestsIn>(friendshipRequestsInput)));
        }

        #endregion

        #region Friends

        public IEnumerable<FriendsOutput> Friends(FriendsInput friendsInput)
        {
            return _mapper.Map<IEnumerable<FriendsOutput>>(_userRepository.Friends(_mapper.Map<FriendsIn>(friendsInput)));
        }

        #endregion

        #region CommonFriends

        public IEnumerable<CommonFriendsOutput> CommonFriends(CommonFriendsInput commonFriendsInput)
        {
            return _mapper.Map<IEnumerable<CommonFriendsOutput>>(_userRepository.CommonFriends(_mapper.Map<CommonFriendsIn>(commonFriendsInput)));
        }

        #endregion

        #region BlockedFriends

        public IEnumerable<BlockedFriendsOutput> BlockedFriends(BlockedFriendsInput blockedFriendsInput)
        {
            return _mapper.Map<IEnumerable<BlockedFriendsOutput>>(_userRepository.BlockedFriends(_mapper.Map<BlockedFriendsIn>(blockedFriendsInput)));
        }

        #endregion

        #region RequestOrAcceptFriendship

        public RequestOrAcceptFriendshipOutput RequestOrAcceptFriendship(RequestOrAcceptFriendshipInput requestOrAcceptFriendshipInput)
        {
            return _mapper.Map<RequestOrAcceptFriendshipOutput>(_userRepository.RequestOrAcceptFriendship(_mapper.Map<RequestOrAcceptFriendshipIn>(requestOrAcceptFriendshipInput)));
        }

        #endregion

        #region DeclineFriendship

        public DeclineFriendshipOutput DeclineFriendship(DeclineFriendshipInput declineFriendshipInput)
        {
            return _mapper.Map<DeclineFriendshipOutput>(_userRepository.DeclineFriendship(_mapper.Map<DeclineFriendshipIn>(declineFriendshipInput)));
        }

        #endregion

        #region BlockUser

        public BlockUserOutput BlockUser(BlockUserInput blockUserInput)
        {
            return _mapper.Map<BlockUserOutput>(_userRepository.BlockUser(_mapper.Map<BlockUserIn>(blockUserInput)));
        }

        #endregion

        #region RemoveBlockUser

        public RemoveBlockUserOutput RemoveBlockUser(RemoveBlockUserInput removeBlockUserInput)
        {
            return _mapper.Map<RemoveBlockUserOutput>(_userRepository.RemoveBlockUser(_mapper.Map<RemoveBlockUserIn>(removeBlockUserInput)));
        }

        #endregion

        #region RemoveFriendship

        public RemoveFriendshipOutput RemoveFriendship(RemoveFriendshipInput removeFriendshipInput)
        {
            return _mapper.Map<RemoveFriendshipOutput>(_userRepository.RemoveFriendship(_mapper.Map<RemoveFriendshipIn>(removeFriendshipInput)));
        }

        #endregion

        #region RemoveFriendshipForUseWithFollow

        public RemoveFriendshipForUseWithFollowOutput RemoveFriendshipForUseWithFollow(RemoveFriendshipForUseWithFollowInput removeFriendshipForUseWithFollowInput)
        {
            return
                _mapper.Map<RemoveFriendshipForUseWithFollowOutput>(
                    _userRepository.RemoveFriendshipForUseWithFollow(_mapper.Map<RemoveFriendshipForUseWithFollowIn>(removeFriendshipForUseWithFollowInput)));
        }

        #endregion

        #region CheckFriendship

        public CheckFriendshipOutput CheckFriendship(CheckFriendshipInput checkFriendshipInput)
        {
            return _mapper.Map<CheckFriendshipOutput>(_userRepository.CheckFriendship(_mapper.Map<CheckFriendshipIn>(checkFriendshipInput)));
        }

        #endregion

        #region CheckUserBlocked

        public CheckUserBlockedOutput CheckUserBlocked(CheckUserBlockedInput checkUserBlockedInput)
        {
            return _mapper.Map<CheckUserBlockedOutput>(_userRepository.CheckUserBlocked(_mapper.Map<CheckUserBlockedIn>(checkUserBlockedInput)));
        }

        #endregion

        #region FriendshipCompletedDate

        public FriendshipCompletedDateOutput FriendshipCompletedDate(FriendshipCompletedDateInput friendshipCompletedDateInput)
        {
            return _mapper.Map<FriendshipCompletedDateOutput>(_userRepository.FriendshipCompletedDate(_mapper.Map<FriendshipCompletedDateIn>(friendshipCompletedDateInput)));
        }

        #endregion

        #region DaysToYears

        public DaysToYearsOutput DaysToYears(DaysToYearsInput daysToYearsInput)
        {
            var years = 0;

            if (daysToYearsInput.Days >= 365)
            {
                years = daysToYearsInput.Days / 365;
            }

            return new DaysToYearsOutput {Years = years};
        }

        #endregion

        #region DaysWithoutYears

        public DaysWithoutYearsOutput DaysWithoutYears(DaysWithoutYearsInput daysWithoutYearsInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var numberOfDays = days;

            //if (days >= 365)
            //{
            //    numberOfDays = days % 365;
            //}

            //return numberOfDays;
        }

        #endregion

        #region FriendshipTime

        public FriendshipTimeOutput FriendshipTime(FriendshipTimeInput friendshipTimeInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var intervalTime = new TimeSpan(0, 0, 0);

            ////Check if Users are friends
            //try
            //{
            //    var friendshipCompletedDate = FriendshipCompletedDate(idUserFriend1, idUserFriend2);

            //    intervalTime = DateTime.UtcNow.Subtract(friendshipCompletedDate);
            //}
            //catch
            //{
            //    //ERROR LOG
            //    try
            //    {
            //        var logRow = new LogRowIn()
            //        {
            //            ErrorMessage = "Error in FriendshipTime()",
            //            ErrorMessageCode = "US-ER-9999",
            //            ErrorSeverity = LogLevel.Errors,
            //            FileOrigin = _networkManager.GetCurrentPageName(),
            //            IdUser = HttpContext.Current.Session["IDUser"].ToString(),
            //        };

            //        _logManager.WriteLog(logRow);
            //    }
            //    catch
            //    {
            //    }
            //}

            //return intervalTime;
        }

        #endregion

        #region FriendShipTimeString

        public FriendShipTimeStringOutput FriendShipTimeString(FriendShipTimeStringInput friendShipTimeStringInput)
        {
            //TODO: RESTORE...
            throw new NotImplementedException();

            //var friendShipTimeString = "";

            ////Check if Users are friends
            //if (CheckFriendship(idUserFriend1, idUserFriend2))
            //{
            //    var ftProp = FriendshipTime(idUserFriend1, idUserFriend2);

            //    //TODO: Put in configuration
            //    friendShipTimeString = " (Friends for " + DaysToYears(ftProp.Days).ToString() + "y " +
            //                           DaysWithoutYears(ftProp.Days) + "d " + ftProp.Hours + "h " +
            //                           ftProp.Minutes + "m)";
            //}

            //return friendShipTimeString;
        }

        #endregion

        #region NumberOfFriends

        public NumberOfFriendsOutput NumberOfFriends(NumberOfFriendsInput numberOfFriendsInput)
        {
            return _mapper.Map<NumberOfFriendsOutput>(_userRepository.NumberOfFriends(_mapper.Map<NumberOfFriendsIn>(numberOfFriendsInput)));
        }

        #endregion

        #region FindFriendsByWords

        public IEnumerable<FindFriendsByWordsOutput> FindFriendsByWords(FindFriendsByWordsInput findFriendsByWordsInput)
        {
            //TODO: QUI SERVE LA QRY CHE IN BASE ALLE PAROLE PASSATE TROVA EMAIL O NOME O COGNOME O USERNAME DI PERSONE..
            //IN BASE ALLE RIGHE CHE RITORNA SI CICLERA' PER AGGIUNGERE ALLA LISTA......

            return _mapper.Map<IEnumerable<FindFriendsByWordsOutput>>(_userRepository.FindFriendsByWords(_mapper.Map<FindFriendsByWordsIn>(findFriendsByWordsInput)));
        }

        #endregion
    }
}