namespace Application.Responses.Account
{
    public class LoginAccountResponse
    {

        public string userId { get; set; }
        public List<string> claims { get; set; }
        public List<string> roles { get; set; }
    }
}
