using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using clsFile_ns;
using static System.Net.Mime.MediaTypeNames;

namespace AbrateEsWordpad
{
    public partial class frmWordpad : Form
    {

        clsFile fileManager;
        public frmWordpad()
        {
            InitializeComponent();
        }

        private void frmWordpad_Load(object sender, EventArgs e)
        {
            fileManager = new clsFile();
            fileManager.Modificato = true;
           
        }


        private void nuovoToolStripButton_Click(object sender, EventArgs e)
        {
            nuovo();
        }

        private void nuovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuovo();
        }

        private void nuovo()
        {
            bool annulla = false;
            annulla = controllaModificato();
            if (!annulla)
            {
                
                rtb.Clear();
                fileManager.Modificato = false;

            }
            
        }
        private void tagliaToolStripButton_Click(object sender, EventArgs e)
        {
            taglia();
        }
        private void tagliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taglia();
        }
        private void taglia()
        {
            rtb.Cut();
        }

        private void copiaToolStripButton_Click(object sender, EventArgs e)
        {
            copia();
        }
        private void copiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copia();
        }
        private void copia()
        {
            rtb.Copy();
        }

        private void incollaToolStripButton_Click(object sender, EventArgs e)
        {
            incolla();
        }
        private void incollaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            incolla();
        }
        private void incolla()
        {
            rtb.Paste();
        }

        private void AnnullaToolStripButton_Click(object sender, EventArgs e)
        {
            annulla();
        }

        private void annullaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            annulla();
        }

        private void annulla()
        {
            rtb.Undo(); 
        }

        private void RipristinaToolStripButton_Click(object sender, EventArgs e)
        {
            ripristina();
        }

        private void ripristinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ripristina();
        }
        private void ripristina()
        {
            rtb.Redo();
        }

        private void AlignSxToolStripButton_Click(object sender, EventArgs e)
        {
            rtb.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void AlignCenterToolStripButton_Click(object sender, EventArgs e)
        {
            rtb.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void AlignRxToolStripButton_Click(object sender, EventArgs e)
        {
            rtb.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void ColorToolStripButton_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
                rtb.SelectionColor = cd.Color;
        }

        private void FontToolStripButton_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();

            if (fd.ShowDialog() == DialogResult.OK)
                rtb.SelectionFont = fd.Font;
        }

        private void ElencoPuntToolStripButton_Click(object sender, EventArgs e)
        {

            rtb.SelectionBullet = true;
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void salvaToolStripButton_Click(object sender, EventArgs e)
        {
            salva(rtb.Text);
        }

        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salva(rtb.Text);
        }

        private void salva(string testo)
        {

            if (fileManager.Modificato)
                if (fileManager.Filename != "")
                    rtb.SaveFile(fileManager.Filename);
                else
                    salvaconNome(this.Text);
        }

        private void salvaconnomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salvaconNome(rtb.Text);
        }

        private void salvaconNome(string testo)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;

            sfd.Filter = "File wordpad (*.rtf)|.rtf";
            sfd.Title = "Workpad - Salva con nome";
            DialogResult ris = sfd.ShowDialog();
            if (ris == DialogResult.OK)
            {
                rtb.SaveFile(sfd.FileName);
            }

            this.Text = sfd.FileName.ToString();
        }

        private bool controllaModificato()
        {
            bool annulla = false;
            if (fileManager.Modificato)
            {
                //chiedo se si vuole salvare il documento aperto 
                string nomeFile = fileManager.getFileName();
                DialogResult ris;
                ris = MessageBox.Show("Salvare le modifiche a " + nomeFile, "File di testo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (ris == DialogResult.Yes)
                    fileManager.salva(rtb.Text);
                else if (ris == DialogResult.Cancel)
                    annulla = true;

            }

            return annulla;
        }

        private void apriToolStripButton_Click(object sender, EventArgs e)
        {
            apri();
        }

        private void apriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            apri();
        }

        private void apri()
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;

            ofd.Filter = "File wordpad (*.rtf)|.rtf";
            ofd.Title = "Workpad - Apri";
            DialogResult ris = ofd.ShowDialog();
            if (ris == DialogResult.OK)
            {
                rtb.LoadFile(ofd.FileName);
            }

        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
            if (rtb.Text.Length > 0)
                fileManager.Modificato = true;
            else
                fileManager.Modificato = false;
        }

        private void BoldToolStripButton_Click(object sender, EventArgs e)
        {

            Font fontNew, fontOld;

            fontOld = rtb.SelectionFont;
            if(fontOld.Bold)
                fontNew = new Font(fontOld, fontOld.Style & ~FontStyle.Bold);
            else
                fontNew = new Font(fontOld, fontOld.Style | FontStyle.Bold);

            rtb.SelectionFont = fontNew;

        }

        private void ItalicToolStripButton_Click(object sender, EventArgs e)
        {
            Font fontNew, fontOld;

            fontOld = rtb.SelectionFont;
            if (fontOld.Italic)
                fontNew = new Font(fontOld, fontOld.Style & ~FontStyle.Italic);
            else
                fontNew = new Font(fontOld, fontOld.Style | FontStyle.Italic);

            rtb.SelectionFont = fontNew;
        }

        private void UnderlineToolStripButton_Click(object sender, EventArgs e)
        {
            Font fontNew, fontOld;

            fontOld = rtb.SelectionFont;
            if (fontOld.Underline)
                fontNew = new Font(fontOld, fontOld.Style & ~FontStyle.Underline);
            else
                fontNew = new Font(fontOld, fontOld.Style | FontStyle.Underline);

            rtb.SelectionFont = fontNew;
        }
    }
}
