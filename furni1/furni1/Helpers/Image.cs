namespace furni1.Helpers
{
    public static class Image
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }
        public static bool IsMoreThan2Mb(this IFormFile file)
        {
            return file.Length / 1024 > 2*1024;
        }
        public static async Task<string> SaveFileAsync(this IFormFile file, string folder)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;

            string path = Path.Combine(folder, filename);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filename;
        }
    }
}
