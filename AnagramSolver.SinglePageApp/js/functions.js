const anagramTable = document.querySelector(".anagramTBody");
const wordsToFindTable = document.querySelector(".wordsToFindTbody");
const searchHistoryTable = document.querySelector(".searchHistoryTBody");

let myWord = document.getElementById("myWord");
let wordToFind = document.getElementById("wordToFind");
let wordNumber = document.getElementById("numberToFind");
let pageNumber = document.getElementById("pageToFind");

let wordToSearch = document.getElementById("myWord");

function getAllAnagrams(){ 
    
    var myInput = myWord.value;

    try{
        if (myInput.length < 3){
        throw "The word is too short. It has to be longer than 3 symbols";
        }
    }
    catch(err){
        alert(err);
        return;
    };

    var anagrams = Service.getAnagrams(myWord.value);
    anagrams.then((anagram) =>
    {
        anagram.forEach(element => {
            createTable(element);
        })
    });
}

function getPaginatedWords(){

    var paginatedWords = Service.getWordsPaginated(pageNumber.value, wordNumber.value, wordToFind.value);
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



