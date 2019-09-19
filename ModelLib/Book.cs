using System;
using System.Text.RegularExpressions;

namespace ModelLib
{
    public class Book
    {
        private string _title;
        private string _author;
        private int _pagenumber;
        private string _isbn13;

        public Book(string title, string author, int pagenumber, string isbn13)
        {
            Title = title;
            Author = author;
            Pagenumber = pagenumber;
            Isbn13 = isbn13;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                CheckTitleLength(value);
                _title = value;
            }
        }

        public string Author
        {
            get => _author;
            set => _author = value;
        }

        public int Pagenumber
        {
            get => _pagenumber;
            set
            {
                CheckPagenumber(value);
                _pagenumber = value;
            }
        }

        public string Isbn13
        {
            get => _isbn13;
            set
            {
                CheckIsbn13(value);
                _isbn13 = value;
            }
        }

        private void CheckTitleLength(string stringToCheck)
        {
            int minimumLength = 2;

            if (string.IsNullOrWhiteSpace(stringToCheck))
            {
                throw new ArgumentException("Title is empty. Title must be atleast 2 characters long.");
            }

            if (stringToCheck.Length < minimumLength)
            {
                throw new ArgumentException("Title too short. Title must be atleast 2 characters long.");
            }
        }

        private void CheckPagenumber(int pagenumber)
        {
            int minimumPagenumber = 10;
            int maximumPagenumber = 1000;

            if (pagenumber < minimumPagenumber || pagenumber > maximumPagenumber)
            {
                throw new ArgumentException("Not a valid pagenumber. Pagenumber must be between 10 and 1000.");
            }
        }

        private void CheckIsbn13(string isbn13)
        {
            Regex regex = new Regex(@"^\d+$");

            if (isbn13.Length != 13)
            {
                throw new ArgumentException("Not a valid isbn13. Isbn13 must be exactly 13 characters long");
            }
            else if (!regex.IsMatch(isbn13))
            {
                throw new ArgumentException("Not a valid isbn13. Isbn13 must only contain numbers");
            }
        }
    }
}
