namespace FlappyBird.Web.Models
{
    public class BirdModel
    {
        public int DistanceFromGround { get; private set; } = 100;
        public int JumpStrength { get; private set; } = 50;

        //este metodo es para informar al loop que se detenga cuando los la distancia hacia es suelo es igual a 0 si es igual a 0 pon el bool running en false
        public bool IsOnGround()
        {
            return DistanceFromGround <= 0;
        }

        //cuando se ejecute este metodo, la Distancia desde el suelo(DistanceFromGround) sera menor o igual a la gravedad(propiedad int)
        public void Fall(int gravity)
        {
            DistanceFromGround -= gravity;
        }

        //metodo que actualiza la posicion del div del pajarito respescto al div del suelo
        public void Jump() 
        {
            if (DistanceFromGround < 530) //si la distancia es mayor a 530px
                DistanceFromGround += JumpStrength;//quitar 50 px , esto para que no se salga del div contendedor del paisaje
        }
    }
}