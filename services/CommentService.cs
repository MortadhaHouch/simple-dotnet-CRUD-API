using CommentHookInterface;
using CommentModel;
using MongoDB.Bson;
using MongoDB.Driver;
using PostModel;

namespace CommentModelService
{
    public class CommentService : ICommentHook{
        private readonly IMongoCollection<Comment> commentsCollection;
        private readonly IMongoCollection<Post> postsCollection;
        public CommentService(IMongoDatabase database){
            commentsCollection = database.GetCollection<Comment>("comment") ?? throw new ArgumentNullException(nameof(database));
            postsCollection = database.GetCollection<Post>("post") ?? throw new ArgumentNullException(nameof(database));
        }
        public async Task<Comment> GetCommentByIdAsync(ObjectId id){
            return await commentsCollection.Find(c=>c.Id == id).FirstAsync();
        }
        public async Task<List<Comment>> GetCommentsAsync(int? p){
            List<Comment> comments = await commentsCollection.Find(c=>true).ToListAsync();
            if(comments is not null){
                if(p is not null){
                    return comments.Skip(p.Value*10).Take(10).ToList();
                }else{
                    return comments;
                }
            }else{
                return new List<Comment>();
            }
        }
        public async Task<Comment> AddCommentAsync(Comment comment){
            await commentsCollection.InsertOneAsync(comment);
            return comment;
        }
        public async Task UpdateCommentAsync(ObjectId id, Comment updatedComment){
            await commentsCollection.ReplaceOneAsync(c=>c.Id == id, updatedComment);
        }
        public async Task DeleteCommentAsync(ObjectId id){
            await commentsCollection.DeleteOneAsync(c=>c.Id == id);
        }
        //HOOKS
        public void OnBeforeCommentCreated(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void OnAfterCommentCreated(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void OnBeforeCommentUpdated(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void OnAfterCommentUpdated(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void OnAfterCommentRetrieved(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void OnBeforeCommentRetrieved(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void OnAfterCommentDeleted(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async void OnBeforeCommentDeleted(Comment comment)
        {
            foreach (ObjectId id in comment.Replies)
            {
                await commentsCollection.DeleteOneAsync(c=>c.Id == id);
            }
        }
    }
}