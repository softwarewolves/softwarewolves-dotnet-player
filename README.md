softwarewolves-dotnet-player
============================

# Softwarewolves-java-player


This is an example implementation of a bot in C# for the digital version of the werewolves party game (rebranded to softwarewolves game). The bot does not do much - it implements the lazy villager story.

More information on the softwarewolves game can be found at : [Softwarewolves documentation][1].

## Setting up the project


### 1. Get the code 


With github, there are several possibilities:
* Download the project as a zipfile from github (github button somewhere on page). 
* Fork the project to your own github repository (github button somewhere on page), then clone it. This requires a github account.
* Clone the repository to your own computer. This requires git to be installed on your system. For cloning, you can use your favorite git tool or the following command:

```
        git clone https://github.com/supernelis/softwarewolves-dotnet-player.git 
```

### 2. Setup your editor

Get the project in your favorite editor:
* for Visual Studio addicts: 
 * the solution (.sln) and project (.csproj) files are on github


### 3. Setup the build path


Add a reference to the agsXMPP DLL libary.

### 4. Configure the bot

Make sure you have a user on the jabber server for your bot and that you know the username of the game coordinator. See [Softwarewolves documentation][1] for more information on how to do that.

Change the configuration in the arguments of DotNetPlayer.DotNetBotBody.Main to point to the correct user and server, e.g. 


```
 DotNetBotBody client = new DotNetBotBody("jabber.org", "myjabberusername", "myjabberpassword", "mypreferrednickname");
```

### 5. Run

Run the main method in the class DotNetPlayer.DotNetBotBody. A seperate window should pop-up with the console output.


## agsXMPP

This project uses the -dated but freely available- agsXMPP library for xmpp. Although it is not considered as good practice, 
we included the library in this git project to simplify setup, limit the 
amount of external dependencies that can fail and simplify offline working.

- [agsXMPP homepage][2].

[1]: https://github.com/supernelis/softwarewolves-doc
[2]: http://www.ag-software.net/agsxmpp-sdk/

