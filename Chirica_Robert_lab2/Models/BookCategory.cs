﻿namespace Chirica_Robert_lab2.Models
{
    public class BookCategory
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
