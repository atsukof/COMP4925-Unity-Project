using System;

[System.Serializable]
public class ApiResponse<T>
{
    public bool ok;
    public string msg;
    public T data;
}

[Serializable]
public class TokenData {
    public string accessToken;
    public string refreshToken;
}