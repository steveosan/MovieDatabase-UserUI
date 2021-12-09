using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieLibraryOO.Context;
using MovieLibraryOO.DataModels;

namespace MovieLibraryOO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Console.WriteLine("\n###### Unity Framework Terminal #######\n");
            System.Console.WriteLine("\t1) Access User Interface");
            System.Console.WriteLine("\t2) Access Movie Database\n");
            System.Console.Write("Input:");
            string userInput = Console.ReadLine();
            System.Console.WriteLine();
            if (userInput == "1") //Enter User Interface
            {
                string sort;
                do
                {
                    System.Console.WriteLine("User Interface");
                    System.Console.WriteLine("1) Add New User");
                    System.Console.WriteLine("2) Display Users");
                    System.Console.Write("Input:");
                    sort = Console.ReadLine();
                if (sort == "1")
                {
                System.Console.WriteLine("Enter New Age of User");
                string UserAge = Console.ReadLine();
                long UserAgeRes = long.Parse(UserAge);

                System.Console.WriteLine("Enter New Gender of User:");
                string UserGender = Console.ReadLine();

                System.Console.WriteLine("Enter New User ZipCode:");
                string UserZipCode = Console.ReadLine();



                using (var db = new MovieContext())
                        {

                        // foreach (var b in db.Occupations)
                        // {
                        //     System.Console.WriteLine($"Blog: {b.Id}:  {b.Name}");
                        // }
                              
                            var newUser = new User();
                                newUser.Age = UserAgeRes;
                                newUser.Gender = UserGender;
                                newUser.ZipCode = UserZipCode;
                                // newUser.Occupation.Id = "Artist";
                                // newUser.Occupation.Id= UserOccIdRes;
                                // newUser.Occupation.Id = 0;
                            // db.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Occupations] ON");
                            db.Users.Add(newUser);
                            db.SaveChanges();
                            // db.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Occupations] OFF");
                            // db.SaveChanges();
                        }
                }
                if (sort == "2") //Display user information
                {
                    System.Console.WriteLine("1) Display All User Information");
                    System.Console.WriteLine("2) Sort User By ID#");
                    string SortChoise = Console.ReadLine();
                    if (SortChoise == "1")
                    {
                        System.Console.WriteLine("Display All User Information");

                        using (var db = new MovieContext())
                        {
                            // SELECT
                            var users = db.Users.Include(x=>x.Occupation)
                                                .OrderBy(x => x.Id);
                                                // .Where(x=> x.Age == 69);
                            try{
                            foreach (var user in users) 
                            {

                                System.Console.WriteLine($"User Id#: ({user.Id}) User Gender: {user.Gender} User Occupation:{user.Occupation.Name} Age:{user.Age}");
                            } 
                            }
                            catch (Exception e)
                            {
                                System.Console.WriteLine(e.Message);
                            }                 

                        } 
                    }

                    if(SortChoise == "2")
                    {
                        System.Console.WriteLine("Sort by ID#");

                        System.Console.WriteLine("Enter ID Number: ");
                        var UserId = long.Parse(Console.ReadLine());


                        using (var db = new MovieContext())
                        {
                            // SELECT
                            var users = db.Users.Include(x=>x.Occupation)
                                            .Where(x=> x.Id == UserId);
                            foreach (var user in users) 
                            {
                                System.Console.WriteLine($"User Id#: ({user.Id}) User Gender: {user.Gender} User Occupation:{user.Occupation.Name}");
                            }
                        } 
                    }
                }

                }while (sort == "1" || sort == "2");
            }
            else if (userInput == "2") //Enter Movie Database
            {
            string choice;
            do
            {

                // ask user a question
                    //Menu//
                System.Console.WriteLine("\n ####  Main Menu  ####");
                Console.WriteLine("1) Search Movie");
                Console.WriteLine("2) Add Movie");
                System.Console.WriteLine("3) Update Movie");
                System.Console.WriteLine("4) Delete Movie");
                Console.WriteLine("Enter any other key to exit.");
                System.Console.Write("User Input: ");


                // input response
                choice = Console.ReadLine();
                if (choice == "1")  // Search title of all movies in database
                {   
                    string sortChoise;
                    System.Console.WriteLine("View all movie records?");
                    System.Console.WriteLine("1) View all movies");
                    System.Console.WriteLine("2) Sort by movie title");
                    sortChoise = Console.ReadLine();                    

                    if (sortChoise == "1")
                    {
                    var titleChoice = "";
                    
                    System.Console.WriteLine(""); 
                        using (var db = new MovieContext())
                        {
                            var movie = db.Movies.Include(x=>x.MovieGenres).ThenInclude(x=>x.Genre)
                                .FirstOrDefault(movie=>movie.Title.Contains(titleChoice));
                            foreach (var m in db.Movies)
                            {
                                if (m.Title.ToUpper().Contains(titleChoice))
                                {
                                System.Console.WriteLine("Movie Id: " + m.Id + "\nTitle: " + m.Title + "\nReleased: " + m.ReleaseDate);
                                System.Console.Write("Genres:");
                                foreach (var genre in movie.MovieGenres) 
                                    {
                                        System.Console.WriteLine($"\t{genre.Genre.Name}");
                                    }
                                }
                            }   
                        }
                    }
                    if (sortChoise == "2")
                    {
                    System.Console.WriteLine("\nPlease enter the title of the movie you would like to view.");
                    System.Console.Write("User Input:\n");
                    var titleChoice = Console.ReadLine().ToUpper();
                    
                    System.Console.WriteLine(""); 
                        using (var db = new MovieContext())
                        {
                            var movie = db.Movies.Include(x=>x.MovieGenres).ThenInclude(x=>x.Genre)
                                .FirstOrDefault(movie=>movie.Title.Contains(titleChoice));
                            foreach (var m in db.Movies)
                            {
                                if (m.Title.ToUpper().Contains(titleChoice))
                                {
                                System.Console.WriteLine("Movie Id: " + m.Id + "\nTitle: " + m.Title + "\nReleased: " + m.ReleaseDate);
                                System.Console.Write("Genres:");
                                foreach (var genre in movie.MovieGenres) 
                                    {
                                        System.Console.WriteLine($"\t{genre.Genre.Name}");
                                    }
                                }
                            }   
                        }
                    }
                }
                if (choice == "2") // Add new movie to database
                {
                System.Console.WriteLine("Enter New Movie Name");
                string newMovieName = Console.ReadLine();

                System.Console.WriteLine("Enter New Movie Release Date:");
                string dateInput = Console.ReadLine();
                var parsedDate = DateTime.Parse(dateInput);

                // System.Console.WriteLine("Enter New Movie Genre:");
                // string newMovieGenre = Console.ReadLine();

                using (var db = new MovieContext())
                        {
                            var newMovie = new Movie() {
                                Title = newMovieName,
                                ReleaseDate = parsedDate,
                                // MovieGenres = newMovieGenre
                            };
                            db.Movies.Add(newMovie);
                            db.SaveChanges();
                        }
                }
                if (choice == "3") // Update existing movie from database
                {
                System.Console.WriteLine("Please enter the title of the movie you would like to update.");
                var titleChoice = Console.ReadLine().ToUpper();
                System.Console.WriteLine("");

                    using (var db = new MovieContext())
                    {

                        foreach (var m in db.Movies)
                        {
                            if (m.Title.ToUpper().Contains(titleChoice))
                            {
                            System.Console.WriteLine("Movie Id: " + m.Id + "\nTitle: " + m.Title + "\nReleased: " + m.ReleaseDate + "\n");
                            }
                        }
                    }
                    System.Console.WriteLine("Do you see multiple results?");
                    System.Console.WriteLine("1) Yes");
                    System.Console.WriteLine("2) No");
                    string response = Console.ReadLine();
                    Convert.ToInt32(response);
                    if (response == "1")
                    {
                        System.Console.WriteLine("Please Type the Exact Title you would like to update.");
                    }
                    else if (response == "2")
                    {
                        System.Console.WriteLine("Enter new title");
                        string newTitle = Console.ReadLine();

                            using (var db = new MovieContext())
                            {    
                                var movie = (from s in db.Movies
                                            where s.Title.ToUpper() == titleChoice
                                            select s).FirstOrDefault<Movie>();
                                    movie.Title = newTitle;
                                    db.SaveChanges();
                                System.Console.WriteLine(movie.Id + " " + movie.Title);
                            }

                        System.Console.WriteLine("Title Updated");

                    }
                }
                if (choice == "4") // Delete Movie from database
                {

                    System.Console.WriteLine("Enter Movie Name to Delete: ");
                    var deleteMovie = Console.ReadLine();

                    using (var db = new MovieContext())
                            {    
                                var movie = (from s in db.Movies
                                            where s.Title.ToUpper() == deleteMovie
                                            select s).FirstOrDefault<Movie>();

                                    db.Movies.Remove(movie);
                                    db.SaveChanges();

                            }

                }
            }while (choice == "1" || choice == "2" || choice == "3" || choice == "4");
            Console.WriteLine("\nThanks for using the Movie Database!");
            }
            else if (userInput != "1" && userInput != "2") //Quit Program
            {
                System.Console.WriteLine("Ending Program");
            }
        }
    }
}





            // // cRud - READ
            // System.Console.WriteLine("Enter Occupation Name: ");
            // var occ = Console.ReadLine();

            // using (var db = new MovieContext())
            // {
            //     var occupation = db.Occupations.FirstOrDefault(x => x.Name == occ);
            //     System.Console.WriteLine($"({occupation.Id}) {occupation.Name}");
            // }

            // Crud - CREATE
            // System.Console.WriteLine("Enter NEW Occupation Name: ");
            // var occ2 = Console.ReadLine();

            // using (var db = new MovieContext())
            // {
            //     var occupation = new Occupation() {
            //         Name = occ2
            //     };
            //     db.Occupations.Add(occupation);
            //     db.SaveChanges();

            //     var newOccupation = db.Occupations.FirstOrDefault(x => x.Name == occ2);
            //     System.Console.WriteLine($"({newOccupation.Id}) {newOccupation.Name}");
            // }

            // crUd - UPDATE
            // System.Console.WriteLine("Enter Occupation Name to Update: ");
            // var occ3 = Console.ReadLine();

            // System.Console.WriteLine("Enter Updated Occupation Name: ");
            // var occUpdate = Console.ReadLine();

            // using (var db = new MovieContext())
            // {
            //     var updateOccupation = db.Occupations.FirstOrDefault(x => x.Name == occ3);
            //     System.Console.WriteLine($"({updateOccupation.Id}) {updateOccupation.Name}");

            //     updateOccupation.Name = occUpdate;

            //     db.Occupations.Update(updateOccupation);
            //     db.SaveChanges();

            // }

            // cruD - DELETE
            // System.Console.WriteLine("Enter Occupation Name to Delete: ");
            // var occ4 = Console.ReadLine();

            // using (var db = new MovieContext())
            // {
            //     var deleteOccupation = db.Occupations.FirstOrDefault(x => x.Name == occ4);
            //     System.Console.WriteLine($"({deleteOccupation.Id}) {deleteOccupation.Name}");

            //     // verify exists first
            //     db.Occupations.Remove(deleteOccupation);
            //     db.SaveChanges();
            // }


            // using (var db = new MovieContext()) {
            //     var users = db.Users.Include(x=>x.Occupation)
            //                     .Take(10).ToList();
            //     foreach (var user in users) {
            //         System.Console.WriteLine($"({user.Id}) {user.Gender} {user.Occupation.Name}");
            //     }
            // }

            // SELECT
            // using (var db = new MovieContext()) {
            //     var users = db.Users.Include(x=>x.Occupation)
            //                         .Where(x=> x.Id == 1).ToList();
            //     foreach (var user in users) {
            //         System.Console.WriteLine($"Display user: ({user.Id}) {user.Gender} {user.Occupation.Name}");
            //     }
            // }

            // MANY-TO-MANY Relationship
            // using (var db = new MovieContext()) {
            //     var selectedUser = db.Users.Where(x=> x.Id == 2);
            //     var users = selectedUser.Include(x=>x.UserMovies).ThenInclude(x=>x.Movie).ToList();

            //     foreach (var user in users) {
            //         System.Console.WriteLine($"Added user: ({user.Id}) {user.Gender} {user.Occupation.Name}");

            //         foreach (var movie in user.UserMovies.OrderBy(x=>x.Rating)) {
            //             System.Console.WriteLine($"\t\t\t{movie.Rating} {movie.Movie.Title}");
            //         }
            //     }
            // }

            // Display genres for a movie
            // using (var db = new MovieContext())
            // {
            //     var movie = db.Movies.Include(x=>x.MovieGenres).ThenInclude(x=>x.Genre)
            //         .FirstOrDefault(movie=>movie.Title.Contains("Babe"));
            //     System.Console.WriteLine($"Movie: {movie.Title} {movie.ReleaseDate.ToString("MM-dd-yyyy")}");

            //     System.Console.WriteLine("Genres:");
            //     foreach (var genre in movie.MovieGenres) 
            //     {
            //         System.Console.WriteLine($"\t{genre.Genre.Name}");
            //     }
            // }

            // Add userMovie
            // using (var db = new MovieContext()) { 

            //     // build user object (not database)
            //     var user = db.Users.FirstOrDefault(u=>u.Id==944);
            //     var movie = db.Movies.FirstOrDefault(m=>m.Title == "asdfasdf");

            //     // var user =  new User() {
            //     //     Age = 32,
            //     //     Gender = "M",
            //     //     ZipCode = "53186"
            //     // };

            //     // build user/movie relationship object (not database)
            //     var userMovie = new UserMovie() {
            //         Rating = 2,
            //         RatedAt = DateTime.Now
            //     };
            //     var userMovies = new List<UserMovie>();
            //     userMovies.Add(userMovie);
                
            //     // set up the database relationships
            //     // user.UserMovies = userMovies;
            //     userMovie.User = user;
            //     userMovie.Movie = movie;

            //     // db.Users.Add(user);
            //     db.UserMovies.Add(userMovie);

            //     // commit
            //     db.SaveChanges();

            // }

            // // DEPENDENCY INJECTION
            // var serviceProvider = new ServiceCollection()
            //     .AddSingleton<IRepository, FileRepository>()
            //     .AddSingleton<IContext, MovieContext>()
            //     .AddSingleton<IMenu, Menu>()
            //     .BuildServiceProvider();

            // // ** still have a dependency here **
            // // var repository = new MyNewRepository();
            // // var context = new MovieContext(repository);
            // // var menu = new Menu(repository, context);

            // var menu = serviceProvider.GetService<IMenu>();
            // var userSelection = menu.GetMainMenuSelection();

            // while (menu.IsValid)
            // {
            //     menu.Process(userSelection);

            //     userSelection = menu.GetMainMenuSelection();
            // }