using System.Collections.ObjectModel;
using DatabaseConfig;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using UserModel;
using UserModelService;
namespace UserControllerRoute.API
{
    public class UserController: ControllerBase{
        private static DBConfig dBConfig = new DBConfig();
        private static IMongoDatabase userDB = dBConfig.GetDatabase();
        UserService userService = new UserService(userDB);
        public RouteGroupBuilder Map(WebApplication application){
            RouteGroupBuilder userRouterBuilder = application.MapGroup("/user");
            userRouterBuilder.MapGet("/get",async ()=>{
                List<User> users = await userService.GetUsersAsync();
                return users;
            });
            userRouterBuilder.MapGet("/get/{id}",async (ObjectId id)=>{
                User? user = await userService.GetByIdAsync(id);
                return user;
            });
            userRouterBuilder.MapPost("/add",async ([FromBody] User user)=>{
                await userService.AddAsync(user);
                return user;
            });
            userRouterBuilder.MapPut("/edit/{id}",async (ObjectId id,[FromBody] User user)=>{
                return await userService.UpdateAsync(id,user);
            });
            userRouterBuilder.MapDelete("/delete/{id}",async(ObjectId id)=>{
                return await userService.DeleteByIdAsync(id);
            });
            return userRouterBuilder;
        }
    }
}