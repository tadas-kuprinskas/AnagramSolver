document.write(`
    <div class="container text-center">
        <div class="row vertical-center">
          <div class="col">
            <form action="#">
              <div class="col-md-3 mt-3">
                <label for="wordToFind">Enter a whole word or it's fragment to search for existing words:</label>
                <input type="text" class="form-control input-normal mt-3" id="wordToFind">
              </div>
              <div class="col-md-3 mt-3">
                <label for="wordToFind">Enter the number of words to be found:</label>
                <input type="number" class="form-control input-normal mt-3" id="numberToFind">
              </div>
              <div class="col-md-3 mt-3">
                <label for="wordToFind">Enter the page number:</label>
                <input type="number" class="form-control input-normal mt-3" id="pageToFind">
              </div>
              <button onclick="getPaginatedWords()" type="submit" class="btn btn-primary mt-3">Search</button>
              <button onclick="cleanTable()" type="submit" class="btn btn-danger mt-3">Clear table</button>
            </form>
          </div>
          <div class="container mt-5" id="wordTable">
            <table class="table table-primary mt-3">
                <thead class="thead-light">
                <tr>
                    <div class ="words">
                        <th scope="col">Existing words</th>
                    <div>
                </tr>
                </thead>
                <tbody class ="wordsToFindTbody" id="wordsToFindTbody">                   
                </tbody>
            </table>
          </div>
        </div>
    </div>
`);

  