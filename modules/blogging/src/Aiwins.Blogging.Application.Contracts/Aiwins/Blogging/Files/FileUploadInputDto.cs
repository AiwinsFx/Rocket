using System.ComponentModel.DataAnnotations;

namespace Aiwins.Blogging.Files
{
    public class FileUploadInputDto
    {
        [Required]
        public byte[] Bytes { get; set; }

        [Required]
        public string Name { get; set; }
    }
}