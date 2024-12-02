using UserControllerRoute.API;
using CommentControllerRoute.API;
using PostControllerRoute.API;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();
WebApplication application = builder.Build();
CommentController commentController = new CommentController();
UserController userController = new UserController();
PostController postController = new PostController();
commentController.Map(application);
userController.Map(application);
postController.Map(application);
if (application.Environment.IsDevelopment()){
    application.UseSwagger();
    application.UseSwaggerUI();
}
application.MapControllers();
application.MapGet("/", (HttpRequest request) => {
    return request.QueryString;
});
application.Run();
