document.write(`
<div class="container text-center">
    <div class="row vertical-center">
      <div class="col information">
        <button class="info" onclick="getSearchInformation()">Get search history</button>
        <button onclick="cleanTable()" type="submit" class="btn btn-danger mt-3">Clear table</button>
      </div>
      <div class="container mt-5" id="wordTable">
        <table class="table table-primary mt-3">
            <thead class="thead-light">
            <tr>
                <div class ="words">
                    <th scope="col">User Ip</th>
                    <th scope="col">Search Time</th>
                    <th scope="col">Searched Word</th>
                    <th scope="col">Anagrams</th>
                <div>
            </tr>
            </thead>
            <tbody class ="searchHistoryTBody" id="searchHistoryTBody">                   
            </tbody>
        </table>
      </div>
    </div>
</div>
`);