namespace SpanJson.Shared.Models
{
    public sealed class MobileCareersJobAd : IMobileFeedBase<MobileCareersJobAd>
    {
        public int? job_id { get; set; }

        public string link { get; set; }

        public string company_name { get; set; }

        public string location { get; set; }

        public string title { get; set; }

        public int? group_id { get; set; }

        public long? added_date { get; set; }

        public bool Equals(MobileCareersJobAd obj)
        {
            return
                added_date == obj.added_date &&
string.Equals(company_name, obj.company_name, System.StringComparison.Ordinal) &&
                group_id == obj.group_id &&
                job_id == obj.job_id &&
string.Equals(link, obj.link, System.StringComparison.Ordinal) &&
string.Equals(location, obj.location, System.StringComparison.Ordinal) &&
string.Equals(title, obj.title, System.StringComparison.Ordinal);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
string.Equals(company_name, (string)obj.company_name, System.StringComparison.Ordinal) &&
                group_id == (int?) obj.group_id &&
                job_id == (int?) obj.job_id &&
string.Equals(link, (string)obj.link, System.StringComparison.Ordinal) &&
string.Equals(location, (string)obj.location, System.StringComparison.Ordinal) &&
string.Equals(title, (string)obj.title, System.StringComparison.Ordinal);
        }
    }
}