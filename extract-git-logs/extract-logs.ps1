<#
INSTRUCTIONS
place this script in the container folder that contains all your git projects. 
it will extract any git log for git projects nested on that container, up to a depth level of 4 ( confirubale in the empty params)
Getting the author name:
- Check a gitlog of a repo, and search for your authorname
- run "git config user.name" and grab the name from there
#>

$pathDelimiter = "\"
if ($IsMacOS -or $IsLinux) {
    #only works in powershell core, change delimiter if UNIX system
    $pathDelimiter = "/"
}
write-host "Using path delimiter = $pathDelimiter"
$gitRelateFolder = "$PSScriptRoot\"
$gitlogFolder = "zartis-logs" #Read-Host -Prompt "Please enter a name for the folder in which you want to save the git logs"
$gitAuthor = ""
while (!$gitAuthor) {
    $gitAuthor = Read-Host -Prompt "Please enter the author name for the git commits"
}
$semester = -1
while ($semester -notin 0, 1, 2, 3) {
    $semester = Read-Host -Prompt "Please enter target semester of the report ( 0 = full year (just press enter), 1 = 1st semester, 2 = 2nd semester, 3 - full previous year )"
    if ($semester -eq -1) { $semester = 0 }
}
$currentYear = Get-Date -Format "yyyy"
switch ($semester) {
    1 { 
        $startDate = "{0}-01-01" -f $currentYear
        $endDate = "{0}-05-30" -f $currentYear
    }
    2 { 
        $startDate = "{0}-06-01" -f $currentYear
        $endDate = "{0}-12-31" -f $currentYear
    }
    3 { 
        $startDate = "{0}-01-01" -f $currentYear - 1
        $endDate = "{0}-12-31" -f $currentYear - 1
    }
    Default {
        $startDate = "{0}-01-01" -f $currentYear
        $endDate = "{0}-12-31" -f $currentYear
    }
}

$nestedFolders = 4 # used for the dept level
# create ( if needed ) the folder where we want to store the gitlogs
if ( !$(Test-Path $gitRelateFolder$gitlogFolder -ErrorAction SilentlyContinue) ) {
    New-Item -ItemType Directory $gitRelateFolder$gitlogFolder > $null
}
$gitlogFolder = Get-Item -path $gitRelateFolder$gitlogFolder -ErrorAction Stop
#generate the git logs for all existing repos and for the given username
$gitRepos = Get-ChildItem -recurse -Directory -Path $gitRelateFolder -Force -depth $nestedFolders | Where-Object { $_.name -eq ".git" }
$gitRepos | ForEach-Object {
    $gitrepo = $_
    $gitrepoFolder = $gitrepo.FullName.Substring(0, $gitrepo.FullName.LastIndexOf($pathDelimiter) + 1)
    $gitreponame = $(get-item -path $gitrepoFolder ).name
    Set-Location -Path $gitrepoFolder -ErrorAction Stop > $null
    $gitlogfilename = "{0}-{1}.csv" -f $gitreponame, $($gitAuthor -replace " ", "")
    $trycheckout = git checkout master *>&1
    if ( "$trycheckout".indexOf( "error") -ge 0) {
        write-host "There are pending changes on current branch. Please commit and merge to master, then try again '$gitlogfilename'" -foregroundcolor red
    }
    else {
        . { git log --author="$gitAuthor" --branches --pretty="format:%h;%an;%ad;%s;" --date=local --no-merges --shortstat --after="$startDate" --before "$endDate" > $gitlogFolder$pathDelimiter$gitlogfilename }
        write-host "Log exported '$gitlogfilename'" -foregroundcolor green
    }
}
#cleanup empty logs
Get-ChildItem -Recurse -Path $gitlogFolder | ForEach-Object {
    $gitlog = $_
    if ($(Get-Content -Path $gitlog.FullName) -eq $null) {
        Remove-Item -Path $gitlog.FullName -Force
        write-host "Empty log deleted '$($gitlog.FullName)'" -foregroundcolor darkyellow
    }
    else {
        #cleanup format
        ((Get-Content -path $gitlog.FullName -Raw).Replace(";`r`n ", ";")) | Set-Content -Path $gitlog.FullName
        ((Get-Content -path $gitlog.FullName -Raw).Replace("`r`n`r`n", "`r`n")) | Set-Content -Path $gitlog.FullName
    }
}
Set-Location -Path $gitRelateFolder -ErrorAction Stop > $null
write-host "=> All Logs can be found here: '$($gitlogFolder.Fullname)$pathDelimiter'" -foregroundcolor green