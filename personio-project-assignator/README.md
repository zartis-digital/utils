# Personio Project Assignator

This script is useful to update all the pending work periods on a month with an specific project, 
as the copying feature from Personio won't do it by default.

## Usage

* Enter the "Attendance" section in Personio. The URL should look like https://zartis.personio.de/attendance/employee/0000000/2022-03
* Open the integrated javascript console on the browser (usually F12 on Windows, or OPT+CMD+I on Mac)
* Copy the contents of the "main.js" file from this folder directly on the console, hit ENTER.
* A small popup should appear on the top-right corner of the page, with a dropdown selector for the different Zartis projects, select the one appropriate to you.
* Navigate to the appropriate month using Personio's interface.
* Click the "check" button. The days that on "pending" days and don't have the proper project assigned on a work period will be highlighted and the "Assign" and "Reset" button will be enabled.
* Click on the "assign" button. The progress will be displayed in this script's UI.
* Repeat this process for any other months.

**NOTE 1:** Switching between months will be necessary to check the changes on Personio's UI. Reloading the page is also fine, but this script will need to be run again if further use is needed.

**NOTE 2:** Consider that there is a small delay between requests to not swarm Personio's API, so the process will take some more than 1 second per day to update.


