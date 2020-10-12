using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


namespace PdfConvert
{
    public partial class Form1 : Form
    {
        string doc_Xml = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            //lista donde guardare los datos que necesito de cada libro
            List<Libro> coleccion = new List<Libro>();

            //Inicializamos el directorio inicial del InputFile
            string ruta_doc = "C:/Users/julio/OneDrive/Escritorio/";
            openFileDialog1.InitialDirectory= ruta_doc;

            //Elegimos el titulo de la ventana exploradora de documentos
            openFileDialog1.Title = "Carga tu documento XML";

            //openFileDialog1.AddExtension = true;
            //openFileDialog1.DefaultExt = ".xml";

            openFileDialog1.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                doc_Xml = openFileDialog1.FileName;

                string text_doc = File.ReadAllText(doc_Xml);

                // se Carga todo el XML en el objeto libro
                //XDocument libroRaiz = XDocument.Load(openFileDialog1.FileName, LoadOptions.None);
                //usa éste siguiente para cargar desde texto (string) en vez de un archivo

                XDocument doc = XDocument.Parse(text_doc);

                try
                {
                    //Obtener objeto librosEjemplo
                    XElement catalogo = doc.Element("catalog");

                    //Obtener lista de libros dentro de librosEjemplo
                    IEnumerable<XElement> libros = catalogo.Descendants("book");

                    //has un foreach y por cada uno haz lo que tengas que hacer
                    foreach (XElement libro in libros)
                    {
                        XAttribute atributo_id = libro.Attribute("id");
                        string id_libro = atributo_id.Value;

                        int edad_autor = Convert.ToInt32(libro.Element("author").Attribute("edad").Value);

                        string autor = libro.Element("author").Value;
                        string titulo = libro.Element("title").Value;

                        coleccion.Add(new Libro { id = id_libro, titulo = titulo, autor = autor, edadAutor = edad_autor });

                        richTextBox1.Text = richTextBox1.Text + " * " + id_libro + " " + titulo + " " + autor + " " + edad_autor + "\n";
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("error localizado: "+ err.Message);
                    MessageBox.Show("error localizado: " + err.Message, "ERROR");
                }

                

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
