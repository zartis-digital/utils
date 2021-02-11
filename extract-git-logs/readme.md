# INSTRUCTIONS

1. Duplicate run template file `run-extract-logs.temlpate.ps1` so it follows the name `run-extract-logs.local.ps1`
2. Update `run-extract-logs.local.ps1` with the required parameters for your scenario.
3. Run `run-extract-logs.local.ps1` ( out of windows, you probally need to run `pwsh` first)
4. Logs will be saved on the `temp` folder, organized in folders by year/date

## NOTES

### Dependecies

> Non-windows machines will need the powershell core module installed on their computer ( compatible with linux / mac )

### Getting the author name:

- Check a gitlog of a repo, and search for your authorname
- OR run `git config user.name` and grab the name from there
