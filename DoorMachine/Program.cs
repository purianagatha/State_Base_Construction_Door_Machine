namespace DoorMachine
{
    public enum DoorState { Terkunci, Terbuka };
    public enum Trigger { BukaPintu, KunciPintu };

    class DoorTransition
    {
        public DoorState prevState;
        public DoorState nextState;
        public DoorState currentState;
        public Trigger trigger;


        public DoorTransition(DoorState prevState, DoorState nextState, Trigger trigger)
        {
            this.prevState = prevState;
            this.nextState = nextState;
            this.trigger = trigger;
        }

        private static DoorTransition[] transitions =
        {
        new DoorTransition(DoorState.Terkunci, DoorState.Terbuka, Trigger.BukaPintu),
        new DoorTransition(DoorState.Terkunci, DoorState.Terkunci, Trigger.KunciPintu),
        new DoorTransition(DoorState.Terbuka, DoorState.Terkunci, Trigger.KunciPintu),
        new DoorTransition(DoorState.Terbuka, DoorState.Terbuka, Trigger.BukaPintu)
    };

        public DoorState getNextState(DoorState prevState, Trigger trigger)
        {
            DoorState nextState = prevState;

            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].prevState == prevState && transitions[i].trigger == trigger)
                {
                    nextState = transitions[i].nextState;
                }
            }
            return nextState;
        }

        public void activeTrigger(Trigger trigger)
        {
            DoorState nextState = getNextState(currentState, trigger);
            this.currentState = nextState;
            
        }
    }

    public class main
    {
        public static void Main(string[] args)
        {
            DoorTransition door = new DoorTransition(DoorState.Terbuka, DoorState.Terbuka, Trigger.BukaPintu);
            door.currentState = DoorState.Terbuka;
            Console.WriteLine("Pintu saat ini " + Enum.GetName(typeof(DoorState), door.currentState));
            Console.WriteLine("Fitur yang tersedia BukaPintu, KunciPintu, KELUAR");
            Console.WriteLine("Masukkan pilihan fitur: ");
            String pilihanFitur = Console.ReadLine();
            while (pilihanFitur != "KELUAR")
            {
                if (Enum.TryParse<Trigger>(pilihanFitur, out Trigger trigger))
                {
                    door.activeTrigger(trigger);
                    if (door.currentState == DoorState.Terkunci)
                    {
                        Console.WriteLine("Pintu terkunci");
                    }
                    else if (door.currentState == DoorState.Terbuka)
                    {
                        Console.WriteLine("Pintu tidak terkunci");
                    }
                }
                else
                {
                    Console.WriteLine("Masukkan pilihan fitur tidak valid");
                }
                Console.WriteLine("Masukkan pilihan fitur: ");
                pilihanFitur = Console.ReadLine();
            }
        }
    }
}
