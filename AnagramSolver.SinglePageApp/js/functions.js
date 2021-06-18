anagramTable = document.querySelector(".anagramTBody");
wordsToFindTable = document.querySelector(".wordsToFindTbody");
searchHistoryTable = document.querySelector(".searchHistoryTBody");

let wordToSearch = document.getElementById("myWord");

function getAllAnagrams(){ 

    var anagrams = Service.getAnagrams();
    anagrams.then((anagram) =>
    {
        anagram.forEach(element => {
            createTable(element);
        })
    });
}

function getPaginatedWords(){

    var paginatedWords = Service.getWordsPaginated();
    paginatedWords.then((pWord) =>
    {
        pWord.forEach(el => {
            createTableForWords(el);
        })
    })
}

function cleanTable()
{
    document.querySelectorAll(".table tbody tr").forEach(function(e){e.remove()});
}

function createTable(element)
{       
    let tRow= document.createElement("tr");

    let columnOne = document.createElement("th");
    columnOne.innerHTML = wordToSearch.value;
    tRow.appendChild(columnOne);

    let columnTwo = document.createElement("th");
    columnTwo.innerHTML = element;
    tRow.appendChild(columnTwo);

    anagramTable.appendChild(tRow);  

}

function createTableForWords(el)
{
    let tRow= document.createElement("tr");

    let column = document.createElement("th");
    column.innerHTML = el;
    tRow.appendChild(column);

    wordsToFindTable.appendChild(tRow);  
}

function downloadDictionaryFile(){
    Service.downloadFile();
}

function getSearchInformation(){ 

    var searchInfos = Service.getSearchInformation();
    searchInfos.then( (searchInfo) =>
    {
        searchInfo.forEach(element => {
            let searchInformation = new SearchInformation(element.id, element.userIp, element.searchTime, element.searchedWord, element.anagram);
            createInfoTable(searchInformation);
        })
    });
}

function createInfoTable(info){

    let tRow= document.createElement("tr");

    let col = document.createElement("th");
    col.innerHTML=info.userIp;
    tRow.appendChild(col);

    let secondCol = document.createElement("th");
    secondCol.innerHTML=info.searchTime;
    tRow.appendChild(secondCol);

    let thirdCol = document.createElement("th");
    thirdCol.innerHTML=info.searchedWord;
    tRow.appendChild(thirdCol);

    let fourthCol = document.createElement("th");
    fourthCol.innerHTML=info.anagram;
    tRow.appendChild(fourthCol);

    searchHistoryTable.appendChild(tRow);  
}



