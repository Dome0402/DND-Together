using DND_Together.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Together_neu_Test
{
    public static class Consts
    {
        public static Scene scene = new Scene
        {
            Name = "NUnit Test",
            Categories =
            {
                new Category()
                {
                    Name = "Category 1",
                    Pages =
                    {
                        new Page()
                        {
                            Title = "Page 1",
                            Url = "https://www.google.de"
                        },
                        new Page()
                        {
                            Title = "Page 2",
                            Url = "https://www.youtube.de"
                        },
                        new Page()
                        {
                            Title = "Page 3",
                            Url = "https://www.google.de"
                        },
                        new Page()
                        {
                            Title = "Page 4",
                            Url = "https://www.youtube.de"
                        }
                    }
                },
                new Category()
                {
                    Name = "Category 27",
                    Pages =
                    {
                        new Page()
                        {
                            Title = "Page 18",
                            Url = "https://www.google.de"
                        },
                        new Page()
                        {
                            Title = "Page 23",
                            Url = "https://www.youtube.de"
                        },
                        new Page()
                        {
                            Title = "Page 36",
                            Url = "https://www.google.de"
                        },
                        new Page()
                        {
                            Title = "Page 49",
                            Url = "https://www.youtube.de"
                        }
                    }
                },
                new Category()
                {
                    Name = "Category 38475",
                    Pages =
                    {
                        new Page()
                        {
                            Title = "Page 185",
                            Url = "https://www.google.de"
                        },
                        new Page()
                        {
                            Title = "Page 233",
                            Url = "https://www.youtube.de"
                        },
                        new Page()
                        {
                            Title = "Page 367",
                            Url = "https://www.google.de"
                        },
                        new Page()
                        {
                            Title = "Page 494",
                            Url = "https://www.youtube.de"
                        }
                    }
                }
            }
        };
    }
}
