namespace Rest_Api.Model
{
    public interface IMusicRecords
    {
        public IEnumerable<MusicRecord> GetAll();
    }
}
