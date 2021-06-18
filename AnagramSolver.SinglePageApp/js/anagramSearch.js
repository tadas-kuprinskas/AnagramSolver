document.write(`
  <div class="container text-center anagramSection">
    <div class="row vertical-center">
      <div class="col">
        <img class="mt-3" src="https://www.computerhope.com/jargon/u/user-friendly.jpg" alt="Img">
        <form action="#">
          <div class="col-md-3 mt-3">
            <label for="word">Enter your word to get anagrams:</label>
            <input type="text" class="form-control input-normal mt-3" id="myWord">
          </div>
          <button onclick="getAllAnagrams()" type="submit" class="btn btn-primary mt-3">Search</button>
          <button onclick="cleanTable()" type="submit" class="btn btn-danger mt-3">Clear table</button>
        </form>
      </div>
      <div class="container mt-5 mb-5" id="anagramTable">
        
        <table class="table table-primary mt-3">
            <thead class="thead-light">
            <tr>
                <div class ="words">
                    <th scope="col">My word</th>
                    <th scope="col">Anagram</th>
                <div>
            </tr>
            </thead>
            <tbody class ="anagramTBody" id="anagramTBody">                   
            </tbody>
        </table>
      </div>
    </div>
  </div>
`);