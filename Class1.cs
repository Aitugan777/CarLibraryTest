namespace MyTestLibrary
{
    public class SportCar : Car
    {

        /// <summary>
        ///  Аргументы: обьем бака, оставшееся топливо, скорость в км/ч, расход топлива на 1км
        /// </summary>
        public SportCar(double fuelTankVolume, double realFuelTank, double speed, double fuelConsumption)
        {
            this.FuelTankVolume = fuelTankVolume;
            this.RealFuelTank = realFuelTank;
            this.Speed = speed;
            this.FuelConsumption = fuelConsumption;
        }
    }


    public class PassengerCar : Car
    {

        /// <summary>
        ///  Аргументы: обьем бака, оставшееся топливо, скорость в км/ч, расход топлива на 1км, кол-во пассажиров
        /// </summary>
        public PassengerCar(double fuelTankVolume, double realFuelTank, double speed, double  fuelConsumption, byte passengers)
        {
            this.FuelTankVolume = fuelTankVolume;
            this.RealFuelTank = realFuelTank;
            this.Speed = speed;
            this.FuelConsumption = fuelConsumption;
            this.Passengers = passengers;
        }

        public override double GetFuelConsumption(double distance)
        {
            double percentFuelConsumption = 0;

            percentFuelConsumption = Passengers * 6;

            return (base.GetFuelConsumption(distance) / 100) * percentFuelConsumption;
        }

        public double Passengers
        {
            get { return this.passengers; }
            set 
            {
                if (value < MaxPassengers)
                    passengers = value;
                else
                {
                    passengers = MaxPassengers;
                    Console.WriteLine($"В машину можно сажать до {MaxPassengers}, кол-во пассажиров определено {MaxPassengers}");
                }
            }
        }

        double passengers { get; set; }
        public byte MaxPassengers { get => 8; }
    }

    public class Truck : Car
    {

        /// <summary>
        ///  Аргументы: обьем бака, оставшееся топливо, скорость в км/ч, расход топлива на 1км, вес груза в кг
        /// </summary>
        public Truck(double fuelTankVolume, double realFuelTank, double speed, double fuelConsumption, double cargoWeight)
        {
            this.FuelTankVolume = fuelTankVolume;
            this.RealFuelTank = realFuelTank;
            this.Speed = speed;
            this.FuelConsumption = fuelConsumption;
            this.CargoWeight = cargoWeight;
        }

        public override double GetFuelConsumption(double distance)
        {
            double percentFuelConsumption = 0;

            percentFuelConsumption = (CargoWeight / 200) * 4;

            return (base.GetFuelConsumption(distance) / 100) * percentFuelConsumption;
        }

        public double CargoWeight
        {
            get { return this.cargoWeight; }
            set 
            {
                if (value < MaxCargoWeight)
                    cargoWeight = value;
                else
                {
                    cargoWeight = MaxCargoWeight;
                    Console.WriteLine($"Груз привышает максимальную грузоподьемность ({MaxCargoWeight}кг), борт определён пустым!");
                }
            }
        }

        double cargoWeight { get; set; }
        public double MaxCargoWeight { get => 3000; }

    }

    public class Car : ICar
    {
        public double FuelTankVolume { get => fuelTankVolume; set => fuelTankVolume = value; }
        double fuelTankVolume;

        public double Speed { get => speed; set => speed = value; }
        double speed;

        public double FuelConsumption { get => fuelConsumption; set => fuelConsumption = value; }
        double fuelConsumption;
        public double RealFuelTank { get => realFuelTank; set => realFuelTank = value; }
        double realFuelTank;

        public virtual double GetFuelConsumption(double distance)
        {
            if (!WillReach(distance))
                Console.WriteLine($"Топливо хватит только на {GetDistance}км!");

            return distance * FuelConsumption;
        }

        public double GetTime(double distance)
        {
            if (!WillReach(distance))
                Console.WriteLine($"Топливо хватит только на {GetDistance}км!");
            return distance / speed;
        }

        public bool WillReach(double distance)
        {
            if (GetFuelConsumption(distance) >= RealFuelTank)
                return true;
            return false;
        }

        public double GetDistance(bool fullFuel = false)
        {
            if (fullFuel)
                return FuelTankVolume * FuelConsumption;
            return RealFuelTank * FuelConsumption;
        }

        public override string ToString()
        {
            return String.Format("Скорость: {0}\nВместительность бака {1}/{2}\nТопливо хватит на {3}км, при полном баке {4}\nРасход на 100км: {5}", Speed, RealFuelTank, FuelConsumption, GetDistance(), GetDistance(true), GetFuelConsumption(100));
        }
    }

    public interface ICar
    {
        /// <summary>
        ///  Расчет времени на основе данным средней скорости, возвращает значение в часах
        /// </summary>
        double GetTime(double distance);

        /// <summary>
        ///  Расчет расоходуемого топлива на основе данным груза/пассажиров
        /// </summary>
        double GetFuelConsumption(double distance);

        /// <summary>
        /// Расчет дистанции которую сможет проехать транспорт,
        /// при передачи истинного аргумента расчитывает дистанцию на полном баке
        /// </summary>
        double GetDistance(bool fullFuel = false);

        /// <summary>
        /// Возвращает истину если автомобиль сможет проехать такую дистанцию
        /// </summary>
        bool WillReach(double distance);


        /// <summary>
        /// Обьем топливного бака
        /// </summary>
        public abstract double FuelTankVolume { get; set; }

        /// <summary>
        /// Обьем оставшегося топлива
        /// </summary>
        public abstract double RealFuelTank { get; set; }

        /// <summary>
        /// Скорость в км/ч
        /// </summary>
        public abstract double Speed { get; set; }

        /// <summary>
        /// Расход топлива на 1км
        /// </summary>
        public abstract double FuelConsumption { get; set; }

    }

}