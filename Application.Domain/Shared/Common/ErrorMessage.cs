using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Common
{
    public static class ErrorMessage
    {
        public const string EMPTY_RECORD = "Record found but no associated data";
        public const string RECORD_NOT_FOUND = "Record not found";
        public const string NO_EXIST_PROVIDER = "This provider doesn't exist";
        public const string NOT_FOUND_PRODUCT = "This product isn't found !";
        public const string PRODUCT_NOT_DISCOUNT = "Products haven't applied disount code";
    }
}
