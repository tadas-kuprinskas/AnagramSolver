let myWord = document.getElementById("myWord");
let wordToFind = document.getElementById("wordToFind");
let wordNumber = document.getElementById("numberToFind");
let pageNumber = document.getElementById("pageToFind");

let url = 'https://localhost:44379/Word/Search?myWord=';
let pagesUrl = 'https://localhost:44379/Word?currentPage=';
let downloadUrl = 'https://localhost:44379/FileDownload/Dictionary';
let searchInfoUrl = 'https://localhost:44379/SearchInformation';

class Service{

    static getAnagrams(){
        var anagrams = fetch(`${url}${myWord.value}`)
            .then(res => res.json())

        return anagrams;
    }
    
    static getWordsPaginated(){
        var foundWords = fetch(`${pagesUrl}${pageNumber.value}&pagesize=${wordNumber.value}&myWord=${wordToFind.value}`)
            .then(res => res.json())

        return foundWords;
    }

    static downloadFile(){
        location.href = downloadUrl;
    }

    static getSearchInformation(){
        var searchHistories = fetch(searchInfoUrl)
            .then(res => res.json())

        return searchHistories;
    }
}