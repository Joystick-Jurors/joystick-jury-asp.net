# joystick-jury-asp.net
Trying this again with [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/).

## Contributing
This time around, main has some _branch protection_ rules set up so only @slaugaus gets to push to it... if you're interested in contributing, make a [pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request).

![this is literally 1984 austin how could you do this to us](https://preview.redd.it/1984-cropped-image-meme-v0-lg4jr38f277a1.jpg?width=640&crop=smart&auto=webp&s=0a59cdb0e22aa42d759eea18e6a4f40dc65f24fa)

### Running Locally
[IGDB](https://api-docs.igdb.com/) connections will fail unless you have a Twitch developer client ID and secret defined on the host PC:
1. [Register a Twitch application](https://api-docs.igdb.com/#account-creation). (Add `https://localhost` as a redirect URL and set it to Confidential.)
2. Define an [environment variable](https://www.twilio.com/en-us/blog/how-to-set-environment-variables-html) `IGDB:ClientId` with your app's Client ID as its value.
3. Define an environment variable `IGDB:ClientSecret` with your app's Client Secret as its value.
    * If you can't use : in an environment variable, use __ (2 underscores) instead.

## Credits
* User management setup (custom user class, etc.) was based on a [Code With Mukesh tutorial](https://codewithmukesh.com/blog/user-management-in-aspnet-core-mvc/)
* Light/dark mode selector "inspired by" [the one in the Bootstrap docs](https://getbootstrap.com/docs/5.3/customize/color-modes/#javascript)