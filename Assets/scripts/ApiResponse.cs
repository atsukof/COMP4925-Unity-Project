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
    public string userId;
    public string username;
    public string accessToken;
    public string refreshToken;
}

[Serializable]
public class FastestTimeData {
    public float timerSeconds;
}

[Serializable]
public class TotalScoreData
{
    public int totalScore;
}

[Serializable]
public class LevelCompleteData
{
    public int levelCompleted;
}