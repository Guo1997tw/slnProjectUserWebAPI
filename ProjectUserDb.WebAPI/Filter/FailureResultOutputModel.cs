namespace ProjectUser.WebAPI.Filter
{
    internal class FailureResultOutputModel
    {
        public object? Id { get; set; }
        public object? ApiVersion { get; set; }
        public string? Method { get; set; }
        public string? Status { get; set; }
        public List<FailureInformation>? Errors { get; set; }
    }
}