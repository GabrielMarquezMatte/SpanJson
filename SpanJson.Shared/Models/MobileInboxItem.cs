namespace SpanJson.Shared.Models
{
    public sealed class MobileInboxItem : IMobileFeedBase<MobileInboxItem>
    {
        public int? answer_id { get; set; }

        public string body { get; set; }

        public int? comment_id { get; set; }

        public long? creation_date { get; set; }

        public string item_type { get; set; }

        public string link { get; set; }

        public int? question_id { get; set; }

        public string title { get; set; }

        public string site { get; set; }

        public int? group_id { get; set; }

        public long? added_date { get; set; }

        public bool Equals(MobileInboxItem obj)
        {
            return
                added_date == obj.added_date &&
                answer_id == obj.answer_id &&
string.Equals(body, obj.body, System.StringComparison.Ordinal) &&
                comment_id == obj.comment_id &&
                creation_date == obj.creation_date &&
                group_id == obj.group_id &&
string.Equals(item_type, obj.item_type, System.StringComparison.Ordinal) &&
string.Equals(link, obj.link, System.StringComparison.Ordinal) &&
                question_id == obj.question_id &&
string.Equals(site, obj.site, System.StringComparison.Ordinal) &&
string.Equals(title, obj.title, System.StringComparison.Ordinal);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                answer_id == (int?) obj.answer_id &&
string.Equals(body, (string)obj.body, System.StringComparison.Ordinal) &&
                comment_id == (int?) obj.comment_id &&
                creation_date == (long?) obj.creation_date &&
                group_id == (int?) obj.group_id &&
string.Equals(item_type, (string)obj.item_type, System.StringComparison.Ordinal) &&
string.Equals(link, (string)obj.link, System.StringComparison.Ordinal) &&
                question_id == (int?) obj.question_id &&
string.Equals(site, (string)obj.site, System.StringComparison.Ordinal) &&
string.Equals(title, (string)obj.title, System.StringComparison.Ordinal);
        }
    }
}