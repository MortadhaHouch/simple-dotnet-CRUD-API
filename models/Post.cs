using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
namespace PostModel
{
    public class Post{
        public ObjectId Id { get; set; } = new ObjectId();
        [Required(ErrorMessage = "First Name should not be empty")]
        [MaxLength(50, ErrorMessage = "Last Name should 50 characters length or less")]
        public string Title { get; set; }
        [Required(ErrorMessage = "First Name should not be empty")]
        [MaxLength(100, ErrorMessage = "Last Name should 100 characters length or less")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt{ get; set; }
        public List<ObjectId> Likers { get; set; } = new List<ObjectId>();
        public List<ObjectId> Dislikers { get; set; } = new List<ObjectId>();
        public List<ObjectId> Comments { get; set; } = new List<ObjectId>();
        public int ShareCount { get; set; } = 0 ;
        public int SaveCount { get; set; } = 0 ;
        Post(string title, string content){
            this.Title = title;
            this.Content = content;
        }

        public override string ToString(){
            return $"Post ID: {Id}, Title: {Title}, Content: {Content}, Created At: {CreatedAt}";
        }
        public List<ObjectId> GetComments(){
            return Comments;
        }
        public void EditPost(string newTitle, string newContent){
            Title = newTitle;
            Content = newContent;
            UpdatedAt = DateTime.Now;
        }
        public int GetNumberOfComments(){
            return Comments.Count;
        }
        public ObjectId GetId(){
            return Id;
        }
        public void AddComment(ObjectId comment){
            Comments.Add(comment);
        }
        public ObjectId? GetCommentById(ObjectId commentId){
            return Comments.Find(comment => comment == commentId);
        }
        public void DeleteCommentById(ObjectId commentId){
            Comments.RemoveAll(comment => comment == commentId);
        }
        public string GerTitle(){
            return Title;
        }
        public string GetContent(){
            return Content;
        }
        public DateTime GetCreatedAt(){
            return CreatedAt;
        }
        public DateTime? GetUpdatedAt(){
            return UpdatedAt;
        }
    }
}