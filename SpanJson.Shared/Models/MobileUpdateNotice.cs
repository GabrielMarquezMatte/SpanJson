namespace SpanJson.Shared.Models
{
    public sealed class MobileUpdateNotice : IGenericEquality<MobileUpdateNotice>
    {
        public bool? should_update { get; set; }

        public string message { get; set; }

        public string minimum_supported_version { get; set; }

        public bool Equals(MobileUpdateNotice obj)
        {
            return string.Equals(message, obj.message, System.StringComparison.Ordinal) &&
string.Equals(minimum_supported_version, obj.minimum_supported_version, System.StringComparison.Ordinal) &&
                should_update == obj.should_update;
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return string.Equals(message, (string)obj.message, System.StringComparison.Ordinal) &&
string.Equals(minimum_supported_version, (string)obj.minimum_supported_version, System.StringComparison.Ordinal) &&
                should_update == (bool?) obj.should_update;
        }
    }
}