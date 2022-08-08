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

$(document).ready(function (){
    $("#feedbackForm").on("submit", function (e){
        e.preventDefault();
        let phoneId = $("input[name=phoneId]");
        let text = $("#text");
        let feedback = {
            text: text.val(),
            phoneId: phoneId.attr('id')
        };
        $.post('https://localhost:5001/feedback/create', feedback, function (response){
            $('#feedbacks').before(response);
        });
    });
});