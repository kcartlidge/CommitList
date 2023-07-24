# Commit List

- Do you work on multiple projects?
- Sometimes forget what stuff you've done?
- Need a quick summary of recent commits?

Use *Commit List* to get a nice readable commit summary for multiple repos over
the last *x* number of days, optionally filtered by author.

## Contents

- [Installing](#installing)
- [Running](#running)
- [Sample output](#sample-output)
- [Generating new builds](#generating-new-builds)
- [CHANGELOG](./CHANGELOG.md)
- [LICENSE](./LICENSE)

## Installing

*Commit List* is a single file tool. You do not need to run an installer.
There are [pre-built executables in the builds folder](./builds) for *Windows*, *Mac OS*, and *Linux*.

That said, you may prefer to place it somewhere more convenient than the
folder you downloaded it to. Here's a couple of examples for Linux/MacOS.
*In Windows* you could use Windows Explorer to move it into a folder that
is in your `path` using copy/paste.

Linux/MacOS example for copying the relevant build into your home folder:

```sh
# copy
cd <solution>
cp builds/macos-arm64/CommitList ~

# run
cd <wherever>
~/CommitList
```

Linux/MacOS example for creating a link in a folder that is in your path, making it
available everywhere in your Terminal:

```sh
# link
cd <solution>
sudo ln $(pwd)/builds/macos-arm64/CommitList /usr/local/bin
which CommitList

# run
cd <wherever>
CommitList
```

## Running

```sh
CommitList [-days=<days>] [-author=<author>] [--deep] [--showcommand] <folder>
```

- *days* - inclusive number of previous days to show (defaults to yesterday)
- *author* - match part of the committer name
- *deep* - check within subfolders of non-repos
- *showcommand* - show the 'git log' command used
- *folder* - the parent folder holding your projects

Example:

```sh
CommitList -days=5 -author=cartlidge --deep --showcommand ~/Source
```

You'll get a simple status message for each subfolder which contains a `.git` repository.
Submodules are not listed, though if you specify `--deep` then any folder which
*isn't in itself a git repo* will have it's subfolders scanned (recursing downwards).
In effect it will spider down your folder tree, stopping each branch when it reaches a repo.

```
/Source       # not a repo; scan subfolders
  /Core       # not a repo; scan subfolders
    /MyApp    # repo; list matching commits, ignore subfolders
    /MySite   # repo; list matching commits, ignore subfolders
  /Go         # not a repo; scan subfolders
    /MyTool   # repo; list matching commits, ignore subfolders
```

## Sample output

```
Since yesterday
Scanning /Users/kcartlidge/Documents/Source (deep)

app
ad144ce  24-Jul  K Cartlidge  Support NODE_ENV switching and uvu tests

ProjectOne
b5a19ec  16-Jul  K Cartlidge  Mention where to get the release builds
df98d1a  16-Jul  K Cartlidge  Support specifying max commit age in days
e3016bb  15-Jul  K Cartlidge  Add executables and update the readme
5ca0385  15-Jul  K Cartlidge  Add git log output parsing and display
cc0afda  15-Jul  K Cartlidge  Scan for subfolders of the provided folder
```

## Generating new builds

*For CommitList *developers* only.*

Update the `CHANGELOG.md` file and the version number in `Program.cs`.
Navigate to the top folder (the one containing this `README.md` file).
Now run the relevant script file from the two below, according to *your* platform.
Each will then create new builds for *all* the supported platforms.

### MacOS and Linux

```sh
cd <solution>
./make.sh
```

### Windows

```batch
cd <solution>
make.bat
```

When run from the top folder, the builds will automatically go into
the `builds` subfolders as they are generated.
