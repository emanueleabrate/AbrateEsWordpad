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
using Microsoft.VisualBasic;
using clsFile_ns;


namespace AbrateEsWordpad
{
    public partial class frmWordpad : Form
    {

        private string fileName = null;
        private bool modificato = false;
        public frmWordpad()
        {
            InitializeComponent();
        }

        private void frmWordpad_Load(object sender, EventArgs e)
        {
            
           
        }


        private void nuovoToolStripButton_Click(object sender, EventArgs e)
        {
            nuovo();
        }

        private void nuovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuovo();
        }

        private bool nuovo()
        {
            bool annulla = false;
            annulla = controllaModificato();
            if (!annulla)
            {
               
                rtb.Clear();
                modificato = false;
                fileName = null;
                this.Text = null;

            }

            return annulla;
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
            if(!nuovo())
                this.Close();
        }

        private void salvaToolStripButton_Click(object sender, EventArgs e)
        {
            salva();
        }

        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salva();
        }

        private void salva()
        {
            Console.WriteLine(fileName);
           
                if (!string.IsNullOrEmpty(fileName))
                    rtb.SaveFile(fileName);
                else
                    salvaconNome();
        }

        private void salvaconnomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salvaconNome();
        }

        private void salvaconNome()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;

            sfd.Filter = "File wordpad (*.rtf)|*.rtf";
            sfd.Title = "Workpad - Salva con nome";
            DialogResult ris = sfd.ShowDialog();
            if (ris == DialogResult.OK)
            {
                fileName = sfd.FileName;
                rtb.SaveFile(sfd.FileName);
                this.Text = sfd.FileName.ToString();
            }

          
        }

        private bool controllaModificato()
        {
            bool annulla = false;
            if (modificato)
            {
                
                string nomeFile = getFileNameRelativo();
                DialogResult ris;
                ris = MessageBox.Show("Salvare le modifiche a " + nomeFile, "File di testo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (ris == DialogResult.Yes)
                    salva();
                else if (ris == DialogResult.Cancel)
                    annulla = true;

            }

            return annulla;
        }

        private string getFileNameRelativo()
        {
            string s = "";
            if (!string.IsNullOrEmpty(fileName))
            {
               
                int pos = fileName.LastIndexOf('\\');
                s = fileName.Substring(pos + 1);
            }
            else
                s = " senza nome ";

            return s;
        }

        private void apriToolStripButton_Click(object sender, EventArgs e)
        {
            bool annulla = false;
            annulla = controllaModificato();
            if(!annulla)
                apri();
        }

        private void apriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool annulla = false;
            annulla = controllaModificato();
            if (!annulla)
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
                fileName = ofd.FileName;
                rtb.LoadFile(ofd.FileName);
                this.Text = ofd.FileName;
            }

        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
            if (rtb.Text.Length > 0)
                modificato = true;
            else
                modificato = false;
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

        private void ImgToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "(*.png)|*.png|(*.jpg)|*.jpg|(*.gif)|*.gif";

            DialogResult ris;

            ris = ofd.ShowDialog();

            if(ris == DialogResult.OK)
            {
                Image img = Image.FromFile(ofd.FileName);
                Clipboard.SetImage(img);
                rtb.Paste();
            }
            
        }

        private void FindToolStripButton_Click(object sender, EventArgs e)
        {
            string mex = Interaction.InputBox("Che parola vuoi cercare?", "Cerca");
            int pos = 0;

            if (!string.IsNullOrEmpty(mex))
            {

                while ((pos = rtb.Text.IndexOf(mex, pos, StringComparison.CurrentCultureIgnoreCase)) != -1)
                {
                    Console.WriteLine(pos.ToString());
                    rtb.Select(pos, mex.Length);
                    rtb.SelectionBackColor = Color.LightBlue;
                    pos++;
         
                }
                
            }
            else
            {
                MessageBox.Show("Inserire un valore valido ");
            }

        }
    }
}
