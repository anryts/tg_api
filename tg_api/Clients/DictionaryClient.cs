﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using tg_api.Clients;
using tg_api.Modes;
namespace tg_api.Cleints
{
    public class DictionaryClient : IDictionaryClient
    {

        private static string _adress;
        HttpClient _client;
       public List<Word> words = new();

        public DictionaryClient()
        {
            _adress = Constants._adress;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);

        }

        public async Task<Word> GetWordByWord(string word_t)
        {
            var response = await _client.GetAsync(word_t);
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var tmp = JsonConvert.DeserializeObject<List<Word>>(content);
            var result = tmp[0];
            return result;
        }

        public async Task<List<Word>> AllWords ()
        {
            return words;
        }

        public void TakeToWordCollection(Word item)
        {
            words.Add(item);
            return;
        }


        public void DeleteWordFromCollection(Word word)
        {
            var result = words.Find(x => x.word ==word.word);
            if (result == null)
                return;
            words.Remove(result);
        }
    }
}
