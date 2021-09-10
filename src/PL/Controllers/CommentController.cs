using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ILoggerManager _logger;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            try
            {
                var comments = await _commentService.GetAllCommentsAsync();
                return Ok(await MapList(comments));
            }
            catch (CustomException)
            {
                return Ok(new List<CommentModel>());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("AddComment")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentModel commentModel)
        {
            try
            {
                var commentDto = new CommentDto()
                {
                    UserName = commentModel.UserName,
                    Text = commentModel.Text,
                    Date = commentModel.Date
                };

                await _commentService.CreateCommentAsync(commentDto);
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("AddAnswerToComment")]
        [Authorize]
        public async Task<IActionResult> AddAnswerToComment([FromBody] AnswerToCommentModel answerToCommentModel)
        {
            try
            {
                var answerToCommentDto = new AnswerToCommentDto()
                {
                    CommentId = answerToCommentModel.CommentId,
                    UserName = answerToCommentModel.UserName,
                    Text = answerToCommentModel.Text,
                    Date = answerToCommentModel.Date,
                };

                await _commentService.CreateAnswerToCommentAsync(answerToCommentDto);
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            try
            {
                await _commentService.DeleteCommentAsync(id);
                return Ok();
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(BLL.Services.CommentService.DeleteCommentAsync)} action {ex}");
                return StatusCode(500, "Internal server error, you can`t delete comment that does not exist");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteComment)} action {ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete("DeleteAnswerToComment")]
        [Authorize]
        public async Task<IActionResult> DeleteAnswerToComment(Guid id)
        {
            try
            {
                await _commentService.DeleteAnswerToCommentAsync(id);
                return Ok();
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(BLL.Services.TreeService.DeleteTreeAsync)} action {ex}");
                return StatusCode(500, "Internal server error, you can`t delete comment that doesn`t exist");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteAnswerToComment)} action {ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        private async Task<IEnumerable<CommentModel>> MapList(IEnumerable<CommentDto> commentsDto)
        {
            var commentsModel = new List<CommentModel>();
            foreach (var comment in commentsDto)
            {
                var answersToComment = await _commentService.GetAllAnswersToCommentAsync(comment.Id);
                List<AnswerToCommentModel> answerToCommentModels = new List<AnswerToCommentModel>();
                foreach (var answerToComment in answersToComment)
                {
                    answerToCommentModels.Add(new AnswerToCommentModel()
                    {
                        Id = answerToComment.Id,
                        CommentId = comment.Id,
                        UserName = answerToComment.UserName,
                        Text = answerToComment.Text,
                        Date = answerToComment.Date,
                    });
                }
                commentsModel.Add(new CommentModel()
                {
                    Id = comment.Id,
                    UserName = comment.UserName,
                    Text = comment.Text,
                    Date = comment.Date,
                    AnswerToCommentModels = answerToCommentModels

                });
            }
            return commentsModel;
        }
    }
}
