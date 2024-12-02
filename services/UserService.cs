using CommentModel;
using MongoDB.Bson;
using MongoDB.Driver;
using PostModel;
using UserHookInterface;
using UserModel;

namespace UserModelService
{
    public class UserService: IUserHook
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Post> _postCollection;
        private readonly IMongoCollection<Comment> _commentCollection;
        public UserService(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("user") ?? throw new ArgumentNullException(nameof(database));
            _postCollection = database.GetCollection<Post>("post") ?? throw new ArgumentNullException(nameof(database));
            _commentCollection = database.GetCollection<Comment>("comment") ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(ObjectId id)
        {
            return await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<(User? u, string? ex)> AddAsync(User user)
        {
            try
            {
                User? userFoundByEmail = await GetUserByEmail(user.Email);
                if (userFoundByEmail is not null)
                {
                    return (null, "User with this email already exists.");
                }

                await _userCollection.InsertOneAsync(user);
                return (user, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<string> DeleteByIdAsync(ObjectId id)
        {
            var result = await _userCollection.DeleteOneAsync(u => u.Id == id);
            return result.DeletedCount > 0 ? "User has been deleted." : "The user to be deleted does not exist.";
        }

        public async Task<User?> UpdateAsync(ObjectId id, User updatedUser)
        {
            var result = await _userCollection.ReplaceOneAsync(u => u.Id == id, updatedUser);
            return result.IsAcknowledged && result.ModifiedCount > 0 ? updatedUser : null;
        }

        public async Task<List<User>> GetUsersByFirstName(string firstName)
        {
            return await _userCollection.Find(u => u.FirstName == firstName).ToListAsync();
        }

        public async Task<List<User>> GetUsersByLastName(string lastName)
        {
            return await _userCollection.Find(u => u.LastName == lastName).ToListAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByFullMatch(User user)
        {
            return await _userCollection
                .Find(u =>
                    u.FirstName == user.FirstName &&
                    u.LastName == user.LastName &&
                    u.Email == user.Email)
                .FirstOrDefaultAsync();
        }
        //HOOKS
        public void OnBeforeUserCreated(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }

        public void OnAfterUserCreated(User user)
        {
            Console.WriteLine("before-creation-hook invoked");
        }

        public void OnBeforeUserUpdated(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        }

        public void OnAfterUserUpdated(User user)
        {
            Console.WriteLine("before-creation-hook invoked");
        }

        public void OnAfterUserRetrieved(User user)
        {
            Console.WriteLine("after-retrieval-hook invoked");
        }

        public void OnBeforeUserRetrieved(User user)
        {
            Console.WriteLine("before-retrieval-hook invoked");
        }

        public void OnAfterUserDeleted(User user)
        {
            throw new NotImplementedException();
        }

        public async void OnBeforeUserDeleted(User user){
            foreach(ObjectId id in user.Posts){
                await _postCollection.DeleteOneAsync(p=>p.Id == id);
            }
        }
    }
}