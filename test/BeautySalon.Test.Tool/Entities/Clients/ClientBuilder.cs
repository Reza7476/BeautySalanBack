using BeautySalon.Entities.Clients;

namespace BeautySalon.Test.Tool.Entities.Clients;
public class ClientBuilder
{
    private readonly Client _client;


    public ClientBuilder()
    {
        _client = new Client()
        {
            Id=Guid.NewGuid().ToString(),
            CreatedAt = DateTime.Now,
        };
    }

    public ClientBuilder WithUser(string id)
    {
        _client.UserId = id;
        return this;
    }

    public Client Build()
    {
        return _client;
    }

}
