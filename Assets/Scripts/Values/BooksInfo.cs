using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using Models;

namespace Values
{
    public static class BooksInfo
    {
        private const string CountKey = "Books.Count";

        public static bool HaveBooks => Count > 0;

        public static string Status => Count >= 20 ? "Guru" : "Beginner";

        private static int Count
        {
            get => PlayerPrefs.GetInt(CountKey, 0);
            set
            {
                PlayerPrefs.SetInt(CountKey, value);

                switch (value)
                {
                    case 1:
                        RewardInfo.SetFirstBookReward();
                        break;
                    case 2:
                        RewardInfo.SetReadingTwoBooksReward();
                        break;
                    case 5:
                        RewardInfo.SetReadingFiveBooksReward();
                        break;
                    case 20:
                        RewardInfo.SetNewStatusReward();
                        break;
                    case 25:
                        RewardInfo.SetTopReaderReward();
                        break;
                }
            }
            
        }

        public static void SaveCount(int count)
        {
            Count = count;
        }

        public static async Task SaveBookModelAsync(List<BookModel> books, string path)
        {
            foreach (var book in books)
            {
                bool containReward = book.GenreIndexes.Contains(5) && book.GenreIndexes.Contains(6);

                if (containReward)
                {
                    RewardInfo.SetFanHorrorAndFantasyReward();
                    break;
                }
            }
            
            await Task.Run(() =>
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(stream, books);
                }
            });
        }

        public static List<BookModel> LoadBooks(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        return (List<BookModel>)formatter.Deserialize(stream);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Error loading books: " + e.Message);
                    return new List<BookModel>();
                }
            }

            return new List<BookModel>();
        }
    }
}