using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EntitiesDTO
{
    public class AnswerToCommentDto
    {
        [Column("AnswerId")]
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
