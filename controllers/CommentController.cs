using Microsoft.AspNetCore.Mvc;
using CommentModel;
using MongoDB.Bson;
namespace CommentControllerRoute.API
{
    public class CommentController: ControllerBase{
        readonly List<Comment> comments = new List<Comment>();
        public RouteGroupBuilder Map(WebApplication application){
            RouteGroupBuilder commentRouteBuilder = application.MapGroup("/comment");
            commentRouteBuilder.MapGet("/get", () => comments);
            // Get a specific comment by ID
            commentRouteBuilder.MapGet("/get/{id}", (ObjectId id) => {
                var foundComment = comments.Find(c => c.Id == id);
                return foundComment is not null ? Results.Ok(foundComment) : Results.NotFound("Comment not found");
            });

            // Add a new comment
            commentRouteBuilder.MapPost("/add", (Comment comment) => {
                comments.Add(comment);
                return Results.Created($"/{comment.Id}", comment);
            });

            // Edit an existing comment
            commentRouteBuilder.MapPut("/edit/{id}", (ObjectId id, Comment comment) => {
                var existingComment = comments.Find(c => c.Id == id);
                if (existingComment is not null) {
                    existingComment.Content = comment.Content;
                    existingComment.Views++;
                    return Results.Ok(existingComment);
                } else {
                    return Results.NotFound("Comment not found");
                }
            });

            // Delete a comment by ID
            commentRouteBuilder.MapDelete("/delete/{id}", (ObjectId id) => {
                var foundComment = comments.Find(c => c.Id == id);
                if (foundComment is not null) {
                    comments.Remove(foundComment);
                    return Results.Ok("Comment successfully deleted");
                } else {
                    return Results.NotFound("Comment not found");
                }
            });
            return commentRouteBuilder;
        } 
    }
}