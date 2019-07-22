# RainmeterLastFmGrabber

## Overview
Lots of us in the Rainmeter community like it when our desktops show the music we are currently listening to. Unfortunately, not all media players are supported, and while there are a ton of them supported, sometimes we are forced to use an unsupported player. Most unsupported players support Last.fm scrobbling though, and Rainmeter can read text files! So this project hopes to widen the array of media players supported by allowing you to run an application locally that will simply hit an API I created for your most recently played tracks and write that stuff to a file in a format of your choice! For example, I don't like using Tidal.com online, I use the desktop application since it means I don't have to run Chrome. Tidals desktop player isn't supported but Tidal supports scrobbling, so now we can get somewhere!

For those of you unsure of what rainmeter is or what I am specifically talking about check out this image: ![album](https://preview.redd.it/yokh2vxcxtb31.png?width=1024&auto=webp&s=2eb869af4b66ce5c30480db9efd41e2852c0e014)

[Original Post](https://www.reddit.com/r/Rainmeter/comments/cgbc2d/minimalist_setup_to_go_with_my_150_wallpaper/)

In the top left you see a now playing section, that's hard to do without a supported player. 

Be sure to check out reddit.com/r/rainmeter for cool pics of what people do with rainmeter! :) 

## How It Works

Right now the architecture is really straght forward. 

App Running on Your Desktop -> My Lambda Function -> Last.fm API

The app simply polls my API every 5 seconds to see get the latest of your most recently played music. Down the line, I may add support for downloading the cover image and saving the cover file, for now this will do. If you pause the application it will simply check every three seconds to see if you have unpaused.

## Future Plans

Probably just add the ability to download the cover image. Last.fm doesn't have as many good cover images, so maybe will query spotify for this info instead. Not sure yet ¯\\_(ツ)_/¯