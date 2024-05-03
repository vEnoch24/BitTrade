namespace BitTrade.Dto
{
    public class UserDto
    {
        public UserDto(Guid Id, string Name, string Email, bool isOnline = false)
        {
            this.Id = Id;
            this.Name = Name;
            this.Email = Email;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
