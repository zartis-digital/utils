<#
INSTRUCTIONS
please read the readme.md for details
#>

param(
    [Parameter(Mandatory = $false, ValueFromPipeline = $true)]
    [string] $gitAuthor = "",  
    [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
    [string] $targetBranch = "",  
    [Parameter(Mandatory = $false, ValueFromPipeline = $true)]
    [int] $semester = -1,
    [Parameter(Mandatory = $false, ValueFromPipeline = $true)]
    [string] $targetYear = "",    
    [Parameter(Mandatory = $false, ValueFromPipeline = $true)]
    [int] $nestedFolders = 4, # used for the dept level
    [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
    [string] $allProjectsFolder,
    [Parameter(Mandatory = $false, ValueFromPipeline = $true)]
    [System.Boolean] $debuginfo # used for extra messages
)
try {
    # Other params
    $semesterLabel = "" # used for a better namind of the folder where the logs are going to be stored
    [string] $gitlogExtractOutputFolder = Join-Path -Path $PSScriptRoot -childPath "/temp/logs-for-zartis"   # OS-agnostic filepath creation, default output

    if ($(Test-Path $allProjectsFolder -ErrorAction Stop) -eq $false) {
        throw "Folder with repos: '$allProjectsFolder' cant be accesed" 
    }

    Write-Host "`n=> Lets extract all the git logs from any git project located under '$allProjectsFolder'" -ForegroundColor Green
    if ($IsMacOS -or $IsLinux) {
        Write-Host "- Current host is UNIX based" -ForegroundColor Green
    }
    else {
        Write-Host "- Current host is Windows based" -ForegroundColor Green
    }

    while (!$gitAuthor) {
        Write-Host "Git Author is required, maybe is it '$(.{git config user.name})'? " -ForegroundColor Yellow
        $gitAuthor = Read-Host -Prompt "Please enter the author name for the git commits"
    }
    Write-Host "- Target user is '$gitAuthor'" -ForegroundColor Green

    while (!$targetYear) {
        $targetYear = Read-Host -Prompt "Please enter target year of the report (press enter for current year)"
        if (!$targetYear) { $targetYear = Get-Date -Format "yyyy" }
        Write-Host "- Year selected is '$targetYear'" -foregroundcolor green
    }

    while ($semester -notin 0, 1, 2) {
        $semester = Read-Host -Prompt "Please enter target semester of the report ( 0 = full year (just press enter), 1 = 1st semester, 2 = 2nd semester )"
        if ($semester -eq -1) { $semester = 0 }
    }
    if ($semester -gt 0) {
        $semesterLabel = "-s{0}" -f $semester
    }

    switch ($semester) {
        1 {
            $startDate = Get-Date -Year $targetYear -Month 1 -Day 1 -Hour 0 -Minute 0 -Second 0 -Format "yyyy-MM-ddTHH.mm:ssK"
            $endDate = Get-Date -Year $targetYear -Month 6 -Day 30 -Hour 23 -Minute 59 -Second 59 -Format "yyyy-MM-ddTHH.mm:ssK"
        }
        2 {
            $startDate = Get-Date -Year $targetYear -Month 07 -Day 1 -Hour 0 -Minute 0 -Second 0 -Format "yyyy-MM-ddTHH.mm:ssK"
            $endDate = Get-Date -Year $targetYear -Month 12 -Day 31 -Hour 23 -Minute 59 -Second 59 -Format "yyyy-MM-ddTHH.mm:ssK"
        }
        0 { 
            $startDate = Get-Date -Year $targetYear -Month 1 -Day 1 -Hour 0 -Minute 0 -Second 0 -Format "yyyy-MM-ddTHH.mm:ssK"
            $endDate = Get-Date -Year $targetYear -Month 12 -Day 31 -Hour 23 -Minute 59 -Second 59 -Format "yyyy-MM-ddTHH.mm:ssK"
        }
        Default {
            throw "Invalid semester value '$semester'"
        }
    } 

    $outputFolderBasedOnDate = "logs-for-zartis-y{0}{1}" -f $targetYear, $semesterLabel
    $gitlogExtractOutputFolder = Join-Path -Path $PSScriptRoot -childPath "/temp/$outputFolderBasedOnDate"  # Folder update based on input
    write-host "- Script will be searching git logs from '$startDate' to '$endDate', and save them on '$outputFolderBasedOnDate'" -ForegroundColor Green

    # create ( if needed ) the folder where we want to store the gitlogs
    if ( !$(Test-Path $gitlogExtractOutputFolder -ErrorAction SilentlyContinue) ) {
        New-Item -ItemType Directory $gitlogExtractOutputFolder -ErrorAction Stop > $null
    }

    #generate the git logs for all existing repos, filtering by the given username
    $gitRepos = Get-ChildItem -recurse -Directory -Path $allProjectsFolder -Force -depth $nestedFolders -ErrorAction stop | Where-Object { $_.name -eq ".git" }
    write-host "Extracting logs.." -ForegroundColor DarkYellow -NoNewline
    foreach ($gitrepo in $gitRepos) {
        $gitrepoFolderPath = $gitrepo.Parent 
        $gitrepoName = $(get-item -path $gitrepoFolderPath ).name
        Set-Location -Path $gitrepoFolderPath -ErrorAction Stop > $null
        $gitlogfilename = "{0}-{1}.csv" -f $gitrepoName, $($gitAuthor -replace " ", "")
        $trycheckout = git checkout $targetBranch *>&1
        $repoCurrentBranch = git branch --show-current *>&1
        write-host $trycheckout2 
        if ( "$trycheckout".indexOf( "error") -ge 0) {
            write-host "`nThere are pending changes on current branch '$repoCurrentBranch' on '$gitrepoFolderPath' (repo '$gitrepoName').`nSome logs from branch '$targetBranch' may not be included. Consider committing and merging to '$targetBranch'" -foregroundcolor DarkRed
        }
        else {
            write-host "." -ForegroundColor DarkYellow -NoNewline
        }
        $gitLogFilepath = Join-Path -Path $gitlogExtractOutputFolder -childPath "/$gitlogfilename"
        . { git log --author="$gitAuthor" --branches --pretty="format:%h;%an;%ad;%s;" --date=local --no-merges --shortstat --after="$startDate" --before "$endDate" > $gitLogFilepath }
        
    }
    write-host "`nLogs extracted" -ForegroundColor DarkYellow
    write-host "Logs cleanup ( remove empty ones )..." -ForegroundColor DarkYellow
    Get-ChildItem -Recurse -Path $gitlogExtractOutputFolder -ErrorAction stop | ForEach-Object {
        $gitlog = $_
        if ($(Get-Content -Path $gitlog.FullName) -eq $null) {
            Remove-Item -Path $gitlog.FullName -Force -ErrorAction stop
            if ($debuginfo) { write-host "Empty log deleted '$($gitlog.FullName)'" -foregroundcolor darkyellow }
        }
        else {
            #add csv headers
            "hash;author;date;message;diff`r`n" + (Get-Content $gitlog.FullName -Raw) | Set-Content $gitlog.FullName
            #cleanup format
            ((Get-Content -path $gitlog.FullName -Raw).Replace(";`r`n ", ";")) | Set-Content -Path $gitlog.FullName
            ((Get-Content -path $gitlog.FullName -Raw).Replace("`r`n`r`n", "`r`n")) | Set-Content -Path $gitlog.FullName
            write-host "Log exported '$gitlog'" -foregroundcolor green
        }
    }
    Set-Location -Path $PSScriptRoot -ErrorAction Stop > $null
    write-host "=> All Logs can be found here: '$gitlogExtractOutputFolder'`n`n" -foregroundcolor green

}
catch {
    Write-Host "There was an error: $($_.FullyQualifiedErrorId)" -ForegroundColor Red
    $_
}

