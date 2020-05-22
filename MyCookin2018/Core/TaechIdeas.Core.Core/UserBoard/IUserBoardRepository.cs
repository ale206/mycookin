using System.Collections.Generic;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.Core.UserBoard
{
    public interface IUserBoardRepository
    {
        IEnumerable<BlockElementOut> BlockElement(BlockElementIn blockElementIn);
        IEnumerable<FatherOrSonOut> FatherOrSon(FatherOrSonIn fatherOrSonIn);
        IEnumerable<WithPaginationOut> WithPagination(WithPaginationIn withPaginationIn);
        IEnumerable<BlockLoadOut> BlockLoad(BlockLoadIn blockLoadIn);
        IEnumerable<MyNewsBlockLoadOut> MyNewsBlockLoad(MyNewsBlockLoadIn myNewsBlockLoadIn);
        CheckIfSharedOut CheckIfShared(CheckIfSharedIn checkIfSharedIn);
        InsertActionSharedOut InsertActionShared(InsertActionSharedIn insertActionSharedIn);
        InsertActionOut InsertAction(InsertActionIn insertActionIn);
        CheckIfSharedOnPersonalBoardOut CheckIfSharedOnPersonalBoard(CheckIfSharedOnPersonalBoardIn checkIfSharedOnPersonalBoardIn);
        CountLikesOrCommentOut CountLikesOrComment(CountLikesOrCommentIn countLikesOrCommentIn);
        CountNumberOfActionsByUserAndTypeOut CountNumberOfActionsByUserAndType(CountNumberOfActionsByUserAndTypeIn countNumberOfActionsByUserAndTypeIn);
        DeleteLikeOut DeleteLike(DeleteLikeIn deleteLikeIn);
        ActionOwnerByFatherOut ActionOwnerByFather(ActionOwnerByFatherIn actionOwnerByFatherIn);
        IdUserFromIdUserActionOut IdUserFromIdUserAction(IdUserFromIdUserActionIn idUserFromIdUserActionIn);
        IEnumerable<IdUserByActionTypeAndActionFatherOut> IdUserByActionTypeAndActionFather(IdUserByActionTypeAndActionFatherIn idUserByActionTypeAndActionFatherIn);
        ObjectYouLikeOut ObjectYouLike(ObjectYouLikeIn objectYouLikeIn);
        UserActionInfoByIdOut UserActionInfoById(UserActionInfoByIdIn userActionInfoByIdIn);
        DeleteUserActionOut DeleteUserAction(DeleteUserActionIn deleteUserActionIn);
        TemplateByTypeAndLanguageOut TemplateAccordingToTypeAndLanguage(TemplateByTypeAndLanguageIn templateByTypeAndLanguageIn);
    }
}