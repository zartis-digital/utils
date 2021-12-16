# INSTRUCTIONS

1. Duplicate run template file `run-extract-logs.temlpate.ps1` and rename that copy to `run-extract-logs.local.ps1`
2. Update `run-extract-logs.local.ps1` with the required parameters for your scenario.
3. Run `run-extract-logs.local.ps1`

   > out of windows, you probally need to run `pwsh` first
   > in windows, please ensure that `allProjectsFolder` ends with a `\` at the end

4. Logs will be saved on the `temp` folder, organized in folders by year/date

## NOTES

### Dependecies

> Non-windows machines will need the [powershell core module installed](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.1) on their computer ( compatible with linux / mac )

### Getting the author name:

- Check a gitlog of a repo, and search for your authorname
- OR run `git config user.name` and grab the name from there
