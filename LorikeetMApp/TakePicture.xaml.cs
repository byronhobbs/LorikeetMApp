using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Plugin.FileUploader;
using Plugin.FileUploader.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class TakePicture : ContentPage
    {
        private int memberID = -1;
        private string fileToUpload = string.Empty;
        private Queue<string> paths = new Queue<string>();
        private bool isBusy = false;
        public static Data.MemberManager memberManager { get; private set; }

        public TakePicture()
        {
            InitializeComponent();
        }

        public TakePicture(int memberID)
        {
            InitializeComponent();

            this.memberID = memberID;
            memberManager = new Data.MemberManager(new Data.RestService());

            takePhoto.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = false,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 1000,
                    DefaultCamera = CameraDevice.Front,
                    Name = this.memberID + ".jpg"
                });

                if (file == null)
                    return;

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                string fileToCopyTo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), memberID + ".jpg");

                File.Copy(file.Path, fileToCopyTo, true);

                fileToUpload = fileToCopyTo;

                uploadPhoto.IsVisible = true;
            };

            pickPhoto.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });


                if (file == null)
                    return;

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                string fileToCopyTo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), memberID + ".jpg");

                File.Copy(file.Path, fileToCopyTo, true);

                fileToUpload = fileToCopyTo;

                uploadPhoto.IsVisible = true;
            };

            uploadPhoto.Clicked += async (sender, args) =>
            {
                if (fileToUpload != null)
                {
                    if (isBusy)
                        return;
                    isBusy = true;
                    progress.IsVisible = true;

                    //Uploading multiple images at once

                    /*List<FilePathItem> pathItems = new List<FilePathItem>();
                    while (paths.Count > 0)
                    {
                        pathItems.Add(new FilePathItem("image",paths.Dequeue()));
                    }*/

                    await CrossFileUploader.Current.UploadFileAsync(Constants.UPLOAD_URL, new FilePathItem("file", fileToUpload));
                }
            };

            okButton.Clicked += (sender, args) =>
            {
                this.Navigation.PopAsync();
            };

            downloadPhoto.Clicked += async (sender, args) =>
            {
                var fileToDownloadTo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), memberID + ".jpg");

                var worked = await memberManager.DownloadImageFileAsync(memberID + ".jpg", fileToDownloadTo);

                if ((image != null) && worked.Equals("worked"))
                {
                    image.Source = ImageSource.FromFile(fileToDownloadTo);
                }
                else
                {
                    await DisplayAlert("File Download Error", worked, "Ok");
                }
            };

            CrossFileUploader.Current.FileUploadCompleted += Current_FileUploadCompleted;
            CrossFileUploader.Current.FileUploadError += Current_FileUploadError;
            CrossFileUploader.Current.FileUploadProgress += Current_FileUploadProgress;
        }

        private void Current_FileUploadProgress(object sender, FileUploadProgress e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                progress.Progress = e.Percentage / 100.0f;
            });
        }

        private void Current_FileUploadError(object sender, FileUploadResponse e)
        {
            isBusy = false;
            System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("File Upload", "Upload Failed", "Ok");
                progress.IsVisible = false;
                progress.Progress = 0.0f;
            });
        }

        private void Current_FileUploadCompleted(object sender, FileUploadResponse e)
        {
            isBusy = false;
            System.Diagnostics.Debug.WriteLine($"{e.StatusCode} - {e.Message}");
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("File Upload", "Upload Completed", "Ok");
                progress.IsVisible = false;
                progress.Progress = 0.0f;
            });
        }
    }
}
