// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using ykbredis.core.global;
using ykbredis.data.repository;

var carName = "ykb_1";
var location = GetDbLocation(carName);
RedisStore.RedisCache.StringSet(carName,location);


RedisStore.Subscriber.Subscribe("CarChannel").OnMessage(_channelMessage =>
{
    ProcessMessage(JsonConvert.DeserializeObject<CarDtoModel>(_channelMessage.Message));
});


Console.ReadKey();

void ProcessMessage(CarDtoModel carDtoModel)
{
    LogWriter(carDtoModel);
    var cacheLocation = GetCacheLocation(carDtoModel.CarName);
    var currentLocation = GetLocation(carDtoModel.Distance);
    if (cacheLocation!=currentLocation)
    {
        UpdateLocation(carDtoModel.CarName, currentLocation);
        Console.WriteLine(string.Format("{0} {1} konumunda {2} konumuna geçildi", DateTime.Now, cacheLocation, currentLocation));
    }
}

void UpdateLocation(string carName,string location)
{
    RedisStore.RedisCache.StringSet(carName, location);
    CarRepository carRepository = new CarRepository();

    var car = carRepository.Get(x=>x.Name == carName);
    car.Location = location;
    car.UpdateDate = DateTime.Now;
    carRepository.Update(car);
}

string GetDbLocation(string carName) {
    return new CarRepository().Get(x => x.Name == carName).Location;
}

string GetCacheLocation(string carName)
{
    return RedisStore.RedisCache.StringGet(carName);
}

string GetLocation(int location)
{
    if(location>0 && location<200)
    {
        return "A";
    }
    else if (location >= 200 && location < 600)
    {
        return "B";
    }
    else if (location >= 600 && location < 700)
    {
        return "C";
    }
    else 
    {
        return "D";
    }
}


void LogWriter(CarDtoModel carDto)
{

    string log = string.Format("{0} Araç Adı: {1} Mevcut Lokasyon: {2}", DateTime.Now, carDto.CarName, carDto.Distance);
    Console.WriteLine(log);
}
