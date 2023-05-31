namespace WebClient
{
    public class CustomerCreateRequest
    {
        public CustomerCreateRequest()
        {
        }

        public CustomerCreateRequest(int id, string firstName, string lastName)
        {
            Id = id;
            Firstname = firstName;
            Lastname = lastName;
        }

        public int Id { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
    }
}