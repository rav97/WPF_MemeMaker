# WPF Meme Maker
## _The Meme generator made with WPF_

WPF Meme Maker is a an app for creating memes on your Windows device.
- Upload image
- Add captions
- Generate meme
- No watermarks or logs

## About

In general WPF Meme Maker is an app for creating memes from template. It is solution similar to [Imgflip - Meme Generator](https://imgflip.com/memegenerator) but it doesn't require internet connection. To create meme you just need an template image, add some captions, generate meme and save it. Generated meme won't have any watermarks or logos.

### Tech

The apliaction was created with the use of following technologies and tools:
- Visual Studio 2022
- TortoiseGit
- .NET Framework 4.7.2
- WPF
- MVVM pattern
- [MaterialDesign in XAML](http://materialdesigninxaml.net/)

## How to run

1. Clone repository
2. Open solution in Visual Studio (I used VS2022, but older versions should be just fine)
3. Compile project
4. You can run app directly from Visual Studio or from .exe file created in `/bin/Debug` or `/bin/Release` folder.

## How it looks like
![Offline Overview](https://raw.githubusercontent.com/rav97/ResourcesRepository/main/MemeMaker/OfflineOverview.png)

## How to use

1. To create your meme you need to have an image or meme template on your device (.jpg or .png format).
2. Click on `Add image` button.
3. The OpenFileDialog should pop up, where you can select which image to use.
4. After selection of the image it should be visible on app background.
5. By default two caption texts should appear on your image.
6. You can set the size and boundaries of that area by using drag&drop on some of it's corner.
7. You can set the text of the captions by editing textboxes on the left.
8. If you need more captions you can create them by clicking on `Add text` button
9. When your meme is ready you can simpy save it by clicking `Generate Meme` button.
