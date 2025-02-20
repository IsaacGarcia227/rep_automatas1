using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace practica_7
{
    public partial class Form1 : Form

    {
        public object Token { get; private set; } 
        int token = -1;
        string tokenString, caso; 
        
        char auxiliar; 
        string arregloDeCaracteres = "";
        Dictionary<char, int> simboloToken = new Dictionary<char, int>
        {
            {'@',64}, {'#',35}, {'$',36}, {'^',94},{'(',40}, {')',41},
            {'[',91}, {']',93}, {'{',123}, {'}',125}, {';', 59}
        };

        Dictionary<char, int> operadorAritmeticoToken = new Dictionary<char, int>
        {
            {'+',43}, {'-', 45 },{'*', 42 },{'/',47  },{'%',37},{'=',61}
        };

        Dictionary<string, int> operadorLogicoToken = new Dictionary<string, int>
        {
               {"&&",200},{"||",201},{ "!",202}
        };

        Dictionary<string, int> operadorRelacionalToken = new Dictionary<string, int>
        {
        { "<",220 },{ ">",221 },{ ">=",222 },{ "<=",223 },{ "==",224 },{ "!=",225 },
        };

        Dictionary<string, int> palabraReservadaToken = new Dictionary<string, int>
        {
            {"abstract",250}, {"as",251 },{"base",253},{"break",254},
            {"case",256},{"catch",257},{"checked",258},{"class",259},{"const",260},
            {"continue",261},{"default",263},{"delegate",264},{"do",265},
            {"else",267 },{ "enum",268 },{"event",269 },{ "explicit",270 },
            {"extern",271},{"false",272},{"finally",273},{"fixed",274 },{"for",276},{"foreach",277},{"goto",278},
            {"if",279},{"implicit",280},{"in",281},{"interface",283},{"internal",284},{"is",285},{"lock",286},
            {"namespace",288}, { "new", 289 }, { "null", 290 }, { "operator", 292 },
            {"out", 293 }, { "override", 294}, { "params", 295 }, { "private", 296 },{ "printf", 297 },{ "protected", 298 },
            { "public", 299 },{ "readonly", 300 },{ "ref", 301 },{ "return", 302 }, {"sealed", 304 },
            { "sizeof",306}, {"stackalloc",307}, {"static",308}, {"struct",310},
            { "switch", 311 }, { "this", 312 }, { "scanf", 313 }, { "throw", 314 }, { "true", 315 }, { "try", 316 },
            { "typeof", 317 }, { "unchecked", 320 }, { "unsafe", 321 },
            { "using", 323 },{ "virtual", 324 },{ "void", 325 }, {"volatile",326}, {"while",327}, {"include",328}
        };
       
        Dictionary<string, int> tipoDeDatoToken = new Dictionary<string, int> 
        {
         { "int", 400 }, { "float", 401 }, { "double", 402 }, { "char", 403 }, { "string", 404 }, { "bool", 405 },
         { "decimal", 406 }, { "long", 407 }, { "short", 408 }, { "byte", 409 }, { "sbyte", 410 }, { "uint", 411 },
         { "ulong", 412 }, { "ushort", 413 }, { "object", 414 }, { "dynamic", 415 } };


        public Form1()
        {
            InitializeComponent();
            compilarToolStripMenuItem.Enabled = false;

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pantalla.Clear();
            archivo = null;
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog VentanaAbrir = new OpenFileDialog();
            if (VentanaAbrir.ShowDialog() == DialogResult.OK)
            {
                archivo = VentanaAbrir.FileName;
                using (StreamReader Leer = new StreamReader(archivo))
                {
                    pantalla.Text = Leer.ReadToEnd();
                }
            }
            //Form1.ActiveForm.Text = "Mi compilador - " + archivo;
            compilarToolStripMenuItem.Enabled  = true;
        }


        private void guardar() 
        {
            SaveFileDialog VentanaGuardar = new SaveFileDialog();
            if (archivo != null)
            {
                using (StreamWriter Escribir = new StreamWriter(archivo))
                {
                    Escribir.Write(pantalla.Text);
                }
            }

            else
            {
                if (VentanaGuardar.ShowDialog() == DialogResult.OK)
                {
                    archivo = VentanaGuardar.FileName;
                    using (StreamWriter Escribir = new StreamWriter(archivo))
                    {
                        Escribir.Write(pantalla.Text);
                    }

                }

            }
            //Form1.ActiveForm.Text = "Mi Compilador - " + archivo;

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardar();

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog VentanaGuardar = new SaveFileDialog();
            if (VentanaGuardar.ShowDialog() == DialogResult.OK)
            {
                archivo = VentanaGuardar.FileName;
                using (StreamWriter Escribir = new StreamWriter(archivo))
                {
                    Escribir.Write(pantalla.Text);
                }
            }
        }

        private void Cadena()
        {
            do
            {
                i_caracter = Leer.Read();
                if (i_caracter == 10)
                {
                    Numero_linea++;
                } 

            } while (i_caracter != 34 && i_caracter != -1);
            if (i_caracter == -1) Error(39);
        }

        private void caracter()
        {
            i_caracter = Leer.Read();   //Programar donde los casos el caracter se imprime '\n', '\r', etc
            if (i_caracter == 10 || i_caracter == 13)
            {
                Numero_linea++;
            }
            i_caracter = Leer.Read();
            if (i_caracter != 39) 
                Error(39);
        }
        private bool esLetra(int letra) {

            if (letra >= 65 && letra <= 90 || letra >= 97 && letra <= 122)
            {
                return true;
            }
            else {

                return false;
            }

        }
        private void identificarLibreria()     //
        {
            int contadorLetras = 0;
            do
            {
                i_caracter = Leer.Read();
                if (esLetra(i_caracter))
                {
                    contadorLetras++;
                }
                else
                {
                    break;
                }
                
            } while (esLetra(i_caracter)  && i_caracter != -1);

            if ((i_caracter == 61 || i_caracter == 10) && contadorLetras == 0)  //Si es token relacional
            {
                string operador = "<";
                
                if (i_caracter == 61)
                {
                    operador = operador + "=";
                }
               
                findTokenRelacional(operador);
                i_caracter = 0;
            }
            else
            {
                if (i_caracter != 62)
                {
                    N_error ++;
                    Error(62);
                    Escribir.Write("Error al importar libreria\n");
                    Escribirtok.Write("Error al importar libreria\n");
                }
            }

            if (i_caracter == 62 && contadorLetras > 0)
            {
                Escribir.Write("Libreria\n");
                Escribirtok.Write("Libreria \n");
            }

        }

        private void Error(int i_caracter)
        {
            Rtbx_salida.AppendText("Error lexico " + i_caracter + ", linea " + Numero_linea + "\n");
            N_error++;

        }
        private bool Comentario()
        {
            i_caracter = Leer.Read();
            switch (i_caracter)
            {
                case 47:
                    do
                    {
                        i_caracter = Leer.Read();
                    } while (i_caracter != 10);
                    return true;
                case 42:
                    do
                    {
                        do
                        {
                            i_caracter = Leer.Read();
                            if (i_caracter == 10) { Numero_linea++; }
                        } while (i_caracter != 42 && i_caracter != -1);
                        i_caracter = Leer.Read();
                    } while (i_caracter != 47 && i_caracter != -1);
                    if (i_caracter == -1) { Error(i_caracter); }
                    i_caracter = Leer.Read();
                    return true;
                default: return false;
            }
        }
        private void almacenarPalabra(int letraInt)
        {
            char letra = (char)letraInt;
            arregloDeCaracteres = arregloDeCaracteres + letra.ToString();
            do
            {
                i_caracter = Leer.Read();
                if (i_caracter == 10)
                {
                    Numero_linea++;
                    break;
                }
                if (esLetra(i_caracter))
                {
                    letra = (char)i_caracter;
                    arregloDeCaracteres = arregloDeCaracteres + letra.ToString();
                }
                else
                {
                    if(i_caracter != 32)
                    {
                        Error(i_caracter);
                        Tipo_caracter(i_caracter);
                    }
                    break;
                }

            } while (i_caracter != -1);

                                
        }
        
        private void busquedaInterna(string cadena)
        {
            if (palabraReservadaToken.TryGetValue(cadena, out token))
            {
                Escribir.Write("Palabra Reservada \n");
                Escribirtok.Write(token.ToString() + " - Palabra reservada "  + "\n");
                token = -1;
                arregloDeCaracteres = "";

            }
            else if (tipoDeDatoToken.TryGetValue(cadena, out token))
            {
                Escribir.Write("Tipo de dato \n");
                Escribirtok.Write(token.ToString() + " - Tipo de dato " + " \n");
                token = -1;
                arregloDeCaracteres = "";
            }else
            {   
                if (cadena != "")
                {
                    Escribir.Write("Identificador \n");
                    Escribirtok.Write("25 - Identificador " + "\n");
                    token = -1;
                    arregloDeCaracteres = "";
                }
                token = -1;
                arregloDeCaracteres = "";
            }
        }
            
        private char Tipo_caracter(int caracter)
        {
            if (caracter >= 65 && caracter <= 90 || caracter >= 97 && caracter <= 122) {

                 
                return 'x'; 
            }
            else
            {
                if (caracter >= 48 && caracter <= 57) { return 'd'; }
                else
                {
                    switch (caracter)
                    {
                        case  0: return 'n';   //Null
                        case 10: return 'n';   //Salto de linea
                        case 13: return 'n';   //Salto de linea
                        case 34: return '"';
                        case 39: return 'c';   //apostrofe
                        case 64: return 's';   //Simbolo
                        case 123:return 's';
                        case 59: return 's';
                        case 125:return 's';
                        case 91: return 's';
                        case 93: return 's';
                        case 40: return 's';
                        case 41: return 's';
                        case 36: return 's';
                        case 94: return 's';
                        case 35: return 's';
                        case 60: return 'L'; // L mayúscula para identificar librerias
                        case 37: return 'A'; // A para identificar operador Aritmetico
                        case 42: return 'A';
                        case 43: return 'A';
                        case 45: return 'A';
                        case 47: return 'A';
                        case 61: return 'A';   //(=)
                        case 33: return 'Q';   //Q para identificar operadores logicos
                        case 38: return 'Q';
                        case 124:return 'Q';
                        case 62: return 'R';   //(>) R para identificar operadores relacionales
                        case 92: return 'R';   //(<)
                        
                        case 32: return 'W';   
                        
                        default: return 'e';      //si no es de los casos anteriores, error lexico               
                    };
                }
            }
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void aSintacticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardar();
            archivobak = archivo.Remove(archivo.Length - 1) + "bak";
            EscribirBak = new StreamWriter(archivobak);
            N_error = 0;
            Numero_linea = 1;
            analisisSintactico();
            EscribirBak.Close();
        }
        private int contarApariciones(List<string> codigoFiltrado, string[] lista)
        {

            int contador = 0;
            for (int i = 0; i < codigoFiltrado.Count; i++)
            {
                for (int j = 0; j < lista.Length; j++)
                {
                    string palReservada = lista[j];
                    string elemento = codigoFiltrado[i];
                    if (palReservada == elemento)
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }
      
        private List<string> limpiarCodigo(string codigo)
        {
            string[] lineas = codigo.Split(new[] { '\n', '\t', ' ' }, StringSplitOptions.None);
            List<string> elementos = new List<string>(lineas);
            List<string> elementosPuros = new List<string>();
            for (int i = 0; i < elementos.Count; i++)
            {
                String elemento = elementos[i];
                if (!string.IsNullOrWhiteSpace(elementos[i]))//si no es espacio blanco
                {
                    elementosPuros.Add(elemento);
                }

            }
            Console.WriteLine("Elementos " + elementosPuros.Count);

            return elementosPuros;
        }
        private void findTokenRelacional (string operador)
        {
            
            token = operadorRelacionalToken[operador];
               
            Escribir.Write("Operador Relacional \n");
            Escribirtok.Write(token.ToString() + " - Operador Relacional" + "\n");
            
        }
        private void compilarSolucionToolStripMenuItem_Click(object sender, EventArgs e) // A. Lexico
        {
            guardar();
            N_error = 0;
            Numero_linea = 1;
            
            archivotok = archivo.Remove(archivo.Length - 1) + "tok";
            Escribirtok = new StreamWriter(archivotok);
            

            archivobak = archivo.Remove(archivo.Length - 1) + "back";
            Escribir = new StreamWriter(archivobak);
            Leer = new StreamReader(archivo);
            Escribir.Write("Hola");
            
            do
            {
                
                i_caracter = Leer.Read(); //la funcion read del objeto leer me regresa el valor ascii de cada elemento de mi archivo.
                char caso = Tipo_caracter(i_caracter);
                switch (caso)
                {
                    case 'A':
                        busquedaInterna(arregloDeCaracteres);
                        char operadorAritmetico = (char)i_caracter;
                        token = operadorAritmeticoToken[operadorAritmetico];

                        Escribir.Write("Operador Aritmetico \n");
                        Escribirtok.Write(token.ToString() + " - Operador Aritmetico" + "\n");
                        
                        break;

                    case 'Q':
                        if (i_caracter == 33)
                        {
                            char operadorChar = (char)i_caracter;
                            i_caracter = Leer.Read();
                            if (i_caracter == 61)
                            {
                                string operador = operadorChar.ToString();
                                operadorChar = (char)i_caracter;
                                operador += operadorChar.ToString();
                                token = operadorRelacionalToken[operador];
                                Escribir.Write("Operador Relacional \n");
                                Escribirtok.Write(token.ToString() + " - Operador Relacional" + "\n");
                                break;
                            }
                            //char operadorR = (char)i_caracter;
                            token = operadorLogicoToken[operadorChar.ToString()];
                            Escribir.Write("Operador Logico \n");
                            Escribirtok.Write(token.ToString() + " - Operador Logico" + "\n");
                            break;
                        }
                        i_caracter = Leer.Read();
                        if (i_caracter == 38 || i_caracter == 124)
                        {
                            char operadorLogico = (char)i_caracter;
                            token = operadorLogicoToken[operadorLogico.ToString()];
                            Escribir.Write("Operador Logico \n");
                            Escribirtok.Write(token.ToString() + " - Operador Logico" + "\n");
                            break;
                        }                    
                                            
                        break;

                    case 'R':
                        busquedaInterna(arregloDeCaracteres);
                        auxiliar = (char)i_caracter;//convertir el valor ascii de i_caracter a tipo char                        
                        if (i_caracter == 62)
                        {
                            findTokenRelacional(auxiliar.ToString());
                            break;
                        }
                        if (i_caracter == 60)
                        {
                            findTokenRelacional(auxiliar.ToString());
                            break;
                        }
                        break;

                    /*case 'l': Escribir.Write("letra\n");
                        Escribirtok.Write(i_caracter.ToString()+ "\n"); 
                        break;*/
                    case 'd': Escribir.Write("Digito\n");
                        Escribirtok.Write(i_caracter.ToString() + " - Digito " + "\n"); 
                        break;
                    case 's':
                        busquedaInterna(arregloDeCaracteres);
                        char simbolo = (char) i_caracter;
                        token = simboloToken[simbolo];
                        Escribir.Write("simbolo\n");
                        Escribirtok.Write(token.ToString() + "  - Simbolo" + "\n");
                        break;
                    case '"': 
                        Cadena(); 
                        Escribir.Write("cadena\n"); 
                        Escribirtok.Write("texto\n");
                        break;
                    case 'c': caracter(); Escribir.Write("caracter\n");
                        Escribirtok.Write(i_caracter.ToString() + "\n");
                        break;
                    case 'e':
                        Escribirtok.Write("error\n");
                        break;
                    case 'n':
                        busquedaInterna(arregloDeCaracteres);
                        Numero_linea++; 
                        break;
                    case 'L':
                        identificarLibreria();
                        
                        Numero_linea++; 
                        break;
                    case 'W':
                        busquedaInterna(arregloDeCaracteres);
                        break;
                    case 'x':
                        almacenarPalabra(i_caracter);
                        busquedaInterna(arregloDeCaracteres);
                        break;

                    default:
                            break;
                };
            } while (i_caracter != -1);
            Rtbx_salida.AppendText("Errores: " + N_error);
            Escribir.Close();
            Escribirtok.Close();
            Leer.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            compilarToolStripMenuItem.Enabled = true;
            
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /*##################################### ##################################### */
        /*##################################### ##################################### */
        /* ##### funciones para el Analisis Sintactico #####################################*/

        private void analisisSintactico()
        {
            
            Rtbx_salida.AppendText("\n ANALISIS SINTACTICO\n");
            Leer = new StreamReader(archivo);
            leerTexto();
            Rtbx_salida.AppendText("Errores sintácticos: " + N_error);
            Leer.Close();
        }
        private int leerTexto()
        {
            bool encontrado = false;
            tokenString = Leer.ReadLine();//primera linea del archivo
            string palabraEncontrada = "";

            do
            {
                if (tokenString.Length == 0)
                {
                    EscribirBak.Write("Salto de linea  \n");
                }
                else 
                {  
                    foreach (string palabra in palReservadas)
                    {
                        if (tokenString != null && tokenString.Contains(palabra))
                        {
                            palabraEncontrada = palabra;
                            encontrado = true;
                            break;
                        }

                    }

                    if (encontrado == false)
                    {
                    //agregar logica de comentarios
                        error("Palabra no encontrada");
                        break;
                    }

                    if (palabraEncontrada == "include")
                    {
                        caso = "#";
                    }
                    else if (tiposDeDatos.Contains(palabraEncontrada))
                    {
                        caso = "tipo";
                    }
                    else if (funciones.Contains(palabraEncontrada))
                    {
                        caso = "sentencias";
                    }

                    switch (caso)
                    {    //en este caso practico solamente se considera la directiva #include
                        case "#":
                            f_include(tokenString);
                            break;
                        case "tipo":
                            if (tokenString.Contains("main"))      // Metodo para trabajaar con la funcion main
                            {//agregar validacion para parentesis de la funcion main()
                                EscribirBak.Write("Tipo de dato \n");
                                EscribirBak.Write("Funcion \n");
                                f_Main();
                            }
                            else 
                            {
                                f_declaracion(tokenString, palabraEncontrada);
                            }
                            break;

                        case "sentencias":
                            sentencias(tokenString, palabraEncontrada);
                            break;

                        default:
                            tokenString = Leer.ReadLine();
                            break;

                    }
                    
                    Numero_linea++;
                    palabraEncontrada = "";

                }
                //aqui
                tokenString = Leer.ReadLine();
            } while ( tokenString != null);
            return 0;
        }

        private void f_declaracion(string cadena, string palabraE)
        {
            if (cadena.IndexOf(";") == cadena.Length-1)
            {
                if (cadena.IndexOf("=") > palabraE.Length - 1)//asignacion de variable
                {
                    int inicio = palabraE.Length;

                    int cantidadElementos = cadena.IndexOf("=") - inicio;
                    string identificador = cadena.Substring(inicio, cantidadElementos);
                    if (identificador.Length >= 2)
                    {
                        EscribirBak.Write("Declaracion \n");
                        EscribirBak.Write("Identificador \n");
                        EscribirBak.Write("Asignacion de variable \n");
                    }
                    else
                    {
                        error("Error al declarar variable \n");
                    }
                }
                else{
                    int start = palabraE.Length;

                    int cantidadE = cadena.IndexOf(";") - start;
                    string identificador = cadena.Substring(start, cantidadE);
                    if (identificador.Length >= 2)
                    {
                        EscribirBak.Write("Declaracion \n");
                        EscribirBak.Write("Identificador \n");
                    }

                }               
            }
            else
            {
                error("error al declarar variable, se espera punto y coma");
            }
        }

        private void f_include(string cadena)
        {//(
            if (cadena.IndexOf("#") == 0 && ((cadena.IndexOf("<") > 0 && cadena.IndexOf(">") > cadena.IndexOf("<"))
                 || (cadena.IndexOf('"') > 0  ))) 
            {
                EscribirBak.Write("# \n");
                EscribirBak.Write("Include \n");
                EscribirBak.Write("Libreria \n");
            }
            else
            {
                error( "Error al declarar libreria");
            }           
        }
        private void f_Main()
        {
            string palabraEncontrada = "";
            bool encontrado = false;
            do
            {
                /*if (tokenString.Contains("{"))
                {
                    EscribirBak.Write(" '{' Inicio de sentencias \n");
                }
                 if (tokenString.Contains("}"))
                 {
                    EscribirBak.Write("'}' Fin de sentencias \n");
                 }*/
                if (tokenString.Length == 0)
                {
                    EscribirBak.Write("Salto de linea  \n");
                }
                else
                {
                    foreach (string palabra in palReservadas)
                    {
                        if (tokenString != null && tokenString.Contains(palabra))
                        {
                            palabraEncontrada = palabra;
                            encontrado = true;
                            break;
                        }

                    }

                    if (encontrado == false)
                    {
                        //agregar logica de comentarios
                        error("Palabra no encontrada");
                        break;
                    }

                    
                    if (tiposDeDatos.Contains(palabraEncontrada))
                    {
                        caso = "tipo";
                    }
                    
                    else if (funciones.Contains(palabraEncontrada))
                    {
                        caso = "sentencias";
                    }

                    switch (caso)
                    {                            
                        case "tipo":

                            f_declaracion(tokenString, palabraEncontrada);
                            
                            break;

                        case "sentencias":
                            sentencias(tokenString, palabraEncontrada);
                            break;

                        default:
                            tokenString = Leer.ReadLine();
                            break;

                    }

                    Numero_linea++;
                    palabraEncontrada = "";

                }
                encontrado = false;
                tokenString = Leer.ReadLine();
            } while (tokenString != null);
        }
        private void bloqueSentencia()
        {
            tokenString = Leer.ReadLine();
            if (tokenString == "{")
            {
                tokenString = Leer.ReadLine();

                // Usamos un bucle para procesar múltiples sentencias dentro del bloque
                while (tokenString != "}" && tokenString != null)
                {
                    switch (tokenString)
                    {
                        case "{":
                            bloqueSentencia();
                            break;
                        case "}":
                            return;  // Salimos del bloque al encontrar '}'
                        default:
                            break;
                    }

                    // Leer el siguiente token para el próximo ciclo
                    tokenString = Leer.ReadLine();
                }

                // Validamos si el bloque no fue cerrado correctamente
                if (tokenString != "}")
                {
                    ErrorS(tokenString, "Se esperaba '}' para cerrar el bloque");
                }
            }
            else
            {
                ErrorS(tokenString, "{");  // Si el bloque no empieza con '{'
            }
        }
        
        private void ErrorS(string e, string s)
        {
            Rtbx_salida.AppendText("\nLinea: " + Numero_linea + ". Error de sintaxis " + e + ", se esperaba " + s + "\n");
             N_error++;
        }
        private void error(string e)
        {
            Rtbx_salida.AppendText("\nLinea: " + Numero_linea+" "  + e + "\n");
            N_error++;
        }
        private void f_if(string cadena )
        {
            if (cadena.Contains("(") && cadena.Contains(")") &&
                (cadena.IndexOf("(") < cadena.IndexOf(")"))) 
            {
                EscribirBak.Write("if (Condicion) \n"); 
            }
            else
            {
                ErrorS(tokenString, "error en sentencia if");
            }
        }
        private void f_do()
        {

        }
        private void llamada_funcion()
        {
            
        }

        private void asignacion()
        {
            
        }

        private void f_switch()
        {
            
        }

        private void f_while()
        {
        }

        private void sentencias(string cadena, string sentenciaEncontrada)
        {
            
            switch (sentenciaEncontrada)
            {
                case "if": 
                    f_if(cadena);
                    break;
                case "case": 
                    f_switch();
                    break;
                case "do": 
                    f_do(); 
                    break;
                case "while":
                    f_while();
                    break;
                case "switch": 
                    f_switch();
                    break;
                case "for":
                    f_switch();
                    break;

                default: 
                    ErrorS(tokenString, "Sentencia");
                    break;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Error(string tokenString, string mensaje)
        {
            Rtbx_salida.AppendText("Error de sintaxis: " + tokenString + ", " + mensaje + "\n");
            N_error++;
        }

    }

         
}
   
