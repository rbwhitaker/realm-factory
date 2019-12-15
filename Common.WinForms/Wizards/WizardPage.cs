using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starbound.Common.WinForms.Wizards
{
    /// <summary>
    /// A page in the Wizard, which presents certain options or requests certain
    /// information from the user.  Each page has the ability to indicate, on it's own,
    /// whether the page is complete or not, which the wizard will use to know if
    /// the Next/Finish button is enabled or not, as well as what page should come up
    /// next, however it sees fit.  (Typically, based on the user's input on the current
    /// page.)
    /// </summary>
    public partial class WizardPage : UserControl
    {
        /// <summary>
        /// Creates an instance of a WizardPage.
        /// </summary>
        public WizardPage()
        {
            InitializeComponent();

            canAdvance = false;
        }

        /// <summary>
        /// Gives the page a chance to initialize, providing access to the data
        /// that the wizard is tracking.
        /// </summary>
        /// <param name="wizardData"></param>
        public virtual void Initialize(Dictionary<string, object> wizardData)
        {
        }
        
        /// <summary>
        /// Stores whether the user can advance to the next page yet or not.
        /// </summary>
        private bool canAdvance;

        /// <summary>
        /// Gets or sets whether the current page is "complete", and the user
        /// can advance yet (either to another page, or the end of the wizard,
        /// as determined by the NextPage property).
        /// </summary>
        public bool CanAdvance
        {
            get
            {
                return canAdvance;
            }
            set
            {
                canAdvance = value;
                OnAdvanceStateChanged();
            }
        }

        /// <summary>
        /// An event that is raised to indicate that whether 
        /// </summary>
        public event EventHandler CanAdvanceStateChanged;

        /// <summary>
        /// Raises the CanAdvanceStateChanged event, letting event handlers know that
        /// the panel has changed its mind about whether the user can advance through
        /// the wizard or not yet.
        /// </summary>
        public void OnAdvanceStateChanged()
        {
            if (CanAdvanceStateChanged != null)
            {
                CanAdvanceStateChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Stores the next page to be shown.  A value of null indicates that the
        /// wizard should end.
        /// </summary>
        private Type nextPage;

        /// <summary>
        /// Indicates which page should be shown next.  A value of null indicates
        /// that the wizard should end.
        /// </summary>
        public Type NextPage
        {
            get
            {
                return nextPage;
            }
            set
            {
                nextPage = value;
                OnNextPageChanged();
            }
        }

        /// <summary>
        /// An event that is raised whenever the next page in the wizard is changed.
        /// </summary>
        public event EventHandler NextPageChanged;

        /// <summary>
        /// Called when the NextPage property is changed, raising the NextPageChanged
        /// event.
        /// </summary>
        public void OnNextPageChanged()
        {
            if (NextPageChanged != null)
            {
                NextPageChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when the user presses the forward button.  This allows the
        /// panel one last chance to add data to the wizard data before advancing.
        /// Additionally, this allows the page to indicate which page should be 
        /// shown next, by returning the key of that page.
        /// If null is returned, the wizard will end.
        /// </summary>
        /// <param name="wizardData"></param>
        public virtual void Advance(Dictionary<string, object> wizardData)
        {
        }
    }
}
