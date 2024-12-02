using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using PostModel;
namespace UserModel{
    public class User{
        [Required(ErrorMessage = "First Name should not be empty")]
        [MaxLength(50, ErrorMessage = "Last Name should 50 characters length or less")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "First Name should not be empty")]
        [MaxLength(50, ErrorMessage = "Last Name should 50 characters length or less")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email should not be empty")]
        [MaxLength(50, ErrorMessage = "Email should 50 characters length or less")]
        public string Email { get; set; }
        [Required(ErrorMessage = "First Name should not be empty")]
        [MaxLength(50, ErrorMessage = "Last Name should 50 characters length or less")]
        public string Password { get; set; }
        public ObjectId Id { get; set; } = new ObjectId();
        public List<ObjectId> Comments { get; set; } = new List<ObjectId>();
        public List<ObjectId> Likes { get; set; } = new List<ObjectId>();
        public List<ObjectId> Dislikes { get; set; } = new List<ObjectId>();
        public List<ObjectId> Followers { get; set; } = new List<ObjectId>();
        public List<ObjectId> Following { get; set; } = new List<ObjectId>();
        public int Views { get; set; } = 0;
        public int SearchAppearances { get; set; } = 0;
        public List<ObjectId> Posts { get; set; } = new List<ObjectId>();
        public int __V { get; set; } = 0;
        public int __PV { get; set; } = 0;
        public User(string firstName, string lastName, string email, string password){
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Id = new ObjectId();
        }
    }
}