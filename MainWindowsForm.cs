using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace TallerArchivoVectores
{
    public partial class MainWindowsForm : Form
    {
        public class Estudiante {
            public string carnetEst;
            public string nombreEst;
            public double indiceEst;
            public string sexoEst;
        }
        public MainWindowsForm()
        {
            InitializeComponent();

            cargarVectorEstudiantes();
            lbIndiceProm.Text = promedioEstudiantes().ToString("#.##");
            //lbNumEst91.Text = carnetMenoresA91().ToString();
            cargarEstIndice35();
            llenarDataGridEstIndice35();            

        }

        public Estudiante[] vecEstudiantes = new Estudiante[1000];
        public Estudiante[] vecEstSup35;

        public Estudiante newEstudiante = new Estudiante();
        TextReader readFile;
        int indice = 0;
        int numEstIndice35=0;


        private void cargarVectorEstudiantes()
        {

            readFile = new StreamReader("estudiantes.txt");
            string readLine;

            while ((readLine = readFile.ReadLine()) != null)
            {
                string[] datos = readLine.Split(';');

                newEstudiante = new Estudiante();
                newEstudiante.carnetEst = datos[0];
                newEstudiante.nombreEst = datos[1];
                newEstudiante.indiceEst = Convert.ToDouble(datos[2]);
                newEstudiante.sexoEst = datos[3];

                vecEstudiantes[indice] = newEstudiante;
                indice++;
            }
            vecEstSup35 = new Estudiante[indice];
        }

        private void mostrarEstudiantes()
        {
            for (int i = 0; i < indice; i++)
            {
                MessageBox.Show($"{vecEstudiantes[i].carnetEst} {vecEstudiantes[i].nombreEst} {vecEstudiantes[i].indiceEst} {vecEstudiantes[i].sexoEst}");
            }
        }

        private double promedioEstudiantes()
        {
            double promedio, sumaIndice = 0;

            for (int i = 0; i < indice; i++)
            {
                sumaIndice += vecEstudiantes[i].indiceEst;
            }
            promedio = sumaIndice / indice;
            return promedio;
        }

        private int carnetMenoresA91(int carnetNum) {
            int numEst = 0;
            for (int i = 0; i < indice; i++) {
                string[] carnet = vecEstudiantes[i].carnetEst.Split('-');
                if (Convert.ToInt32(carnet[0]) < carnetNum) {
                    int posicion = dtg1.Rows.Add();
                    dtg1.Rows[posicion].Cells[0].Value = vecEstudiantes[i].carnetEst;
                    dtg1.Rows[posicion].Cells[1].Value = vecEstudiantes[i].nombreEst;
                    numEst++;
                }
            }
            lbNumEst91.Text = numEst.ToString();
            return numEst;
        }

        private int carnetMenoresA91(string carnetNum)
        {
            int numEst = 0;
            lbNumEst91.Text = "0";
            return numEst;
        }

        private void BtnBuscarEst_Click(object sender, EventArgs e)
        {
            if (txtCarnetBuscar.Text != "")
            {
                int encontrado = 0;
                string carnetEst = txtCarnetBuscar.Text;
                for (int i = 0; i < indice; i++)
                {
                    if (vecEstudiantes[i].carnetEst == carnetEst) 
                    {
                        lbNomEst.Text = vecEstudiantes[i].nombreEst;
                        lbIndiceEst.Text = vecEstudiantes[i].indiceEst.ToString();
                        encontrado++;
                    }
                }

                if (encontrado == 0)
                {
                    MessageBox.Show("No se ha encontrado el estudiante");
                }
            }
            else {
                MessageBox.Show("No has dijitado ningun carnet");
            }

        }
         
        private void cargarEstIndice35(){
            
            for (int i = 0; i < indice; i++) {
               if (vecEstudiantes[i].indiceEst > 3.5 && vecEstudiantes[i].sexoEst == "M") {
                    vecEstSup35[numEstIndice35] = vecEstudiantes[i];
                    numEstIndice35++;
                }
            }
            lbNumEst35.Text = numEstIndice35.ToString();

            if (numEstIndice35 == 0) {
                comBdtg2.Enabled = false;
            }
        }


        private void llenarDataGridEstIndice35() {

            for (int i = 0; i < numEstIndice35; i++)
            {
                int posicion = dtg2.Rows.Add();
                dtg2.Rows[posicion].Cells[0].Value = vecEstSup35[i].carnetEst;
                dtg2.Rows[posicion].Cells[1].Value = vecEstSup35[i].nombreEst;
                dtg2.Rows[posicion].Cells[2].Value = vecEstSup35[i].indiceEst;
            }

        }

        private void ordenarVector(Estudiante[] vector, int cantElem, int ordenarPor) {
            string carnet, nombre;
            double indc;
            Estudiante estAux = new Estudiante();
            
            switch (ordenarPor) {

                case 0:
                    //CarnetAscen
                    for (int i = 0; i < cantElem - 1; i++)
                    {
                        for (int j = 0; j < cantElem - 1; j++)
                        {
                            if (vector[j].carnetEst.CompareTo(vector[j+1].carnetEst) > 0)
                            {
                                estAux = vector[j];
                                vector[j] = vector[j + 1];
                                vector[j + 1] = estAux;
                            }
                        }
                    }
                    break;
                
                case 1:
                    //CarnetDesc
                    for (int i = 0; i < cantElem - 1; i++)
                    {
                        for (int j = 0; j < cantElem - 1; j++)
                        {
                            if (vector[j].carnetEst.CompareTo(vector[j + 1].carnetEst) < 0)
                            {
                                estAux = vector[j];
                                vector[j] = vector[j + 1];
                                vector[j + 1] = estAux;
                            }
                        }
                    }
                    break;

                case 2:

                    //ApeAscen A-Z
                    for (int i = 0; i < cantElem - 1; i++)
                    {
                        for (int j = 0; j < cantElem - 1; j++)
                        {
                            string[] datoActual = vector[j].nombreEst.Split(' ');
                            string[] datoSiguiente = vector[j+1].nombreEst.Split(' ');

                            if (datoActual[1].ToUpper().Substring(0,1).CompareTo( datoSiguiente[1].ToUpper().Substring(0, 1) ) > 0) {
                                estAux = vector[j];
                                vector[j] = vector[j + 1];
                                vector[j + 1] = estAux;
                            }
                        }
                    }
                    break;
                
                case 3:
                    //ApeDesc Z-A
                    for (int i = 0; i < cantElem - 1; i++)
                    {
                        for (int j = 0; j < cantElem - 1; j++)
                        {
                            string[] datoActual = vector[j].nombreEst.Split(' ');
                            string[] datoSiguiente = vector[j + 1].nombreEst.Split(' ');

                            if (datoActual[1].ToUpper().Substring(0, 1).CompareTo(datoSiguiente[1].ToUpper().Substring(0, 1)) < 0)
                            {
                                estAux = vector[j];
                                vector[j] = vector[j + 1];
                                vector[j + 1] = estAux;
                            }
                        }
                    }
                    break;

                case 4:
                    //IndiceAscen
                    for (int i = 0; i < cantElem - 1; i++)
                    {
                        for (int j = 0; j < cantElem - 1; j++)
                        {

                            if (vector[j].indiceEst < vector[j + 1].indiceEst)
                            {
                                estAux = vector[j];
                                vector[j] = vector[j + 1];
                                vector[j + 1] = estAux;
                            }
                        }
                    }
                    break;

                case 5://IndiceDesc
                    for (int i = 0; i < cantElem - 1; i++)
                    {
                        for (int j = 0; j < cantElem - 1; j++)
                        {

                            if (vector[j].indiceEst > vector[j + 1].indiceEst)
                            {
                                estAux = vector[j];
                                vector[j] = vector[j + 1];
                                vector[j + 1] = estAux;
                            }
                        }
                    }
                    break;

                
            }
            
            if (vector == vecEstudiantes)
            {
                dtg1.Rows.Clear();
                carnetMenoresA91(Convert.ToInt32(lbCarnetNum.Text));
            }
            else {
                dtg2.Rows.Clear();
                llenarDataGridEstIndice35();
            }
            

        }

        private void ComBdtg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ordenarVector(vecEstudiantes, indice, comBdtg1.SelectedIndex);
        }

        private void ComBdtg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ordenarVector(vecEstSup35, numEstIndice35, comBdtg2.SelectedIndex);
        }


        private void Dtg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCarnetBuscar.Text = dtg1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void BtnBuscarCarnets_Click(object sender, EventArgs e)
        {

            dtg1.Rows.Clear();
            lbCarnetNum.Text = txtBuscarCarnets.Text;

            int dtg1EstNum;
            if (txtBuscarCarnets.Text == "")
            {
                dtg1EstNum = carnetMenoresA91(lbCarnetNum.Text);
            }
            else {
                dtg1EstNum = carnetMenoresA91(Convert.ToInt32(lbCarnetNum.Text));
            }
            
            if (dtg1EstNum != 0)
            {
                comBdtg1.Enabled = true;
            }
            else {
                comBdtg1.Enabled = false;
            }
        }


        private void TxtBuscarCarnets_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else {
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else {
                    e.Handled = true;
                }
                
            }
        }
    }
}
