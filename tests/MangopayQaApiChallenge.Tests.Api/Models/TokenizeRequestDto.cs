namespace MangopayQaApiChallenge.Tests.Api.Models;

public class TokenizeRequestDto
{
    public string AccessKeyRef { get; set; } = null!;

    public string Data { get; set; } = null!;
    
    public long CardNumber { get; set; }
    
    public int CardExpirationDate { get; set; }
    
    public int CardCvx { get; set; }

    public string Url { get; set; } = null!;
}