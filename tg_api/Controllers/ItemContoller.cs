﻿ using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using tg_api.Modes;
using tg_api.Cleints;
using System.Threading.Tasks;
using tg_api.DataManipulation;

namespace tg_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemContoller : ControllerBase
    {
        private readonly DictionaryClient _dictionaryClient;
        public Word tmp = new();
        List<Word> words = new List<Word>();
        public ItemContoller(DictionaryClient dictionaryClient)
        {
            //this.words = words;
            _dictionaryClient = dictionaryClient;

        }


        [HttpGet("{word}")]
        public async Task<WordResponesForTG> GetWord(string word)
        {
            tmp = await _dictionaryClient.GetWordByWord(word);
            TransferFromWordToResponse transfer = new();
            return transfer.GetResponseFromWord(tmp);
        }


        [HttpGet("getExample")]
        public string GetExample()
        {
            return null;
        }

        [HttpGet("getCollection")]
        public async Task<List<Word>> GetAllWords()
        {
            var result = await _dictionaryClient.AllWords();
            return result;
        }

        // POST /items
        //[HttpPost("{word}")]
        //public async Task CreateItem(string word)
        //{
        //    var result = await _dictionaryClient.GetWordByWord(word);

        //    _dictionaryClient.TakeToWordCollection(result);
        //    return;
        //}

        //Put /items/{id
    
    [HttpPut("{word}")]
    public  async Task PutWordToCollection(string word)
    {
        var tmp = await _dictionaryClient.GetWordByWord(word);
            _dictionaryClient.TakeToWordCollection(tmp);

    }

    //delete /items/{id}
    [HttpDelete("{word}")]
        public async Task DeleteItem(string word)
        {
            var tmp = await _dictionaryClient.GetWordByWord(word);
            _dictionaryClient.DeleteWordFromCollection(tmp);   
        }

    }
}
