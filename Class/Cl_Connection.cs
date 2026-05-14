namespace Project_API.Class
{
    public class Cl_Connection
    {
        // Nơi chứa thông tin dữ liệu, các file của chương trình 
        public const string Root_data = @"C:\Khanh_Project\Keyence_Project\PocketPC_Web\Pocket_Data";
        //public const string Root_data = @"D:\Keyence_Project\Pocket_Data";
        // Nơi chứa thông tin các file csv
        public static string csv_folder = Path.Combine(Root_data,"Csv_file");
        // Nơi chứa dữ liệu quét(qua file sdf)
        public static string sdf_path = Path.Combine(Root_data, "KeyenceData.sdf");
        // Nơi chứa thông tin các file cập nhật của chương trình
        public static string download_folder = Path.Combine(Root_data, "DownloadedFiles");
        // Nơi chứa thông tin phiên bản trên server 
        public static string server_version = Path.Combine(Root_data, "Keyence_Program_Version.txt");
    }
}
