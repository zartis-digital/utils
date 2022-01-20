namespace ZartisLogs
{
    public class GitService
    {
        public (string gitCommand, string tempFileName, string projectName, DateTime dateFrom, DateTime dateTo) BuildGitCommand(DateTime dateFrom, DateTime dateTo, string userName, string projectName, string prettierFormat)
        {
            var tempFileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";

            var formattedDayDateFrom = dateFrom.Day < 10 ? "0" + dateFrom.Day.ToString() : dateFrom.Day.ToString();
            var formattedDayDateTo = dateTo.Day < 10 ? "0" + dateTo.Day.ToString() : dateTo.Day.ToString();

            var formattedMonthDateFrom = dateFrom.Month < 10 ? "0" + dateFrom.Month.ToString() : dateFrom.Month.ToString();
            var formattedMonthDateTo = dateTo.Month < 10 ? "0" + dateTo.Month.ToString() : dateTo.Month.ToString();
            var _prettierFormat = string.IsNullOrEmpty(prettierFormat) ? "email" : prettierFormat;

            var gitCommand = $@"git log --author='{userName}*' --pretty={_prettierFormat} --since='{dateFrom.Year}-{formattedMonthDateFrom}-{formattedDayDateFrom}' --until='{dateTo.Year}-{formattedMonthDateTo}-{formattedDayDateTo}' > {tempFileName}";

            return (gitCommand, tempFileName, projectName, dateFrom, dateTo);
        }
    }
}
