namespace WebAppApiJWT.Model
{
    public class UserLogin
    {
        public string UserName {get; set; }
        public string Email {get; set; }
        public string Token {get; set; }
        public int Expire{ get; set; }
    }
}
