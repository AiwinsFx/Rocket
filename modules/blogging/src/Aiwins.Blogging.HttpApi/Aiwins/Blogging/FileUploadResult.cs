namespace Aiwins.Blogging.Areas.Blog.Models
{
    public class FileUploadResult
    {
        public string FileUrl { get; set; }

        public FileUploadResult(string fileUrl)
        {
            FileUrl = fileUrl;
        }
    }
}