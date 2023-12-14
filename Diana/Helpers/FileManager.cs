namespace Diana.Helpers
{
    public static class FileManager
    {
        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckLength(this IFormFile file, int length)
        {
            return file.Length <= length * 1024;
        }

        public static string Upload(this IFormFile file, string env, string folder_name)
        {
            string file_name = file.FileName;
            if (file_name.Length > 64)
            {
                file_name = file_name.Substring(file_name.Length-64);
            }

            file_name =Guid.NewGuid().ToString()+file_name;

            string path = env + folder_name + file_name;

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return file_name;
        }

        public static void DeleteFile(this IFormFile file, string env_path, string folder_name)
        {
            string path = env_path + folder_name + file.FileName;

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
    }
}
