using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.EntitiesDTO;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllCommentsAsync();
        Task<IEnumerable<AnswerToCommentDto>> GetAllAnswersToCommentAsync(Guid id);
        Task CreateCommentAsync(CommentDto commentDto);
        Task CreateAnswerToCommentAsync(AnswerToCommentDto answerToCommentDto);
        Task DeleteCommentAsync(Guid id);
        Task DeleteAnswerToCommentAsync(Guid id);
    }
}