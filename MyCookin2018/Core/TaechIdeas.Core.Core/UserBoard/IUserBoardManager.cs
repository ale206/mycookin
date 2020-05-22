using System.Collections.Generic;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.Core.UserBoard
{
    public interface IUserBoardManager
    {
        IEnumerable<BlockElementOutput> BlockElement(BlockElementInput blockElementInput);
        IEnumerable<FatherOrSonOutput> FatherOrSon(FatherOrSonInput fatherOrSonInput);
        IEnumerable<WithPaginationOutput> WithPagination(WithPaginationInput withPaginationInput);
        IEnumerable<BlockLoadOutput> BlockLoad(BlockLoadInput blockLoadInput);
        IEnumerable<MyNewsBlockLoadOutput> MyNewsBlockLoad(MyNewsBlockLoadInput myNewsBlockLoadInput);
        CheckIfSharedOutput CheckIfShared(CheckIfSharedInput checkIfSharedInput);
        InsertActionSharedOutput InsertActionShared(InsertActionSharedInput insertActionSharedInput);
        InsertActionOutput InsertAction(InsertActionInput insertActionInput);
        CheckIfSharedOnPersonalBoardOutput CheckIfSharedOnPersonalBoard(CheckIfSharedOnPersonalBoardInput checkIfSharedOnPersonalBoardInput);
        CountLikesOrCommentOutput CountLikesOrComment(CountLikesOrCommentInput countLikesOrCommentInput);
        CountNumberOfActionsByUserAndTypeOutput CountNumberOfActionsByUserAndType(CountNumberOfActionsByUserAndTypeInput countNumberOfActionsByUserAndTypeInput);
        DeleteLikeOutput DeleteLike(DeleteLikeInput deleteLikeInput);
        ActionOwnerByFatherOutput ActionOwnerByFather(ActionOwnerByFatherInput actionOwnerByFatherInput);
        IdUserFromIdUserActionOutput IdUserFromIdUserAction(IdUserFromIdUserActionInput idUserFromIdUserActionInput);
        IEnumerable<IdUserByActionTypeAndActionFatherOutput> IdUserByActionTypeAndActionFather(IdUserByActionTypeAndActionFatherInput idUserByActionTypeAndActionFatherInput);
        ObjectYouLikeOutput ObjectYouLike(ObjectYouLikeInput objectYouLikeInput);
        UserActionInfoByIdOutput UserActionInfoById(UserActionInfoByIdInput userActionInfoByIdInput);
        DeleteUserActionOutput DeleteUserAction(DeleteUserActionInput deleteUserActionInput);
        TemplateByTypeAndLanguageOutput TemplateByTypeAndLanguage(TemplateByTypeAndLanguageInput templateByTypeAndLanguageInput);
    }
}