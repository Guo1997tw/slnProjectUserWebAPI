namespace ProjectUser.WebAPI.Filter
{
    internal class FailureInformation
    {
        public int? ErrorCode { get; internal set; }
        public string? Description { get; internal set; }
        public string? Message { get; internal set; }
    }
}