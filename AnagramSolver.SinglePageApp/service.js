let myWord = document.getElementById("myWord");

let url = 'https://localhost:44379/Word/Search?myWord=';

class Service{

    static getAnagrams(){
        var anagrams = fetch(`${url}${myWord.value}`)
            .then(res => res.json())

        return anagrams;
    }   
}