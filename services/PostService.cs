using CommentModel;
using MongoDB.Bson;
using MongoDB.Driver;
using PostHookInterface;
using PostModel;

namespace PostModelService
{
    public class PostService : IPostHook
    {
        private readonly IMongoCollection<Post> _postCollection;
        private readonly IMongoCollection<Comment> _commentCollection;

        public PostService(IMongoDatabase database)
        {
            _postCollection = database.GetCollection<Post>("post") ?? throw new ArgumentNullException(nameof(database));
            _commentCollection = database.GetCollection<Comment>("comment") ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<List<Post>> GetAllPosts(int limit = 10, int offset = 0)
        {
            return await _postCollection.Find(post => true)
                .Skip(offset)
                .Limit(limit)
                .ToListAsync();
        }

        public async Task<Post?> GetPostById(ObjectId postId)
        {
            return await _postCollection.Find(post => post.Id == postId).FirstOrDefaultAsync();
        }

        public async Task<Post> AddPost(Post post)
        {
            await _postCollection.InsertOneAsync(post);
            return post;
        }

        public async Task<(Post? p, string message)> UpdatePost(ObjectId postId, Post updatedPost)
        {
            try
            {
                Post? foundPost = await GetPostById(postId);
                if (foundPost is not null)
                {
                    await _postCollection.ReplaceOneAsync(post => post.Id == postId, updatedPost);
                    return (updatedPost, "Post updated successfully.");
                }
                else
                {
                    return (null, "Post not found.");
                }
            }
            catch (Exception ex)
            {
                return (null, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<string> DeletePost(ObjectId postId)
        {
            try
            {
                var result = await _postCollection.DeleteOneAsync(post => post.Id == postId);
                return result.DeletedCount > 0 ? "Post deleted successfully." : "Post not found.";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
        //HOOKS
        public void OnBeforeUserCreated(Post post)
        {
            throw new NotImplementedException();
        }

        public void OnAfterUserCreated(Post post)
        {
            throw new NotImplementedException();
        }

        public void OnBeforeUserUpdated(Post post)
        {
            throw new NotImplementedException();
        }

        public void OnAfterUserUpdated(Post post)
        {
            throw new NotImplementedException();
        }

        public void OnAfterUserRetrieved(Post post)
        {
            throw new NotImplementedException();
        }

        public void OnBeforeUserRetrieved(Post post)
        {
            throw new NotImplementedException();
        }

        public void OnAfterUserDeleted(Post post)
        {
            throw new NotImplementedException();
        }

        public async void OnBeforeUserDeleted(Post post){
            foreach(ObjectId id in post.Comments){
                await _commentCollection.DeleteOneAsync(c=>c.Id == id);
            }
        }
    }
}
