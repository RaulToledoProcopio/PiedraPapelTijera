namespace Torneo
{
    class Torneo2
    { 
        // Generador de números aleatorios para decidir los movimientos de los jugadores 
        private static readonly Random random = new Random(); 
        
        // Contador de victorias 
        private static int ganaJugador1 = 0; 
        private static int ganaJugador2 = 0; 
        
        // Objeto de bloqueo para que dos hilos no intenten acceder a la vez 
        private static readonly object lockObj = new object(); 
        
        public static void Iniciar () 
        { 
            // Reiniciar los contadores antes de cada torneo
            ReiniciarContadores();

            Thread Jugador1 = new Thread(Jugar); 
            Thread Jugador2 = new Thread(Jugar); 
        
            // Iniciamos los hilos 
            Jugador1.Start("Jugador 1"); 
            Jugador2.Start("Jugador 2"); 
        
            /* El método Join es para asegurar que los hilos terminan y evitar problemas como 
            que no nos aparezca el mensaje de victoria antes de que las rondas terminen */ 
            Jugador1.Join(); 
            Jugador2.Join(); 
        
            // Imprimimos el resultado final del juego 
            Console.WriteLine("Juego terminado."); 
            Console.WriteLine(ganaJugador1 > ganaJugador2 ? "Jugador 1 gana" : "Jugador 2 gana"); 
        } 

        static void Jugar(object jugador) 
        { 
            // Bucle infinito para que cada hilo juegue continuamente 
            while (true) 
            { 
                int movimientoJugador1, movimientoJugador2; 
        
                // Utilizamos block para bloquear el acceso compartido a las variables 
                lock (lockObj) 
                { 
                    // Si alguno de los jugadores ha ganado 2 rondas termina 
                    if (ganaJugador1 == 2 || ganaJugador2 == 2) 
                        return; 
        
                    // Movimientos aleatorios para ambos jugadores. 0: Piedra, 1: Papel, 2: Tijera 
                    movimientoJugador1 = random.Next(3); 
                    movimientoJugador2 = random.Next(3); 
        
                    string resultado = Ganador(movimientoJugador1, movimientoJugador2); 
        
                    // Incrementamos el contador 
                    if (resultado == "Jugador 1") 
                    { 
                        ganaJugador1++; 
                        Console.WriteLine($"{jugador} ({Movimientos(movimientoJugador1)}) vs Jugador 2 ({Movimientos(movimientoJugador2)}): {ganaJugador1}-{ganaJugador2}"); 
                    } 
                    else if (resultado == "Jugador 2") 
                    { 
                        ganaJugador2++; 
                        Console.WriteLine($"{jugador} ({Movimientos(movimientoJugador1)}) vs Jugador 2 ({Movimientos(movimientoJugador2)}): {ganaJugador1}-{ganaJugador2}"); 
                    } 
                    else 
                    { 
                        // Caso de empate sin que use el contador 
                        Console.WriteLine($"{jugador} ({Movimientos(movimientoJugador1)}) vs Jugador 2 ({Movimientos(movimientoJugador2)}): Empate"); 
                    } 
                } 
            } 
        } 
        
        static string Ganador(int movimiento1, int movimiento2) 
        { 
            // Comprobamos si ha sido empate 
            if (movimiento1 == movimiento2) 
                return "Empate"; 
        
            // Comprobamos si gana el jugador1 en caso contrario ha ganado el 2 
            if ((movimiento1 == 0 && movimiento2 == 2) || // Piedra gana a Tijera 
                (movimiento1 == 1 && movimiento2 == 0) || // Papel gana a Piedra 
                (movimiento1 == 2 && movimiento2 == 1)) // Tijera gana a Papel 
            { 
                return "Jugador 1"; 
            } 
        
            return "Jugador 2"; 
        } 

        // Método que reinicia los contadores de victorias
        private static void ReiniciarContadores()
        {
            ganaJugador1 = 0;
            ganaJugador2 = 0;
        }

        // Función que convierte un número en su correspondiente movimiento
        static string Movimientos(int movimiento)
        {
            return movimiento switch
            {
                0 => "Piedra",
                1 => "Papel",
                2 => "Tijera",
                _ => "Desconocido"
            };
        }
    }
}