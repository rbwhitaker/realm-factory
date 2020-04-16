using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starbound.Common.WinForms.Wizards
{
    public partial class Wizard : Form
    {
        private Dictionary<string, object> wizardData;
        private Dictionary<Type, WizardPage> wizardPages;

        public Wizard()
        {
            InitializeComponent();

            wizardData = new Dictionary<string, object>();
        }

        public static Dictionary<string, object> ShowWizard(string title, UserControl header, IEnumerable<WizardPage> pages, Type startPage)
        {
            Wizard wizard = new Wizard();
            int oldHeaderHeight = wizard.headerContainer.Height;
            wizard.Text = title;
            header.Dock = DockStyle.Fill;
            wizard.headerContainer.Controls.Add(header);
            int newHeaderHeight = header.PreferredSize.Height;
            wizard.headerContainer.Height = newHeaderHeight;
            wizard.Height = wizard.Height - oldHeaderHeight + newHeaderHeight;

            wizard.UsePages(pages);
            wizard.ShowPage(startPage);
            if (wizard.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return wizard.wizardData;
            }

            return null;
        }

        private void ShowPage(Type pageName)
        {
            pageContainer.Controls.Clear();
            WizardPage page = wizardPages[pageName];
            page.Dock = DockStyle.Fill;
            pageContainer.Controls.Add(page);
            ActivePage = page;
        }

        private WizardPage activePage;

        private WizardPage ActivePage
        {
            get
            {
                return activePage;
            }
            set
            {
                if (activePage != null)
                {
                    activePage.CanAdvanceStateChanged -= CanAdvanceStateChanged;
                    activePage.NextPageChanged -= NextPageChanged;
                }
                activePage = value;
                if (activePage != null)
                {
                    activePage.CanAdvanceStateChanged += CanAdvanceStateChanged;
                    activePage.NextPageChanged += NextPageChanged;
                }
            }
        }

        public void CanAdvanceStateChanged(object sender, EventArgs e)
        {
            nextButton.Enabled = (sender as WizardPage).CanAdvance;
        }

        private Type nextPage;

        public void NextPageChanged(object sender, EventArgs e)
        {
            nextPage = ActivePage.NextPage;
            if (nextPage == null) { nextButton.Text = "Finish"; }
            else { nextButton.Text = "Next"; }
        }

        private void UsePages(IEnumerable<WizardPage> pages)
        {
            wizardPages = new Dictionary<Type, WizardPage>();
            foreach (WizardPage page in pages)
            {
                wizardPages[page.GetType()] = page;
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            ActivePage.Advance(wizardData);
            if (ActivePage.NextPage == null)
            {
                this.Close();
                return;
            }

            ShowPage(ActivePage.NextPage);
        }
    }
}
