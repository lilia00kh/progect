using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class AnswerToCommentModel
    {
        [Column("AnswerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Comment id is a required field.")]
        public Guid CommentId { get; set; }

        [Required(ErrorMessage = "User name is a required field.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Answer is a required field.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Data is a required field.")]
        public DateTime Date { get; set; }

    }
}
