using Rest_Api.Model;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Getall_ReturnsAllMusicRecords()
        {
            // Arrange

            var musicRecordsList = new MusicRecordsList(includeData: true);

            // Act
            var result = musicRecordsList.GetAllMusicRecords();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllMusicRecords_WithTitleFilter_ReturnsMatchingRecords()
        {
            // Arrange
            var musicRecordsList = new MusicRecordsList(includeData: true);
            var allRecords = musicRecordsList.GetAllMusicRecords().ToList();
            var title = allRecords.First().Title;
            var expected = allRecords
                .Where(record => record.Title.Contains(
                    title, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Act
            var result = musicRecordsList
                .GetAllMusicRecords(titleFilter: title.ToUpperInvariant())
                .ToList();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAllMusicRecords_WithArtistAndYear_ReturnsMatchingRecords()
        {
            // Arrange
            var musicRecordsList = new MusicRecordsList(includeData: true);
            var allRecords = musicRecordsList.GetAllMusicRecords().ToList();
            var firstRecord = allRecords.First();
            var expected = allRecords
                .Where(record => record.Artist.Contains(
                    firstRecord.Artist, StringComparison.OrdinalIgnoreCase)
                    && record.PublicationYear == firstRecord.PublicationYear)
                .ToList();

            // Act
            var result = musicRecordsList.GetAllMusicRecords(
                artistFilter: firstRecord.Artist,
                publicationYear: firstRecord.PublicationYear).ToList();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAllMusicRecords_WithoutMatches_ReturnsEmptyResult()
        {
            // Arrange
            var musicRecordsList = new MusicRecordsList(includeData: true);

            // Act
            var result = musicRecordsList.GetAllMusicRecords(titleFilter: "xyz123");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
