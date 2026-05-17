using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace PillIdentifierForm.Helpers
{
    public static class GoogleDriveHelper
    {
        // private static readonly string[] Scopes = { DriveService.Scope.Drive };
        //
        // private static readonly string ServiceAccountJsonPath =
        //     Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "google_credential.json");
        //
        // private static DriveService GetDriveService()
        // {
        //     if (!System.IO.File.Exists(ServiceAccountJsonPath))
        //         throw new FileNotFoundException(
        //             "Không tìm thấy file xác thực Google:\n" + ServiceAccountJsonPath);
        //
        //     GoogleCredential credential;
        //     using (var stream = new FileStream(ServiceAccountJsonPath, FileMode.Open, FileAccess.Read))
        //     {
        //         credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
        //     }
        //     return new DriveService(new BaseClientService.Initializer
        //     {
        //         HttpClientInitializer = credential,
        //         ApplicationName = "PillIdentifierForm"
        //     });
        // }
        
        private static readonly string[] Scopes = { DriveService.Scope.Drive };

        // Trỏ tới file JSON mới
        private static readonly string ClientSecretJsonPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "client_secret.json");

        // Thư mục để app tự động lưu Token sau khi đăng nhập lần đầu
        private static readonly string TokenFolder = 
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DriveTokens");

        private static DriveService GetDriveService()
        {
            if (!System.IO.File.Exists(ClientSecretJsonPath))
                throw new FileNotFoundException(
                    "Không tìm thấy file client_secret.json:\n" + ClientSecretJsonPath);

            UserCredential credential;

            using (var stream = new FileStream(ClientSecretJsonPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user", // Tên định danh cho token đang lưu
                    CancellationToken.None,
                    new FileDataStore(TokenFolder, true)).Result;
            }

            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "PillIdentifierForm"
            });
        }

        /// <summary>
        /// Uploads a local image file to the specified Google Drive folder.
        /// Returns the Drive file ID.
        /// </summary>
        public static string UploadFile(string localFilePath, string folderId)
        {
            var service = GetDriveService();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = Path.GetFileName(localFilePath),
                Parents = new List<string> { folderId }
            };
            string mimeType = GetMimeType(localFilePath);
            using (var stream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
            {
                var request = service.Files.Create(fileMetadata, stream, mimeType);
                request.Fields = "id";
                var progress = request.Upload();

                if (progress.Status != Google.Apis.Upload.UploadStatus.Completed)
                {
                    string reason = (progress.Exception != null)
                        ? GetFullMessage(progress.Exception)
                        : "Status: " + progress.Status;
                    throw new Exception("Upload failed — " + reason);
                }

                if (request.ResponseBody == null)
                    throw new Exception("Upload completed but Drive returned no file ID.");

                return request.ResponseBody.Id;
            }
        }

        /// <summary>
        /// Sets the file permission to public (anyone with the link can view).
        /// </summary>
        public static void SetFilePublic(string fileId)
        {
            var service = GetDriveService();
            var permission = new Permission
            {
                Type = "anyone",
                Role = "reader"
            };
            service.Permissions.Create(permission, fileId).Execute();
        }

        /// <summary>
        /// Returns a public URL suitable for use in an &lt;img&gt; tag.
        /// Format: https://drive.google.com/uc?id={fileId}
        /// </summary>
        public static string GetPublicUrl(string fileId, string fileName)
        {
            return "https://drive.google.com/uc?id=" + fileId + $"&name={Uri.EscapeDataString(fileName)}";
        }

        /// <summary>
        /// Permanently deletes a file from Google Drive.
        /// </summary>
        public static void DeleteFile(string fileId)
        {
            if (string.IsNullOrEmpty(fileId)) return;
            var service = GetDriveService();
            service.Files.Delete(fileId).Execute();
        }

        /// <summary>
        /// Extracts the Google Drive file ID from a stored public URL.
        /// Handles: https://drive.google.com/uc?id=FILE_ID
        /// </summary>
        public static string ExtractFileId(string publicUrl)
        {
            if (string.IsNullOrEmpty(publicUrl)) return null;
            var match = Regex.Match(publicUrl, @"[?&]id=([^&]+)");
            return match.Success ? match.Groups[1].Value : null;
        }

        private static string GetFullMessage(Exception ex)
        {
            var sb = new System.Text.StringBuilder();
            while (ex != null)
            {
                sb.AppendLine(ex.GetType().Name + ": " + ex.Message);
                ex = ex.InnerException;
                if (ex != null) sb.AppendLine("  --> caused by:");
            }
            return sb.ToString().TrimEnd();
        }

        private static string GetMimeType(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            switch (ext)
            {
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png":  return "image/png";
                case ".gif":  return "image/gif";
                case ".bmp":  return "image/bmp";
                case ".webp": return "image/webp";
                default:      return "application/octet-stream";
            }
        }
    }
}
