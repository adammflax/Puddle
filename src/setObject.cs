using System.Management.Automation;
using System.IO.IsolatedStorage;
using System.Text;
using IsolatedStorage;
using IsolatedStorage.Properties;
using IsolatedStorage.oAuth2;
using Puddle.parsing.Objects;

namespace Puddle
{
    [Cmdlet("New", "object")]
    public class SetObject : PSCmdlet
    {
        private string _name;
        private string _desc;

        #region Parameters

        /// <summary>
        /// The names of the processes retrieved by the cmdlet.
        /// </summary>


        /// <summary>
        /// Gets or sets the list of process names on which 
        /// the Get-Proc cmdlet will work.
        /// </summary>
        [Parameter(Mandatory = true)]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        [Parameter(Mandatory = true)]
        public string Desc
        {
            get { return this._desc; }
            set { this._desc = value; }
        }

        #endregion Parameters

        protected override void ProcessRecord()
        {
            var folder = new Folder(Name, Desc);
            WriteObject(folder);

            base.ProcessRecord();
        }
    }
}
