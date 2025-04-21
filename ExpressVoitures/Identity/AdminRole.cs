namespace Microsoft.AspNetCore.Identity
{
    public class AdminRole : IdentityRole
    {
        public override string Id { get; set; } = new Guid().ToString();
        public override string? Name { get; set; } = "Admin";
        public override string? NormalizedName { get; set; } = "Admin";
        public override string? ConcurrencyStamp { get; set; } = "Admin";
    }
}