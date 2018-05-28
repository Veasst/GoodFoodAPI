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
                new DishType { dishType = "KOLACJA"},
                new DishType { dishType = "DZIWNE"},
                new DishType { dishType = "DZIWNE2"}};

            Dish[] dishes = {
                new Dish { dishName = "Ziemniaki z kotletem schabowym", description="Tłuczone ziemniaki z kotletem ze schabu", dishType=dishTypes[4], ingredients="Ziemniaki, schab", price = 13.0f, logoPath="https://polki.pl/pub/wieszjak/p/_wspolne/pliki_infornext/670000/obiad_2.jpg" },
                new Dish { dishName = "Pieczone ziemniaki z kotletem schabowym", description = "Piczone ziemniaki z kotletem ze schabu", dishType = dishTypes[1], ingredients = "Ziemniaki, schab", price = 16.0f, logoPath ="http://www.studio1.nazwa.pl/spa/wordpress/wp-content/uploads/2013/07/spa-smaczne-przepisy-ani-schabowy-ziemniaki-1.jpg" },
                new Dish { dishName = "Jajecznica", description = "Jajecznica z jajek", dishType = dishTypes[0], ingredients = "Jajko, masło, pieprz", price = 10f, logoPath="https://cosdobrego.pl/wp-content/uploads/2016/09/domowa-jajecznica-z-cebulka-i-kielbasa-03.jpg" },
                new Dish { dishName = "Kiełbasa", description = "Kiełbasa Olchowa", dishType = dishTypes[2], ingredients="Kiełbasa", price = 10f, logoPath="http://www.koniarek.pl/products/kielbasa_olchowa_1.jpg"},
                new Dish { dishName = "Bigos", description = "Bigos z kapusty", dishType = dishTypes[1], ingredients="Kapusta, pomidory, kiełbasa", price = 8f, logoPath="https://static.gotujmy.pl/ZDJECIE_KAFELEK_B/bigos-z-pieczarkami-319082.jpg"},
                new Dish { dishName = "Jajko na miękko", description = "Jajko, ale nie ugotowane", dishType = dishTypes[0], ingredients="Jajko, skorupka jajka", price = 6f, logoPath="https://d3iamf8ydd24h9.cloudfront.net/pictures/articles/2017/02/29639-v-1080x666.jpg"},
                new Dish { dishName = "Sałatka arabska", description = "Sałatka arabska", dishType = dishTypes[3], ingredients="Sałata, ser feta, oliwki", price = 13.99f, logoPath="http://4.bp.blogspot.com/-SKE4fHyGjFw/UKe-2N3oU8I/AAAAAAAAASQ/nXEcQq8ybX4/s1600/IMG_8172.JPG"},
                // Śniadania
                new Dish {dishName = "Jajecznica na maśle", description = "Jajecznica z 3 jajek z masłem", dishType=dishTypes[0], ingredients="Jajko, masło", price = 8f, logoPath="http://www.przepisykulinarne.info/wp-content/uploads/2016/06/jajecznica-na-masle.jpg"},
                new Dish {dishName = "Jajecznica na boczku", description = "Jajecznica z 3 jajek z boczkiem", dishType=dishTypes[0], ingredients="Jajko, masło, boczek", price = 10f, logoPath="http://www.przepisykulinarne.info/wp-content/uploads/2015/01/jajecznica-z-boczkiem-2.jpg"},
                new Dish {dishName = "Omlet wege lub z szynką i serem", description = "Omlet + bułka i masło", dishType=dishTypes[0], ingredients="Jajko, masło, szynka, ser", price = 12f, logoPath="https://3.bp.blogspot.com/-fwNmb2UNdQk/VL5noFx59uI/AAAAAAAAH1k/0FZc6Jp9yvE/s1600/IMG_8950.jpg"},
                new Dish {dishName = "Tarta z warzywami", description = "Tarta z warzywami", dishType=dishTypes[0], ingredients="Mąka, masło, jajko, papryka, cebula, ser", price = 7f, logoPath="http://kotlet.tv/wp-content/uploads/2012/04/tarta-warzywna-900x599.jpg"},
                new Dish {dishName = "Tarta z szynką", description = "Tarta z szynką", dishType=dishTypes[0], ingredients="Mąka, masło, jajko, papryka, cebula, ser, szynka", price = 9f, logoPath="http://cdn17.beszamel.smcloud.net/t/thumbs/660/441/1/user_photos/ThinkstockPhotos-476098742.jpg"},
                new Dish {dishName = "Frankfurterki", description = "Cztery kiełbaski frankfurterki z wody/podsmażane", dishType=dishTypes[0], ingredients="Kiełbasa", price = 8f, logoPath="http://esklep.niewiescin.pl/wp-content/uploads/2016/03/Frankfurterki-2.png"},
                new Dish {dishName = "Owsianka/Płatki/Musli na mleku", description = "Owsianka/Płatki/Musli na mleku", dishType=dishTypes[0], ingredients="Płatki, Mleko", price = 8f, logoPath="https://akademiasmaku.pl/upload/recipes/2752/big/owsianka-na-wodzie-czyli-pelnowartosciowe-sniadanie-2752.jpg"},
                // Obiady
                new Dish {dishName = "Spaghetti bolognese", description = "Spaghetti w sosie bolognese", dishType=dishTypes[1], ingredients="Makaron, sos bolognese", price = 10f, logoPath="https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/spaghetti_bolognese_01.jpg"},
                new Dish {dishName = "Udko z kurczaka z ziemniakami", description = "Pieczone udko z kurczaka + ziemniaki", dishType=dishTypes[1], ingredients="Udko z kurczaka", price = 13f, logoPath="https://www.mojegotowanie.pl/media/cache/gallery_view/uploads/media/default/0001/45/e2c73b57700fe43ac9021ab907f4aead0e2dc6d8.jpeg"},
                new Dish {dishName = "Grillowana pierś z kurczaka z dipem ziołowym", description = "Pierś z kurczaka grillowana z dipem", dishType=dishTypes[1], ingredients="Pierś z kurczaka", price = 16f, logoPath="http://www.przepisykulinarne.info/wp-content/uploads/2014/08/grillowana-piers-z-kurczaka-2.jpg"},
                new Dish {dishName = "Zapiekanka ziemniaczana z dipem ziołowym", description = "Zapiekanka z dipem", dishType=dishTypes[1], ingredients="Ziemniaki, dip", price = 10f, logoPath="https://static.gotujmy.pl/ZDJECIE_KAFELEK_B/zapiekanka-ziemniaczana-z-kiszona-kapusta-236021.jpg"},
                new Dish {dishName = "Kotlet schabowy panierowany", description = "Kotlet schabowy w panierce", dishType=dishTypes[1], ingredients="Schab, ziemniaki, surówka", price = 13f, logoPath="https://polki.pl/pub/wieszjak/p/_wspolne/pliki_infornext/670000/obiad_2.jpg"},
                new Dish {dishName = "Kotlet De Volaille z serem", description = "Pierś z kurczaka z serem", dishType=dishTypes[1], ingredients="Pierś z kurczaka, ser żółty", price = 16f, logoPath="https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/page261_4.jpg"},
                // Kolacje
                new Dish {dishName = "Ryż smażony z kurczakiem po meksykańsku", description = "Ryż z warzywami", dishType=dishTypes[2], ingredients="Ryż, papryka, kukurydza, pomidor, awokado", price = 10f, logoPath="https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/ryz_smazony_z_kurczakiem_po_meksykansku_.jpg"},
                new Dish {dishName = "Zestaw lunchowy z kaszą gryczaną i warzywami", description = "Kasza gryczana smażona z jajkiem, kapustą pekińską i papryką", dishType=dishTypes[2], ingredients="Kasza gryczana, czosnek, papryczka chili, jajka, por, papryka, kapusta pekińska", price = 13f, logoPath="https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/lunchbox_kasza_gryczana_warzywa.jpg"},
                new Dish {dishName = "Leczo z cukinią", description = "Leczo z cukinią", dishType=dishTypes[2], ingredients="Cebula, czosnek, kiełbasa, papryka, cukinia, passata pomidorowa", price = 9f, logoPath="https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/leczo_z_cukinia_1.jpg"},
                new Dish {dishName = "Frytki z cukinii", description = "Frytki z cukinii", dishType=dishTypes[2], ingredients="Cukinia, czosnek, jajko, mleko, bułka tarta", price = 9f, logoPath="https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/frytki_z_cukinii.jpg"},
                new Dish {dishName = "Gofry z szynką i serem", description = "Gofry z szynką i serem", dishType=dishTypes[2], ingredients="Mąka, cukier, mleko, masło, szynka, ser żółty, papryka", price = 12f, logoPath="https://www.kwestiasmaku.com/sites/kwestiasmaku.com/files/gofry_z_serem_i_szynka_0.jpg"}

                // Napoje
            };


            Local[] locals = {
                new Local { latitude=51.109788f, longitude=17.064000f, address = "Curie-Skłodowskiej 55, Wrocław", name = "Przegryź", description = "Lokal ze schabowym", logoPath = "http://przegryz.com/wp-content/uploads/2017/04/logo-przegryz-2.jpg"},
                new Local { latitude=51.112551f, longitude=17.059255f, address = "Plac Grunwaldzki 22, Wrocław", name = "Pizza Hut", description = "Pizza Hut w pasażu", logoPath = "https://vignette.wikia.nocookie.net/logopedia/images/d/d2/Pizza_Hut_logo.svg/revision/latest/scale-to-width-down/200?cb=20180501084032"},
                new Local { latitude=51.111455f, longitude=17.058413f, address = "Plac Grunwaldzki 18, Wrocław", name = "Bravo", description = "Lokal z Pizzą", logoPath = "http://www.bravopizzawc.com/wp-content/uploads/2015/01/Bravo-Logo1.gif"},
                new Local { latitude=51.112551f, longitude=17.059255f, address = "Plac Grunwaldzki 22, Wrocław", name = "KFC", description = "KFC w pasażu", logoPath = "http://logonoid.com/images/kfc-logo.png"}

            };

            LocalDishes[] localDishes = {
                new LocalDishes { dish = dishes[0], local = locals[1] },
                new LocalDishes { dish = dishes[1], local = locals[1] },
                new LocalDishes { dish = dishes[2], local = locals[2] },
                new LocalDishes { dish = dishes[3], local = locals[2] },
                new LocalDishes { dish = dishes[4], local = locals[3] },
                new LocalDishes { dish = dishes[5], local = locals[3] },
                new LocalDishes { dish = dishes[6], local = locals[1] },
                // Przegryź
                new LocalDishes { dish = dishes[7], local = locals[0] },
                new LocalDishes { dish = dishes[8], local = locals[0] },
                new LocalDishes { dish = dishes[9], local = locals[0] },
                new LocalDishes { dish = dishes[10], local = locals[0] },
                new LocalDishes { dish = dishes[11], local = locals[0] },
                new LocalDishes { dish = dishes[12], local = locals[0] },
                new LocalDishes { dish = dishes[13], local = locals[0] },
                new LocalDishes { dish = dishes[14], local = locals[0] },
                new LocalDishes { dish = dishes[15], local = locals[0] },
                new LocalDishes { dish = dishes[16], local = locals[0] },
                new LocalDishes { dish = dishes[17], local = locals[0] },
                new LocalDishes { dish = dishes[18], local = locals[0] },
                new LocalDishes { dish = dishes[19], local = locals[0] },
                new LocalDishes { dish = dishes[20], local = locals[0] },
                new LocalDishes { dish = dishes[21], local = locals[0] },
                new LocalDishes { dish = dishes[22], local = locals[0] },
                new LocalDishes { dish = dishes[23], local = locals[0] },
                new LocalDishes { dish = dishes[24], local = locals[0] },
            };

            Code[] codes = {
                new Code { code = "POTATO" },
                new Code { code = "ZIEMNIAK" }
            };

            locals[1].localDishes.Add(localDishes[0]);
            locals[1].localDishes.Add(localDishes[1]);
            locals[1].localDishes.Add(localDishes[6]);
            locals[2].localDishes.Add(localDishes[2]);
            locals[2].localDishes.Add(localDishes[3]);
            locals[3].localDishes.Add(localDishes[4]);
            locals[3].localDishes.Add(localDishes[5]);

            for (int i = 0; i < 7; ++i)
            {
                locals[i % locals.Count()].localDishes.Add(localDishes[i]);
            }

            for (int i = 7; i < 25; ++i)
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