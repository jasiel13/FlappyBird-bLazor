using System;

namespace FlappyBird.Web.Models
{
    public class PipeModel
    {
        private readonly int _speed = 2;//veloicdad a la que se movera el paisaje en este caso el movimiento de los tubos
        public int DistanceFromLeft { get; private set; } = 500;//distancia en pixeles entre cada tubo
        public int DistanceFromBottom { get; private set; } = new Random().Next(1, 60);
        public int GapLower => 300 - 150 + DistanceFromBottom; // pipe height - ground height + pipe distance from bottom
        public int GapUpper => 430 - 150 + DistanceFromBottom; // pipe gap - ground height + pipe distance from bottom

        //metodo que ejecutara el desplazamiento del paisaje
        public void Move()
        {
            DistanceFromLeft -= _speed;
        }

        public bool IsCentered()
        {
            //la mitad del ancho del juego menos la mitad del ancho del pájaro
            // half of the game width minus half the width of the bird
            var gameCenterLeft = (500 - 60) / 2;

            //la mitad del ancho del juego más la mitad del ancho del pájaro
            // half of the game width plus half the width of the bird
            var gameCenterRight = (500 + 60) / 2;            

            return (DistanceFromLeft < gameCenterRight && DistanceFromLeft > gameCenterLeft - 60); 
        }
    }
}