namespace Permitt.Services
{
    public abstract class PermissionResponseBase<TRequest> where TRequest : PermissionRequestBase
    {
        public PermissionResponseBase(TRequest request)
        {
            Request = request;
        }
        public bool Permitted { get; set; }
        public TRequest Request { get; }
    }
}