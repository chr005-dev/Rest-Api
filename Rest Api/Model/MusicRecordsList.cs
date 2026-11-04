namespace Rest_Api.Model
{
    public class MusicRecordsList : IMusicRecords
    {
        private int _nextId = 1;
        private readonly List<MusicRecord> _musicRecords = new();

        public MusicRecordsList() // Skal rettes til at vi bruger vores add metode senere
        {
            _musicRecords.Add(new MusicRecord { Title = "Song 1", Artist = "Artist 1", Duration = 180, PublicationYear = 2020 });
            _musicRecords.Add(new MusicRecord { Title = "Song 2", Artist = "Artist 2", Duration = 200, PublicationYear = 2021 });
        }
    }
}
