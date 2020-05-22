using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.Core.Core;
using TaechIdeas.Core.Core.UserBoard;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.DataAccessLayer
{
    public class UserBoardRepository : IUserBoardRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserBoardRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBUsersBoardConnectionString");
        }

        public IEnumerable<BlockElementOut> BlockElement(BlockElementIn blockElementIn)
        {
            IEnumerable<BlockElementOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<BlockElementOut>>("USP_UsersBoardBlockElement",
                    new
                    {
                        IDUserAction = blockElementIn.UserActionId,
                        IDLanguage = blockElementIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<FatherOrSonOut> FatherOrSon(FatherOrSonIn fatherOrSonIn)
        {
            IEnumerable<FatherOrSonOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<FatherOrSonOut>>("USP_UsersBoardFatherOrSons",
                    new
                    {
                        IDUserActionType = fatherOrSonIn.UserActionTypeId,
                        IDUser = fatherOrSonIn.UserId,
                        IDUserActionFather = fatherOrSonIn.UserActionFatherId,
                        fatherOrSonIn.NumberOfResults,
                        fatherOrSonIn.SortOrder,
                        IDLanguage = fatherOrSonIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<WithPaginationOut> WithPagination(WithPaginationIn withPaginationIn)
        {
            IEnumerable<WithPaginationOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<WithPaginationOut>>("USP_UsersBoardWithPagination",
                    new
                    {
                        IDUser = withPaginationIn.UserId,
                        offset = withPaginationIn.Offset,
                        count = withPaginationIn.Count,
                        orderBy = withPaginationIn.OrderBy,
                        isAscendent = withPaginationIn.IsAscendant,
                        search = withPaginationIn.Search
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<BlockLoadOut> BlockLoad(BlockLoadIn blockLoadIn)
        {
            IEnumerable<BlockLoadOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<BlockLoadOut>>("USP_UsersBoardBlockLoad",
                    new
                    {
                        IDUser = blockLoadIn.UserId,
                        blockLoadIn.SortOrder,
                        blockLoadIn.NumberOfResults,
                        OtherIDActionsToShow = blockLoadIn.OtherActionsIdToShow,
                        IDLanguage = blockLoadIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<MyNewsBlockLoadOut> MyNewsBlockLoad(MyNewsBlockLoadIn myNewsBlockLoadIn)
        {
            IEnumerable<MyNewsBlockLoadOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<MyNewsBlockLoadOut>>("USP_UsersBoardMyNewsBlockLoad",
                    new
                    {
                        IDUser = myNewsBlockLoadIn.UserId,
                        myNewsBlockLoadIn.SortOrder,
                        myNewsBlockLoadIn.NumberOfResults,
                        OtherIDActionsToShow = myNewsBlockLoadIn.OtherActionsIdToShow,
                        IDLanguage = myNewsBlockLoadIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CheckIfSharedOut CheckIfShared(CheckIfSharedIn checkIfSharedIn)
        {
            CheckIfSharedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CheckIfSharedOut>("USP_CheckIfShared",
                    new
                    {
                        checkIfSharedIn.UserActionId,
                        IDUser = checkIfSharedIn.UserId,
                        IDSocialNetwork = checkIfSharedIn.SocialNetworkId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public InsertActionSharedOut InsertActionShared(InsertActionSharedIn insertActionSharedIn)
        {
            InsertActionSharedOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<InsertActionSharedOut>("USP_InsertActionShared",
                    new
                    {
                        insertActionSharedIn.UserActionId,
                        IDUser = insertActionSharedIn.UserId,
                        IDSocialNetwork = insertActionSharedIn.SocialNetworkId,
                        IDShareOnSocial = insertActionSharedIn.ShareIdOnSocial
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public InsertActionOut InsertAction(InsertActionIn insertActionIn)
        {
            InsertActionOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<InsertActionOut>("USP_UsersBoardInsertAction",
                    new
                    {
                        IDUser = insertActionIn.UserId,
                        IDUserActionFather = insertActionIn.UserActionFatherId,
                        IDUserActionType = insertActionIn.UserActionTypeId,
                        IDActionRelatedObject = insertActionIn.ActionRelatedObjectId,
                        insertActionIn.UserActionMessage,
                        IDVisibility = insertActionIn.VisibilityId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CheckIfSharedOnPersonalBoardOut CheckIfSharedOnPersonalBoard(CheckIfSharedOnPersonalBoardIn checkIfSharedOnPersonalBoardIn)
        {
            CheckIfSharedOnPersonalBoardOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CheckIfSharedOnPersonalBoardOut>("USP_CheckIfSharedOnPersonalBoard",
                    new
                    {
                        IDUser = checkIfSharedOnPersonalBoardIn.UserId,
                        IDUserActionType = checkIfSharedOnPersonalBoardIn.UserActionTypeId,
                        IDActionRelatedObject = checkIfSharedOnPersonalBoardIn.ActionRelatedObjectId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CountLikesOrCommentOut CountLikesOrComment(CountLikesOrCommentIn countLikesOrCommentIn)
        {
            CountLikesOrCommentOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CountLikesOrCommentOut>("USP_CountLikesOrComment",
                    new
                    {
                        IDUserActionType = countLikesOrCommentIn.UserActionTypeId,
                        IDUserActionFather = countLikesOrCommentIn.UserActionFatherId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CountNumberOfActionsByUserAndTypeOut CountNumberOfActionsByUserAndType(CountNumberOfActionsByUserAndTypeIn countNumberOfActionsByUserAndTypeIn)
        {
            CountNumberOfActionsByUserAndTypeOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CountNumberOfActionsByUserAndTypeOut>("USP_CountNumberOfActionsByUserAndType",
                    new
                    {
                        IDUserActionType = countNumberOfActionsByUserAndTypeIn.UserActionTypeId,
                        IDUserActionFather = countNumberOfActionsByUserAndTypeIn.UserActionFatherId,
                        IDUser = countNumberOfActionsByUserAndTypeIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteLikeOut DeleteLike(DeleteLikeIn deleteLikeIn)
        {
            DeleteLikeOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteLikeOut>("USP_DeleteLike",
                    new
                    {
                        IDUserActionType = deleteLikeIn.UserActionTypeId,
                        IDUserActionFather = deleteLikeIn.UserActionFatherId,
                        IDUser = deleteLikeIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public ActionOwnerByFatherOut ActionOwnerByFather(ActionOwnerByFatherIn actionOwnerByFatherIn)
        {
            ActionOwnerByFatherOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<ActionOwnerByFatherOut>("USP_GetActionOwnerByFather",
                    new
                    {
                        IDUserActionFather = actionOwnerByFatherIn.UserActionFatherId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IdUserFromIdUserActionOut IdUserFromIdUserAction(IdUserFromIdUserActionIn idUserFromIdUserActionIn)
        {
            IdUserFromIdUserActionOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IdUserFromIdUserActionOut>("USP_GetIdUserFromIdUserAction",
                    new
                    {
                        IDUserAction = idUserFromIdUserActionIn.UserActionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<IdUserByActionTypeAndActionFatherOut> IdUserByActionTypeAndActionFather(IdUserByActionTypeAndActionFatherIn idUserByActionTypeAndActionFatherIn)
        {
            IEnumerable<IdUserByActionTypeAndActionFatherOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IdUserByActionTypeAndActionFatherOut>>("USP_GetIdUserByActionTypeAndActionFather",
                    new
                    {
                        IDUserActionType = idUserByActionTypeAndActionFatherIn.UserActionTypeId,
                        IDUserActionFather = idUserByActionTypeAndActionFatherIn.UserActionFatherId,
                        idUserByActionTypeAndActionFatherIn.NumberOfResults
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public ObjectYouLikeOut ObjectYouLike(ObjectYouLikeIn objectYouLikeIn)
        {
            ObjectYouLikeOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<ObjectYouLikeOut>("USP_GetObjectYouLike",
                    new
                    {
                        IDUserActionFather = objectYouLikeIn.UserActionFatherId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UserActionInfoByIdOut UserActionInfoById(UserActionInfoByIdIn userActionInfoByIdIn)
        {
            UserActionInfoByIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UserActionInfoByIdOut>("USP_GetUserActionInfoById",
                    new
                    {
                        IDUserAction = userActionInfoByIdIn.UserActionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteUserActionOut DeleteUserAction(DeleteUserActionIn deleteUserActionIn)
        {
            DeleteUserActionOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteUserActionOut>("USP_DeleteUserAction",
                    new
                    {
                        IDUserAction = deleteUserActionIn.UserActionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public TemplateByTypeAndLanguageOut TemplateAccordingToTypeAndLanguage(TemplateByTypeAndLanguageIn templateByTypeAndLanguageIn)
        {
            TemplateByTypeAndLanguageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<TemplateByTypeAndLanguageOut>("USP_GetTemplateAccordingToTypeAndLanguage",
                    new
                    {
                        IDUserActionType = templateByTypeAndLanguageIn.UserActionTypeId,
                        IDLanguage = templateByTypeAndLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}