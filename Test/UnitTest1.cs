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
            var result = musicRecordsList.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
