anagramTable = document.querySelector(".anagramTBody");

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

function cleanTable()
{
    document.querySelectorAll(".table .anagramTBody tr").forEach(function(e){e.remove()});
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