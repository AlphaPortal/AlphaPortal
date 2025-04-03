namespace Domain.Responses;

public class UserResult : ResponseResult
{

}

public class UserResult<T> : RepositoryResult
{
    public T? Result { get; set; }
}