using System.ComponentModel.DataAnnotations.Schema;     // To use the [NotMapped] attribute

namespace NZWalksAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]     // Because we're not going to store the file in the database, we will store it in the file system. So, we don't need to map this property to the database.
        public IFormFile File { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
