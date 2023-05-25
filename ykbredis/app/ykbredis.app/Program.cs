

//                200                                                 400                                100              150
//|-----------------A-------------------|-------------------------------B----------------------------|-----C-----|----------D--------|
//Toplam Uzunluk 850
// ykb1 300 ms de 1 birim yol gider 



using Newtonsoft.Json;
using ykbredis.core.global;
var carName = "ykb_1";
CarOperation(carName, 1);

void CarOperation(string name, int unit)
{
    for (int i = 0; i < 850; i += unit)
    {
        CarDtoModel model = new CarDtoModel();
        model.CarName = name;
        model.Distance = i;

        LogWriter(model);

        RedisStore.Subscriber.Publish("CarChannel", JsonConvert.SerializeObject(model));

        Thread.Sleep(300);


    }
}



void LogWriter(CarDtoModel carDto)
{

    string log = string.Format("{0} Araç Adı: {1} Mevcut Lokasyon: {2}", DateTime.Now, carDto.CarName, carDto.Distance);
    Console.WriteLine(log);
}


