using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
       
        public CommentService( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _database = unitOfWork;
        }

        public async Task CreateCommentAsync(CommentDto commentDto)
        {
            await _database.Comment.Create(_mapper.Map<CommentDto, Comment>(commentDto));
            _database.Save();
        }

        public async Task CreateAnswerToCommentAsync(AnswerToCommentDto answerToCommentDto)
        {
            await _database.AnswerToComment.Create(_mapper.Map<AnswerToCommentDto, AnswerToComment>(answerToCommentDto));
            _database.Save();
        }

        public async Task<IEnumerable<AnswerToCommentDto>> GetAllAnswersToCommentAsync(Guid id)
        {
            var answersToComment = await _database.AnswerToComment.FindByCondition(x => x.CommentId == id, trackChanges: false);
            return _mapper.Map<IEnumerable<AnswerToComment>, IEnumerable<AnswerToCommentDto>>(answersToComment);
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
        {
            var comments = await _database.Comment.FindAll(trackChanges: false);
            if (comments.Count() == 0)
                throw new CustomException("List is empty", "");
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await _database.Comment.FindByCondition(x => x.Id == id, trackChanges: false);
            if (comment == null)
                throw new CustomException($"Comment with id {id} does not exist", "");
            await _database.Comment.Delete(comment.FirstOrDefault());
            _database.Save();
        }

        public async Task DeleteAnswerToCommentAsync(Guid id)
        {
            var answerToComment = await _database.AnswerToComment.FindByCondition(x => x.Id == id, trackChanges: false);
            if (answerToComment == null)
                throw new CustomException($"Answer to comment with id {id} does not exist", "");
            await _database.AnswerToComment.Delete(answerToComment.FirstOrDefault());
            _database.Save();
        }
    }
}
