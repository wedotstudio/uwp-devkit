using System;

namespace WeCode_Next.DataModel
{
    [Flags]
    public enum TypeOfSource
    {
        PublicRelease = 0,
        InternalLeak = 1,
        UpdateGDR = 2,
        UpdateLDR = 3,
        AppPackage = 4,
        BuildTools = 5,
        Documentation = 6,
        Logging = 7,
        PrivateLeak = 8
    }
    public class APIs
    {
        public string Title { get; set; }
        public string PubDate { get; set; }
        public string Link { get; set; }
    }
    public class BuildFeedItem
    {
        public string Id { get; set; }
        public string FullBuildString { get; set; }
        public string AlternateBuildString { get; set; }
        public string LabUrl { get; set; }
        public bool IsLeaked { get; set; }
        public string Added { get; set; }
        public string Modified { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int Number { get; set; }
        public int Revision { get; set; }
        public string Lab { get; set; }
        public string BuildTime { get; set; }
        public TypeOfSource SourceType { get; set; }
        public object SourceDetails { get; set; }
        public object LeakDate { get; set; }
    }
}
