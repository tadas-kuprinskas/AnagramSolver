class SearchInformation
{
    id;
    userIp;
    searchTime;  
    searchedWord;
    anagram;
    
  constructor(id, userIp, searchTime, searchedWord, anagram) {
        this.id = id
        this.userIp = userIp
        this.searchTime = searchTime
        this.searchedWord = searchedWord
        this.anagram = anagram
  }
}