namespace Torneo

{
    class Program
    {
        static void Main(string[] args)
        {
            string opcion;
            
            while (true)
            {
                
                Console.Clear();
                Console.WriteLine("Introduce una opción:");
                Console.WriteLine("1. Torneo de 2 jugadores");
                Console.WriteLine("2. Torneo de 16 jugadores");
                Console.WriteLine("3. Salir");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Torneo2.Iniciar();  
                        Console.WriteLine("Presiona una tecla para continuar...");
                        Console.ReadLine();
                        break;

                    case "2":
                        Torneo16.Iniciar();
                        Console.WriteLine("Presiona una tecla para continuar...");
                        Console.ReadLine();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Introduce una opción válida");
                        break;
                }
            }
        }
    }
}