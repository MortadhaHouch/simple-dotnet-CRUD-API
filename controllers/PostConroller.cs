using DatabaseConfig;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PostModel;
using PostModelService;

namespace PostControllerRoute.API
{
    public class PostController : ControllerBase{
        readonly List<Post> posts = new List<Post>();
        private static DBConfig dBConfig = new DBConfig();
        private static IMongoDatabase userDB = dBConfig.GetDatabase();
        PostService userService = new PostService(userDB);
        public RouteGroupBuilder Map(WebApplication application){
            RouteGroupBuilder postRouteGroup = application.MapGroup("/post");
            postRouteGroup.MapGet("/",()=>posts);
            postRouteGroup.MapGet("/{postId}",(ObjectId postId)=>{
                return posts.Find(p=>p.Id == postId);
            });
            postRouteGroup.MapPost("/add",([FromBody]Post post)=>{
                posts.Add(post);
                return post;
            });
            postRouteGroup.MapPut("/edit/{postId}",(ObjectId postId,[FromBody] Post updatedPost)=>{
                Post? foundPost = posts.Find(p=>p.Id == postId);
                if(foundPost is not null){
                    foundPost.Content = updatedPost.Content;
                    foundPost.Title = updatedPost.Title;
                    foundPost.UpdatedAt = DateTime.Now;
                    return updatedPost;
                }else{
                    return null;
                }
            });
            postRouteGroup.MapDelete("/delete/{postId}",(ObjectId postId)=>{
                posts.RemoveAll(p=>p.Id == postId);
                return "post has been deleted";
            });
            return postRouteGroup;
        }
    }
}