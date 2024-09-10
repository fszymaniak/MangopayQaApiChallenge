namespace MangopayQaApiChallenge.Tests.Api.Steps;

public class UserPayerSteps
{
    public UserPayerSteps()
    {
    }
    
    public async Task<UserNaturalDTO> CreateUserViaPostApiCall(IUserFactory userFactory, MangoPayApi api)
    {
        var userNaturalRequestData = userFactory.CreateValidUser();
        var userNaturalResponse = await api.Users.CreatePayerAsync(userNaturalRequestData);
        
        return userNaturalResponse;
    }
}