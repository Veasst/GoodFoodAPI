using GoodFoodAPI.Data;
using GoodFoodAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ContosoUniversity.Data
{
    public static class GoodFoodInitializer
    {
        public static void Initialize(GoodFoodContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var users = new User[]
            {
            new User{username="potato",password="potato123", stamps=0, freeDishes=0},
            new User{username="free", password="dish", stamps=7, freeDishes=3}
            };

            DishType[] dishTypes = {
                new DishType { dishType = "ŚNIADANIE" },
                new DishType { dishType = "OBIAD" },
                new DishType { dishType = "KOLACJA"} };

            Dish[] dishes = {
                new Dish { dishName = "Ziemniaki z kotletem schabowym", description="Tłuczone ziemniaki z kotletem ze schabu", dishType=dishTypes[1], ingredients="Ziemniaki, schab", price = 13.0f, logoPath="https://polki.pl/pub/wieszjak/p/_wspolne/pliki_infornext/670000/obiad_2.jpg" },
                new Dish { dishName = "Pieczone ziemniaki z kotletem schabowym", description = "Piczone ziemniaki z kotletem ze schabu", dishType = dishTypes[1], ingredients = "Ziemniaki, schab", price = 16.0f, logoPath ="http://www.studio1.nazwa.pl/spa/wordpress/wp-content/uploads/2013/07/spa-smaczne-przepisy-ani-schabowy-ziemniaki-1.jpg" },
                new Dish { dishName = "Jajecznica", description = "Jajecznica z jajek", dishType = dishTypes[0], ingredients = "Jajko, masło, pieprz", price = 10f, logoPath="https://cosdobrego.pl/wp-content/uploads/2016/09/domowa-jajecznica-z-cebulka-i-kielbasa-03.jpg" },
                new Dish { dishName = "Kiełbasa", description = "Kiełbasa Olchowa", dishType = dishTypes[2], ingredients="Kiełbasa", price = 10f, logoPath="http://www.koniarek.pl/products/kielbasa_olchowa_1.jpg"},
                new Dish { dishName = "Bigos", description = "Bigos z kapusty", dishType = dishTypes[1], ingredients="Kapusta, pomidory, kiełbasa", price = 8f, logoPath="https://static.gotujmy.pl/ZDJECIE_KAFELEK_B/bigos-z-pieczarkami-319082.jpg"},
                new Dish { dishName = "Jajko na miękko", description = "Jajko, ale nie ugotowane", dishType = dishTypes[0], ingredients="Jajko, skorupka jajka", price = 6f, logoPath="https://d3iamf8ydd24h9.cloudfront.net/pictures/articles/2017/02/29639-v-1080x666.jpg"},
                new Dish { dishName = "Sałatka arabska", description = "Sałatka arabska", dishType = dishTypes[2], ingredients="Sałata, ser feta, oliwki", price = 13.99f, logoPath="http://4.bp.blogspot.com/-SKE4fHyGjFw/UKe-2N3oU8I/AAAAAAAAASQ/nXEcQq8ybX4/s1600/IMG_8172.JPG"}
            };


            Local[] locals = {
                new Local { address = "Plac Grunwaldzki 22, Wrocław", name = "Pizza Hut", description = "Pizza Hut w pasażu", logoPath = "https://vignette.wikia.nocookie.net/logopedia/images/d/d2/Pizza_Hut_logo.svg/revision/latest/scale-to-width-down/200?cb=20180501084032"},
                new Local { address = "Plac Grunwaldzki 18, Wrocław", name = "Bravo", description = "Lokal z Pizzą", logoPath = "http://www.bravopizzawc.com/wp-content/uploads/2015/01/Bravo-Logo1.gif"},
                new Local { address = "Curie-Skłodowskiej 55, Wrocław", name = "Przegryź", description = "Lokal ze schabowym", logoPath = "http://przegryz.com/wp-content/uploads/2017/04/logo-przegryz-2.jpg"},
                new Local { address = "Plac Grunwaldzki 22, Wrocław", name = "KFC", description = "KFC w pasażu", logoPath = "http://logonoid.com/images/kfc-logo.png"}

            };

            LocalDishes[] localDishes = {
                new LocalDishes { dish = dishes[0], local = locals[2] },
                new LocalDishes { dish = dishes[1], local = locals[2] },
                new LocalDishes { dish = dishes[2], local = locals[3] },
                new LocalDishes { dish = dishes[3], local = locals[0] },
                new LocalDishes { dish = dishes[4], local = locals[1] },
                new LocalDishes { dish = dishes[5], local = locals[3] },
                new LocalDishes { dish = dishes[6], local = locals[1] }
            };

            Code[] codes = {
                new Code { code="POTATO"},
                new Code{ code = "ZIEMNIAK"}
            };


            for (int i = 0; i < 7; ++i)
            {
                locals[0].localDishes.Add(localDishes[i]);
            }

            UserDishes[] userDishes =
            {
                new UserDishes { user=users[0], dish = dishes[0]},
                new UserDishes { user=users[0], dish = dishes[2]},
                new UserDishes { user=users[0], dish = dishes[3]},
                new UserDishes { user=users[0], dish = dishes[4]},
                new UserDishes { user=users[0], dish = dishes[6]},
            };

            UserLocals[] userLocals =
            {
                new UserLocals {user=users[0], local = locals[0]}
            };

            for (int i = 0; i < 5; ++i)
            {
                users[0].userDishes.Add(userDishes[i]);
            }


            users[0].userLocals.Add(userLocals[0]);

            context.Local.AddRange(locals);
            context.Dish.AddRange(dishes);
            context.DishType.AddRange(dishTypes);
            context.User.AddRange(users);
            context.Code.AddRange(codes);

            context.SaveChanges();
        }
    }
}