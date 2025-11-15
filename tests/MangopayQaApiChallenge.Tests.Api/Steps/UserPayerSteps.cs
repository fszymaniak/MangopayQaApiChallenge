namespace MangopayQaApiChallenge.Tests.Api.Steps;

public class UserPayerSteps
{
    public async Task<UserNaturalDTO> CreateUserViaPostApiCallAsync(IUserFactory userFactory, MangoPayApi api)
    {
        var userNaturalRequestData = userFactory.CreateValidUser();
        var userNaturalResponse = await api.Users.CreatePayerAsync(userNaturalRequestData);

        return userNaturalResponse;
    }
}