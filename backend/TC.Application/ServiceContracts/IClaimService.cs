namespace TC.Application.ServiceContracts
{
    public interface IClaimService
    {
        bool IsAuthenticated { get; }

        string UserId { get; }

        string Name { get; }

        string Email { get; }

        string IpAddress { get; }
    }
}
