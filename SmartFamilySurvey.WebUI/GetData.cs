using ESS.SFS.BLL.ICollection;
using ESS.SFS.DAL;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartFamilySurvey.WebUI
{
    public class GetData
    {
        private IForm _form;
        public GetData(IForm form)
        {
            _form = form;
        }
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Smart Family Survey";
        string error = "";
        public string GetDriveData()
        {
            try
            {
                UserCredential credential;

                using (var stream =
                    new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Drive API service.
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define parameters of request.
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 10;
                listRequest.Fields = "nextPageToken, files(id, name,fullFileExtension,fileExtension,exportLinks,mimeType,viewersCanCopyContent,webContentLink,webViewLink,originalFilename,properties,shared)";
                // listRequest.Fields = "nextPageToken, files(appProperties,capabilities,contentHints,contentRestrictions,copyRequiresWriterPermission,createdTime,createdTimeRaw,description,driveId, eTag, explicitlyTrashed,exportLinks,fileExtension,folderColorRgb,fullFileExtension,hasAugmentedPermissions,hasThumbnail,headRevisionId,iconLink,id,imageMediaMetadata,isAppAuthorized,kind,lastModifyingUser,md5Checksum,mimeType,modifiedByMe,modifiedByMeTime,modifiedByMeTimeRaw,modifiedTime,modifiedTimeRaw,name,originalFilename,ownedByMe,owners,parents,permissionIds,permissions,properties,quotaBytesUsed,shared,sharedWithMeTime,sharedWithMeTimeRaw,sharingUser,shortcutDetails,size,spaces,starred,teamDriveId,thumbnailLink,thumbnailVersion,trashed,trashedTime,trashedTimeRaw,trashingUser,version,videoMediaMetadata,viewedByMe,viewedByMeTime,viewedByMeTimeRaw,viewersCanCopyContent,webContentLink,webViewLink,writersCanShare)";

                // List files.
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                    .Files;
                Console.WriteLine("Files:");
                List<TblForm> tblForms = new List<TblForm>();
                TblForm tblForm;
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        string link;
                        if (file.MimeType.Contains("google-apps.form"))
                        {
                            tblForm = new TblForm();
                            link = ESS.SFS.Helper.Constants.GooglePageIFrame;
                            link = link.Replace("<<url>>", file.WebViewLink.Replace("edit?usp=drivesdk", ""));
                            tblForm.CreatedDate = DateTime.Now;
                            tblForm.Name = file.Name;
                            tblForm.IsActive = 1;
                            tblForm.Url = link;
                            tblForm.GoogleFormId = file.Id;
                            tblForms.Add(tblForm);
                        }

                    }
                    if (tblForms.Count > 0)
                    {
                         error =  _form.ImportData(tblForms);
                    }
                }

                return error = "";
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ex.InnerException);
                sb.Append(ex.Message);

                // flush every 20 seconds as you do it
                System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt", sb.ToString());

               // System.IO.File.AppendAllText(Path.Combine(_webHostEnvironment.WebRootPath + "/App_Shared/log.txt") , sb.ToString());


                sb.Clear();
                throw;
            }

        }
    }
}
