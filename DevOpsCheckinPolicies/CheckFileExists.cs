using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevOpsCheckinPolicies
{
    [Serializable]
    public class CheckFileExists : PolicyBase
    {
        public CheckFileExistConfig Config { get; set; }
        public override string Description
        {
            get { return "Developers needs to add file describing changes in a specific folder"; }
        }

        public override string InstallationInstructions
        {
            get { return "You need to istall Check File Exists Checkin Policy"; }
        }

        public override string Type
        {
            get { return "Check Files Exist"; }
        }

        public override string TypeDescription
        {
            get { return "This policy will prompt the user to decide whether or not they should be allowed to check in."; }
        }

        public override bool Edit(IPolicyEditArgs args)
        {
            Config = new CheckFileExistConfig();
            using (var form = new CheckFileExistSettings(Config))
            {
                var res = form.ShowDialog(args.Parent);
                if (res == DialogResult.OK)
                {
                    Config = form.Config;
                    return true;
                }
                return false;
            }
        }

        public override PolicyFailure[] Evaluate()
        {
            bool FoundFile = false;
            foreach (PendingChange pc in PendingCheckin.PendingChanges.AllPendingChanges)
            {
                FileInfo file = new FileInfo(pc.LocalOrServerItem.ToString());

                if (file.Name == Config.Filename)
                {
                    if (string.IsNullOrEmpty(Config.location))
                    {
                        FoundFile = true;
                    }
                    else
                    {
                        if (file.Directory.Name == Config.location)
                        {
                            FoundFile = true;
                        }
                        else
                        {
                            FoundFile = false;
                        }
                    }

                }
            }


            if (FoundFile)
            {
                return new PolicyFailure[0];
            }
            else
            {
                return new PolicyFailure[] { new PolicyFailure($"You need to include a document named {Config.Filename},({Config.location})", this) };
            }
        }

        public override void Activate(PolicyFailure failure)
        {
            MessageBox.Show($"Please include {Config.Filename} with your changeset");
        }
        public override void DisplayHelp(PolicyFailure failure)
        {
            MessageBox.Show("This policy helps you to remember to include a specific file with your changeset.", "Prompt Policy Help");
        }
    }
}
