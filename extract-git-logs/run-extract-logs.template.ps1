
# => Duplicate this file so it follows the name `run-extract-logs.local.ps1`. Run this new file with the required parameters

$fnParams = @{
    gitAuthor         = "" # use when you want to start with a specific user ( app will promt u otherwise )
    targetBranch      = "master"   # look for logs on this branch
    semester          = -1  # available values: 0 (full year ) 1 ( 1st semester ) 2 ( 2nd semester ). ( app will promt u otherwise )
    targetYear        = "" # define target year( app will promt u otherwise )
    nestedFolders     = 2 # define how many nested folder level the script should analysis ( app will use "4" as default )
    allProjectsFolder = "" # put the full path of the folder where your working git repos live in
    # for instace: "C:\Users\[user]\Documents\vscode"
    # or: "/home/[user]/vscode"
    debuginfo         = $false # wheter to print or not extra info
}


$scriptlocation = Join-Path -Path $PSScriptRoot -ChildPath "/extract-logs.ps1"

.$scriptlocation  @fnParams

