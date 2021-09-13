using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class AnswerToCommentRepository : RepositoryBase<AnswerToComment>, IAnswerToCommentRepository
    {
        public AnswerToCommentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
