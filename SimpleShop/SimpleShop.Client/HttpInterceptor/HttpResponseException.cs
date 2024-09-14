namespace SimpleShop.Client.HttpInterceptor;

[Serializable]
public class HttpResponseException : Exception
{
    public HttpResponseException()
    {
            
    }

    public HttpResponseException(string message) : base(message)
    {
        
    }


}
