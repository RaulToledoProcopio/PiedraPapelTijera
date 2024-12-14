namespace Torneo;

class Torneo16
{
    // Generador de números aleatorios para decidir los movimientos de los jugadores
    private static readonly Random Random = new Random();
    
    // Objeto de bloqueo para que dos hilos no intenten acceder a la vez
    private static readonly object LockObj = new object();

    public static void Iniciar ()
    {
        // Lista de jugadores
        List<string> jugadores = new List<string>
        {
            "Jugador 1", "Jugador 2", "Jugador 3", "Jugador 4",
            "Jugador 5", "Jugador 6", "Jugador 7", "Jugador 8",
            "Jugador 9", "Jugador 10", "Jugador 11", "Jugador 12",
            "Jugador 13", "Jugador 14", "Jugador 15", "Jugador 16"
        };

        // Hilos para todos los jugadores
        Thread jugador1 = new Thread(() => Jugar("Jugador 1"));
        Thread jugador2 = new Thread(() => Jugar("Jugador 2"));
        Thread jugador3 = new Thread(() => Jugar("Jugador 3"));
        Thread jugador4 = new Thread(() => Jugar("Jugador 4"));
        Thread jugador5 = new Thread(() => Jugar("Jugador 5"));
        Thread jugador6 = new Thread(() => Jugar("Jugador 6"));
        Thread jugador7 = new Thread(() => Jugar("Jugador 7"));
        Thread jugador8 = new Thread(() => Jugar("Jugador 8"));
        Thread jugador9 = new Thread(() => Jugar("Jugador 9"));
        Thread jugador10 = new Thread(() => Jugar("Jugador 10"));
        Thread jugador11 = new Thread(() => Jugar("Jugador 11"));
        Thread jugador12 = new Thread(() => Jugar("Jugador 12"));
        Thread jugador13 = new Thread(() => Jugar("Jugador 13"));
        Thread jugador14 = new Thread(() => Jugar("Jugador 14"));
        Thread jugador15 = new Thread(() => Jugar("Jugador 15"));
        Thread jugador16 = new Thread(() => Jugar("Jugador 16"));

        // Iniciamos los hilos
        jugador1.Start();
        jugador2.Start();
        jugador3.Start();
        jugador4.Start();
        jugador5.Start();
        jugador6.Start();
        jugador7.Start();
        jugador8.Start();
        jugador9.Start();
        jugador10.Start();
        jugador11.Start();
        jugador12.Start();
        jugador13.Start();
        jugador14.Start();
        jugador15.Start();
        jugador16.Start();

        /* El método Join es para asegurar que los hilos terminan y evitar problemas como
         que no nos aparezca el mensaje de victoria antes de que las rondas terminen */
        jugador1.Join();
        jugador2.Join();
        jugador3.Join();
        jugador4.Join();
        jugador5.Join();
        jugador6.Join();
        jugador7.Join();
        jugador8.Join();
        jugador9.Join();
        jugador10.Join();
        jugador11.Join();
        jugador12.Join();
        jugador13.Join();
        jugador14.Join();
        jugador15.Join();
        jugador16.Join();
        
        List<string> ganadores = jugadores;
        
        // Inicio del torneo comprobando en cada ronda que quede más de un jugador
        while (ganadores.Count > 1)
        {
            Console.WriteLine($"\nIniciando ronda con {ganadores.Count} jugadores\n");
            ganadores = Rondas(ganadores);  // Llamada a la función que organiza las rondas
        }

        // Muestra al ganador del torneo
        Console.WriteLine($"\nEl ganador del torneo es: {ganadores[0]}\n");
    }

    // Función que simula la preparación de un jugador
    private static void Jugar(string jugador)
    {
        lock (LockObj)  // Bloqueamos para evitar problemas de acceso concurrente
        {
            Console.WriteLine($"{jugador} está listo para jugar.");
        }
    }

    // Función que maneja las rondas del torneo
    static List<string> Rondas(List<string> jugadores)
    {
        List<string> ganadores = new List<string>(); // Lista de ganadores de la ronda
        List<Thread> hilos = new List<Thread>(); // Lista de hilos para cada enfrentamiento

        // Para cada par de jugadores, creamos un hilo que simule el enfrentamiento
        for (int i = 0; i < jugadores.Count; i += 2)
        {
            string jugador1 = jugadores[i];
            string jugador2 = jugadores[i + 1];

            // Hilo que simula un enfrentamiento entre dos jugadores
            Thread hilo = new Thread(() =>
            {
                string ganador = Enfrentamientos(jugador1, jugador2);
                    lock (LockObj)
                    {
                        ganadores.Add(ganador);
                    }
            });

            hilos.Add(hilo);
            hilo.Start();
        }

        // Esperamos a que todos los hilos de la ronda actual terminen antes de continuar
        foreach (var hilo in hilos)
        {
            hilo.Join();
        }

        // Devolvemos la lista de ganadores de la ronda
        return ganadores;
    }

    // Función que simula un enfrentamiento entre dos jugadores
    static string Enfrentamientos(string jugador1, string jugador2)
    {
        int ganaJugador1 = 0;
        int ganaJugador2 = 0;

        // El enfrentamiento continúa hasta que uno de los jugadores gane dos rondas
        while (ganaJugador1 < 2 && ganaJugador2 < 2)
        {
            lock (LockObj)  // Bloqueamos para evitar que los hilos accedan simultáneamente a los recursos
            {
                int movimientoJugador1 = Random.Next(3);
                int movimientoJugador2 = Random.Next(3);

                string resultado = Ganador(movimientoJugador1, movimientoJugador2);
                
                if (resultado == "Jugador 1")
                {
                    ganaJugador1++; 
                }
                else if (resultado == "Jugador 2")
                {
                    ganaJugador2++;
                }
                
                if (resultado == "Empate")
                {
                    Console.WriteLine($"{jugador1} ({Movimientos(movimientoJugador1)}) vs {jugador2} ({Movimientos(movimientoJugador2)}): Empate");
                }
                else
                {
                    Console.WriteLine($"{jugador1} ({Movimientos(movimientoJugador1)}) vs {jugador2} ({Movimientos(movimientoJugador2)}): {ganaJugador1} - {ganaJugador2}");
                }
            }
        }
        
        string ganador = ganaJugador1 > ganaJugador2 ? jugador1 : jugador2;
        Console.WriteLine($"\n{ganador} gana el enfrentamiento y avanza a la siguiente ronda\n");

        return ganador;
    }

    // Función que determina quién ganó según los movimientos de ambos jugadores
    static string Ganador(int movimiento1, int movimiento2)
    {
        
        if (movimiento1 == movimiento2)
            return "Empate";

       
        if ((movimiento1 == 0 && movimiento2 == 2) || // Piedra gana a Tijera
            (movimiento1 == 1 && movimiento2 == 0) || // Papel gana a Piedra
            (movimiento1 == 2 && movimiento2 == 1)) // Tijera gana a Papel
        {
            return "Jugador 1";
        }

        return "Jugador 2";
    }

    // Función que convierte un número en su correspondiente movimiento
    static string Movimientos(int movimiento)
    {
        return movimiento switch
        {
            0 => "Piedra",
            1 => "Papel",
            2 => "Tijera",
        };
    }
}