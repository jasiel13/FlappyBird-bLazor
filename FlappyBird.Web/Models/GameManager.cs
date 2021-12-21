using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Web.Models
{
    public class GameManager
    {
        //hardcodeamos el valor de gravity para madarla al metodo fall en el modelo brid model
        private readonly int _gravity = 2;

        //es un evneto el cual avisara que el loop a terminado cuando el div del pajarito hay cumplido con la condicion del metodo fall
        public event EventHandler MainLoopCompleted;

        //variable para comenzar el movimento del pajarito
        public bool IsRunning { get; private set; } = false;

        public BirdModel Bird { get; private set; }
        public List<PipeModel> Pipes { get; private set; }

        public GameManager()
        {
            ResetGameObjects();
        }

        //cuando se ejecuta este metodo quiere decir que el pajarito se empezo a mover
        public async void MainLoop()
        { 
            //comenzo a moverse por que si es true running
            IsRunning = true;

            //mientras este en movimiento (isrunning) el pajarito, ejecuta los metodos
            while(IsRunning)
            {
                MoveGameObjects();//movimeinto de tubos en loop
                CheckForCollisions();//metodo que se ejecuta en el loop es para un gameover por colision con un tubo            
                ManagePipes();

                //se invoca al metodo eventargs solo cuando el loop haya finalizado
                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);//esto es para que el div del pajarito baje a su posicion original con un delay para ver la trans icion hacia abajo
            }
        }

        //al ejecutar este metodo jump se ejecuta el metodo jump de la clase birdmodel solo cuando running is true
        public void Jump()
        {
            if (IsRunning) 
                Bird.Jump();
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                ResetGameObjects();
                MainLoop();//iniciamos el loop al ejecutar el metodo startgame y si la propiedad running es false
            }           
        }

        private void CheckForCollisions()
        {
            //cuando colisiona con un tubo pierdes y se ejecutan los dos metodos IsOnGround GameOver
            if (Bird.IsOnGround())
                GameOver();

            var centeredPipe = GetCenteredPipe();

            //si se cumple esta condicion el div del pajarito llego a los limites establecidos por las clases de los tubos gaplower y gapupper y por lo tanto es una colision perdio !!
            if (centeredPipe != null &&
                (Bird.DistanceFromGround < centeredPipe.GapLower ||
                Bird.DistanceFromGround > centeredPipe.GapUpper - 45)) // <-- minus bird height
            {
                GameOver();
            }
                                    
        }

        private void ManagePipes()
        {
            //si no encuentra un tubo o la distancia ya recorrio 250px entonces genera un nuevo tubo
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft < 250)
                GeneratePipe();

            //si el primer tubo es menor que -60 px remuevelo de la lista de tubos , esto es para que en patanlla se vayan desapareciendo
            if (Pipes.First().DistanceFromLeft < -60)
                Pipes.Remove(Pipes.First());
        }

        //metodo que cuando se ejecuta recorre una lista de tubos para ejecutar el movimento de dichos tubos en el metodo move de la clase pipe
        private void MoveGameObjects()
        {
            Bird.Fall(_gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
        }

        //pone en false cuando pierdes cuando el metodo IsOnGround se ejecuta
        private void GameOver()
        {
            IsRunning = false;
        }

        //metodo para ir llenado la lista de tubos el cual primero crea el tubo y lo almacena en la lista
        private void GeneratePipe()
        {
            Pipes.Add(new PipeModel());
        }

        //centrar los tubos con respecto al pajarito
        private PipeModel GetCenteredPipe()
        {
            //de mi lista de tubos buscar las propiedades para centrar los divs correspondientes
            return Pipes.FirstOrDefault(p => p.IsCentered());
        }

        private void ResetGameObjects()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }
    }
}
