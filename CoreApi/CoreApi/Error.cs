using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi
{
    public class Error
    {
        public Errors error { get; set; }
        public void Init()
        {
            switch (error)
            {
                case Errors.BadAuth:
                    throw new AuthExeption();
                case Errors.BadToken:
                    throw new TokenExeption();
                case Errors.BadParams:
                    throw new ParamsExeption();
                case Errors.BadStock:
                    throw new StockExeption();
                default:
                    return;
            }
        }
    }

    public enum Errors
    {
        BadStock = 25632,
        BadAuth = 1,
        BadToken,
        BadParams
    }
    public class AuthExeption : Exception{ }
    public class TokenExeption : Exception{ }
    public class ParamsExeption : Exception{ }
    public class StockExeption : Exception{ }

}
