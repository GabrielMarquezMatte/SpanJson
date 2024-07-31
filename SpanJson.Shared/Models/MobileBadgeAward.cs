namespace SpanJson.Shared.Models
{
    public sealed class MobileBadgeAward : IMobileFeedBase<MobileBadgeAward>
    {
        public enum BadgeRank : byte
        {
            bronze = 1,
            silver = 2,
            gold = 3
        }

        public enum BadgeType
        {
            named = 1,
            tag_based = 2
        }

        public string site { get; set; }

        public string badge_name { get; set; }

        public string badge_description { get; set; }

        public int? badge_id { get; set; }

        public int? post_id { get; set; }

        public string link { get; set; }

        public BadgeRank? rank { get; set; }

        public BadgeType? badge_type { get; set; }

        public int? group_id { get; set; }

        public long? added_date { get; set; }

        public bool Equals(MobileBadgeAward obj)
        {
            return
                added_date == obj.added_date &&
string.Equals(badge_description, obj.badge_description, System.StringComparison.Ordinal) &&
                badge_id == obj.badge_id &&
string.Equals(badge_name, obj.badge_name, System.StringComparison.Ordinal) &&
                badge_type == obj.badge_type &&
                group_id == obj.group_id &&
string.Equals(link, obj.link, System.StringComparison.Ordinal) &&
                post_id == obj.post_id &&
                rank == obj.rank &&
string.Equals(site, obj.site, System.StringComparison.Ordinal);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
string.Equals(badge_description, (string)obj.badge_description, System.StringComparison.Ordinal) &&
                badge_id == (int?) obj.badge_id &&
string.Equals(badge_name, (string)obj.badge_name, System.StringComparison.Ordinal) &&
                badge_type == (BadgeType?) obj.badge_type &&
                group_id == (int?) obj.group_id &&
string.Equals(link, (string)obj.link, System.StringComparison.Ordinal) &&
                post_id == (int?) obj.post_id &&
                rank == (BadgeRank?) obj.rank &&
string.Equals(site, (string)obj.site, System.StringComparison.Ordinal);
        }
    }
}