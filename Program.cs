using System;

namespace AnalizadorSintactico
{
    class Program
    {
        static void Main(string[] args)
        {
            string sintaxis = "(s*i+a)";
            AnalizadorSintactico analizador = new AnalizadorSintactico(sintaxis);


            Console.WriteLine("¡Bienvenido al Analizador Sintáctico!");

            Console.WriteLine("Expresión: " + sintaxis);


            if (analizador.Analizador())
            {
                Console.WriteLine("¡La expresión es válida y está escrita correctamente!");
            }
            else
            {
                Console.WriteLine("¡La expresión es inválida o está escrita incorrectamente!");
            }            
        }
    }

    class AnalizadorSintactico
    {
        private readonly string sintaxis;
        private int posicionActual;

        public AnalizadorSintactico(string sintaxis)
        {
            this.sintaxis = sintaxis;
            posicionActual = 0;
        }

        public bool Analizador()
        {
            try
            {
                Expresion();
                if (posicionActual != sintaxis.Length)
                {
                    throw new Exception("Falta paréntesis de entrada");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la sintaxis de entrada: " + ex.Message);
                return false;
            }
            finally
            {
                posicionActual = 0;
            }
        }

        private void Factor()
        {
            if (posicionActual < sintaxis.Length && char.IsLetter(sintaxis[posicionActual]))
            {
                Console.WriteLine("Variable encontrada: " + sintaxis[posicionActual]);
                posicionActual++;
            }
            else if (posicionActual < sintaxis.Length && sintaxis[posicionActual] == '(')
            {
                posicionActual++;
                Expresion();
                if (posicionActual >= sintaxis.Length || sintaxis[posicionActual] != ')')
                {
                    throw new Exception("Falta paréntesis de cierre");
                }
                posicionActual++;
            }
            else
            {
                throw new Exception("Elemento no válido.");
            }
        }

        private void Expresion()
        {
            Termino();
            while (posicionActual < sintaxis.Length && sintaxis[posicionActual] == '+')
            {
                posicionActual++;
                Termino();
            }
        }

        private void Termino()
        {
            Factor();
            while (posicionActual < sintaxis.Length && sintaxis[posicionActual] == '*')
            {
                posicionActual++;
                Factor();
            }
        }


    }
}
