namespace RestBnb.Core.Contracts.V1.Requests.Auth
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}