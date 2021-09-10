using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class AnswerToComment
    {
        [Column("AnswerId")]
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
