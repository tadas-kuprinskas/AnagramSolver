let url = 'https://localhost:44379/Word/Search?myWord=';
let pagesUrl = 'https://localhost:44379/Word?currentPage=';
let downloadUrl = 'https://localhost:44379/FileDownload/Dictionary';
let searchInfoUrl = 'https://localhost:44379/SearchInformation';

class Service{

    static getAnagrams(myWord){
        var anagrams = fetch(`${url}${myWord}`)
            .then(res => res.json())

        return anagrams;
    }
    
    static getWordsPaginated(pageNumber, wordNumber, wordToFind){
        var foundWords = fetch(`${pagesUrl}${pageNumber}&pagesize=${wordNumber}&myWord=${wordToFind}`)
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