# Factorial Clock-in Copier

This script is usefult to replicate the now deprecated "copy on empty days" feature Factorial used to have, that allowed
to automatically fill the empty days on the "clock in" section with the time periods already set on a day.

## Usage

* Enter the "Clock in" section on Factorial. The URL should look like https://app.factorialhr.com/attendance/clock-in/2020/1
  * **Important:** At least one day should have been already filled manually.
* Open the integrated javascript console on the browser (usually F12 on Windows, or OPT+CMD+I on Mac)
* Copy the contents of the "main.js" file from this folder directly on the console, hit ENTER.
* Click on the "Copy on empty" days action that should have appeared on any of the already filled days.

After a small delay, the page should reload and all empty allowed days should have been filled.

**NOTE:** The script already takes care of not filling any weekends or "future" days (should be disabled), but it may possible
that days marked as "vacation" or "holidays" are filled, so be aware of deleting values for those days if applies.
