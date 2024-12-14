namespace CompanyManager.UI.Extensions
{
    public static class FormFilesExtensions
    {
        public static async Task<byte[]> ReadContentAsBytesAsync(this IFormFile file, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(file);
            if (file.Length == 0)
            {
                return [];
            }
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }
}