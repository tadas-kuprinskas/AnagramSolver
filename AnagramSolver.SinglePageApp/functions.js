anagramTable = document.querySelector(".anagramTBody");
wordsToFindTable = document.querySelector(".wordsToFindTbody");

let wordToSearch = document.getElementById("myWord");
//document.getElementById("buttonToFindWords").addEventListener('click', getPaginatedWords());

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
