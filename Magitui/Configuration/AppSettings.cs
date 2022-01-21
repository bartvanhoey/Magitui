using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.Configuration
{
    public class AppSettings
    {
        public string PersonalAccessToken { get; set; }
        public string GitHubUserName { get; set; }
        public string RepoName { get; set; }
        public string BranchName { get; set; }
        public string SavingsDataFileName { get; set; }
        public string OwnersDataFileName { get; set; }
        public string AppName { get; set; }

    }
}
