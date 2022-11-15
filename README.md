# WPF Meme Maker (Online)
## _The Meme generator made with WPF_

WPF Meme Maker is a an app for creating memes on your Windows device.
- Upload image
- Add captions
- Generate meme
- No watermarks or logs

And also with this online solution:
- Connection to Web API
- Search and select templates from remote database
- Upload your own templates
- Upload your generated meme
- View gallery of memes created and uploaded by community

## About

In general WPF Meme Maker is an app for creating memes from template. It is solution similar to [Imgflip - Meme Generator](https://imgflip.com/memegenerator) but as stand-alone app. To create meme you just need an template image (or select one from remote database), add some captions, generate meme and save it. Generated meme won't have any watermarks or logos. You can upload your meme to share it with community.

### Tech

The apliaction was created with the use of following technologies and tools:
- Visual Studio 2022
- TortoiseGit
- .NET Framework 4.7.2
- WPF
- MVVM pattern
- [MaterialDesign in XAML](http://materialdesigninxaml.net/)
- HttpClient
- Newtonsoft.Json

## How to run

1. Clone repository
2. Open solution in Visual Studio (I used VS2022, but older versions should be just fine)
3. In order to connect with API you need to set URL to [MemeMakerAPI](https://github.com/rav97/MemeMaker_API) in `Properties/Settings.settings` file and make sure that the API is running. Otherwise APP will be a bit laggy.
4. Compile project
5. You can run app directly from Visual Studio or from .exe file created in `/bin/Debug` or `/bin/Release` folder.

## How it looks like

![Offline Overview](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/OnlineOverview.png?raw=true)

### Select background

![PopularTemplates](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/SelectBackgroundPopular.png?raw=true)
![SearchForTemplate](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/SearchForTemplate.png?raw=true)

### Meme making

![MouseEnter](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/MemeMaking.png?raw=true)
![MouseLeave](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/MemeMaking2.png?raw=true)
![SaveMeme](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/SaveMeme.png?raw=true)
![Saved](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/Saved.png?raw=true)
![UploadQuestion](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/UploadToCommunity.png?raw=true)
![Upload](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/Upload.png?raw=true)

### Meme Gallery

![MemeGallery](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/MemeGallery.png?raw=true)
![MemeGalleryPreview](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/MemeGalleryPreview.png?raw=true)
![MemeGalleryNextPage](https://github.com/rav97/ResourcesRepository/blob/main/MemeMaker/MemeGalleryPage.png?raw=true)

## How to use

1. To create your meme you need to have an image or meme template on your device (.jpg or .png format).
2. Click on `Add image` button.
3. The `Select background template` view should pop up. Now you can select image from your device or search for it in remote database.
3.1. If you select image from device, you can upload it to remote database by clicking `Upload` button.
4. After selection of the image it should be visible on app background.
5. By default two caption texts should appear on your image.
6. You can set the size and boundaries of that area by using drag&drop on some of it's corner.
7. You can set the text of the captions by editing textboxes on the left. Size of text caption can be changed by pressing blue buttons next to textbox. 
8. If you need more captions you can create them by clicking on `Add text` button
9. When your meme is ready you can simpy save it by clicking `Generate Meme` button.

## Future plans
Although project is pretty much ready and working it still needs some more work to do. For sure there are some refactoring needed and minor bugfixes. The functionality of web API and implemented functions can be improved or extended, but these works are time-consuming and the effect of their implementation will be insignificant so I decided to leave it as is.
The main purpose of this project was to refresh my WPF and C# skills and expand my portfolio and I think it's done pretty well.
