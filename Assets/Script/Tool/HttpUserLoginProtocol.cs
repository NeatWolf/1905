using System;

[Serializable]
public class HttpUserLoginProtocol
{
    [Serializable]
    public class TokenData
    {
        public string Token;
    }

    public int Code;
    public TokenData Data;
}
