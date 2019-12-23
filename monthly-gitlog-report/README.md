# Monthly Gitlog Report

This script is usefult to get Git commit reports in a monthly basis. It will iterate all subfolders of a defined one passed as parameter and grab gitlog from each one.

## Usage

* First Parameter: Author (typically author's email)
* Second Paramter: Month's ago that i want my report. Assuming the day i am executing the script is in April, putting 3 as parameter, will return the report from January 1st to January 31st
* Third Parameter: Report folder where the script will return the gitlogs.
* Fourth Parameter: Root folder where you want to search for git reports.

get-gitlog-report.sh anybody@zartis.com 3 /home/myuser/git-reports /home/myuser/development/git

If the git repo that you are executing the script does not contain any commit from the requested author, the script will return an empty file.