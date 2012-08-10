using System.Management.Automation;
using System.IO.IsolatedStorage;
using System.Text;
using IsolatedStorage;
using IsolatedStorage.Properties;
using IsolatedStorage.oAuth2;
using Puddle.parsing.Objects;

namespace Puddle
{
    [Cmdlet("New", "filepath")]
    public class NewFilePath : PSCmdlet
    {
        private string _path;
        #region Parameters

        /// <summary>
        /// The names of the processes retrieved by the cmdlet.
        /// </summary>


        /// <summary>
        /// Gets or sets the list of process names on which 
        /// the Get-Proc cmdlet will work.
        /// </summary>
        [Parameter(Mandatory = true)]
        public string Path
        {
            get { return this._path; }
            set { this._path = value; }
        }


        #endregion Parameters

        protected override void ProcessRecord()
        {
            WriteObject(Path);
            base.ProcessRecord();
        }
    }
}
