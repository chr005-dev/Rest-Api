namespace Rest_Api.Model
{
    public interface IMusicRecords
    {
        public IEnumerable<MusicRecord> GetAllMusicRecords(string? titleFilter = null,string? artistFilter = null,int? publicationYear = null);
    }
}
