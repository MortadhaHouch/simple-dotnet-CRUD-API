using CommentModel;

namespace CommentHookInterface
{
    public interface ICommentHook{
        public void OnBeforeCommentCreated(Comment comment);
        public void OnAfterCommentCreated(Comment comment);
        public void OnBeforeCommentUpdated(Comment comment);
        public void OnAfterCommentUpdated(Comment comment);
        public void OnAfterCommentRetrieved(Comment comment);
        public void OnBeforeCommentRetrieved(Comment comment);
        public void OnAfterCommentDeleted(Comment comment);
        public void OnBeforeCommentDeleted(Comment comment);
    }
}