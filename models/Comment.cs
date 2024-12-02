using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using UserModel;
namespace CommentModel
{
    public class Comment{
        [Required(ErrorMessage = "First Name should not be empty")]
        [MaxLength(100, ErrorMessage = "Last Name should 100 characters length or less")]
        public string Content { get; set; }
        public ObjectId Id { get; set; } = new ObjectId();
        public int Views { get; set; } = 0;
        public List<User> Likers { get; set; } = new List<User>();
        public List<User> Dislikers { get; set; } = new List<User>();
        public List<ObjectId> Replies { get; set; } = new List<ObjectId>();
        Comment(string Content){
            this.Content = Content;
        }

        public string GetComment(){
            return Content;
        }
        public ObjectId GetId(){
            return Id;
        }
        public List<User> GetLikers(){
            return Likers;
        }
        public List<User> GetDislikers(){
            return Dislikers;
        }
        public void AddLiker(User user){
            Likers.Add(user);
        }
        public void AddDisliker(User user){
            Dislikers.Add(user);
        }
        public void RemoveLiker(User user){
            Likers.Remove(user);
        }
        public void RemoveDisliker(User user){
            Dislikers.Remove(user);
        }
        public void IncreaseViews(){
            Views++;
        }
        public void DecreaseViews(){
            if(Views > 0)
                Views--;
        }
        public int GetViews(){
            return Views;
        }
    }
}