using TableSearch.Service.Validation.Error;
using TableSearch.Service.Validation.Restriction;
using TableSearch.Shared.Functional;
using TableSearch.Shared.MethodResult;

namespace TableSearch.Service.Validation.SearchValidator
{
    public class SearchValidator
    {
        public static MethodResult<bool> ValidateThatSearchTextIsLongEnough(string randomString)
        {
            return 
                When<MethodResult<bool>>
                    .True(randomString.Length >= SearchRestriction.MinimumSearchTestLength)
                    .Then(() => new MethodResult<bool>())
                    .Else(() => new MethodResult<bool>().AddErrorMessage(string.Format(SearchValidationError.SearchTextIsTooShort, SearchRestriction.MinimumSearchTestLength)));
        }
    }
}