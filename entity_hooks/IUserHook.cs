using UserModel;

namespace UserHookInterface
{
    public interface IUserHook{
        public void OnBeforeUserCreated(User user);
        public void OnAfterUserCreated(User user);
        public void OnBeforeUserUpdated(User user);
        public void OnAfterUserUpdated(User user);
        public void OnAfterUserRetrieved(User user);
        public void OnBeforeUserRetrieved(User user);
        public void OnAfterUserDeleted(User user);
        public void OnBeforeUserDeleted(User user);
    }
}