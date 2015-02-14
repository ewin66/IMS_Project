using Common.Helpers;
using LinqDataModel;
using LinqDataModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace Viktor.IMS.Presentation
{
    public partial class BaseForm : Form
    {
        public InputLanguage myCurrentLanguage;
        public ISO9TransliterationProvider convertor;
        public IDataRepository _repository { get; set; }
        public SerialPort _serialPort { get; set; }
        public DataRow LastDataRow = null; //tracks for the PositionChanged event the last row
        public Product CurrentProduct { get; set; }
        //public string activeFormName = "";

        public BaseForm()
        {
            InitializeComponent();
        }
        public InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.Name.ToLower().StartsWith(inputName))
                    return lang;
            }
            return null;
        }
        public void SetKeyboardLayout(InputLanguage layout)
        {
            InputLanguage.CurrentInputLanguage = layout;
        }
        public virtual void textBox_Enter(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("mk"));
        }
        public virtual void Form_Activated(object sender, EventArgs e)
        {
            SetKeyboardLayout(GetInputLanguageByName("mk"));
        }
    }
}
