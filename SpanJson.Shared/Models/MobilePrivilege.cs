namespace SpanJson.Shared.Models
{
    public sealed class MobilePrivilege : IMobileFeedBase<MobilePrivilege>
    {
        public string site { get; set; }

        public string privilege_short_description { get; set; }

        public string privilege_long_description { get; set; }

        public int? privilege_id { get; set; }

        public int? reputation_required { get; set; }

        public string link { get; set; }

        public int? group_id { get; set; }

        public long? added_date { get; set; }

        public bool Equals(MobilePrivilege obj)
        {
            return
                added_date == obj.added_date &&
                group_id == obj.group_id &&
string.Equals(link, obj.link, System.StringComparison.Ordinal) &&
                privilege_id == obj.privilege_id &&
string.Equals(privilege_long_description, obj.privilege_long_description, System.StringComparison.Ordinal) &&
string.Equals(privilege_short_description, obj.privilege_short_description, System.StringComparison.Ordinal) &&
                reputation_required == obj.reputation_required &&
string.Equals(site, obj.site, System.StringComparison.Ordinal);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                group_id == (int?) obj.group_id &&
string.Equals(link, (string)obj.link, System.StringComparison.Ordinal) &&
                privilege_id == (int?) obj.privilege_id &&
string.Equals(privilege_long_description, (string)obj.privilege_long_description, System.StringComparison.Ordinal) &&
string.Equals(privilege_short_description, (string)obj.privilege_short_description, System.StringComparison.Ordinal) &&
                reputation_required == (int?) obj.reputation_required &&
string.Equals(site, (string)obj.site, System.StringComparison.Ordinal);
        }
    }
}