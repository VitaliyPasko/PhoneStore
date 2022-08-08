$(document).ready(function (){
    $('#search').on('keyup', function (e){
        e.preventDefault();
        let searchTerm = $(this).val();
        console.log(searchTerm)
        searchTerm = encodeURIComponent(searchTerm);
        console.log(searchTerm)
        $('#results')
            .load(`https://localhost:5001/Account/SearchAccounts?searchTerm=${searchTerm}`);
    })
});