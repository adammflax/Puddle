using System.Management.Automation;
using System.ComponentModel;

namespace HuddleCmdLets
{
    [RunInstaller(true)]
    public class SnapIn : PSSnapIn
    {
        public override string Description
        {
            get { return "description"; }
        }

        public override string Name
        {
            get { return "name"; }
        }

        public override string Vendor
        {
            get { return "huddle"; }
        }
    }
}
