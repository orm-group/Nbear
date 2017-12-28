using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NBear.Common.Design;

namespace NBear.Tools.EntityDesignToEntity
{
    public partial class AdvOptForm : Form, IAdvOpt
    {
        public AdvOptForm()
        {
            InitializeComponent();
        }

        private void checkEnableAdvOpt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEnableAdvOpt.Checked)
            {
                checkSelectAll.Enabled = true;
                listEntities.Enabled = true;
            }
            else
            {
                checkSelectAll.Enabled = false;
                listEntities.Enabled = false;
            }
        }

        private void checkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSelectAll.Checked)
            {
                for (int i = 0; i < listEntities.Items.Count; i++)
                {
                    listEntities.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < listEntities.Items.Count; i++)
                {
                    listEntities.SetItemChecked(i, false);
                }
            }
        }

        public bool EnableAdvOpt
        {
            get
            {
                return checkEnableAdvOpt.Checked;
            }
        }

        public bool IsEntityEnabled(string name)
        {
            if (!EnableAdvOpt)
            {
                return true;
            }

            for (int i = 0; i < listEntities.Items.Count; i++)
            {
                if (listEntities.GetItemChecked(i) && listEntities.Items[i].ToString() == name)
                {
                    return true;
                }
            }
            return false;
        }

        public void RefreshEntities(Type[] types)
        {
            checkEnableAdvOpt.Checked = false;
            listEntities.Items.Clear();
            foreach (Type type in types)
            {
                if (typeof(NBear.Common.Design.Entity).IsAssignableFrom(type) && typeof(NBear.Common.Design.Entity) != type && CodeGenHelper.GetEntityAttribute<DraftAttribute>(type) == null)
                {
                    listEntities.Items.Add(type.Name);
                }
            }
        }
    }
}