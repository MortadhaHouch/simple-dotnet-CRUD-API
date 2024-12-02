using PostModel;

namespace PostHookInterface
{
    public interface IPostHook{
        public void OnBeforeUserCreated(Post post);
        public void OnAfterUserCreated(Post post);
        public void OnBeforeUserUpdated(Post post);
        public void OnAfterUserUpdated(Post post);
        public void OnAfterUserRetrieved(Post post);
        public void OnBeforeUserRetrieved(Post post);
        public void OnAfterUserDeleted(Post post);
        public void OnBeforeUserDeleted(Post post);
        
    }
}